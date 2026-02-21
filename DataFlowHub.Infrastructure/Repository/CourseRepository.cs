using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        //Inyeccion de dependencias

        private readonly DBconnectionFactory _dBconnectionFactory;

        public CourseRepository(DBconnectionFactory dBconnectionFactory)
        {
            _dBconnectionFactory = dBconnectionFactory;
        }
        // Crear nuevo curso
        public Task<int> CreateAsync(Course course)
        {
            throw new NotImplementedException();
        }
        // Eliminar curso
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        // Listar todas las materias/cursos
        public Task<IEnumerable<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        // Obtener curso por ID
        public Task<IEnumerable<Course>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        // Listar cursos de un profesor especifico
        public Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId)
        {
            throw new NotImplementedException();
        }
        // Editar curso
        public Task UpdateAsync(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
