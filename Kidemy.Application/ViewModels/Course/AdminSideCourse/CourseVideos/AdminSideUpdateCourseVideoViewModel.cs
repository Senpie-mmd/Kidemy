using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos
{
    public class AdminSideUpdateCourseVideoViewModel : LocalizableViewModel<LocalizedAdminSideUpdateCourseVideoViewModel>
    {
        [Display(Name = "CourseVideoTitle")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "CourseVideoCategory")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string CourseVideoCategoryTitle { get; set; }

        //[Display(Name = "ThumbnailImageForVideo")]
        //public IFormFile? VideoThumbnailImage { get; set; }

        //public string VideoThumbnailImageFileName { get; set; }

        [Display(Name = "CourseVideoFile")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string VideoFileName { get; set; }

        [Display(Name = "VideoLength")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string VideoLength { get; set; }

        [Display(Name = "SessionNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int Priority { get; set; } = 1;

        [Display(Name = "IsFree")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsFree { get; set; } = true;

        [Display(Name = "IsPublished")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsPublished { get; set; }

        public List<AdminSideCourseVideoCategoryViewModel>? VideoCategories { get; set; }
        public int CourseId { get; set; }

        [Display(Name = "AttachFile")]
        public string? AttachFileName { get; set; }

        public bool? CanChageIsFree { get; set; }
    }

    public class LocalizedAdminSideUpdateCourseVideoViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
