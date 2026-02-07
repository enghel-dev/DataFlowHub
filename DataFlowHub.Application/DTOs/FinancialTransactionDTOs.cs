using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class FinancialTransactionDTOs
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public int TransactionType { get; set; } // 1=Cargo, 2=Abono
        public string Description { get; set; }

        // Relaciones
        public int StudentId { get; set; }
        public virtual StudentDTOs Student { get; set; }
    }
}
