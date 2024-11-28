using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Course
{
    public enum CourseLevel
    {
        [Display(Name = "Beginner")]
        Beginner = 0,
        [Display(Name = "Intermediate")]
        Intermediate = 1,
        [Display(Name = "Advanced")]
        Advanced = 2
    }
}
