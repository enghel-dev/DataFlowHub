using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;

namespace DataFlowHub.Infrastructure.Repository
{
    public class ClassrooomRepository : IClassroomRepository
    {
        //Inyeccion de dependencias
        private readonly DBconnectionFactory _dbconnectionFactory;
        public ClassrooomRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        //Crear salon
        public async Task CreateAsync(Classroom classroom)
        {
            using var con =  _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("Scheduling.sp_CreateClassroom", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", classroom.Name));
                cmd.Parameters.Add(new SqlParameter("@Location", classroom.Location));
                cmd.Parameters.Add(new SqlParameter("@Capacity", classroom.Capacity));

                await cmd.ExecuteNonQueryAsync();

            }
        }
        //Borrar salon
        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("Scheduling.sp_DeleteClassroom", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", id));
                await cmd.ExecuteNonQueryAsync();

            }
        }

        //Listar salones
        public async Task<IEnumerable<Classroom>> GetAllAsync()
        {
            var olist = new List<Classroom>();
            using var con = _dbconnectionFactory.CreateConection();

            await con.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("Scheduling.sp_GetAllClassrooms", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        olist.Add(new Classroom
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            Location = dr["Location"].ToString(),
                            Capacity = Convert.ToInt32(dr["Capacity"])
                        });   
                    }
                }
            }
            return olist;
        }
        // Obtener salon por id
        public async Task<IEnumerable<Classroom>> GetByIdAsync(int id)
        {
            var olist = new List<Classroom>();
            using var con = _dbconnectionFactory.CreateConection();

            await con.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("Scheduling.sp_GetClassroomById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        olist.Add(new Classroom
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            Location = dr["Location"].ToString(),
                            Capacity = Convert.ToInt32(dr["Capacity"])
                        });
                    }
                }
            }
            return olist;
        }
        //Actualizar info del salón
        public async Task UpdateAsync(Classroom classroom)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using (SqlCommand cmd = new SqlCommand("Scheduling.sp_UpdateClassroom", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", classroom.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", classroom.Name));
                cmd.Parameters.Add(new SqlParameter("@Location", classroom.Location));
                cmd.Parameters.Add(new SqlParameter("@Capacity", classroom.Capacity));

                await cmd.ExecuteNonQueryAsync();

            }
        }
    }
}
