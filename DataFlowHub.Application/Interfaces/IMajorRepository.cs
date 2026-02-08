using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IMajorRepository
    {
        // Listar todas las carreras (Ingeniería, Derecho, etc.)
        Task<IEnumerable<Major>> GetAllAsync();

        // Obtener carrera por ID
        Task<Major> GetByIdAsync(int id);

        // Crear nueva carrera
        Task<int> CreateAsync(Major major);

        // Editar nombre de carrera
        Task UpdateAsync(Major major);

        // Eliminar carrera
        Task DeleteAsync(int id);
    }
}