using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Blog
{
    public class ClientSideFilterBlogViewModel : BasePaging<ClientSideBlogViewModel>
    {
        [Display(Name = "Title")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        public string? Slug { get; set; }
    }
}
