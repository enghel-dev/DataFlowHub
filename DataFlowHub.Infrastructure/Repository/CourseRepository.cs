using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public CourseRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = new List<Course>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Academic.usp_Courses_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                courses.Add(MapToEntity(dr));
            }
            return courses;
        }

        public async Task<IEnumerable<Course>> GetByIdAsync(int id)
        {
            var courses = new List<Course>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Academic.usp_Courses_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                courses.Add(MapToEntity(dr));
            }
            return courses;
        }

        public async Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId)
        {
            var courses = new List<Course>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Academic.usp_Courses_GetByTeacherId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TeacherId", SqlDbType.Int) { Value = teacherId });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                courses.Add(MapToEntity(dr));
            }
            return courses;
        }

        public async Task CreateAsync(Course course)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Academic.usp_Courses_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = course.Name });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = course.Description });
            cmd.Parameters.Add(new SqlParameter("@Credits", SqlDbType.Int) { Value = course.Credits });
            cmd.Parameters.Add(new SqlParameter("@TeacherId", SqlDbType.Int) { Value = course.TeacherId });
            cmd.Parameters.Add(new SqlParameter("@SchoolTermId", SqlDbType.Int) { Value = course.SchoolTermId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Academic.usp_Courses_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = course.Id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = course.Name });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = course.Description });
            cmd.Parameters.Add(new SqlParameter("@Credits", SqlDbType.Int) { Value = course.Credits });
            cmd.Parameters.Add(new SqlParameter("@TeacherId", SqlDbType.Int) { Value = course.TeacherId });
            cmd.Parameters.Add(new SqlParameter("@SchoolTermId", SqlDbType.Int) { Value = course.SchoolTermId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Academic.usp_Courses_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private static Course MapToEntity(SqlDataReader dr)
        {
            return new Course
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                Name = dr.GetString(dr.GetOrdinal("Name")),
                Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? string.Empty : dr.GetString(dr.GetOrdinal("Description")),
                Credits = dr.GetInt32(dr.GetOrdinal("Credits")),
                TeacherId = dr.GetInt32(dr.GetOrdinal("TeacherId")),
                SchoolTermId = dr.GetInt32(dr.GetOrdinal("SchoolTermId"))
            };
        }
    }
}