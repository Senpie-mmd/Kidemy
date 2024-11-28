using Kidemy.Application.Services.Interfaces;
using Quartz;

namespace Kidemy.Web.Jobs
{
    public class CloseCourseQuestionAnswerAfter1Monthjob : IJob
    {
        private readonly ICourseQuestionService  _courseQuestionService;

        public CloseCourseQuestionAnswerAfter1Monthjob(ICourseQuestionService courseQuestionService)
        {
            _courseQuestionService = courseQuestionService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
             await _courseQuestionService.CloseCourseQuestionAfter1MonthAsync();
        }
    }
}
