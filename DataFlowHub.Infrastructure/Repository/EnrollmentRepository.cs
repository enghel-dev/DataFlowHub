using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public EnrollmentRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            var list = new List<Enrollment>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Enrollment.usp_Enrollments_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            var list = new List<Enrollment>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Enrollment.usp_Enrollments_GetByStudentId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task CreateAsync(Enrollment enrollment)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Enrollment.usp_Enrollments_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Usamos DateTime2 para coincidir con el SP
            cmd.Parameters.Add(new SqlParameter("@EnrollmentDate", SqlDbType.DateTime2) { Value = enrollment.EnrollmentDate });
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = enrollment.Status });
            cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = enrollment.StudentId });
            cmd.Parameters.Add(new SqlParameter("@CourseId", SqlDbType.Int) { Value = enrollment.CourseId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Enrollment.usp_Enrollments_UpdateStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = status });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Enrollment.usp_Enrollments_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private static Enrollment MapToEntity(SqlDataReader dr)
        {
            return new Enrollment
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                EnrollmentDate = dr.GetDateTime(dr.GetOrdinal("EnrollmentDate")),
                Status = dr.GetString(dr.GetOrdinal("Status")),
                StudentId = dr.GetInt32(dr.GetOrdinal("StudentId")),
                CourseId = dr.GetInt32(dr.GetOrdinal("CourseId"))
            };
        }
    }
}