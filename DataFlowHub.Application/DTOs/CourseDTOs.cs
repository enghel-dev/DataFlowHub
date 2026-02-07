using DataFlowHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class CourseDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }

        // Relaciones
        public int TeacherId { get; set; }
        public virtual TeacherDTOs Teacher { get; set; }

        public int SchoolTermId { get; set; }
        public virtual SchoolTermDTOs SchoolTerm { get; set; }
    }
}
