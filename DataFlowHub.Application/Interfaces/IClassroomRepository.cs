using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IClassroomRepository
    {
        // Listar salones disponibles
        Task<IEnumerable<Classroom>> GetAllAsync();

        // Obtener info del salón
        Task<IEnumerable<Classroom>> GetByIdAsync(int id);

        // Crear salón
        Task CreateAsync(Classroom classroom);

        // Editar salón
        Task UpdateAsync(Classroom classroom);

        // Eliminar salón
        Task DeleteAsync(int id);
    }
}