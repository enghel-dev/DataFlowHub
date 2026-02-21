using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class SchoolTermRepository : ISchoolTermRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public SchoolTermRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<SchoolTerm>> GetAllAsync()
        {
            var list = new List<SchoolTerm>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_SchoolTerms_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task<SchoolTerm?> GetActiveTermAsync()
        {
            SchoolTerm? term = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_SchoolTerms_GetActiveTerm", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                term = MapToEntity(dr);
            }
            return term;
        }

        public async Task CreateAsync(SchoolTerm term)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_SchoolTerms_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = term.Name });
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime2) { Value = term.StartDate });
            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime2) { Value = term.EndDate });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = term.IsActive });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(SchoolTerm term)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_SchoolTerms_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = term.Id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = term.Name });
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime2) { Value = term.StartDate });
            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime2) { Value = term.EndDate });
            cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = term.IsActive });

            await cmd.ExecuteNonQueryAsync();
        }

        private static SchoolTerm MapToEntity(SqlDataReader dr)
        {
            return new SchoolTerm
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                StartDate = dr.GetDateTime(dr.GetOrdinal("StartDate")),
                EndDate = dr.GetDateTime(dr.GetOrdinal("EndDate")),
                IsActive = dr.GetBoolean(dr.GetOrdinal("IsActive"))
            };
        }
    }
}