using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public TeacherRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            var list = new List<Teacher>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Teachers_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            Teacher? teacher = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Teachers_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                teacher = MapToEntity(dr);
            }
            return teacher;
        }

        public async Task<Teacher?> GetByEmployeeNumberAsync(string employeeNumber)
        {
            Teacher? teacher = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Teachers_GetByEmployeeNumber", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EmployeeNumber", SqlDbType.NVarChar, 20) { Value = employeeNumber });

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                teacher = MapToEntity(dr);
            }
            return teacher;
        }

        public async Task CreateAsync(Teacher teacher)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Teachers_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SetParameters(cmd, teacher);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Teachers_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = teacher.Id });
            SetParameters(cmd, teacher);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Teachers_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private void SetParameters(SqlCommand cmd, Teacher teacher)
        {
            cmd.Parameters.Add(new SqlParameter("@EmployeeNumber", SqlDbType.NVarChar, 20) { Value = teacher.EmployeeNumber });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = teacher.FirstName });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = teacher.LastName });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = teacher.Email });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = (object)teacher.Phone ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@HireDate", SqlDbType.DateTime2) { Value = teacher.HireDate });
        }

        private static Teacher MapToEntity(SqlDataReader dr)
        {
            return new Teacher
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                EmployeeNumber = dr.GetString(dr.GetOrdinal("EmployeeNumber")),
                FirstName = dr.GetString(dr.GetOrdinal("FirstName")),
                LastName = dr.GetString(dr.GetOrdinal("LastName")),
                Email = dr.GetString(dr.GetOrdinal("Email")),
                Phone = dr.IsDBNull(dr.GetOrdinal("Phone")) ? null : dr.GetString(dr.GetOrdinal("Phone")),
                HireDate = dr.GetDateTime(dr.GetOrdinal("HireDate"))
            };
        }
    }
}