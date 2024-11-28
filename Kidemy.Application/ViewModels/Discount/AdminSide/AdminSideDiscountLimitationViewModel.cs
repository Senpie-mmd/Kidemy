using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Domain.Enums.Discount;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Discount.AdminSide
{
    public class AdminSideDiscountLimitationViewModel : BaseEntityViewModel<int>
    {
        #region Properties

        public int DiscountId { get; set; }

        [Display(Name = "DiscountLimitationType")]
        public DiscountLimitationType Type { get; set; }

        #endregion

    }
}
