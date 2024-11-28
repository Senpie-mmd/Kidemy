using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.WithrawRequest
{
    public class AcceptWithdrawRequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "RefId")]
        [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? RefId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
