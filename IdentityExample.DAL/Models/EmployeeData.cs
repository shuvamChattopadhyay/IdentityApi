using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.DAL.Models
{
    public class EmployeeData
    {
        public int Id { get; set; } 
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public int EmployeeUniqueId { get; set; }
    }
}
