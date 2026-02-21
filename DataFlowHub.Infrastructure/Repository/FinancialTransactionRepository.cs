using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace DataFlowHub.Infrastructure.Repository
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly DBconnectionFactory _dbConnectionFactory;

        public FinancialTransactionRepository(DBconnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        // Registrar un pago o deuda
        public Task<int> CreateAsync(FinancialTransaction transaction)
        {
            throw new NotImplementedException();
        }
        // Ver estado de cuenta de un estudiante
        public Task<IEnumerable<FinancialTransaction>> GetByStudentIdAsync(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
