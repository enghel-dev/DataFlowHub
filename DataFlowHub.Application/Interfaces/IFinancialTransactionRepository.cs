using DataFlowHub.Domain.Entities;

namespace DataFlowHub.Application.Interfaces
{
    public interface IFinancialTransactionRepository
    {
        // Ver estado de cuenta de un estudiante
        Task<IEnumerable<FinancialTransaction>> GetByStudentIdAsync(int studentId);

        // Registrar un pago o deuda
        Task CreateAsync(FinancialTransaction transaction);
    }
}