using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCRUD.DTOs
{
    public class EmployeeUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "Name";
        public string LastName { get; set; } = "Surname";
        [DataType(DataType.Date)]
        [Column(TypeName="Date")]
        public DateTime DoB { get; set; }
        public string Email { get; set; } = "Email";
        public int SkillLevel { get; set; } = 1;
        public bool Active { get; set; }
    }
}