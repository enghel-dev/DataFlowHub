namespace DataFlowHub.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Relaciones
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}