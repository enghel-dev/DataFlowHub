using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public FinancialTransactionRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<FinancialTransaction>> GetByStudentIdAsync(int studentId)
        {
            var transactions = new List<FinancialTransaction>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Finance.usp_FinancialTransactions_GetByStudentId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                transactions.Add(MapToEntity(dr));
            }
            return transactions;
        }

        public async Task CreateAsync(FinancialTransaction transaction)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Finance.usp_FinancialTransactions_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Mapeo riguroso de tipos financieros
            cmd.Parameters.Add(new SqlParameter("@TransactionDate", SqlDbType.DateTime2) { Value = transaction.TransactionDate });
            cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = transaction.Amount });
            cmd.Parameters.Add(new SqlParameter("@TransactionType", SqlDbType.Int) { Value = transaction.TransactionType });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 200) { Value = transaction.Description });
            cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = transaction.StudentId });

            await cmd.ExecuteNonQueryAsync();
        }

        private static FinancialTransaction MapToEntity(SqlDataReader dr)
        {
            return new FinancialTransaction
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                TransactionDate = dr.GetDateTime(dr.GetOrdinal("TransactionDate")),
                Amount = dr.GetDecimal(dr.GetOrdinal("Amount")),
                TransactionType = dr.GetInt32(dr.GetOrdinal("TransactionType")),
                Description = dr.GetString(dr.GetOrdinal("Description")),
                StudentId = dr.GetInt32(dr.GetOrdinal("StudentId"))
            };
        }
    }
}