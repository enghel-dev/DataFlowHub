using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class UserDTOs
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Relaciones
        public int? StudentId { get; set; }
        public virtual StudentDTOs Student { get; set; }

        public int? TeacherId { get; set; }
        public virtual TeacherDTOs Teacher { get; set; }
    }
}
