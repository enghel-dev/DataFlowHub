using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataFlowHub.Application.DTOs;
using DataFlowHub.Application.Interfaces;
using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Services
{
    public class FinancialTransactionService
    {
        // 1. Inyección del Repositorio
        private readonly IFinancialTransactionRepository _financialRepository;

        public FinancialTransactionService(IFinancialTransactionRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        // 2. Implementación de métodos

        public async Task<IEnumerable<FinancialTransactionDTOs>> GetByStudentIdAsync(int studentId)
        {
            // Obtener transacciones del repositorio
            var transactions = await _financialRepository.GetByStudentIdAsync(studentId);

            // Mapear Entidad en DTO
            return transactions.Select(t => new FinancialTransactionDTOs
            {
                Id = t.Id,
                TransactionDate = t.TransactionDate,
                Amount = t.Amount,
                TransactionType = t.TransactionType, // 1=Cargo, 2=Abono
                Description = t.Description,
                StudentId = t.StudentId
            });
        }

        public async Task<int> CreateAsync(FinancialTransactionDTOs transactionDto)
        {
            // Mapear DTO en Entidad para guardar
            var transactionEntity = new FinancialTransaction
            {
                TransactionDate = transactionDto.TransactionDate,
                Amount = transactionDto.Amount,
                TransactionType = transactionDto.TransactionType,
                Description = transactionDto.Description,
                StudentId = transactionDto.StudentId
            };

            return await _financialRepository.CreateAsync(transactionEntity);
        }
    }
}