using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.CourseNotification
{
    public enum CourseNotificationType
    {
        [Display(Name = "UpdateCourse")]
        UpdateCourse,
        [Display(Name = "FinishCourse")]
        FinishCourse,
    }
}
