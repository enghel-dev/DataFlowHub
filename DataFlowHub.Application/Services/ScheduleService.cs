using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        // Obtener horarios por ID de Curso
        public async Task<IEnumerable<ScheduleDTOs>> GetByCourseId(int courseid)
        {
            // Validación: Si el ID es inválido, retornamos lista vacía
            if(courseid <= 0)
                return Enumerable.Empty<ScheduleDTOs>();


            var lista = await _scheduleRepository.GetByCourseIdAsync(courseid);

            //Mapeamos la entidad a DTO
            return lista.Select(s => new ScheduleDTOs
            {
                Id = s.Id,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                CourseId = s.CourseId,
                ClassroomId = s.ClassroomId
            });
           
        }

        // Crear nuevo horario
        public async Task Create(ScheduleDTOs scheduleDTOs)
        {
            // Mapeamos de DTO -> Entidad (Para guardar en BD)
            var oSchedule = new Schedule
            {
                DayOfWeek = scheduleDTOs.DayOfWeek,
                StartTime = scheduleDTOs.StartTime,
                EndTime = scheduleDTOs.EndTime,
                CourseId = scheduleDTOs.CourseId,
                ClassroomId = scheduleDTOs.ClassroomId
            };

            await _scheduleRepository.CreateAsync(oSchedule);
        }

        // Actualizar horario
        public async Task Update(ScheduleDTOs scheduleDTOs)
        {
            var oSchedule = new Schedule
            {
                Id = scheduleDTOs.Id,
                DayOfWeek = scheduleDTOs.DayOfWeek,
                StartTime = scheduleDTOs.StartTime,
                EndTime = scheduleDTOs.EndTime,
                CourseId = scheduleDTOs.CourseId,
                ClassroomId = scheduleDTOs.ClassroomId
            };

            await _scheduleRepository.UpdateAsync(oSchedule);
        }

        // Eliminar horario
        public async Task Delete(int id)
        {
            if (id > 0) // Solo intentamos borrar si el ID es válido
            {
                await _scheduleRepository.DeleteAsync(id);
            }
        }

    }
}
