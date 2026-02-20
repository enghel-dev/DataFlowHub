using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class StudentServices
    {
        private readonly IStudentRepository _repository;

        public StudentServices(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StudentDTO>> GetAll()
        {
            var students = await _repository.GetAllAsync();

            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            });
        }

        public async Task<StudentDTO> GetById(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
                return null;

            return new StudentDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName
            };
        }

        public async Task Create(StudentDTO dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            await _repository.CreateAsync(student);
        }

        public async Task Update(StudentDTO dto)
        {
            var student = new Student
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            await _repository.UpdateAsync(student);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
