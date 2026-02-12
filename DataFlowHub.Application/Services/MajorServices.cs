using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class MajorService
    {
        // 1. Inyección del Repositorio
        private readonly IMajorRepository _majorRepository;

        public MajorService(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        // 2. Implementación de métodos

        public async Task<IEnumerable<MajorDTOs>> GetAllAsync()
        {
            // Obtener entidades
            var majors = await _majorRepository.GetAllAsync();

            // Mapear Entidad -> DTO
            return majors.Select(m => new MajorDTOs
            {
                Id = m.Id,
                Name = m.Name,
                Code = m.Code
            });
        }

        public async Task<MajorDTOs> GetByIdAsync(int id)
        {
            var major = await _majorRepository.GetByIdAsync(id);

            if (major == null) return null;

            return new MajorDTOs
            {
                Id = major.Id,
                Name = major.Name,
                Code = major.Code
            };
        }

        public async Task<int> CreateAsync(MajorDTOs majorDto)
        {
            // Convertir DTO en Entidad para guardar en BD
            var majorEntity = new Major
            {
                Name = majorDto.Name,
                Code = majorDto.Code
            };

            return await _majorRepository.CreateAsync(majorEntity);
        }

        public async Task UpdateAsync(MajorDTOs majorDto)
        {
            // Convertir DTO en Entidad para actualizar
            var majorEntity = new Major
            {
                Id = majorDto.Id,
                Name = majorDto.Name,
                Code = majorDto.Code
            };

            await _majorRepository.UpdateAsync(majorEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _majorRepository.DeleteAsync(id);
        }
    }
}