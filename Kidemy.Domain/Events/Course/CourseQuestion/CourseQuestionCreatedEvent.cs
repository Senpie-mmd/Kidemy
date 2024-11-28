using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.Course.CourseQuestion
{
    public record CourseQuestionCreatedEvent(
        string Title,
        string Description,
        int AskedById,
        int CourseId,
        bool IsClosed
        ) :INotification;
}
