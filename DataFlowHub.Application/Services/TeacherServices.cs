using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class TeacherService
    {
        // 1. Inyección del Repositorio
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        // 2. Implementación de métodos mapeando a DTOs

        public async Task<IEnumerable<TeacherDTOs>> GetAllAsync()
        {
            // Obtener entidades del repositorio
            var teachers = await _teacherRepository.GetAllAsync();

            // Mapear Entidad -> DTO
            return teachers.Select(t => new TeacherDTOs
            {
                Id = t.Id,
                EmployeeNumber = t.EmployeeNumber,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone,
                HireDate = t.HireDate
            });
        }

        public async Task<TeacherDTOs> GetByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null) return null;

            return new TeacherDTOs
            {
                Id = teacher.Id,
                EmployeeNumber = teacher.EmployeeNumber,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                HireDate = teacher.HireDate
            };
        }

        public async Task<TeacherDTOs> GetByEmployeeNumberAsync(string employeeNumber)
        {
            var teacher = await _teacherRepository.GetByEmployeeNumberAsync(employeeNumber);

            if (teacher == null) return null;

            return new TeacherDTOs
            {
                Id = teacher.Id,
                EmployeeNumber = teacher.EmployeeNumber,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                HireDate = teacher.HireDate
            };
        }

        public async Task<int> CreateAsync(TeacherDTOs teacherDto)
        {
           
            var teacherEntity = new Teacher
            {
                EmployeeNumber = teacherDto.EmployeeNumber,
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                Email = teacherDto.Email,
                Phone = teacherDto.Phone,
                HireDate = teacherDto.HireDate
            };

            return await _teacherRepository.CreateAsync(teacherEntity);
        }

        public async Task UpdateAsync(TeacherDTOs teacherDto)
        {
            
            var teacherEntity = new Teacher
            {
                Id = teacherDto.Id,
                EmployeeNumber = teacherDto.EmployeeNumber,
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                Email = teacherDto.Email,
                Phone = teacherDto.Phone,
                HireDate = teacherDto.HireDate
            };

            await _teacherRepository.UpdateAsync(teacherEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _teacherRepository.DeleteAsync(id);
        }
    }
}