using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IUserRepository
    {
        // Buscar usuario por Username (Carnet o No. Empleado) para el Login
        Task<User> GetByUsernameAsync(string username);

        // Crear un nuevo usuario (al registrar estudiante o profesor)
        Task<int> CreateAsync(User user);

        // Actualizar contraseña o rol
        Task UpdateAsync(User user);
    }
}