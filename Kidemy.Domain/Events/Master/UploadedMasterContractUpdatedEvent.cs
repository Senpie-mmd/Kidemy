using Kidemy.Domain.Enums.Master;
using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record UploadedMasterContractUpdatedEvent
     (
         int Id,
         int NewMasterId,
         int OldMasterId,
         int NewMasterContractId,
         int OldMasterContractId,
         UploadedMasterContractStatus NewStatus,
         UploadedMasterContractStatus OldStatus,
         string NewFileName,
         string OldFileName
         ) : INotification;
}
