using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCRUD.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Column(TypeName="Date")]
        public DateTime DoB { get; set; }
        public string Email { get; set; } = string.Empty;
        public EmployeeSkillLevel? SkillLevel { get; set; }
        public bool Active { get; set; }
        public int Age { get; set; }
    }
}
