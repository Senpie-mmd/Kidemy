using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.MasterContract
{
    public class FilterMasterContractViewModel : BasePaging<AdminSideMasterContractViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
        public bool IsPublished { get; set; }
    }
}
