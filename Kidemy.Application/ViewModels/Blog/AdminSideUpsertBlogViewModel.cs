using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Blog;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Blog
{
    public class AdminSideUpsertBlogViewModel : LocalizableViewModel<LocalizedAdminSideUpsertBlogViewModel>
    {
        public AdminSideUpsertBlogViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }


        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "BlogText")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string BlogText { get; set; }

        [Display(Name = "ShortDescription")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string ShortDescription { get; set; }

        [Display(Name = "BlogTags")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string BlogTags { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Slug { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }

        [Display(Name = "ImageName")]
        public string? ImageName { get; set; }

        [Display(Name = "AuthorId")]
        public int AuthorId { get; set; }

        public string? AuthorName { get; set; }

        public IFormFile? Image { get; set; }
    }

    public class LocalizedAdminSideUpsertBlogViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "BlogText")]
        [UIHint("ckeditor")]
        public string? BlogText { get; set; }

        [Display(Name = "ShortDescription")]
        [UIHint("textarea")]
        public string? ShortDescription { get; set; }
    }
}
