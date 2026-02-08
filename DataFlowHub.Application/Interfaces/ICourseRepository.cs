using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface ICourseRepository
    {
        // Listar todas las materias/cursos
        Task<IEnumerable<Course>> GetAllAsync();

        // Obtener curso por ID
        Task<IEnumerable<Course>> GetByIdAsync(int id);

        // Listar cursos de un profesor especifico
        Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);

        // Crear nuevo curso
        Task<int> CreateAsync(Course course);

        // Editar curso
        Task UpdateAsync(Course course);

        // Eliminar curso
        Task DeleteAsync(int id);
    }
}