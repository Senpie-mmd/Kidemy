using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.UserPanel
{
    public class UserPanelCreateNewCourseVideoViewModel
    {
        [Display(Name = "CourseVideoTitle")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "CourseVideoCategory")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string CourseVideoCategoryTitle { get; set; }

        //[Display(Name = "ThumbnailImageForVideo")]
        //[Required(ErrorMessage = ErrorMessages.RequiredError)]
        //public IFormFile VideoThumbnailImage { get; set; }

        [Display(Name = "CourseVideoFile")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string VideoFileName { get; set; }

        [Display(Name = "VideoLength")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string VideoLength { get; set; }

        [Display(Name = "IsFree")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public bool IsFree { get; set; } = true;

        public int CourseId { get; set; }

        [Display(Name = "AttachFile")]
        public string? AttachFileName { get; set; }

        public List<AdminSideCourseVideoCategoryViewModel>? VideoCategories { get; set; }

        [Display(Name = "SessionNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int Priority { get; set; } = 1;

        public bool? CanChangeIsFree { get; set; }
    }
}
