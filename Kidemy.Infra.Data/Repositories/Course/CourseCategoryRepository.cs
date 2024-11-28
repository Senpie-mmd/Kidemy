using Barnamenevisan.Localizing.Repository;
using Kidemy.Application.Convertors;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseCategoryRepository : EfRepository<CourseCategory, int>, ICourseCategoryRepository
    {
        public CourseCategoryRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<List<CategoryModel>> GetCategoriesWithCountOfCourses(Expression<Func<CourseCategory, bool>> where = null)
        {
            IQueryable<CourseCategory> query = _dbSet
                .Include(n => n.SubCategories)
                .ThenInclude(n => n.SubCategories)
                .AsNoTracking()
                .AsQueryable<CourseCategory>();

            if (where != null)
            {
                query = query.Where(where);
            }

            return await query.Select(category => new CategoryModel
            {
                Id = category.Id,
                Title = category.Title,
                logoFileName = category.LogoFileName,
                ParentCourseCategoryId = category.ParentCourseCategoryId,
                CourseCount = category.Courses.Where(c => c.Course.IsPublished).Select(Course => Course.CourseId)
                    .Union(category.SubCategories.SelectMany(subCategory => subCategory.Courses.Where(n => n.Course.IsPublished).Select(Course => Course.CourseId)))
                    .Union(category.SubCategories.SelectMany(subCategory => subCategory.SubCategories
                    .SelectMany(subCategory => subCategory.Courses.Where(n => n.Course.IsPublished).Select(Course => Course.CourseId)))).Count()
            }).ToListAsync();
        }

        public Task<List<CourseCategoryTitleModel>> GetCourseCategoryTitles(List<int> ids)
        {
            return _context.Set<CourseCategory>().Where(c => ids.Contains(c.Id))
                         .Select(c => new CourseCategoryTitleModel
                         {
                             CategoryId = c.Id,
                             Title = c.Title
                         }).ToListAsync();
        }

        public Task<List<CourseCategory>> GetCategoriesWithSubCategories(List<int> ids)
        {
            return _dbSet.AsNoTracking()
                         .Include(n => n.SubCategories)
                         .ThenInclude(n => n.SubCategories)
                         .Where(n => ids.Contains(n.Id))
                         .ToListAsync();
        }

        public async Task<List<PopularCourseCategoriesModel>> GetPopularCategoriesWithCourses()
        {
            IQueryable<CourseCategory> query = _dbSet.AsQueryable().AsNoTracking();

            query = query.Where(n => n.IsPopular && n.Courses.Any());

            return await query.Select(Category => new PopularCourseCategoriesModel
            {
                Id = Category.Id,
                Title = Category.Title,
                PopularCourses = Category.Courses.Select(CategoryMapping => new CoursesForPopularCategoriesModel()
                {
                    Id = CategoryMapping.Course.Id,
                    CategoryId = CategoryMapping.CategoryId,
                    Title = CategoryMapping.Course.Title,
                    ShortDescription = CategoryMapping.Course.ShortDescription,
                    Level = CategoryMapping.Course.Level,
                    Slug = CategoryMapping.Course.Slug,
                    ImageFileName = CategoryMapping.Course.ImageName,
                    VideosCount = CategoryMapping.Course.Videos.Where(n => n.IsPublished).Count(),
                    ScoresList = CategoryMapping.Course.Comments.Where(n => n.IsConfirmed && n.ReplyCommnetId == null).Select(n => (int)n.Score).ToList(),
                    ListOfTimeSpans = CategoryMapping.Course.Videos.Where(n => n.IsPublished).Select(n => n.VideoLength).ToList(),
                    Popularity = CategoryMapping.Course.Users.Count()
                }).OrderByDescending(n => n.Popularity).Take(8).ToList()
            }).ToListAsync();
        }


    }
}