using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Domain.DTOs.Master
{
    public class ClientSideBioForMasterModel : BaseEntityViewModel<int>
    {
        public string? Bio { get; set; }
        public int UserId { get; set; }
    }
}
