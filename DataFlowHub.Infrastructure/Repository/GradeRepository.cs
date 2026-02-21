using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public GradeRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Grade>> GetByEnrollmentIdAsync(int enrollmentId)
        {
            var grades = new List<Grade>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Evaluation.usp_Grades_GetByEnrollmentId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EnrollmentId", SqlDbType.Int) { Value = enrollmentId });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                grades.Add(MapToEntity(dr));
            }
            return grades;
        }

        public async Task CreateAsync(Grade grade)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Evaluation.usp_Grades_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = grade.Name });
            cmd.Parameters.Add(new SqlParameter("@Value", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = grade.Value });
            cmd.Parameters.Add(new SqlParameter("@EvaluationDate", SqlDbType.DateTime2) { Value = grade.EvaluationDate });
            cmd.Parameters.Add(new SqlParameter("@EnrollmentId", SqlDbType.Int) { Value = grade.EnrollmentId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Evaluation.usp_Grades_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = grade.Id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = grade.Name });
            cmd.Parameters.Add(new SqlParameter("@Value", SqlDbType.Decimal) { Precision = 5, Scale = 2, Value = grade.Value });
            cmd.Parameters.Add(new SqlParameter("@EvaluationDate", SqlDbType.DateTime2) { Value = grade.EvaluationDate });
            cmd.Parameters.Add(new SqlParameter("@EnrollmentId", SqlDbType.Int) { Value = grade.EnrollmentId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Evaluation.usp_Grades_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private static Grade MapToEntity(SqlDataReader dr)
        {
            return new Grade
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                Value = dr.GetDecimal(dr.GetOrdinal("Value")),
                EvaluationDate = dr.GetDateTime(dr.GetOrdinal("EvaluationDate")),
                EnrollmentId = dr.GetInt32(dr.GetOrdinal("EnrollmentId"))
            };
        }
    }
}