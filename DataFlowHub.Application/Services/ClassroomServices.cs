using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class ClassroomService
    {
        private readonly IClassroomRepository _repository;

        public ClassroomService(IClassroomRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClassroomDTOs>> GetAllAsync()
        {
            // El SP interno ya filtra WHERE IsActive = 1
            var entities = await _repository.GetAllAsync();

            return entities.Select(c => new ClassroomDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Capacity = c.Capacity
            });
        }

        public async Task<ClassroomDTOs?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;

            // Esperamos un único resultado (o null si IsActive = 0)
            var result = await _repository.GetByIdAsync(id);
            var entity = result.FirstOrDefault();

            if (entity == null) return null;

            return new ClassroomDTOs
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                Capacity = entity.Capacity
            };
        }

        public async Task<bool> CreateAsync(ClassroomDTOs dto)
        {
            var entity = new Classroom
            {
                Name = dto.Name,
                Location = dto.Location,
                Capacity = dto.Capacity
            };

            await _repository.CreateAsync(entity);
            return true; // Asumimos éxito si no hay excepción del SP
        }

        public async Task<bool> UpdateAsync(ClassroomDTOs dto)
        {
            // Validar que el registro existe y está activo antes de editar
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null || !existing.Any()) return false;

            var entity = new Classroom
            {
                Id = dto.Id,
                Name = dto.Name,
                Location = dto.Location,
                Capacity = dto.Capacity
            };

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            // El SP ejecutará: UPDATE Classroom SET IsActive = 0, UpdatedAt = SYSDATETIME()...
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}