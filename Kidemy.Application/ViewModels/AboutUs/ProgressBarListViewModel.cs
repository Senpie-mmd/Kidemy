using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Models.AboutUs;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.AboutUs
{
    public class ProgressBarListViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(AboutUsProgressBar))]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Percent")]
        public int Percent { get; set; }
    }
}
