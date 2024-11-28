using MediatR;

namespace Kidemy.Domain.Events.Master
{
    public record MasterCreatedEvent
      (
        int Id,
        string Bio,
        string FatherName,
        string NationalIDNumber,
        string IdentificationNumber,
        string PlaceOfIssuance,
        string PostalCode,
        string Address,
        string IdentificationFileName
        ) : INotification;
}