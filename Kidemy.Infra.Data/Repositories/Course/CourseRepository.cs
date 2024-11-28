using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseRepository : EfRepository<Domain.Models.Course.Course, int>, ICourseRepository
    {
        #region Ctor

        public CourseRepository(KidemyContext context) : base(context)
        {
        }

        #endregion

        public async Task<int> GetCountOfAllCourses()
        {
            return await _dbSet.Where(n => n.IsPublished).CountAsync();
        }

        public async Task<int> GetCourseIdBySlug(string slug)
        {
            return await _dbSet.Where(n => n.Slug == slug).Select(n => n.Id).FirstOrDefaultAsync();
        }

        public async Task<CourseVideo?> GetVideoAsync(int videoId)
        {
            return await _context.Set<CourseVideo>().FindAsync(videoId);
        }

        public async Task<string?> GetCourseSuffixName(int courseId)
        {
            return await _dbSet.Where(c => c.Id == courseId).Select(c => c.FileSuffix).FirstOrDefaultAsync();
        }

        public async Task<bool> IsOrderDuplicated(int order, int courseId)
        {
            return await _context.Set<CourseVideo>().AnyAsync(v => v.Priority == order && v.CourseId == courseId);
        }

        public Task<List<CourseMasterModel>> GetMastersOfCourses(List<int> courseIds)
        {
            return _dbSet.Where(course => courseIds.Contains(course.Id))
                .Select(course => new CourseMasterModel
                {
                    CourseId = course.Id,
                    MasterId = course.MasterId,
                    CommissionPercentage = course.MasterCommissionPercentage
                }).ToListAsync();
        }

        public Task<List<Domain.Models.Course.Course>> GetCoursesWithCategories(List<int> ids)
        {
            return _dbSet.Where(c => ids.Contains(c.Id))
                         .Include(c => c.Categories)
                         .ThenInclude(c => c.Category)
                         .AsNoTracking()
                         .ToListAsync();
        }

        public Task<CourseType> GetCourseType(int courseId)
        {
            return _dbSet.Where(c => c.Id == courseId).Select(c => c.Type).FirstOrDefaultAsync();
        }

        public Task<Domain.Models.Course.Course?> GetCourseWithVideosAsync(string slug)
        {
            return _dbSet.AsNoTracking()
                         .AsQueryable()
                         .Include(course => course.Videos)
                         .ThenInclude(video => video.VideoCategory)
                         .FirstOrDefaultAsync(n => n.Slug == slug && n.IsPublished);
        }

        public async Task<CourseDetailsModel?> GetCourseWithTagsAndCategoryNameById(string slug)
        {
            IQueryable<Domain.Models.Course.Course> query = _dbSet.AsNoTracking()
                .AsSplitQuery()
                .AsQueryable();

            query = query.Where(n => n.Slug == slug);

            return await query.Select(course => new CourseDetailsModel
            {
                Id = course.Id,
                DemoVideoFileName = course.DemoVideoFileName,
                ImageFileName = course.ImageName,
                Slug = course.Slug,
                Type = course.Type,
                LastUpdatedDate = course.UpdatedDateOnUtc,
                Title = course.Title,
                Price = course.Price,
                MasterId = course.MasterId,
                Level = course.Level,
                ShortDescription = course.ShortDescription,
                Description = course.Description,
                CourseVideosTotalTime = course.Videos.Where(n => n.IsPublished).Select(n => n.VideoLength).ToList(),
                HasCertificate = course.HasCertificate,
                VideoCount = course.Videos.Where(n => n.IsPublished == true).Count(),
                CourseScore = course.Comments.Where(n => n.IsConfirmed == true && n.ReplyCommnetId == null).Select(n => n.Score).ToList(),
                CategoryName = course.Categories
                    .FirstOrDefault(CourseCategoryMapping => CourseCategoryMapping.Category.ParentCourseCategoryId == null)
                    .Category.Title ??
                    course.Categories.FirstOrDefault().Category.Title,
                Tags = course.CourseTags.Select(CourseTagMapping => new CourseTagsModel
                {
                    Id = CourseTagMapping.Tag.Id,
                    Title = CourseTagMapping.Tag.Title,
                }).ToList()
            }).FirstOrDefaultAsync();
        }

        public Task<List<CourseTitleModel>> GetCourseTitles(List<int> ids)
        {
            return _dbSet.Where(c => ids.Contains(c.Id))
                         .Select(c => new CourseTitleModel
                         {
                             CourseId = c.Id,
                             Title = c.Title
                         }).ToListAsync();
        }

        public async Task<Domain.Models.Course.Course?> GetCourseByIdWithTagsAndCategories(int id)
        {
            return await _dbSet.Where(n => n.Id == id)
                .Include(n => n.Categories)
                .Include(n => n.CourseTags)
                .ThenInclude(n => n.Tag)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetRecordingCourseCountAsync()
        {
            return await _dbSet.Where(n => n.IsPublished && n.Status == CourseStatus.Recording).CountAsync();
        }

        public async Task<string> GetCourseSlugById(int courseId)
        {
            return await _dbSet.Where(n => n.Id == courseId).Select(n => n.Slug).FirstOrDefaultAsync();
        }

        public async Task<int> GetCourseMasterIdByCourseId(int courseId)
        {
            return await _dbSet.Where(n => n.Id == courseId).Select(n => n.MasterId).FirstOrDefaultAsync();
        }

        public async Task<List<Domain.Models.Course.Course>> GetOfferedCourses()
        {
            IQueryable<Domain.Models.Course.Course> query = _dbSet
                .Include(n => n.Categories).ThenInclude(n => n.Category)
                .AsQueryable()
                .AsNoTracking();

            query = query.Where(n => n.IsOffer == true);

            return await query.Take(10).ToListAsync();
        }

        public Task<List<CourseCategoryTitleModel>> GetCoursesCategoryWithId(List<int> ids)
        {
            IQueryable<Domain.Models.Course.Course> query = _dbSet;

            query = query.Where(n => ids.Contains(n.Id));

            return query.Select(Course => new CourseCategoryTitleModel
            {
                CourseId = Course.Id,
                Title = Course.Categories.FirstOrDefault().Category.Title,
                CategoryId = Course.Categories.FirstOrDefault().CategoryId
            }).ToListAsync();
        }

        public async Task<List<CourseModel>> GetLastCourses(int take = 12)
        {
            return await _dbSet
                .OrderByDescending(c => c.CreatedDateOnUtc)
                .Take(take)
                .Select(course => new CourseModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    ShortDescription = course.ShortDescription,
                    Level = course.Level,
                    Slug = course.Slug,
                    ImageFileName = course.ImageName,
                    VideosCount = course.Videos.Where(n => n.IsPublished).Count(),
                    ScoresList = course.Comments.Where(n => n.IsConfirmed && n.ReplyCommnetId == null).Select(n => (int)n.Score).ToList(),
                    ListOfTimeSpans = course.Videos.Where(n => n.IsPublished).Select(n => n.VideoLength).ToList(),
                    Popularity = course.Users.Count()
                }).ToListAsync();
        }

        public async Task<List<Domain.Models.Course.Course>> GetCoursesList(Expression<Func<Domain.Models.Course.Course, bool>> where = null, int? take = null, Expression<Func<Domain.Models.Course.Course, object>>? orderBy = null, Expression<Func<Domain.Models.Course.Course, object>>? orderByDesc = null)
        {
            IQueryable<Domain.Models.Course.Course> query = _dbSet
                .Include(n => n.Comments)
                .Include(n => n.Users)
                .Include(n => n.Categories)
                .ThenInclude(n => n.Category)
                .AsSplitQuery()
                .AsNoTracking();

            if (where is not null)
            {
                query = query.Where(where);
            }

            if (orderBy is not null)
            {
                query = query.OrderBy(orderBy);
            }

            if (orderByDesc is not null)
            {
                query = query.OrderByDescending(orderByDesc);
            }

            if (take is not null)
            {
                return await query.Take((int)take).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
    }
}
