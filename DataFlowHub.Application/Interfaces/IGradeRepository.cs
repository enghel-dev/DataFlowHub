using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IGradeRepository
    {
        // Ver notas de una matrícula específica
        Task<IEnumerable<Grade>> GetByEnrollmentIdAsync(int enrollmentId);

        // Ingresar una nota
        Task<int> CreateAsync(Grade grade);

        // Modificar una nota
        Task UpdateAsync(Grade grade);

        // Eliminar una nota
        Task DeleteAsync(int id);
    }
}