using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Infrastructure.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly DBconnectionFactory _dbConnectionFactory;

        public GradeRepository(DBconnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        // Ingresar una nota
        public Task<int> CreateAsync(Grade grade)
        {
            throw new NotImplementedException();
        }
        // Eliminar una nota
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        // Ver notas de una matrícula específica
        public Task<IEnumerable<Grade>> GetByEnrollmentIdAsync(int enrollmentId)
        {
            throw new NotImplementedException();
        }
        // Modificar una nota
        public Task UpdateAsync(Grade grade)
        {
            throw new NotImplementedException();
        }
    }
}
