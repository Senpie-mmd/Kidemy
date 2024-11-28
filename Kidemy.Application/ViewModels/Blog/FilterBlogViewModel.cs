using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Blog;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Blog
{
    public class FilterBlogViewModel : BasePaging<AdminSideBlogViewModel>
    {
        [Display(Name = "Title")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        [Display(Name = "IsPublished")]
        public bool? IsPublished { get; set; }
    }   
}
