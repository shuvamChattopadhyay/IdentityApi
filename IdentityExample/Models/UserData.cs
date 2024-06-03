using System.ComponentModel.DataAnnotations;

namespace IdentityExample.Models
{
    public class UserData
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public int UserUniqueId { get; set; }
        public string password { get; set; }
        public string UserRole { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
