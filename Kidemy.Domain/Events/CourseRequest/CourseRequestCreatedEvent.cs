using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.CourseRequest
{
    public record CourseRequestCreatedEvent
    (int Id,
        int RequestedById,
        string Title,
        int? PreferedMasterId,
        string SelectedTags,
        string Description) : INotification;
}
