using System;
using System.Collections.Generic;
using System.Text;

namespace DataFlowHub.Application.DTOs
{
    public class MajorDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        // Propiedades de navegación
        public virtual ICollection<StudentDTOs> Students { get; set; }
    }
}
