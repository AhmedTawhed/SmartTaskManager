using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Core.Entities;

namespace SmartTaskManager.Infrastructure.Data
{
    public class TaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
