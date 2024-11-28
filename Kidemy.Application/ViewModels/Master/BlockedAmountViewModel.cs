using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Master
{
    public class BlockedAmountViewModel : BaseEntityViewModel<int>
    {
        public int MasterId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "BlockedAmount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Currency,ErrorMessage = ErrorMessages.FileTypeError)]
        public decimal? BlockedAmount { get; set; }
    }
}
