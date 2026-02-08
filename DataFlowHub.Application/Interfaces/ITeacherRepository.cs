using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface ITeacherRepository
    {
        // Listar todos los profesores
        Task<IEnumerable<Teacher>> GetAllAsync();

        // Obtener profesor por ID
        Task<Teacher> GetByIdAsync(int id);

        // Obtener profesor por Número de Empleado
        Task<Teacher> GetByEmployeeNumberAsync(string employeeNumber);

        // Registrar nuevo profesor
        Task<int> CreateAsync(Teacher teacher);

        // Editar datos del profesor
        Task UpdateAsync(Teacher teacher);

        // Eliminar profesor
        Task DeleteAsync(int id);
    }
}