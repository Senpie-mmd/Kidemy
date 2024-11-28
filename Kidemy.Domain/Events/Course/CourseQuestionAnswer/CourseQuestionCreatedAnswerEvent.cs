using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.Course.CourseQuestionAnswer
{
    public record CourseQuestionCreatedAnswerEvent(
        int questionId,
        int answeredId,
        string Answer
        ):INotification;    
}
