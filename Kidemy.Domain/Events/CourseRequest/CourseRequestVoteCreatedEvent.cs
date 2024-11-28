using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.CourseRequest
{
    public record class CourseRequestVoteCreatedEvent
       (int Id,
        int CourseRequestId,
        int UserId,
        bool IsAgree
       ) : INotification;
}
