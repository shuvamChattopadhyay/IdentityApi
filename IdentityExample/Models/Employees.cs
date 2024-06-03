using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityExample.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Full_Name { get; set; }
        public string Address { get; set; }
        public string Mobile_no { get; set; }
        public int DepartmentId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [ForeignKey("UserId")]
        public UserData UserData { get; set; }

        
    }
}
