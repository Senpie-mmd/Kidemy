using Kidemy.Application.ViewModels.CourseRequest;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Models.CourseRequest;
using Kidemy.Domain.Models.Page;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseRequestMapper
    {
        public static Expression<Func<CourseRequest, ClientSideCourseRequestViewModel>> ClientMapCourseRequestViewModel => (CourseRequest courseRequest) =>
         new ClientSideCourseRequestViewModel()
         {
             Id = courseRequest.Id,
             Title = courseRequest.Title,
             RequestedById = courseRequest.RequestedById,
             PreferedMasterId = courseRequest.PreferedMasterId,
             Description = courseRequest.Description,
             SelectedTags = courseRequest.SelectedTags,
             CreatedDateOnUTC = courseRequest.CreatedDateOnUtc,
             VoteCount = courseRequest.Votes.Count(),
         };

        public static Expression<Func<CourseRequest, AdminSideCourseRequestViewModel>> AdminMapCourseRequestViewModel => (CourseRequest courseRequest) =>
     new AdminSideCourseRequestViewModel()
     {
         Id = courseRequest.Id,
         Title = courseRequest.Title,
         RequestedById = courseRequest.RequestedById,
         PreferedMasterId = courseRequest.PreferedMasterId,
         Description = courseRequest.Description,
         SelectedTags = courseRequest.SelectedTags,
         CreatedDateOnUTC = courseRequest.CreatedDateOnUtc,
         VoteCount = courseRequest.Votes.Count(),
         IsConfirm = courseRequest.IsConfirm
     };

        public static AdminSideCoureRequestDetailsViewModel MapFrom(this AdminSideCoureRequestDetailsViewModel model, CourseRequest courseRequest, List<UserNameAndUserProfileModel> userNames,string? preferedMasterName)
        {
            model.Id = courseRequest.Id;
            model.Title = courseRequest.Title;
            model.VoteCount = courseRequest.Votes.Count();
            model.Description = courseRequest.Description;
            model.SelectedTags = courseRequest.SelectedTags;
            model.PreferedMasterName = preferedMasterName;
            model.CreatedDateOnUTC = courseRequest.CreatedDateOnUtc;
            model.MasterVolunteersInfo = courseRequest.MasterVolunteers.Select(volunteer => new AdminSideCourseRequestMasterVolunteerViewModel()
            {
                MasterDescription = volunteer.MasterDescription,
                Mobile = userNames.FirstOrDefault(user => user.UserId == volunteer.MasterId)?.Mobile ?? "-",
                MasterId = volunteer.MasterId,
                CreatedDateOnUtc = volunteer.CreatedDateOnUtc,
                Id = volunteer.Id,
                MasterName = userNames.FirstOrDefault(user => user.UserId == volunteer.MasterId)?.UserName ?? "-"
            }).ToList();

 
            return model;
        }
    }
}
