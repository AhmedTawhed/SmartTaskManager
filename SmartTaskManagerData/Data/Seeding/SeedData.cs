using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Helpers.Enums;

namespace SmartTaskManager.Infrastructure.Data.Seeding
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

            context.Database.Migrate();

            var testUserEmail = "testuser@example.com";
            var testUser = userManager.Users.FirstOrDefault(u => u.Email == testUserEmail);

            if (testUser == null)
            {
                testUser = new ApplicationUser
                {
                    UserName = "testuser",
                    Email = testUserEmail,
                    EmailConfirmed = true
                };
                userManager.CreateAsync(testUser, "Test@123").Wait();
            }

            if (!context.TaskItems.Any(t => t.UserId == testUser.Id))
            {
                var random = new Random();
                var statuses = Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>().ToArray();
                var priorities = Enum.GetValues(typeof(PriorityEnum)).Cast<PriorityEnum>().ToArray();

                var sampleTasks = new[]
                {
                    ("Finish Project Report", "Complete the final report for the project and submit it."),
                    ("Email Client", "Send the monthly update email to the client."),
                    ("Team Meeting", "Prepare slides and agenda for the team meeting."),
                    ("Code Review", "Review pull requests from teammates."),
                    ("Update Documentation", "Update the API documentation with the latest changes."),
                    ("Fix Bug #234", "Resolve the reported issue in the login module."),
                    ("Design Mockups", "Create mockups for the new dashboard feature."),
                    ("Backup Database", "Perform a full backup of the database."),
                    ("Plan Sprint Tasks", "Organize tasks for the next sprint."),
                    ("Optimize Queries", "Improve database query performance."),
                    ("Deploy App", "Deploy the latest version to staging environment."),
                    ("User Feedback Analysis", "Analyze feedback from beta users.")
                };

                foreach (var (title, desc) in sampleTasks)
                {
                    var task = new TaskItem
                    {
                        Title = title,
                        Description = desc,
                        Status = statuses[random.Next(statuses.Length)],
                        Priority = priorities[random.Next(priorities.Length)],
                        Deadline = DateTime.UtcNow.AddDays(random.Next(10, 75)),
                        UserId = testUser.Id
                    };
                    context.TaskItems.Add(task);
                }

                context.SaveChanges();
            }
        }
    }
}
