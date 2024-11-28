using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.MasterContract
{
    public class ClientSideMasterContractDetailsViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [Translate(EntityName = nameof(Domain.Models.Master.MasterContract))]
        public string Title { get; set; }

        [Display(Name = "FileName")]
        public string FileName { get; set; }
        public string File { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
