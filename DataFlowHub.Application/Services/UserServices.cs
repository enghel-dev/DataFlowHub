using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTOs?> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            var user = await _repository.GetByUsernameAsync(username);

            if (user == null) return null;

            return new UserDTOs
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                StudentId = user.StudentId,
                TeacherId = user.TeacherId
                // La contraseña NO se devuelve en el DTO por seguridad
            };
        }

        public async Task<bool> CreateAsync(UserDTOs dto)
        {
            // Validar si el nombre de usuario ya existe
            var existing = await _repository.GetByUsernameAsync(dto.Username);
            if (existing != null) return false;

            var entity = new User
            {
                Username = dto.Username,
                Password = dto.Password, // Nota: En un entorno real, aquí aplicaríamos Hash
                Role = dto.Role ?? "Guest",
                StudentId = dto.StudentId,
                TeacherId = dto.TeacherId
            };

            await _repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(UserDTOs dto)
        {
            if (dto.Id <= 0) return false;

            // En Dapper, el SP UpdateUser debe manejar la lógica de qué campos cambiar
            var entity = new User
            {
                Id = dto.Id,
                Username = dto.Username,
                Password = dto.Password,
                Role = dto.Role,
                StudentId = dto.StudentId,
                TeacherId = dto.TeacherId
            };

            await _repository.UpdateAsync(entity);
            return true;
        }
    }
}