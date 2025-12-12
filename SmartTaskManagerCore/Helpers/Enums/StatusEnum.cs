using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartTaskManager.Core.Helpers.Enums
{
    public enum StatusEnum
    {
        [Display(Name="To Do")]
        ToDo,

        [Display(Name = "In Progress")]
        InProgress,

        Done
    }
}