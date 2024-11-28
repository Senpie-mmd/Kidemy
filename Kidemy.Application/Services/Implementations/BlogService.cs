using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Blog;
using Kidemy.Application.ViewModels.Blog.ClientSideBlog.Comments;
using Kidemy.Domain.Events.Blog;
using Kidemy.Domain.Interfaces.Blog;
using Kidemy.Domain.Models.Blog;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;

namespace Kidemy.Application.Services.Implementations
{
    public class BlogService : IBlogService
    {
        #region Fields

        private readonly IBlogRepository _blogRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBlogCommentRepository _commentRepository;
        private readonly IUserService _userService;
        private readonly IBlogTagRepository _blogTagRepository;

        #endregion

        #region Constructor
        public BlogService(IBlogRepository blogRepository,
            ILocalizingService localizingService,
            IMediatorHandler mediatorHandler,
            IHttpContextAccessor httpContextAccessor,
            IBlogCommentRepository commentRepository,
            IUserService userService,
            IBlogTagRepository blogTagRepository)
        {
            _blogRepository = blogRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
            _httpContextAccessor = httpContextAccessor;
            _commentRepository = commentRepository;
            _userService = userService;
            _blogTagRepository = blogTagRepository;
        }
        #endregion

        #region Blog

        public async Task<Result> CreateAsync(AdminSideUpsertBlogViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.Image is null)
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            if (model.Image is not null && !model.Image.IsImage())
            {
                return Result.Failure(ErrorMessages.PictureFormatError);
            }

            string slug = model.Slug.Replace(" ", "-");

            if (await _blogRepository.ExistBlogBySlug(slug)) return Result.Failure(ErrorMessages.DuplicateSlug);

            if (await _blogRepository.ExistBlogByTitle(model.Title)) return Result.Failure(ErrorMessages.DuplicateTitle);

            Blog blog = new()
            {
                Title = model.Title,
                Body = model.BlogText,
                Slug = slug,
                ShortDescription = model.ShortDescription,
                IsPublished = model.IsPublished,
                AuthorId = model.AuthorId <= 0 ? _httpContextAccessor.HttpContext.User.GetUserId() : model.AuthorId,
                CreatedDateOnUtc = DateTime.UtcNow,
            };

            //Add image
            if (model.Image is not null)
            {
                blog.ImageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(blog.ImageName, SiteTools.BlogImagePath, 400, 300, SiteTools.BlogImageThumPath);
            }


            blog.BlogTags = new List<BlogTagMapping>();

            var addedTags = model.BlogTags.Split(",").ToList();
            var insertedTagsInDB = await _blogTagRepository.GetTagsThatInsertedInDB(addedTags);
            var notInsertedInDB = addedTags.Except(insertedTagsInDB.Select(n => n.Title).ToList()).ToList();

            if (insertedTagsInDB?.Any() ?? false)
            {
                foreach (var DbTags in insertedTagsInDB)
                {
                    blog.BlogTags.Add(new BlogTagMapping
                    {
                        TagId = DbTags.Id
                    });
                }
            }

            if (notInsertedInDB?.Any() ?? false)
            {
                foreach (var notInDbTags in notInsertedInDB)
                {
                    blog.BlogTags.Add(new BlogTagMapping
                    {
                        Tag = new BlogTag { Title = notInDbTags }
                    });
                }
            }

            await _blogRepository.InsertAsync(blog);
            await _blogRepository.SaveAsync();

            await _localizingService.SaveLocalizations(blog, model);

            BlogCreatedEvent BlogCreatedEvent = new(
                blog.Title,
                blog.Body,
                blog.Slug,
                blog.IsPublished,
                blog.ImageName,
                blog.AuthorId
                );

            await _mediatorHandler.PublishEvent(BlogCreatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result> UpdateAsync(AdminSideUpsertBlogViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.AuthorId <= 0)
            {
                return Result.Failure(ErrorMessages.NullValue);
            }

            if (model.Image is not null && !model.Image.IsImage())
            {
                return Result.Failure(ErrorMessages.PictureFormatError);
            }

            var slug = model.Slug.Replace(" ", "-");

            if (await _blogRepository.ExistBlogBySlug(slug, model.Id)) return Result.Failure(ErrorMessages.DuplicateSlug);

            if (await _blogRepository.ExistBlogByTitle(model.Title, model.Id)) return Result.Failure(ErrorMessages.DuplicateTitle);

            var blog = await _blogRepository.GetBlogWithTags(model.Id);

            if (blog is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.Image is null && string.IsNullOrWhiteSpace(blog.ImageName))
            {
                return Result.Failure(ErrorMessages.ImageRequiredError);
            }

            if (model.AuthorId <= 0 && blog.AuthorId <= 0)
            {
                return Result.Failure(ErrorMessages.NullValue);
            }

            var copiedBlog = blog?.DeepCopy<Blog>();

            blog.Title = model.Title;
            blog.Body = model.BlogText;
            blog.Slug = slug;
            blog.ShortDescription = model.ShortDescription;
            blog.IsPublished = model.IsPublished;
            blog.AuthorId = model.AuthorId;

            //Update image
            if (model.Image is not null && model.Image.IsImage())
            {
                if (!string.IsNullOrWhiteSpace(blog.ImageName))
                {
                    blog.ImageName.DeleteImage(SiteTools.BlogImagePath, SiteTools.BlogImageThumPath);
                }

                blog.ImageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(blog.ImageName, SiteTools.BlogImagePath, 400, 300, SiteTools.BlogImageThumPath);
            }

            List<int> removedTagIds = blog.BlogTags.Where(n => !model.BlogTags.Contains(n.Tag.Title)).Select(n => n.TagId).ToList();
            if (removedTagIds?.Any() ?? false)
            {
                foreach (var tagId in removedTagIds)
                {
                    blog.BlogTags.Remove(blog.BlogTags.First(n => n.TagId == tagId));
                }
            }

            var existTags = blog.BlogTags.Where(n => model.BlogTags.Contains(n.Tag.Title) && n.BlogId == blog.Id).Select(n => n.Tag.Title).ToList();
            var addedTags = model.BlogTags
                .Split(",").ToList()
                .Except(existTags).ToList();

            if (addedTags?.Any() ?? false)
            {
                var tagsAlreadyAdded = await _blogTagRepository.GetTagsThatInsertedInDB(addedTags);
                if (tagsAlreadyAdded is not null && tagsAlreadyAdded.Any())
                {
                    foreach (var alreadyAddedTag in tagsAlreadyAdded)
                    {
                        blog.BlogTags.Add(new BlogTagMapping
                        {
                            TagId = alreadyAddedTag.Id,
                            BlogId = blog.Id
                        });
                    }
                }
                else
                {
                    var tagsNotAdded = addedTags.Except(tagsAlreadyAdded.Select(n => n.Title).ToList()).ToList();
                    foreach (var item in tagsNotAdded)
                    {
                        blog.BlogTags.Add(new BlogTagMapping
                        {
                            Tag = new BlogTag { Title = item }
                        });
                    }
                }
            }

            _blogRepository.Update(blog);
            await _blogRepository.SaveAsync();

            await _localizingService.SaveLocalizations(blog, model);

            BlogUpdatedEvent BlogUpdatedEvent = new(
                blog.Id,
                blog.Title,
                copiedBlog.Title,
                blog.Body,
                copiedBlog.Body,
                blog.Slug,
                copiedBlog.Slug,
                blog.IsPublished,
                copiedBlog.IsPublished,
                blog.ImageName,
                copiedBlog.ImageName,
                blog.AuthorId,
                copiedBlog.AuthorId
                );


            await _mediatorHandler.PublishEvent(BlogUpdatedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _blogRepository.SoftDelete(id);
            await _blogRepository.SaveAsync();


            BlogDeletedEvent BlogDeletedEvent = new(id);

            await _mediatorHandler.PublishEvent(BlogDeletedEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        public async Task<Result<FilterBlogViewModel>> FilterAsync(FilterBlogViewModel model)
        {
            if (model is null) return Result.Failure<FilterBlogViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Blog>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            await _blogRepository.FilterAsync(model, filterConditions, BlogMapper.MapBlogViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            //Fill AuthorName
            var userIds = model.Entities.Select(n => n.AuthorId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(blog => blog.MapFrom(usersInfo.Value.First(n => n.Id == blog.AuthorId)))
                .ToList();


            return model;
        }

        public async Task<Result<ClientSideFilterBlogViewModel>> FilterAsync(ClientSideFilterBlogViewModel model)
        {
            if (model is null) return Result.Failure<ClientSideFilterBlogViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<Blog>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                filterConditions.Add(p => p.Title.Contains(model.Title));
            }

            if (!string.IsNullOrWhiteSpace(model.Slug))
            {
                filterConditions.Add(b => b.BlogTags.Any(b => b.Tag.Title == model.Slug));
            }

            filterConditions.Add(p => p.IsPublished == true);

            await _blogRepository.FilterAsync(model, filterConditions, BlogMapper.MapClientSideBlogViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            //Fill AuthorName
            var userIds = model.Entities.Select(n => n.AuthorId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model.Entities = model.Entities
                .Select(blog => blog.MapFrom(usersInfo.Value.First(n => n.Id == blog.AuthorId)))
                .ToList();

            //Fill comment count
            var blogIds = model.Entities.Select(n => n.Id).ToList();
            var commentsCount = await _commentRepository.GetCommentsCountByBlogIds(blogIds);
            if (commentsCount is not null && commentsCount.Any())
            {
                model.Entities = model.Entities
                    .Select(blog => blog.MapFrom(commentsCount?.FirstOrDefault(n => n.BlogId == blog.Id) ?? new Domain.DTOs.Comment.CommentsCountModel()))
                    .ToList();
            }

            return model;
        }

        public async Task<Result<List<ClientSideBlogViewModel>>> GetHomePageBlogs(int count)
        {
            var blogs = (await _blogRepository.GetAllAsync(b => b.IsPublished))?.Take(count);

            if (blogs is null || !blogs.Any())
            {
                return Result.Failure<List<ClientSideBlogViewModel>>(ErrorMessages.ProcessFailedError);
            }

            var result = blogs.Select(n => new ClientSideBlogViewModel().MapFrom(n)).ToList();

            await _localizingService.TranslateModelAsync(result);

            //Fill AuthorName
            var userIds = result.Select(n => n.AuthorId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            result = result
                .Select(blog => blog.MapFrom(usersInfo.Value.First(n => n.Id == blog.AuthorId)))
                .ToList();

            //Fill comment count
            var blogIds = result.Select(n => n.Id).ToList();
            var commentsCount = await _commentRepository.GetCommentsCountByBlogIds(blogIds);
            if (commentsCount is not null && commentsCount.Any())
            {
                result = result.Select(blog => blog.MapFrom(commentsCount?.FirstOrDefault(n => n.BlogId == blog.Id) ?? new Domain.DTOs.Comment.CommentsCountModel()))
                    .ToList();
            }

            return result;
        }

        public async Task<Result<AdminSideUpsertBlogViewModel>> GetBlogForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertBlogViewModel>(ErrorMessages.ProcessFailedError);

            var blog = await _blogRepository.GetBlogWithTags(id);

            if (blog is null) return Result.Failure<AdminSideUpsertBlogViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertBlogViewModel().MapFrom(blog);

            //Fill AuthorName
            var userIds = new List<int>() { model.AuthorId };
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model = model.MapFrom(usersInfo.Value.First(n => n.Id == model.AuthorId));

            await _localizingService.FillModelToEditByAdminAsync(blog, model);

            return model;
        }

        public async Task<Result<ClientSideBlogDetailViewModel>> GetBlogDetailBySlug(string slug)
        {
            if (slug is null)
                return Result.Failure<ClientSideBlogDetailViewModel>(ErrorMessages.FailedOperationError);

            var blog = await _blogRepository.GetPublishedBlogWithTagsAndComments(slug);

            if (blog is null)
                return Result.Failure<ClientSideBlogDetailViewModel>(ErrorMessages.FailedOperationError);

            #region Set comment


            //groubBy comments by replyCommentId
            var comments = blog.Comments
                .GroupBy(n => n.ReplyCommentId)
                .Select(n => new
                {
                    replyId = n.Key,
                    comments = n.Where(c => c.IsConfirmed).ToList()
                })
                .ToList();

            var originalComments = comments.FirstOrDefault(c => c.replyId == null)?
                                           .comments
                                           .OrderByDescending(c => c.CreatedDateOnUtc)
                                           .ToList();

            //Fill reply comments of original comment
            if (originalComments is not null && originalComments.Any())
            {
                foreach (var comment in originalComments)
                {
                    comment.ReplyComments = comments.FirstOrDefault(c => c.replyId == comment.Id)?
                                                    .comments?
                                                    .OrderByDescending(c => c.CreatedDateOnUtc)?
                                                    .ToList();
                }
            }

            blog.Comments = originalComments?
                .Where(n => n.IsConfirmed && n.ReplyCommentId == null)?
                .TakeLast(6)?
                .OrderByDescending(c => c.CreatedDateOnUtc)?
                .ToList() ?? new List<BlogComment>();

            var model = new ClientSideBlogDetailViewModel().MapFrom(blog);

            #endregion

            await _localizingService.TranslateModelAsync(model);

            #region Set users detail

            //Fill AuthorName
            var userIds = new List<int>() { model.AuthorId };
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            model = model.MapFrom(usersInfo.Value.First(n => n.Id == model.AuthorId));

            //Fill commentor detail
            if (model.Comments.Any())
            {
                var commentorIds = model.Comments.Select(n => n.UserId).ToList();
                var commentorsInfo = await _userService.GetUserNameAndUserProfileByUserId(commentorIds);
                model.Comments = model.Comments
                    .Select(comment => comment.MapFrom(commentorsInfo.Value.First(n => n.Id == comment.UserId)))
                    .ToList();
            }

            //Fill reply commentor detail
            if (model.Comments.Any(c => c.ReplyComments.Any()))
            {
                foreach (var comment in model.Comments)
                {
                    var replyCommentorIds = comment.ReplyComments.Select(n => n.UserId).ToList();
                    var replyCommentorsInfo = await _userService.GetUserNameAndUserProfileByUserId(replyCommentorIds);
                    comment.ReplyComments = comment.ReplyComments
                        .Select(replyComment => replyComment.MapFrom(replyCommentorsInfo.Value.First(n => n.Id == replyComment.UserId)))
                        .ToList();
                }
            }

            #endregion

            return model;
        }

        //public async Task<Result<List<BlogLinksForClientSideViewModel>>> GetBlogLinks(LinkPlace link)
        //{
        //    var blogs = await _blogRepository.GetAllAsync(n => n.LinkPlace == link && n.IsPublished == true);

        //    var result = blogs.Select(n => new BlogLinksForClientSideViewModel().MapFrom(n)).ToList();

        //    await _localizingService.TranslateModelAsync(result);

        //    return result;
        //}

        #endregion

        #region Comment

        public async Task<Result<ClientSideFilterBlogCommentsViewModel>> GetCommentsForClientSide(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Result.Failure<ClientSideFilterBlogCommentsViewModel>(ErrorMessages.FailedOperationError);
            int blogId = await _blogRepository.GetBlogIdBySlug(slug);
            if (blogId < 1) return Result.Failure<ClientSideFilterBlogCommentsViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<BlogComment>();
            conditions.Add(n => n.BlogId == blogId);
            conditions.Add(n => n.IsConfirmed == true);

            ClientSideFilterBlogCommentsViewModel filter = new ClientSideFilterBlogCommentsViewModel();
            filter.BlogSlug = slug;

            await _commentRepository.FilterAsync(filter, conditions, BlogMapper.MapCommentViewModel);

            var userIds = filter.Entities.Select(n => n.UserId).ToList();
            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            filter.Entities = filter.Entities
                .Select(Comments => Comments
                .MapFrom(usersInfo.Value.First(n => n.Id == Comments.UserId)))
                .ToList();

            return filter;
        }

        public async Task<Result<string>> CreateComment(ClientSideCreateBlogCommentViewModel model)
        {
            if (model is null) return Result.Failure<string>(ErrorMessages.FailedOperationError);
            if (model.BlogSlug == null || !await _blogRepository.AnyAsync(n => n.Slug == model.BlogSlug)) return Result.Failure<string>(ErrorMessages.FailedOperationError);
            var blog = await _blogRepository.FirstOrDefaultAsync(b => b.Slug == model.BlogSlug && b.IsPublished);

            if (blog is null)
            {
                return Result.Failure<string>(ErrorMessages.FailedOperationError);
            }

            if (model.ReplyCommentId.HasValue &&
                model.ReplyCommentId > 0 &&
                await _commentRepository.AnyAsync(c => c.Id == model.ReplyCommentId && c.ReplyCommentId != null))
            {
                return Result.Failure<string>(ErrorMessages.FailedOperationError);
            }

            var comment = new BlogComment()
            {
                CommentedById = _httpContextAccessor.HttpContext.User.GetUserId(),
                IsConfirmed = false,
                Message = model.CommentText,
                BlogId = blog.Id,
                ReplyCommentId = model.ReplyCommentId,
            };

            await _commentRepository.InsertAsync(comment);
            await _commentRepository.SaveAsync();

            return Result.Success(model.BlogSlug, SuccessMessages.YourCommentIsAwaitingApproval);
        }

        #endregion
    }
}
