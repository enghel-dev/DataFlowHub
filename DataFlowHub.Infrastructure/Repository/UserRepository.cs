using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public UserRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            User? user = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Security.usp_Users_GetByUsername", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username });

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                user = MapToEntity(dr);
            }
            return user;
        }

        public async Task CreateAsync(User user)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Security.usp_Users_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SetParameters(cmd, user);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(User user)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Security.usp_Users_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = user.Id });
            SetParameters(cmd, user);
            await cmd.ExecuteNonQueryAsync();
        }

        private void SetParameters(SqlCommand cmd, User user)
        {
            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = user.Username });
            cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 255) { Value = user.Password });
            cmd.Parameters.Add(new SqlParameter("@Role", SqlDbType.NVarChar, 20) { Value = user.Role });

            // Manejo de nulos para llaves foráneas opcionales
            cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.Int) { Value = (object)user.StudentId ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@TeacherId", SqlDbType.Int) { Value = (object)user.TeacherId ?? DBNull.Value });
        }

        private static User MapToEntity(SqlDataReader dr)
        {
            return new User
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                Username = dr.GetString(dr.GetOrdinal("Username")),
                Password = dr.GetString(dr.GetOrdinal("Password")),
                Role = dr.GetString(dr.GetOrdinal("Role")),
                StudentId = dr.IsDBNull(dr.GetOrdinal("StudentId")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("StudentId")),
                TeacherId = dr.IsDBNull(dr.GetOrdinal("TeacherId")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("TeacherId"))
            };
        }
    }
}