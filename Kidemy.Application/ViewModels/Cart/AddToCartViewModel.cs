using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Cart
{
    public class AddToCartViewModel
    {
        [Display(Name = "Course")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int? CourseId { get; set; }

        public int UserId { get; set; }
    }
}
