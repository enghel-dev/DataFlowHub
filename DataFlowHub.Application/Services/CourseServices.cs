using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.Services
{
    public class CourseServices
    {
        //Inyeccion de dependecias
        private readonly ICourseRepository _Repository;

        public CourseServices(ICourseRepository repository)
        {
            _Repository = repository;
        }

        //Listar todos los cursos
        public async Task<IEnumerable<CourseDTOs>> GetAll()
        {
            var listar = await _Repository.GetAllAsync();

            return listar.Select(c => new CourseDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Credits = c.Credits,
                TeacherId = c.TeacherId,
                SchoolTermId = c.SchoolTermId
            });
        }

        //Listar curso por id
        public async Task<IEnumerable<CourseDTOs>> GetById(int id)
        {
            if (id > 0)
                return Enumerable.Empty<CourseDTOs>();

            var lista = await _Repository.GetByIdAsync(id);

            return lista.Select(c => new CourseDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Credits = c.Credits,
                TeacherId = c.TeacherId,
                SchoolTermId = c.SchoolTermId
            });
        }

        //Listar cursos asignados a un maestro
        public async Task<IEnumerable<CourseDTOs>> GetByTeacherId(int teacherId)
        {
            if (teacherId > 0)
                return Enumerable.Empty<CourseDTOs>();

            var lista = await _Repository.GetByTeacherIdAsync(teacherId);

            return lista.Select(c => new CourseDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Credits = c.Credits,
                TeacherId = c.TeacherId,
                SchoolTermId = c.SchoolTermId
            });
        }

        // Crear nuevo curso

        public async Task Create(CourseDTOs courseDTOs)
        {
            var oCourse = new Course
            {
                Name = courseDTOs.Name,
                Description = courseDTOs.Description,
                Credits = courseDTOs.Credits,
                TeacherId = courseDTOs.TeacherId,
                SchoolTermId = courseDTOs.SchoolTermId
            };

            await _Repository.CreateAsync(oCourse);
        }

        //Actualizar curso
        public async Task Update(CourseDTOs courseDTOs)
        {
            var oCourse = new Course
            {
                Name = courseDTOs.Name,
                Description = courseDTOs.Description,
                Credits = courseDTOs.Credits,
                TeacherId = courseDTOs.TeacherId,
                SchoolTermId = courseDTOs.SchoolTermId
            };

            await _Repository.UpdateAsync(oCourse);
        }


        //Eliminar curso
        public async Task Delete(int id)
        {
            await _Repository.DeleteAsync(id);
        }
    }
}
