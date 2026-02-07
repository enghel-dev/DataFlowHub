using System;

namespace DataFlowHub.Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime EvaluationDate { get; set; }

        // Relaciones
        public int EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }
    }
}