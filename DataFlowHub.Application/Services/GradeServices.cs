using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class GradeServices
    {
        private readonly IGradeRepository _repository;

        public GradeServices(IGradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GradeDTO>> GetAll()
        {
            var grades = await _repository.GetAllAsync();

            return grades.Select(g => new GradeDTO
            {
                Id = g.Id,
                StudentId = g.StudentId,
                Score = g.Score
            });
        }

        public async Task Create(GradeDTO dto)
        {
            var grade = new Grade
            {
                StudentId = dto.StudentId,
                Score = dto.Score
            };

            await _repository.CreateAsync(grade);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
