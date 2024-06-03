using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityExample.Models
{
    public class EmployeeDbContext : IdentityDbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserData> UserData { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
