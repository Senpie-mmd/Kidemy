using MediatR;

namespace Kidemy.Domain.Events.SiteSetting
{
    public record SiteSettingUpdatedEvent(
        int Id,
        string NewEmail,
        string OldEmail,
        string? NewMobile,
        string? OldMobile,
        string? NewMobile2,
        string? OldMobile2,
        string? NewAddress,
        string? OldAddress,
        string? NewLogoName,
        string? OldLogoName,
        string? NewCollectionManagement,
        string? OldCollectionManagement,
        string? NewFooterDescription,
        string? OldFooterDescription,
        string? NewCopyrightDescription,
        string? OldCopyrightDescription

    ) : INotification;
}