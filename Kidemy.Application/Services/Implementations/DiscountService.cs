using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Cart;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Discount;
using Kidemy.Application.ViewModels.Discount.AdminSide;
using Kidemy.Application.ViewModels.Discount.ClientSide;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Enums.Discount;
using Kidemy.Domain.Events.Discount;
using Kidemy.Domain.Interfaces.Discount;
using Kidemy.Domain.Models.Discount;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kidemy.Application.Services.Implementations
{
    public class DiscountService : IDiscountService
    {
        #region Fields

        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountUsageRepository _discountUsageRepository;
        private readonly IDiscountLimitationRepository _discountLimitationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IServiceProvider _serviceProvider;
        private List<IDiscountVerificationService> _allDiscountVerificationServices;

        #endregion

        #region Constructor

        public DiscountService(IDiscountRepository discountRepository, IDiscountLimitationRepository discountLimitationRepository, IHttpContextAccessor httpContextAccessor, IUserService userService, IMediatorHandler mediatorHandler, IDiscountUsageRepository discountUsageRepository, IServiceProvider serviceProvider)
        {
            _discountRepository = discountRepository;
            _discountLimitationRepository = discountLimitationRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mediatorHandler = mediatorHandler;
            _discountUsageRepository = discountUsageRepository;
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        #region Discount

        public async Task<Result<AdminSideFilterDiscountViewModel>> FilterForAdminAsync(AdminSideFilterDiscountViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterDiscountViewModel>(ErrorMessages.FailedOperationError);

            var filterConditions = Filter.GenerateConditions<Discount>();

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                filterConditions.Add(x => x.Title.Contains(model.Title));
            }

            if (!string.IsNullOrWhiteSpace(model.Code))
            {
                filterConditions.Add(x => x.Code.Contains(model.Code));
            }

            if (model.IsActive.HasValue)
            {
                filterConditions.Add(x => x.IsActive == model.IsActive);
            }

            if (model.Type.HasValue)
            {
                filterConditions.Add(x => x.Type == model.Type);
            }

            await _discountRepository.FilterAsync(model, filterConditions, DiscountMapper.MapDiscountViewModel);

            return model;
        }

        public async Task<Result<AdminSideUpsertDiscountViewModel>> GetByIdForEditByAdminAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertDiscountViewModel>(ErrorMessages.FailedOperationError);

            var discount = await _discountRepository.GetByIdAsync(id);

            if (discount == null) return Result.Failure<AdminSideUpsertDiscountViewModel>(ErrorMessages.FailedOperationError);

            return new AdminSideUpsertDiscountViewModel().MapFrom(discount);
        }

        public async Task<Result> CreateAsync(AdminSideUpsertDiscountViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (!model.AutoUse && string.IsNullOrWhiteSpace(model.Code))
            {
                return Result.Failure(ErrorMessages.RequiredDiscountCodeError);
            }

            if (!string.IsNullOrWhiteSpace(model.Code))
            {
                var discountWithSameCode = await _discountRepository
                    .FirstOrDefaultAsync(d => d.Code == model.Code && d.Id != model.Id);

                if (discountWithSameCode != null)
                {
                    return Result.Failure(ErrorMessages.DuplicatedDiscountCodeError);
                }
            }

            if (model.IsPercentage && model.Value > 100)
            {
                model.Value = 100;
            }

            var startDateOnUtc = model.StartDateTime?.ConvertToEnglishNumber()?.ParseUserDateToUTC();
            var endDateOnUtc = model.EndDateTime?.ConvertToEnglishNumber()?.ParseUserDateToUTC();

            if (startDateOnUtc > endDateOnUtc)
            {
                return Result.Failure(ErrorMessages.DatesAreNotValidError);
            }

            var discount = new Discount
            {
                Title = model.Title,
                Code = model.Code,
                Type = model.Type,
                AutoUse = model.AutoUse,
                IsActive = model.IsActive,
                EndDateTimeOnUtc = endDateOnUtc,
                StartDateTimeOnUtc = startDateOnUtc,
                IsPercentage = model.IsPercentage,
                Value = model.Value ?? 0
            };

            await _discountRepository.InsertAsync(discount);
            await _discountRepository.SaveAsync();

            var @event = new DiscountCreatedEvent(
                    discount.Title,
                    discount.Code,
                    discount.IsActive,
                    discount.Type,
                    discount.AutoUse,
                    discount.StartDateTimeOnUtc,
                    discount.EndDateTimeOnUtc,
                    discount.IsPercentage,
                    discount.Value
                );

            await _mediatorHandler.PublishEvent(@event);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(AdminSideUpsertDiscountViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (!model.AutoUse && string.IsNullOrWhiteSpace(model.Code))
            {
                return Result.Failure(ErrorMessages.RequiredDiscountCodeError);
            }

            if (!string.IsNullOrWhiteSpace(model.Code))
            {
                var discountWithSameCode = await _discountRepository
                    .FirstOrDefaultAsync(d => d.Code == model.Code && d.Id != model.Id);

                if (discountWithSameCode != null)
                {
                    return Result.Failure(ErrorMessages.DuplicatedDiscountCodeError);
                }
            }

            if (model.IsPercentage && model.Value > 100)
            {
                model.Value = 100;
            }

            var startDateOnUtc = model.StartDateTime?.ConvertToEnglishNumber()?.ParseUserDateToUTC();
            var endDateOnUtc = model.EndDateTime?.ConvertToEnglishNumber()?.ParseUserDateToUTC();

            if (startDateOnUtc > endDateOnUtc)
            {
                return Result.Failure(ErrorMessages.DatesAreNotValidError);
            }

            var discount = await _discountRepository.GetByIdAsync(model.Id);
            if (discount == null)
            {
                return Result.Failure(ErrorMessages.NullValue);
            }

            var previousDiscount = discount.DeepCopy<Discount>();

            discount.Title = model.Title;
            discount.Code = model.Code;
            discount.Type = model.Type;
            discount.AutoUse = model.AutoUse;
            discount.IsActive = model.IsActive;
            discount.StartDateTimeOnUtc = startDateOnUtc;
            discount.EndDateTimeOnUtc = endDateOnUtc;
            discount.IsPercentage = model.IsPercentage;
            discount.Value = model.Value ?? 0;
            discount.UpdatedDateOnUtc = DateTime.UtcNow;

            _discountRepository.Update(discount);
            await _discountRepository.SaveAsync();

            var @event = new DiscountUpdatedEvent(
                    discount.Id,
                    previousDiscount.Title,
                    discount.Title,
                    previousDiscount.Code,
                    discount.Code,
                    previousDiscount.IsActive,
                    discount.IsActive,
                    previousDiscount.Type,
                    discount.Type,
                    previousDiscount.AutoUse,
                    discount.AutoUse,
                    previousDiscount.StartDateTimeOnUtc,
                    discount.StartDateTimeOnUtc,
                    previousDiscount.EndDateTimeOnUtc,
                    discount.EndDateTimeOnUtc,
                    previousDiscount.IsPercentage,
                    discount.IsPercentage,
                    previousDiscount.Value,
                    discount.Value
                );

            await _mediatorHandler.PublishEvent(@event);

            return Result.Success();

        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.FailedOperationError);

            var isSuccessful = await _discountRepository.SoftDeleteAsync(id);

            if (isSuccessful)
            {
                var @event = new DiscountDeletedEvent(id);

                await _mediatorHandler.PublishEvent(@event);

                return Result.Success();
            }

            return Result.Failure(ErrorMessages.FailedOperationError);
        }

        public Task<Result> ApplyDiscount(ClientSideCourseDetailViewModel courseViewModel, string? code = null)
        {
            return ApplyDiscount(new List<DiscountableViewModel<int>> { courseViewModel }, code);
        }

        public Task<Result> ApplyDiscount(List<ClientSideCourseShortDetailsViewModel> courseViewModels, string? code = null)
        {
            return ApplyDiscount(courseViewModels.Cast<DiscountableViewModel<int>>().ToList(), code);
        }

        public Task<Result> ApplyDiscount(List<ClientSideLastCoursesViewModel> courseViewModels, string? code = null)
        {
            return ApplyDiscount(courseViewModels.Cast<DiscountableViewModel<int>>().ToList(), code);
        }

        public async Task<Result<bool>> DiscountIsUsedByUser(int discountId, int userId)
        {
            if (discountId <= 0 || userId <= 0) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

            return await _discountUsageRepository
                .AnyAsync(usage => usage.DiscountId == discountId && usage.UsedByUserId == userId && usage.IsFinally);
        }

        public async Task<Result> ApplyDiscount(CartViewModel cartViewModel, string? code = null)
        {
            var allActiveDiscounts = await _discountRepository.GetAllActiveDiscounts();

            if (allActiveDiscounts is null || !allActiveDiscounts.Any())
            {
                return Result.Success();
            }

            if (code is not null)
            {
                allActiveDiscounts = allActiveDiscounts.OrderBy(d => d.AutoUse).ToList();
            }

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            // If a discount exists at the cart level, apply it and do not consider any discounts on individual courses
            foreach (Discount toCheckDiscount in allActiveDiscounts)
            {
                var isValid = ValidateAndApplyDiscountOnCartViewModel(
                                    cartViewModel,
                                    currentUserId,
                                    toCheckDiscount,
                                    code);

                if (isValid) return Result.Success();
            }

            // If no discount exists at the cart level, apply discounts on individual courses
            await ApplyDiscount(cartViewModel.Items?.Select(item => item.Course)?.ToList() ?? new List<ClientSideCourseShortDetailsViewModel>(), code);

            CalculateTotalDiscountedPriceForCartViewModel(cartViewModel);

            return Result.Success();
        }

        public Task<Result> ApplyDiscount(List<ClientSideMastersOtherCoursesViewModel> courseViewModels, string? code = null)
        {
            return ApplyDiscount(courseViewModels.Cast<DiscountableViewModel<int>>().ToList(), code);
        }

        public Task<Result> ApplyDiscount(List<ClientSideHomePageOfferedCoursesViewModel> courseViewModels, string? code = null)
        {
            return ApplyDiscount(courseViewModels.Cast<DiscountableViewModel<int>>().ToList(), code);
        }

        public Task<Result> ApplyDiscount(List<ClientSideCourseViewModel> courseViewModels, string? code = null)
        {
            return ApplyDiscount(courseViewModels.Cast<DiscountableViewModel<int>>().ToList(), code);
        }


        #endregion

        #region Discount Limitation

        public async Task<Result<AdminSideFilterDiscountLimitationViewModel>> FilterDiscountLimitationAsync(AdminSideFilterDiscountLimitationViewModel model)
        {
            if (model is null) return Result.Failure<AdminSideFilterDiscountLimitationViewModel>(ErrorMessages.FailedOperationError);

            var filterConditions = Filter.GenerateConditions<DiscountLimitation>();

            if (model.Type.HasValue)
            {
                filterConditions.Add(d => d.Type == model.Type);
            }

            if (model.DiscountId.HasValue)
            {
                filterConditions.Add(d => d.DiscountId == model.DiscountId);
            }

            await _discountLimitationRepository.FilterAsync(model, filterConditions, e => new AdminSideDiscountLimitationViewModel
            {
                Id = e.Id,
                DiscountId = e.DiscountId,
                Type = e.Type
            });

            return model;
        }

        public async Task<Result<AdminSideUpsertDiscountLimitationViewModel>> GetDiscountLimitationForUpdateByAdmin(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertDiscountLimitationViewModel>(ErrorMessages.FailedOperationError);

            var discountLimitation = await _discountLimitationRepository
                    .GetByIdAsync(id,
                                  nameof(DiscountLimitation.Users),
                                  nameof(DiscountLimitation.Courses),
                                  nameof(DiscountLimitation.Categories),
                                  nameof(DiscountLimitation.UsageCount));

            if (discountLimitation == null)
            {
                return Result.Failure<AdminSideUpsertDiscountLimitationViewModel>(ErrorMessages.FailedOperationError);
            }

            var usersFullName = new List<UserFullNameModel>();
            var coursesTitle = new List<CourseTitleModel>();
            var categoriesTitle = new List<CourseCategoryTitleModel>();

            if (discountLimitation.Users != null && discountLimitation.Users.Any())
            {
                var userIds = discountLimitation.Users.Select(u => u.UserId).ToList();
                usersFullName = await _userService.GetUsersFullNames(userIds);
            }

            var courseService = _serviceProvider.GetRequiredService<ICourseService>();

            if (discountLimitation.Courses != null && discountLimitation.Courses.Any())
            {
                var courseIds = discountLimitation.Courses.Select(p => p.CourseId).ToList();
                var coursesTitleResult = await courseService.GetCourseTitles(courseIds);

                if (coursesTitleResult.IsSuccess)
                {
                    coursesTitle = coursesTitleResult.Value;
                }
            }

            if (discountLimitation.Categories != null && discountLimitation.Categories.Any())
            {
                var categoryIds = discountLimitation.Categories.Select(m => m.CategoryId).ToList();
                var categoriesTitleResult = await courseService.GetCourseCategoryTitles(categoryIds);

                if (categoriesTitleResult.IsSuccess)
                {
                    categoriesTitle = categoriesTitleResult.Value;
                }
            }

            var model = new AdminSideUpsertDiscountLimitationViewModel()
                .MapFrom(discountLimitation, usersFullName, coursesTitle, categoriesTitle);

            return model;
        }

        public async Task<Result> CreateDiscountLimitationAsync(AdminSideUpsertDiscountLimitationViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.Type == DiscountLimitationType.UsageCount && !(model.UsageCount?.Count > 0))
            {
                return Result.Failure(ErrorMessages.UsageCountShouldBeGreaterThanZeroError);
            }

            if (model.Type != DiscountLimitationType.UsageCount &&
                (model.Courses?.Count ?? 0) == 0 &&
                (model.Users?.Count ?? 0) == 0 &&
                (model.Categories?.Count ?? 0) == 0)
            {
                return Result.Failure(ErrorMessages.PleaseAddAtLeastOneItemError);
            }

            var discount = await _discountRepository
                    .GetByIdAsync(model.DiscountId, nameof(Discount.DiscountLimitations));

            if (discount == null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (discount.DiscountLimitations.Any(dl => dl.Type == model.Type))
            {
                return Result.Failure(ErrorMessages.DuplicatedDiscountLimitationTypeError);
            }

            var discountLimitation = new DiscountLimitation
            {
                DiscountId = model.DiscountId,
                Type = model.Type.Value,
                Users = model.Users?.Select(u => new DiscountLimitationUserMapping
                {
                    UserId = u.UserId,
                }).ToList(),
                Courses = model.Courses?.Select(p => new DiscountLimitationCourseMapping
                {
                    CourseId = p.CourseId,
                }).ToList(),
                Categories = model.Categories?.Select(m => new DiscountLimitationCategoryMapping
                {
                    CategoryId = m.CategoryId,
                }).ToList(),
                UsageCount = model.UsageCount is null ? null : new DiscountLimitationUsageCountMapping
                {
                    Count = model.UsageCount.Count,
                },
            };

            // Remove unused properties
            if (discountLimitation.Type != DiscountLimitationType.User)
            {
                discountLimitation.Users = null;
            }
            if (discountLimitation.Type != DiscountLimitationType.Category)
            {
                discountLimitation.Categories = null;
            }
            if (discountLimitation.Type != DiscountLimitationType.Course)
            {
                discountLimitation.Courses = null;
            }

            discount.DiscountLimitations ??= new List<DiscountLimitation>();
            discount.DiscountLimitations.Add(discountLimitation);
            discount.UpdatedDateOnUtc = DateTime.UtcNow;

            _discountRepository.Update(discount);
            await _discountRepository.SaveAsync();

            var @event = new DiscountLimitationCreatedEvent(
                discount.Id,
                discountLimitation.Type,
                discountLimitation.Users?.Select(u => u.UserId).ToList(),
                discountLimitation.Courses?.Select(p => p.CourseId).ToList(),
                discountLimitation.Categories?.Select(m => m.CategoryId).ToList()
            );

            await _mediatorHandler.PublishEvent(@event);

            return Result.Success();
        }

        public async Task<Result> UpdateDiscountLimitationAsync(AdminSideUpsertDiscountLimitationViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.Type == DiscountLimitationType.UsageCount && !(model.UsageCount?.Count > 0))
            {
                return Result.Failure(ErrorMessages.UsageCountShouldBeGreaterThanZeroError);
            }

            if (model.Type != DiscountLimitationType.UsageCount &&
                (model.Courses?.Count ?? 0) == 0 &&
                (model.Users?.Count ?? 0) == 0 &&
                (model.Categories?.Count ?? 0) == 0)
            {
                return Result.Failure(ErrorMessages.PleaseAddAtLeastOneItemError);
            }

            string expectedRelationToInclude = "";
            switch (model.Type)
            {
                case DiscountLimitationType.Category:
                    expectedRelationToInclude = nameof(DiscountLimitation.Categories);
                    break;
                case DiscountLimitationType.Course:
                    expectedRelationToInclude = nameof(DiscountLimitation.Courses);
                    break;
                case DiscountLimitationType.UsageCount:
                    expectedRelationToInclude = nameof(DiscountLimitation.UsageCount);
                    break;
                case DiscountLimitationType.User:
                    expectedRelationToInclude = nameof(DiscountLimitation.Users);
                    break;
            }

            var discountLimitation = await _discountLimitationRepository
                .GetByIdAsync(model.Id, expectedRelationToInclude);

            if (discountLimitation == null) return Result.Failure(ErrorMessages.FailedOperationError);

            var prevDiscountLimitation = discountLimitation.DeepCopy<DiscountLimitation>();

            discountLimitation.Users = model.Users?.Select(u => new DiscountLimitationUserMapping
            {
                DiscountLimitationId = u.DiscountLimitationId,
                UserId = u.UserId,
            }).ToList();

            discountLimitation.Courses = model.Courses?.Select(p => new DiscountLimitationCourseMapping
            {
                DiscountLimitationId = p.DiscountLimitationId,
                CourseId = p.CourseId,
            }).ToList();

            discountLimitation.Categories = model.Categories?.Select(m => new DiscountLimitationCategoryMapping
            {
                DiscountLimitationId = m.DiscountLimitationId,
                CategoryId = m.CategoryId,
            }).ToList();

            discountLimitation.UsageCount = model.UsageCount is null ? null : new DiscountLimitationUsageCountMapping
            {
                DiscountLimitationId = model.UsageCount.DiscountLimitationId,
                Count = model.UsageCount.Count,
            };

            _discountLimitationRepository.Update(discountLimitation);
            await _discountLimitationRepository.SaveAsync();

            var @event = new DiscountLimitationUpdatedEvent(
                discountLimitation.Id,
                discountLimitation.DiscountId,
                prevDiscountLimitation.Type,
                discountLimitation.Type,
                prevDiscountLimitation.Users?.Select(u => u.UserId).ToList(),
                discountLimitation.Users?.Select(u => u.UserId).ToList(),
                prevDiscountLimitation.Courses?.Select(p => p.CourseId).ToList(),
                discountLimitation.Courses?.Select(p => p.CourseId).ToList(),
                prevDiscountLimitation.Categories?.Select(m => m.CategoryId).ToList(),
                discountLimitation.Categories?.Select(m => m.CategoryId).ToList()
            );

            await _mediatorHandler.PublishEvent(@event);

            return Result.Success();
        }

        public async Task<Result> DeleteDiscountLimitationAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.FailedOperationError);

            var isSuccessful = await _discountLimitationRepository.SoftDeleteAsync(id);

            if (isSuccessful) return Result.Success();
            return Result.Failure(ErrorMessages.FailedOperationError);
        }

        #endregion

        #endregion

        #region Utilities

        private void PrepareDiscountVerificationServices()
        {
            _allDiscountVerificationServices = _serviceProvider.GetServices<IDiscountVerificationService>().ToList();
        }

        private async Task<Result> ApplyDiscount(List<DiscountableViewModel<int>> courseViewModels, string? code = null)
        {
            var allActiveDiscounts = await _discountRepository.GetAllActiveDiscounts();

            if (allActiveDiscounts is null || !allActiveDiscounts.Any())
            {
                return Result.Success();
            }

            if (code is not null)
            {
                allActiveDiscounts = allActiveDiscounts.OrderBy(d => !d.AutoUse).ToList();
            }

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            var courseService = _serviceProvider.GetRequiredService<ICourseService>();
            var courseFullDetailsViewModelResult = await courseService.GetCoursesFullDetailsByIdsAsync(courseViewModels.Select(p => p.Id).ToList());

            if (courseFullDetailsViewModelResult.IsFailure) return Result.Failure(courseFullDetailsViewModelResult.Message);

            foreach (var courseFullDetailsViewModel in courseFullDetailsViewModelResult.Value)
            {
                var courseViewModel = courseViewModels.First(p => p.Id == courseFullDetailsViewModel.Id);

                foreach (Discount toCheckDiscount in allActiveDiscounts)
                {
                    var isValid = ValidateAndApplyDiscountOnCourseViewModel(
                                        courseViewModel,
                                        courseFullDetailsViewModel,
                                        currentUserId,
                                        toCheckDiscount,
                                        code);

                    if (isValid) break;
                }
            }

            return Result.Success();
        }

        private bool ValidateAndApplyDiscountOnCourseViewModel<TKey>(
            DiscountableViewModel<TKey> discountableCourseViewModel,
            CourseFullDetailsViewModel courseFullDetailsViewModel,
            int currentUserId,
            Discount toCheckDiscount,
            string? enteredDiscountCode = null)
        {
            PrepareDiscountVerificationServices();

            var isValid = _allDiscountVerificationServices.All(
                service => service.IsDiscountValid(courseFullDetailsViewModel, toCheckDiscount, currentUserId, enteredDiscountCode));

            if (isValid)
            {
                var courseMainPrice = discountableCourseViewModel.GetMainPrice();

                // calculate discounted price for course
                decimal discountedPrice = toCheckDiscount.IsPercentage
                    ? courseMainPrice * (100 - toCheckDiscount.Value) / 100
                    : courseMainPrice - toCheckDiscount.Value;

                discountableCourseViewModel.DiscountedPrice = discountedPrice > 0 ? discountedPrice : 0;

                discountableCourseViewModel.AppliedDiscount = new ClientSideDiscountViewModel().MapFrom(toCheckDiscount);
            }

            return isValid;
        }

        private bool ValidateAndApplyDiscountOnCartViewModel(
            CartViewModel cartViewModel,
            int currentUserId,
            Discount toCheckDiscount,
            string? enteredDiscountCode = null)
        {
            PrepareDiscountVerificationServices();

            var isValid = _allDiscountVerificationServices.All(
                service => service.IsDiscountValid(cartViewModel, toCheckDiscount, currentUserId, enteredDiscountCode));

            if (isValid)
            {
                decimal discountedPrice = toCheckDiscount.IsPercentage
                    ? cartViewModel.TotalAmount * (100 - toCheckDiscount.Value) / 100
                    : cartViewModel.TotalAmount - toCheckDiscount.Value;

                cartViewModel.DiscountedTotalAmount = discountedPrice > 0 ? discountedPrice : 0;

                cartViewModel.AppliedDiscount = new ClientSideDiscountViewModel().MapFrom(toCheckDiscount);
            }

            return isValid;
        }

        private void CalculateTotalDiscountedPriceForCartViewModel(CartViewModel expectedCartViewModel)
        {
            if (expectedCartViewModel.AppliedDiscount is null && (expectedCartViewModel.Items?.Any(item => item.Course.AppliedDiscount != null) ?? false))
            {
                var discountedTotalAmount = expectedCartViewModel.Items?.Sum(item =>
                    item.Course.AppliedDiscount != null
                    ? item.Course.DiscountedPrice ?? 0
                    : item.Course.Price ?? 0
                );

                expectedCartViewModel.DiscountedTotalAmount = discountedTotalAmount;
            }
        }

        #endregion
    }
}
