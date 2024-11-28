using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Mapper;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Blog.AdminSideBlog.Blogs;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Interfaces.Blog;
using Kidemy.Domain.Models.Blog;
using Kidemy.Domain.Shared;
using System.Web;

namespace Kidemy.Application.Services.Implementations
{
    public class BlogCommentService : IBlogCommentService
    {
        #region Fields

        private readonly IBlogCommentRepository _commentRepository;
        private readonly IUserService _userService;

        #endregion

        #region Ctor
        public BlogCommentService(IBlogCommentRepository commentRepository,
            IUserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }
        #endregion

        #region Methods

        public async Task<Result<AdminSideFilterBlogCommentsViewModel>> FilterCommentsAdminSide(AdminSideFilterBlogCommentsViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterBlogCommentsViewModel>(ErrorMessages.FailedOperationError);
            var conditions = Filter.GenerateConditions<BlogComment>();

            conditions.Add(n => n.BlogId == filter.BlogId);

            if (filter.UserId is not null)
            {
                conditions.Add(n => n.CommentedById == filter.UserId);
            }

            if (filter.IsConfirmed is not null)
            {
                conditions.Add(n => n.IsConfirmed == filter.IsConfirmed);
            }

            if (!string.IsNullOrWhiteSpace(filter.CommentMessage))
            {
                string decodedMessage = HttpUtility.UrlDecode(filter.CommentMessage);
                conditions.Add(n => n.Message.Contains(decodedMessage));
            }

            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                filter.UserName = HttpUtility.UrlDecode(filter.UserName);
            }

            await _commentRepository.FilterAsync(filter, conditions, BlogCommentMapper.MapCommentsAdminSideViewModel);

            if (filter?.Entities?.Any() ?? false)
            {
                var userIds = filter.Entities.Select(n => n.CommentedById).ToList();
                List<UserFullNameModel> userInfos = await _userService.GetUsersFullNames(userIds);

                foreach (var item in filter.Entities)
                {
                    item.UserName = userInfos.First(n => n.UserId == item.CommentedById).UserFullName;
                }
            }

            return filter;
        }

        public async Task<Result<int>> ConfirmOrDenyComment(int commentId, bool Confirm)
        {
            if (commentId < 1) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment is null) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            if (Confirm == true)
            {
                comment.IsConfirmed = true;
            }
            else
            {
                comment.IsConfirmed = false;
            }

            _commentRepository.Update(comment);
            await _commentRepository.SaveAsync();

            return comment.BlogId;
        }


        #endregion
    }
}
