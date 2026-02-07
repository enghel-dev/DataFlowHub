namespace DataFlowHub.Domain.Entities
{
    public class Major
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        // Propiedades de navegación
        public virtual ICollection<Student> Students { get; set; }
    }
}