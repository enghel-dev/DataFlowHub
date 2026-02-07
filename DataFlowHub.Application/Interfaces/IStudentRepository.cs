using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Domain.Interfaces
{
    public interface IStudentRepository
    {
        // Listar todos los estudiantes
        Task<IEnumerable<Student>> GetAllAsync();

        // Obtener un estudiante por su ID
        Task<Student> GetByIdAsync(int id);

        // Obtener un estudiante por su Carnet
        Task<Student> GetByRegistrationNumberAsync(string registrationNumber);

        // Crear un nuevo estudiante
        Task<int> CreateAsync(Student student);

        // Editar un estudiante existente
        Task UpdateAsync(Student student);

        // Eliminar un estudiante
        Task DeleteAsync(int id);
    }
}