using Kidemy.Domain.Enums.Master;
using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record UploadedMasterContractCreatedEvent
       (
         int Id,
         int MasterId,
         int MasterContractId,
         UploadedMasterContractStatus Status,
         string FileName
         ) : INotification;

}
