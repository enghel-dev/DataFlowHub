using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Infrastructure.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly DBconnectionFactory _dBconnectionFactory;

        public EnrollmentRepository(DBconnectionFactory dBconnectionFactory)
        {
            _dBconnectionFactory = dBconnectionFactory;
        }
        // Matricular estudiante en una clase
        public Task<int> CreateAsync(Enrollment enrollment)
        {
            throw new NotImplementedException();
        }
        // Eliminar matrícula
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        // Listar todas las matrículas
        public Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        // Ver historial de clases de un estudiante
        public Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }
        // Actualizar estado (ej: Retirada, Aprobada)
        public Task UpdateStatusAsync(int id, string status)
        {
            throw new NotImplementedException();
        }
    }
}
