using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.AboutUs
{
    public class ClientSideAboutUsPageViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "PageTitle")]
        public string? PageTitle { get; set; }

        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "OurGoal")]
        public string? OurGoal { get; set; }

        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "OurGoalTitle")]
        public string? OurGoalTitle { get; set; }

        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "OurGoalDescription")]
        public string? OurGoalDescription { get; set; }

        [Translate(EntityName = nameof(AboutUs))]
        [Display(Name = "OurGoalFeatures")]
        public string? OurGoalFeatures { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber1 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber2 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber3 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber4 { get; set; }

        [Display(Name = "AboutUsImage")]
        public string? ImageNumber5 { get; set; }

        public IFormFile? ImageFileNumber1 { get; set; }
        public IFormFile? ImageFileNumber2 { get; set; }
        public IFormFile? ImageFileNumber3 { get; set; }
        public IFormFile? ImageFileNumber4 { get; set; }
        public IFormFile? ImageFileNumber5 { get; set; }
    }
}
