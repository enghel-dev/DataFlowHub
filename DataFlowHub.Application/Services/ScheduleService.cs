using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _repository;

        public ScheduleService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ScheduleDTOs>> GetByCourseIdAsync(int courseId)
        {
            if (courseId <= 0) return Enumerable.Empty<ScheduleDTOs>();

            var schedules = await _repository.GetByCourseIdAsync(courseId);
            return schedules.Select(s => new ScheduleDTOs
            {
                Id = s.Id,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                CourseId = s.CourseId,
                ClassroomId = s.ClassroomId
            });
        }

        public async Task<bool> CreateAsync(ScheduleDTOs dto)
        {
            // Validación de negocio: Horario coherente
            if (dto.StartTime >= dto.EndTime) return false;
            if (dto.CourseId <= 0 || dto.ClassroomId <= 0) return false;

            var entity = new Schedule
            {
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                CourseId = dto.CourseId,
                ClassroomId = dto.ClassroomId
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(ScheduleDTOs dto)
        {
            if (dto.Id <= 0 || dto.StartTime >= dto.EndTime) return false;

            var entity = new Schedule
            {
                Id = dto.Id,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                CourseId = dto.CourseId,
                ClassroomId = dto.ClassroomId
            };

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}