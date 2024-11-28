using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Order;

public class ClientSideFilterOrderViewModel : BasePaging<ClientSideOrderViewModel>
{
    [Display(Name = "User")]
    public int? UserId { get; set; }
}
