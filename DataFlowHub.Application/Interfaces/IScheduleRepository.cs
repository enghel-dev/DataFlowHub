using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IScheduleRepository
    {
        // Ver horarios de un curso especifico
        Task<IEnumerable<Schedule>> GetByCourseIdAsync(int courseId);

        // Asignar un horario
        Task CreateAsync(Schedule schedule);

        // Modificar horario
        Task UpdateAsync(Schedule schedule);

        // Eliminar horario
        Task DeleteAsync(int id);
    }
}