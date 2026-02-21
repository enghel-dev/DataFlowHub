using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public StudentRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var list = new List<Student>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Students_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            Student student = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Students_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                student = MapToEntity(dr);
            }
            return student;
        }

        public async Task<Student> GetByRegistrationNumberAsync(string registrationNumber)
        {
            Student student = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Students_GetByRegistrationNumber", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", SqlDbType.NVarChar, 10) { Value = registrationNumber });

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                student = MapToEntity(dr);
            }
            return student;
        }

        public async Task CreateAsync(Student student)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Students_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SetParameters(cmd, student);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Students_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = student.Id });
            SetParameters(cmd, student);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("People.usp_Students_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private void SetParameters(SqlCommand cmd, Student student)
        {
            cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", SqlDbType.NVarChar, 10) { Value = student.RegistrationNumber });
            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = student.FirstName });
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = student.LastName });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = student.Email });
            cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime2) { Value = student.DateOfBirth });
            cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 200) { Value = student.Address });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = student.Phone });
            cmd.Parameters.Add(new SqlParameter("@MajorId", SqlDbType.Int) { Value = (object)student.MajorId ?? DBNull.Value });
        }

        private static Student MapToEntity(SqlDataReader dr)
        {
            return new Student
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                RegistrationNumber = dr.GetString(dr.GetOrdinal("RegistrationNumber")),
                FirstName = dr.GetString(dr.GetOrdinal("FirstName")),
                LastName = dr.GetString(dr.GetOrdinal("LastName")),
                Email = dr.GetString(dr.GetOrdinal("Email")),
                DateOfBirth = dr.GetDateTime(dr.GetOrdinal("DateOfBirth")),
                Address = dr.IsDBNull(dr.GetOrdinal("Address")) ? null : dr.GetString(dr.GetOrdinal("Address")),
                Phone = dr.IsDBNull(dr.GetOrdinal("Phone")) ? null : dr.GetString(dr.GetOrdinal("Phone")),
                MajorId = dr.IsDBNull(dr.GetOrdinal("MajorId")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("MajorId"))
            };
        }
    }
}