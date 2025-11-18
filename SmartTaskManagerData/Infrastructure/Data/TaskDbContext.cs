using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTaskManagerCore.Core.Entities;

namespace SmartTaskManagerData.Infrastructure.Data
{
    public class TaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
