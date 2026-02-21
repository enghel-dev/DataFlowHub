using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class MajorService 
    {
        private readonly IMajorRepository _repository;

        public MajorService(IMajorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MajorDTOs>> GetAllAsync()
        {
            var majors = await _repository.GetAllAsync();
            return majors.Select(m => new MajorDTOs
            {
                Id = m.Id,
                Name = m.Name,
                Code = m.Code
            });
        }

        public async Task<MajorDTOs?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new MajorDTOs
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code
            };
        }

        public async Task<bool> CreateAsync(MajorDTOs dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Code))
                return false;

            var entity = new Major
            {
                Name = dto.Name,
                Code = dto.Code.ToUpper() // Estandarizamos el código
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(MajorDTOs dto)
        {
            // Verificamos si existe antes de intentar actualizar
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null) return false;

            var entity = new Major
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code.ToUpper()
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