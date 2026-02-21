using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class TeacherService
    {
        private readonly ITeacherRepository _repository;

        public TeacherService(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TeacherDTOs>> GetAllAsync()
        {
            var teachers = await _repository.GetAllAsync();
            return teachers.Select(t => new TeacherDTOs
            {
                Id = t.Id,
                EmployeeNumber = t.EmployeeNumber,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone,
                HireDate = t.HireDate
            });
        }

        public async Task<TeacherDTOs?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;
            var t = await _repository.GetByIdAsync(id);
            if (t == null) return null;

            return new TeacherDTOs
            {
                Id = t.Id,
                EmployeeNumber = t.EmployeeNumber,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone,
                HireDate = t.HireDate
            };
        }

        public async Task<bool> CreateAsync(TeacherDTOs dto)
        {
            // Validar unicidad de número de empleado
            var existing = await _repository.GetByEmployeeNumberAsync(dto.EmployeeNumber);
            if (existing != null) return false;

            var entity = new Teacher
            {
                EmployeeNumber = dto.EmployeeNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                HireDate = dto.HireDate == default ? DateTime.Now : dto.HireDate
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(TeacherDTOs dto)
        {
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null) return false;

            var entity = new Teacher
            {
                Id = dto.Id,
                EmployeeNumber = dto.EmployeeNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                HireDate = dto.HireDate
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