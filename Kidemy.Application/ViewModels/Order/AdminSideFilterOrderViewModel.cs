using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Kidemy.Application.ViewModels.Order;

public class AdminSideFilterOrderViewModel : BasePaging<AdminSideOrderViewModel>
{
    [Display(Name = "OrderId")]
    public int? OrderId { get; set; }

    [Display(Name = "User")]
    public int? UserId { get; set; }

    public string? UserName { get; set; }

    [Display(Name = "FromDate")]
    public string? FromDate { get; set; }

    [Display(Name = "FromDate")]
    public string? ToDate { get; set; }

    [Display(Name = "Status")]
    public bool? IsPaid { get; set; } 
}
