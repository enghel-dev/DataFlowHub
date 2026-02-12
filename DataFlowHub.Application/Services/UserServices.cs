using System;
using System.Threading.Tasks;
using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class UserService
    {
        // 1. Inyección del Repositorio
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // 2. Implementación de métodos

        public async Task<UserDTOs> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null) return null;

            // Mapeo Entidad en DTO
            return new UserDTOs
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role,
                StudentId = user.StudentId, // Relación con Estudiante
                TeacherId = user.TeacherId  // Relación con Profesor
            };
        }

        public async Task<int> CreateAsync(UserDTOs userDto)
        {
            // Mapeo DTO en Entidad para guardar
            var userEntity = new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = userDto.Role,
                StudentId = userDto.StudentId,
                TeacherId = userDto.TeacherId
            };

            return await _userRepository.CreateAsync(userEntity);
        }

        public async Task UpdateAsync(UserDTOs userDto)
        {
            // Mapeo DTO en Entidad para actualizar
            var userEntity = new User
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Password = userDto.Password,
                Role = userDto.Role,
                StudentId = userDto.StudentId,
                TeacherId = userDto.TeacherId
            };

            await _userRepository.UpdateAsync(userEntity);
        }
    }
}