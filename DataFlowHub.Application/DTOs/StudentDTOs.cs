using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class StudentDTOs
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
        public virtual MajorDTOs Major { get; set; }
    }
}
