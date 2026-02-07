using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Domain.Interfaces
{
    public interface ISchoolTermRepository
    {
        // Listar periodos
        Task<IEnumerable<SchoolTerm>> GetAllAsync();

        // Obtener cual es el periodo ACTUAL activo
        Task<SchoolTerm> GetActiveTermAsync();

        // Crear periodo
        Task<int> CreateAsync(SchoolTerm term);

        // Activar/Desactivar periodo
        Task UpdateAsync(SchoolTerm term);
    }
}