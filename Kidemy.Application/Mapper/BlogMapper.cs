using Kidemy.Application.ViewModels.Blog;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs.Comment;
using Kidemy.Domain.Models.Blog;
using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class BlogMapper
    {
        public static Expression<Func<Blog, AdminSideBlogViewModel>> MapBlogViewModel => (Blog blog) =>
           new AdminSideBlogViewModel()
           {
               Id = blog.Id,
               Title = blog.Title,
               Slug = blog.Slug,
               IsPublished = blog.IsPublished,
               ImageName = blog.ImageName,
               AuthorId = blog.AuthorId
           };

        public static Expression<Func<Blog, ClientSideBlogViewModel>> MapClientSideBlogViewModel => (Blog blog) =>
           new ClientSideBlogViewModel()
           {
               Id = blog.Id,
               Title = blog.Title,
               Slug = blog.Slug,
               IsPublished = blog.IsPublished,
               ImageName = blog.ImageName,
               AuthorId = blog.AuthorId,
               CreatedDateOnUTC = blog.CreatedDateOnUtc,
               Body = blog.Body
           };

        public static AdminSideUpsertBlogViewModel MapFrom(this AdminSideUpsertBlogViewModel model, Blog blog)
        {
            model.Id = blog.Id;
            model.Title = blog.Title;
            model.BlogText = blog.Body;
            model.BlogTags = string.Join(",", blog.BlogTags.Select(n => n.Tag.Title).ToList());
            model.Slug = blog.Slug;
            model.IsPublished = blog.IsPublished;
            model.ImageName = blog.ImageName;
            model.AuthorId = blog.AuthorId;
            model.ShortDescription = blog.ShortDescription;

            return model;
        }

        public static ClientSideBlogViewModel MapFrom(this ClientSideBlogViewModel model, Blog blog)
        {
            model.Id = blog.Id;
            model.Title = blog.Title;
            model.Body = blog.Body;
            model.Slug = blog.Slug;
            model.IsPublished = blog.IsPublished;
            model.ImageName = blog.ImageName;
            model.AuthorId = blog.AuthorId;
            model.CreatedDateOnUTC = blog.CreatedDateOnUtc;

            return model;
        }
        public static ClientSideBlogViewModel MapFrom(this ClientSideBlogViewModel model, ClientSideUserNameAndUserProfileViewModel user)
        {
            model.AuthorId = user.Id;
            model.AuthorName = user.UserName;

            return model;
        }

        public static ClientSideBlogDetailViewModel MapFrom(this ClientSideBlogDetailViewModel model, Blog blog)
        {
            model.Id = blog.Id;
            model.Title = blog.Title;
            model.Body = blog.Body;
            model.Slug = blog.Slug;
            model.Tags = blog.BlogTags.Select(b => b.Tag.Title).ToList();
            model.ShortDescription = blog.ShortDescription;
            model.IsPublished = blog.IsPublished;
            model.ImageName = blog.ImageName;
            model.AuthorId = blog.AuthorId;
            model.CreatedDateOnUTC = blog.CreatedDateOnUtc;

            model.Comments = blog.Comments?.Select(c => new ClientSideBlogCommentViewModel
            {
                Id = c.Id,
                Message = c.Message,
                CommentedDate = c.CreatedDateOnUtc,
                UserId = c.CommentedById,
                ReplyComments = c.ReplyComments?.Select(rc => new ClientSideBlogCommentViewModel
                {
                    Id = rc.Id,
                    Message = rc.Message,
                    CommentedDate = rc.CreatedDateOnUtc,
                    UserId = rc.CommentedById,
                })?.ToList() ?? new List<ClientSideBlogCommentViewModel>()

            }).ToList();

            return model;
        }

        public static BlogLinksForClientSideViewModel MapFrom(this BlogLinksForClientSideViewModel model, Blog blog)
        {
            model.Id = blog.Id;
            model.Title = blog.Title;
            model.slug = blog.Slug;

            return model;
        }

        public static Expression<Func<BlogComment, ClientSideBlogCommentViewModel>> MapCommentViewModel = (BlogComment comment) => new ClientSideBlogCommentViewModel
        {
            Id = comment.Id,
            Message = comment.Message,
            CommentedDate = comment.CreatedDateOnUtc.ToLocalTime(),
            UserId = comment.CommentedById,
        };

        public static ClientSideBlogCommentViewModel MapFrom(this ClientSideBlogCommentViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.UserId = userInfo.Id;
            model.UserName = userInfo.UserName;
            model.UserAvatar = userInfo.UserAvatar;

            return model;
        }

        public static AdminSideBlogViewModel MapFrom(this AdminSideBlogViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AuthorName = userInfo.UserName;
            model.AuthorAvatar = userInfo.UserAvatar;

            return model;
        }

        public static AdminSideUpsertBlogViewModel MapFrom(this AdminSideUpsertBlogViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AuthorName = userInfo.UserName;

            return model;
        }

        public static ClientSideBlogDetailViewModel MapFrom(this ClientSideBlogDetailViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AuthorName = userInfo.UserName;
            model.AuthorAvatar = userInfo.UserAvatar;

            return model;
        }
        public static ClientSideBlogViewModel MapFrom(this ClientSideBlogViewModel model, CommentsCountModel commentsCount)
        {
            model.CommentCount = commentsCount.Count;

            return model;
        }
    }
}
