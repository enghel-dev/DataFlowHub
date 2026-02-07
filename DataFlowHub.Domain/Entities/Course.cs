namespace DataFlowHub.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }

        // Relaciones
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public int SchoolTermId { get; set; }
        public virtual SchoolTerm SchoolTerm { get; set; }
    }
}