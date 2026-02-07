using DataFlowHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class EnrollmentDTOs
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }

        // Relaciones
        public int StudentId { get; set; }
        public virtual StudentDTOs Student { get; set; }

        public int CourseId { get; set; }
        public virtual CourseDTOs Course { get; set; }
    }
}
