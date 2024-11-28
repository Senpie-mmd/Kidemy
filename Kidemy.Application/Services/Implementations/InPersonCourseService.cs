using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.InPersonCourse;
using Kidemy.Domain.Interfaces.InPersonCourse;
using Kidemy.Domain.Models.InPersonCourse;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class InPersonCourseService : IInPersonCourseService
    {
        #region Fields

        private readonly IInPersonCourseRepository _inPersonCourseRepository;
        private readonly IInPersonCourseTagRepository _inPersonCourseTagRepository;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public InPersonCourseService(IInPersonCourseRepository inPersonCourseRepository,
            IInPersonCourseTagRepository inPersonCourseTagRepository,
            ILocalizingService localizingService,
            IMediatorHandler mediatorHandler)
        {
            _inPersonCourseRepository = inPersonCourseRepository;
            _inPersonCourseTagRepository = inPersonCourseTagRepository;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        public async Task<Result<ClientSideFilterInPersonCourseViewModel>> FilterInPersonCoursesClientSide(ClientSideFilterInPersonCourseViewModel filter)
        {
            if (filter is null) return Result.Failure<ClientSideFilterInPersonCourseViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<InPersonCourse>();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                conditions.Add(pc => pc.Title.Contains(filter.Title));
            }

            conditions.Add(n => n.IsPublished);

            await _inPersonCourseRepository.FilterAsync(filter, conditions, InPersonCourseMapper.MapInPersonCourseViewModel);

            return filter;

        }
    }
}
