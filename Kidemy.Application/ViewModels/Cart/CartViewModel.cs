using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Discount.ClientSide;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Cart
{
    public class CartViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }

        [Display(Name = "TotalAmount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "DiscountedTotalAmount")]
        public decimal? DiscountedTotalAmount { get; set; }

        [ValidateNever]
        public ClientSideDiscountViewModel? AppliedDiscount { get; set; }

        public List<CartItemViewModel>? Items { get; set; }
    }
}
