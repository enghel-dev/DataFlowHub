using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Domain.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class ClassroomServices
    {
        //Inyeccion de dependecias
        private readonly IClassroomRepository _Repository;

        public ClassroomServices(IClassroomRepository repository)
        {
            _Repository = repository;
        }

        // Listar salones disponibles
        //Task<IEnumerable<Classroom>> GetAllAsync();
        public async Task<IEnumerable<ClassroomDTOs>> GetAll()
        {
            var listar = await _Repository.GetAllAsync();

            return listar.Select(c => new ClassroomDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Capacity = c.Capacity
            });
        }
        //Obtener info del salón
        public async Task<IEnumerable<ClassroomDTOs>> GetById(int id)
        {
            if (id > 0)
                return Enumerable.Empty<ClassroomDTOs>();
            
            var lista = await _Repository.GetByIdAsync(id);

            return lista.Select(c => new ClassroomDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Capacity = c.Capacity
            });
        }

        // Crear salón
        public async Task Create(ClassroomDTOs classroomDTOs)
        {
            var oClassroom = new Classroom
            {
                Name = classroomDTOs.Name,
                Location = classroomDTOs.Location,
                Capacity = classroomDTOs.Capacity
            };

            await _Repository.CreateAsync(oClassroom);
        }

        // Editar salón
        public async Task Update(ClassroomDTOs classroomDTOs)
        {
            var oClassroom = new Classroom
            {
                Id= classroomDTOs.Id,
                Name = classroomDTOs.Name,
                Location = classroomDTOs.Location,
                Capacity = classroomDTOs.Capacity
            };

            await _Repository.UpdateAsync(oClassroom);
        }

        // Eliminar salón
        public async Task Delete(int id)
        {
            await _Repository.DeleteAsync(id);
        }
    }
}
