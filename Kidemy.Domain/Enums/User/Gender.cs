using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.User
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female
    }
}
