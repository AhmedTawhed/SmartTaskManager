using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Interfaces.IRepository;
using SmartTaskManager.Core.Interfaces.IService;
using SmartTaskManager.Infrastructure.Data;
using SmartTaskManager.Infrastructure.Data.Seeding;
using SmartTaskManager.Infrastructure.Repository;
using SmartTaskManager.Infrastructure.Services;
using SmartTaskManager.Middlewares;

namespace SmartTaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SmartTaskDbContext")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<TaskDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAntiforgery();

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            SeedData.Initialize(app.Services);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
