using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Infrastructure.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataFlowHub.Infrastructure.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DBconnectionFactory _dbconnectionFactory;

        public ScheduleRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public async Task<IEnumerable<Schedule>> GetByCourseIdAsync(int courseId)
        {
            var list = new List<Schedule>();
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Schedules_GetByCourseId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CourseId", SqlDbType.Int) { Value = courseId });

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                list.Add(MapToEntity(dr));
            }
            return list;
        }

        public async Task CreateAsync(Schedule schedule)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Schedules_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@DayOfWeek", SqlDbType.NVarChar, 20) { Value = schedule.DayOfWeek });
            cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.Time) { Value = schedule.StartTime });
            cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.Time) { Value = schedule.EndTime });
            cmd.Parameters.Add(new SqlParameter("@CourseId", SqlDbType.Int) { Value = schedule.CourseId });
            cmd.Parameters.Add(new SqlParameter("@ClassroomId", SqlDbType.Int) { Value = schedule.ClassroomId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Schedules_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = schedule.Id });
            cmd.Parameters.Add(new SqlParameter("@DayOfWeek", SqlDbType.NVarChar, 20) { Value = schedule.DayOfWeek });
            cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.Time) { Value = schedule.StartTime });
            cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.Time) { Value = schedule.EndTime });
            cmd.Parameters.Add(new SqlParameter("@CourseId", SqlDbType.Int) { Value = schedule.CourseId });
            cmd.Parameters.Add(new SqlParameter("@ClassroomId", SqlDbType.Int) { Value = schedule.ClassroomId });

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _dbconnectionFactory.CreateConection();
            await con.OpenAsync();

            using var cmd = new SqlCommand("Scheduling.usp_Schedules_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await cmd.ExecuteNonQueryAsync();
        }

        private static Schedule MapToEntity(SqlDataReader dr)
        {
            return new Schedule
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                DayOfWeek = dr.GetString(dr.GetOrdinal("DayOfWeek")),
                StartTime = (TimeSpan)dr.GetValue(dr.GetOrdinal("StartTime")),
                EndTime = (TimeSpan)dr.GetValue(dr.GetOrdinal("EndTime")),
                CourseId = dr.GetInt32(dr.GetOrdinal("CourseId")),
                ClassroomId = dr.GetInt32(dr.GetOrdinal("ClassroomId"))
            };
        }
    }
}