using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class GradeService
    {
        private readonly IGradeRepository _repository;

        public GradeService(IGradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GradeDTOs>> GetByEnrollmentIdAsync(int enrollmentId)
        {
            if (enrollmentId <= 0) return Enumerable.Empty<GradeDTOs>();

            var grades = await _repository.GetByEnrollmentIdAsync(enrollmentId);
            return grades.Select(g => new GradeDTOs
            {
                Id = g.Id,
                Name = g.Name,
                Value = g.Value,
                EvaluationDate = g.EvaluationDate,
                EnrollmentId = g.EnrollmentId
            });
        }

        public async Task<bool> CreateAsync(GradeDTOs dto)
        {
            // Validación: No permitir notas negativas (ajustar según escala local)
            if (dto.Value < 0 || dto.EnrollmentId <= 0) return false;

            var entity = new Grade
            {
                Name = dto.Name,
                Value = dto.Value,
                EvaluationDate = dto.EvaluationDate == default ? DateTime.Now : dto.EvaluationDate,
                EnrollmentId = dto.EnrollmentId
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(GradeDTOs dto)
        {
            if (dto.Id <= 0 || dto.Value < 0) return false;

            // En Dapper, si el SP hace WHERE IsActive = 1, la actualización es segura
            var entity = new Grade
            {
                Id = dto.Id,
                Name = dto.Name,
                Value = dto.Value,
                EvaluationDate = dto.EvaluationDate,
                EnrollmentId = dto.EnrollmentId
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