using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public ClassroomRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Classroom>> GetAllAsync()
        {
            var classrooms = new List<Classroom>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Classrooms_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                classrooms.Add(MapToEntity(dr));
            }
            return classrooms;
        }

        public async Task<IEnumerable<Classroom>> GetByIdAsync(int id)
        {
            var classrooms = new List<Classroom>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Classrooms_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                classrooms.Add(MapToEntity(dr));
            }
            return classrooms;
        }

        public async Task CreateAsync(Classroom classroom)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Classrooms_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = classroom.Name });
            cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.NVarChar, 50) { Value = classroom.Location });
            cmd.Parameters.Add(new SqlParameter("@Capacity", SqlDbType.Int) { Value = classroom.Capacity });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Classroom classroom)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Classrooms_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = classroom.Id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = classroom.Name });
            cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.NVarChar, 50) { Value = classroom.Location });
            cmd.Parameters.Add(new SqlParameter("@Capacity", SqlDbType.Int) { Value = classroom.Capacity });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Classrooms_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        // Helper de mapeo para mantener el código DRY (Don't Repeat Yourself)
        private static Classroom MapToEntity(SqlDataReader dr)
        {
            return new Classroom
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                Location = dr.GetString(dr.GetOrdinal("Location")),
                Capacity = dr.GetInt32(dr.GetOrdinal("Capacity"))
            };
        }
    }
}