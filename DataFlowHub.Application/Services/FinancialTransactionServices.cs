using DataFlowHub.Application.DTOs;
using DataFlowHub.Domain.Entities;
using DataFlowHub.Application.Interfaces;

namespace DataFlowHub.Application.Services
{
    public class FinancialTransactionService
    {
        private readonly IFinancialTransactionRepository _repository;

        public FinancialTransactionService(IFinancialTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FinancialTransactionDTOs>> GetByStudentIdAsync(int studentId)
        {
            if (studentId <= 0) return Enumerable.Empty<FinancialTransactionDTOs>();

            // El SP debe filtrar por IsActive = 1 para no mostrar transacciones anuladas
            var transactions = await _repository.GetByStudentIdAsync(studentId);

            return transactions.Select(t => new FinancialTransactionDTOs
            {
                Id = t.Id,
                TransactionDate = t.TransactionDate,
                Amount = t.Amount,
                TransactionType = t.TransactionType,
                Description = t.Description,
                StudentId = t.StudentId
            });
        }

        public async Task<bool> CreateAsync(FinancialTransactionDTOs dto)
        {
            // Regla de negocio: No permitir montos cero o negativos
            if (dto.Amount <= 0 || dto.StudentId <= 0) return false;

            // Validar que el tipo de transacción sea válido (1 o 2)
            if (dto.TransactionType != 1 && dto.TransactionType != 2) return false;

            var entity = new FinancialTransaction
            {
                TransactionDate = dto.TransactionDate == default ? DateTime.Now : dto.TransactionDate,
                Amount = dto.Amount,
                TransactionType = dto.TransactionType,
                Description = dto.Description,
                StudentId = dto.StudentId
            };

            await _repository.CreateAsync(entity);
            return true;
        }
    }
}