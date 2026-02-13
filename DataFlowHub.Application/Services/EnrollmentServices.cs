using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class EnrollmentServices
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentServices(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EnrollmentDTO>> GetAll()
        {
            var enrollments = await _repository.GetAllAsync();

            return enrollments.Select(e => new EnrollmentDTO
            {
                Id = e.Id,
                StudentId = e.StudentId,
                CourseId = e.CourseId
            });
        }

        public async Task Create(EnrollmentDTO dto)
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            await _repository.CreateAsync(enrollment);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
