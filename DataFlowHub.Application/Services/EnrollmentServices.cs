using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentService(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EnrollmentDTOs>> GetAllAsync()
        {
            var enrollments = await _repository.GetAllAsync();

            return enrollments.Select(e => new EnrollmentDTOs
            {
                Id = e.Id,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                StudentId = e.StudentId,
                CourseId = e.CourseId
            });
        }

        public async Task<IEnumerable<EnrollmentDTOs>> GetByStudentIdAsync(int studentId)
        {
            if (studentId <= 0) return Enumerable.Empty<EnrollmentDTOs>();

            var history = await _repository.GetByStudentIdAsync(studentId);

            return history.Select(e => new EnrollmentDTOs
            {
                Id = e.Id,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status,
                StudentId = e.StudentId,
                CourseId = e.CourseId
            });
        }

        public async Task<bool> CreateAsync(EnrollmentDTOs dto)
        {
            // Validaciones de negocio: IDs válidos
            if (dto.StudentId <= 0 || dto.CourseId <= 0) return false;

            var entity = new Enrollment
            {
                EnrollmentDate = dto.EnrollmentDate == default ? DateTime.Now : dto.EnrollmentDate,
                Status = dto.Status ?? "Active",
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(status)) return false;

            // Nota: Aquí el SP debería validar internamente que IsActive = 1
            await _repository.UpdateStatusAsync(id, status);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            // Soft Delete via SP
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}