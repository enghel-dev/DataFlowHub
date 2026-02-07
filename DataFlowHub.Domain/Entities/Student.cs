using System;

namespace DataFlowHub.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } // Carnet
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Relaciones (Entity Framework las detecta por el nombre Id)
        public int? MajorId { get; set; }
        public virtual Major Major { get; set; }
    }
}