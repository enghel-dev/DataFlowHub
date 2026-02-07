using System; 

namespace DataFlowHub.Domain.Entities
{
    public class SchoolTerm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}