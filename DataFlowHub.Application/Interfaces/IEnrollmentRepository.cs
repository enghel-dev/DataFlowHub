using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IEnrollmentRepository
    {
        // Listar todas las matrículas
        Task<IEnumerable<Enrollment>> GetAllAsync();

        // Ver historial de clases de un estudiante
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);

        // Matricular estudiante en una clase
        Task<int> CreateAsync(Enrollment enrollment);

        // Actualizar estado (ej: Retirada, Aprobada)
        Task UpdateStatusAsync(int id, string status);

        // Eliminar matrícula
        Task DeleteAsync(int id);
    }
}