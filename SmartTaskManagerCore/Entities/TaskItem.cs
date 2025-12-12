using SmartTaskManager.Core.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartTaskManager.Core.Entities
{
    public class TaskItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public StatusEnum Status { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;

        [Required]
        public DateTime Deadline { get; set; } = DateTime.Now;
        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
