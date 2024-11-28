using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Domain.Events.Cart;
using Kidemy.Domain.Events.Course.CourseCategory;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseCategoryService : ICourseCategoryService
    {
        #region Fields
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ILocalizingService _localizingService;
        private readonly ICourseService _courseService;
        private readonly IMediatorHandler _mediatorHandler;
        #endregion

        #region Ctor
        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository,
            ILocalizingService localizingService,
            ICourseService courseService,
            IMediatorHandler mediatorHandler)
        {
            _courseCategoryRepository = courseCategoryRepository;
            _localizingService = localizingService;
            _courseService = courseService;
            _mediatorHandler = mediatorHandler;
        }
        #endregion

        #region Methods
        public async Task<Result<List<AdminSideCategoryAsOptionViewModel>>> GetCategoriesAsOptions(int? excludeCourseCategoryId = null)
        {
            List<CourseCategory> categories = new List<CourseCategory>();
            if (excludeCourseCategoryId != null)
            {
                categories = await _courseCategoryRepository.GetAllAsync(n => n.Id != excludeCourseCategoryId);
            }
            else
            {
                categories = await _courseCategoryRepository.GetAllAsync();
            }

            if (categories is null || !categories.Any()) return Result.Failure<List<AdminSideCategoryAsOptionViewModel>>(ErrorMessages.FailedOperationError);

            var model = categories.Select(n => new AdminSideCategoryAsOptionViewModel().MapFrom(n)).ToList();
            await _localizingService.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result> CreateNewCategory(AdminSideCreateCategoryViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);

            model.ImageFile.AddImageToServer(imageName, SiteTools.CourseCategoryImagePath, null, null, SiteTools.CourseCategoryImageThumbPath, null, true);

            if (model.ParentCategoryId == 0)
            {
                model.ParentCategoryId = null;
            }
            CourseCategory category = new CourseCategory()
            {
                Title = model.Title,
                ParentCourseCategoryId = model.ParentCategoryId,
                LogoFileName = imageName,
                IsPopular = model.IsPopular
            };

            await _courseCategoryRepository.InsertAsync(category);
            await _courseCategoryRepository.SaveAsync();

            CourseCategoryCreatedEvent createEvent = new CourseCategoryCreatedEvent(category.Id, category.Title, category.LogoFileName);
            await _mediatorHandler.PublishEvent(createEvent);

            await _localizingService.SaveLocalizations(category, model);


            return Result.Success();
        }

        public async Task<Result<AdminSideFilterCategoryViewModel>> FilterCategoryListAsync(AdminSideFilterCategoryViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterCategoryViewModel>(ErrorMessages.FailedOperationError);
            filter.TakeEntity = 10;

            var conditions = Filter.GenerateConditions<CourseCategory>();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(n => n.Title.Contains(filter.Title));
            }

            await _courseCategoryRepository.FilterAsync(filter, conditions, CourseCategoryMapper.MapAdminSideCategoryViewModel);

            await _localizingService.TranslateModelAsync(filter.Entities);

            return filter;
        }

        public async Task<Result> DeleteCategory(int id)
        {
            if (id < 1) return Result.Failure(ErrorMessages.FailedOperationError);

            if (!await _courseCategoryRepository.AnyAsync(n => n.Id == id)) return Result.Failure(ErrorMessages.FailedOperationError);

            if (await _courseCategoryRepository.AnyAsync(n => n.ParentCourseCategoryId == id)) return Result.Failure(ErrorMessages.CantDeleteThisCourseCategoryError);

            var result = await _courseService.IsThereAnyCourseWithThisCategoryId(id);
            if (result.IsSuccess && result.Value == true) return Result.Failure(ErrorMessages.CantDeleteThisCourseCategoryError);

            var category = await _courseCategoryRepository.GetByIdAsync(id);
            category.IsDeleted = true;

            _courseCategoryRepository.Update(category);
            await _courseCategoryRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<AdminSideUpdateCategoryViewModel>> GetCourseCategoryForEdit(int id)
        {
            if (id < 1) return Result.Failure<AdminSideUpdateCategoryViewModel>(ErrorMessages.FailedOperationError);

            var courseCategory = await _courseCategoryRepository.GetByIdAsync(id);
            if (courseCategory is null) return Result.Failure<AdminSideUpdateCategoryViewModel>(ErrorMessages.FailedOperationError);

            var model = new AdminSideUpdateCategoryViewModel().MapFrom(courseCategory);

            await _localizingService.FillModelToEditByAdminAsync(courseCategory, model);

            return model;
        }

        public async Task<Result> UpdateCourseCategory(AdminSideUpdateCategoryViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            var courseCategory = await _courseCategoryRepository.GetByIdAsync(model.Id);

            var copy = courseCategory.DeepCopy<CourseCategory>();

            if (courseCategory is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.CategoryImage is not null)
            {
                courseCategory.LogoFileName.DeleteImage(SiteTools.CourseCategoryImagePath);

                string logoName = Guid.NewGuid().ToString() + Path.GetExtension(model.CategoryImage.FileName);
                courseCategory.LogoFileName = logoName;
                model.CategoryImage.AddImageToServer(logoName, SiteTools.CourseCategoryImagePath, null, null, null, null, true);
            }

            courseCategory.Title = model.Title;
            courseCategory.IsPopular = model.IsPopular;

            if (model.ParentCategoryId == 0)
            {
                courseCategory.ParentCourseCategoryId = null;
            }
            else
            {
                courseCategory.ParentCourseCategoryId = model.ParentCategoryId;
            }

            _courseCategoryRepository.Update(courseCategory);
            await _courseCategoryRepository.SaveAsync();

            CourseCategoryUpdatedEvent updateEvent = new CourseCategoryUpdatedEvent(copy.Id, copy.Title, model.Title);
            await _mediatorHandler.PublishEvent(updateEvent);

            await _localizingService.SaveLocalizations(courseCategory, model);

            return Result.Success();
        }

        public async Task<Result<List<ClientSideCourseCategoriesLinkInNavViewModel>>> GetCategoriesForNav()
        {
            var categories = await _courseCategoryRepository.GetAllAsync();
            if (!categories?.Any() ?? true) return Result.Failure<List<ClientSideCourseCategoriesLinkInNavViewModel>>(ErrorMessages.FailedOperationError);

            List<ClientSideCourseCategoriesLinkInNavViewModel> model = new List<ClientSideCourseCategoriesLinkInNavViewModel>();

            foreach (var grandParent in categories?.Where(n => n.ParentCourseCategoryId is null) ?? new List<CourseCategory>())
            {
                ClientSideCourseCategoriesLinkInNavViewModel firstGradeCategory = new ClientSideCourseCategoriesLinkInNavViewModel()
                {
                    Id = grandParent.Id,
                    Title = grandParent.Title,
                    ParentCategoryId = null,
                    SubCategories = new List<ClientSideCourseCategoriesLinkInNavViewModel>()
                };

                foreach (var parent in categories?.Where(n => n.ParentCourseCategoryId == grandParent.Id) ?? new List<CourseCategory>())
                {
                    ClientSideCourseCategoriesLinkInNavViewModel secondGradeCategory = new ClientSideCourseCategoriesLinkInNavViewModel()
                    {
                        Id = parent.Id,
                        Title = parent.Title,
                        ParentCategoryId = grandParent.Id,
                        SubCategories = new List<ClientSideCourseCategoriesLinkInNavViewModel>()
                    };

                    foreach (var child in categories?.Where(n => n.ParentCourseCategoryId == parent.Id) ?? new List<CourseCategory>())
                    {
                        secondGradeCategory.SubCategories.Add(new ClientSideCourseCategoriesLinkInNavViewModel
                        {
                            Id = child.Id,
                            ParentCategoryId = parent.Id,
                            Title = child.Title,
                        });
                    }

                    firstGradeCategory.SubCategories.Add(secondGradeCategory);
                }

                model.Add(firstGradeCategory);
            }

            return model;
        }
        #endregion
    }
}
