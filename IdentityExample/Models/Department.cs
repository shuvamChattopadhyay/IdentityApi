using System.ComponentModel.DataAnnotations;

namespace IdentityExample.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public string DeptHeadName { get; set; }
        public ICollection<Employees> Employees { get; set; }


    }
}
