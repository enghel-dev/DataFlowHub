using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CourseDTOs>> GetAllAsync()
        {
            var courses = await _repository.GetAllAsync();

            return courses.Select(c => new CourseDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Credits = c.Credits,
                TeacherId = c.TeacherId,
                SchoolTermId = c.SchoolTermId
                // Nota: Teacher y SchoolTerm DTOs se llenarían aquí si el SP devuelve JOINs
            });
        }

        public async Task<CourseDTOs?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;

            var result = await _repository.GetByIdAsync(id);
            var entity = result.FirstOrDefault();

            if (entity == null) return null;

            return new CourseDTOs
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Credits = entity.Credits,
                TeacherId = entity.TeacherId,
                SchoolTermId = entity.SchoolTermId
            };
        }

        public async Task<IEnumerable<CourseDTOs>> GetByTeacherIdAsync(int teacherId)
        {
            if (teacherId <= 0) return Enumerable.Empty<CourseDTOs>();

            var courses = await _repository.GetByTeacherIdAsync(teacherId);

            return courses.Select(c => new CourseDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Credits = c.Credits,
                TeacherId = c.TeacherId,
                SchoolTermId = c.SchoolTermId
            });
        }

        public async Task<bool> CreateAsync(CourseDTOs dto)
        {
            // Validaciones básicas de integridad
            if (dto.TeacherId <= 0 || dto.SchoolTermId <= 0) return false;

            var entity = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                Credits = dto.Credits,
                TeacherId = dto.TeacherId,
                SchoolTermId = dto.SchoolTermId
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(CourseDTOs dto)
        {
            // Validar existencia antes de actualizar (Soft Delete Check)
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null || !existing.Any()) return false;

            var entity = new Course
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Credits = dto.Credits,
                TeacherId = dto.TeacherId,
                SchoolTermId = dto.SchoolTermId
            };

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            // El SP se encarga del UPDATE IsActive = 0
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}