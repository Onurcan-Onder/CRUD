using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCRUD.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DoB { get; set; }
        public string Email { get; set; } = string.Empty;
        public int SkillLevelId { get; set; }
        public bool Active { get; set; }
        public int Age { get; set; }
    }
}