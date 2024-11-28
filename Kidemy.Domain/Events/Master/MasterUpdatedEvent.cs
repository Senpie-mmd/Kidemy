using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record MasterUpdatedEvent
     (
        int Id,
        string NewBio,
        string OldBio,
        string NewFatherName,
        string OldFatherName,
        string NewNationalIDNumber,
        string OldNationalIDNumber,
        string NewIdentificationNumber,
        string OldIdentificationNumber,
        string NewPlaceOfIssuance,
        string OldPlaceOfIssuance,
        string NewPostalCode,
        string OldPostalCode,
        string NewAddress,
        string OldAddress,
        string NewIdentificationFileName,
        string OldIdentificationFileName
        ) : INotification;
}
