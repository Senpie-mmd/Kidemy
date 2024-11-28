using Kidemy.Domain.Enums.Course;
using MediatR;

namespace Kidemy.Domain.Events.Course.Course
{
    public record CourseUpdatedEvent(
        int Id,
        string prevTitle,
        string prevShortDescription,
        string prevDescription,
        string prevSlug,
        CourseLevel prevLevel,
        CourseStatus prevStatus,
        bool prevIsOffer,
        int prevMasterId,
        bool prevHasCertificate,
        bool pervAllowCommenting,
        CourseType prevType,
        bool prevIsPublshed,
        string prevImageName,
        string prevDemoVideoFileName,
        List<string> prevTags,
        List<int> prevCategoryIds,
        string newTitle,
        string newShortDescription,
        string newDescription,
        string newSlug,
        CourseLevel newLevel,
        CourseStatus newStatus,
        bool newIsOffer,
        int newMasterId,
        bool newHasCertificate,
        bool newAllowCommenting,
        CourseType newType,
        bool newIsPublshed,
        string newImageName,
        string newDemoVideoFileName,
        List<string> newTags,
        List<int> newCategoryIds
        ) : INotification;
}
