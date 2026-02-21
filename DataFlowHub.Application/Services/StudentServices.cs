using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class StudentService 
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StudentDTOs>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(s => new StudentDTOs
            {
                Id = s.Id,
                RegistrationNumber = s.RegistrationNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                Address = s.Address,
                Phone = s.Phone,
                MajorId = s.MajorId
            });
        }

        public async Task<StudentDTOs?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;
            var s = await _repository.GetByIdAsync(id);
            if (s == null) return null;

            return new StudentDTOs
            {
                Id = s.Id,
                RegistrationNumber = s.RegistrationNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                Address = s.Address,
                Phone = s.Phone,
                MajorId = s.MajorId
            };
        }

        public async Task<bool> CreateAsync(StudentDTOs dto)
        {
            // Validar que el carnet no exista ya
            var existing = await _repository.GetByRegistrationNumberAsync(dto.RegistrationNumber);
            if (existing != null) return false;

            var entity = new Student
            {
                RegistrationNumber = dto.RegistrationNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                Phone = dto.Phone,
                MajorId = dto.MajorId
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(StudentDTOs dto)
        {
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null) return false;

            var entity = new Student
            {
                Id = dto.Id,
                RegistrationNumber = dto.RegistrationNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                Phone = dto.Phone,
                MajorId = dto.MajorId
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