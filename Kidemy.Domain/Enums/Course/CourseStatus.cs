using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Course
{
    public enum CourseStatus
    {
        [Display(Name = "Recording")]
        Recording,

        [Display(Name = "Finished")]
        Finished
    }
}
