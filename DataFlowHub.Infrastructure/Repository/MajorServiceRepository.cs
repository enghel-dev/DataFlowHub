using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class MajorRepository : IMajorRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public MajorRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Major>> GetAllAsync()
        {
            var list = new List<Major>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_Majors_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task<Major> GetByIdAsync(int id)
        {
            Major major = null;
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_Majors_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                major = MapToEntity(dr);
            }
            return major;
        }

        public async Task CreateAsync(Major major)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_Majors_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = major.Name });
            cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 10) { Value = major.Code });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Major major)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_Majors_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = major.Id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = major.Name });
            cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 10) { Value = major.Code });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Catalog.usp_Majors_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private static Major MapToEntity(SqlDataReader dr)
        {
            return new Major
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                Code = dr.GetString(dr.GetOrdinal("Code"))
            };
        }
    }
}