using Kidemy.Domain.Enums.Banner;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Events.Banner
{
    public record BannerUpdatedEvent
    (
        int Id,
        string NewImageName,
        string OldImageName,
        BannerPlace NewBannerPlace,
        BannerPlace OldBannerPlace,
        string? NewURL,
        string? OldURL,
        bool NewIsPublished,
        bool OldIsPublished
    ) : INotification;
}
