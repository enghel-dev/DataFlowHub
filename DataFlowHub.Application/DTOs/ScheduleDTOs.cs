using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class ScheduleDTOs
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Relaciones
        public int CourseId { get; set; }
        public virtual CourseDTOs Course { get; set; }

        public int ClassroomId { get; set; }
        public virtual ClassroomDTOs Classroom { get; set; }
    }
}
