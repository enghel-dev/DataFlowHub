using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class SchoolTermService 
    {
        private readonly ISchoolTermRepository _repository;

        public SchoolTermService(ISchoolTermRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SchoolTermDTOs>> GetAllAsync()
        {
            var terms = await _repository.GetAllAsync();
            return terms.Select(t => new SchoolTermDTOs
            {
                Id = t.Id,
                Name = t.Name,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                IsActive = t.IsActive
            });
        }

        public async Task<SchoolTermDTOs?> GetActiveTermAsync()
        {
            var entity = await _repository.GetActiveTermAsync();
            if (entity == null) return null;

            return new SchoolTermDTOs
            {
                Id = entity.Id,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> CreateAsync(SchoolTermDTOs dto)
        {
            if (dto.StartDate >= dto.EndDate) return false;

            var entity = new SchoolTerm
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(SchoolTermDTOs dto)
        {
            if (dto.Id <= 0) return false;

            var entity = new SchoolTerm
            {
                Id = dto.Id,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            await _repository.UpdateAsync(entity);
            return true;
        }
    }
}