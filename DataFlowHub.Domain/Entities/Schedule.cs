using System;

namespace DataFlowHub.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Relaciones
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; }
    }
}