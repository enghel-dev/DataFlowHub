using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class GradeDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime EvaluationDate { get; set; }

        // Relaciones
        public int EnrollmentId { get; set; }
        public virtual EnrollmentDTOs Enrollment { get; set; }
    }
}
