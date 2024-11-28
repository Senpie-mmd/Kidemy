using Kidemy.Domain.Enums.Consultation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.Consultation.ConsultationRequest
{
    public record ConsultationRequestCreatedEvent
        (
            int selectedDateId,
            int selectedTypeId,
            string topic,
            string description,
            int requestByUserId,
            int adviserId,
            ConsultationRequestState state
        )
        : INotification;
}
