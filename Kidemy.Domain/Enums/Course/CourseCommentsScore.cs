using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Course
{
    public enum CourseCommentsScore
    {
        [Display(Name = "★★★★★ (5/5)")]
        VeryGood = 5,
        [Display(Name = "★★★★☆ (4/5)")]
        Good = 4,
        [Display(Name = "★★★☆☆ (3/5)")]
        Normal = 3,
        [Display(Name = "★★☆☆☆ (2/5)")]
        Bad = 2,
        [Display(Name = "★☆☆☆☆ (1/5)")]
        VeryBad = 1,
    }
}
