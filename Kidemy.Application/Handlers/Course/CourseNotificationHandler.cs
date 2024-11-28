using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Enums.CourseNotification;
using Kidemy.Domain.Events.Course.Course;
using Kidemy.Domain.Events.Course.CourseVideo;
using MediatR;

namespace Kidemy.Application.Handlers.Course
{
    internal class CourseNotificationHandler : INotificationHandler<CourseUpdatedEvent>,
        INotificationHandler<CourseVideoCreatedEvent>
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ICourseService _courseService;

        public CourseNotificationHandler(IEmailService emailService, IUserService userService, ICourseService courseService)
        {
            _emailService = emailService;
            _userService = userService;
            _courseService = courseService;
        }

        public async Task Handle(CourseVideoCreatedEvent notification, CancellationToken cancellationToken)
        {
            var users = await _courseService.GetAllUsersNotificationByCourseIdAsync(notification.CourseId, CourseNotificationType.UpdateCourse);

            if (users.Value != null && users.Value.Any())
            {
                var userNames = await _userService.GetUserNameAndUserProfileByUserId(users.Value);
                var courseIdList = new List<int>();
                courseIdList.Add(notification.CourseId);
                var courseTitle = await _courseService.GetCourseTitles(courseIdList);
                foreach (var userId in users.Value)
                {
                    var userEmail = await _userService.GetUserEmailByIdAsync(userId);
                    if (userEmail.Value != null)
                    {

                        string message = string.Join(" ",
                               $"{userNames.Value.Where(x => x.Id == userId).FirstOrDefault().UserName} گرامی, قسمت {notification.Title} برای دوره {courseTitle.Value.Where(x => x.CourseId == notification.CourseId).FirstOrDefault().Title}  منتشر شد  ");
                        await _emailService.SendEmailAsync(userEmail.Value, "کیدمی ", message);
                    }
                }

            }
        }

        public async Task Handle(CourseUpdatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.prevStatus == Domain.Enums.Course.CourseStatus.Recording && notification.newStatus == Domain.Enums.Course.CourseStatus.Finished)
            {
                var users = await _courseService.GetAllUsersNotificationByCourseIdAsync(notification.Id, CourseNotificationType.UpdateCourse);

                if (users.Value != null && users.Value.Any())
                {
                    var userNames = await _userService.GetUserNameAndUserProfileByUserId(users.Value);
                    var courseIdList = new List<int>();
                    courseIdList.Add(notification.Id);
                    var courseTitle = await _courseService.GetCourseTitles(courseIdList);
                    foreach (var userId in users.Value)
                    {
                        var userEmail = await _userService.GetUserEmailByIdAsync(userId);
                        if (userEmail.Value != null)
                        {

                            string message = string.Join(" ",
                                   $"{userNames.Value.Where(x => x.Id == userId).FirstOrDefault().UserName} گرامی,   دوره  {notification.newTitle}  به پایان رسید شد  ");
                            await _emailService.SendEmailAsync(userEmail.Value, "کیدمی ", message);
                        }

                    }
                }

            }
        }
    }
}
