using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.VIPMembers;
using Kidemy.Domain.Events.VIPMembers;
using Kidemy.Domain.Interfaces.VIPMembers;
using Kidemy.Domain.Models.VIPMembers;
using Kidemy.Domain.Shared;

namespace Kidemy.Application.Services.Implementations
{
    public class VIPMembersService : IVIPMembersService
    {
        #region Fields

        private readonly IVIPMemberReposirory _vIPMemberReposirory;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor
        public VIPMembersService(IVIPMemberReposirory vIPMemberReposirory, IMediatorHandler mediatorHandler)
        {
            _vIPMemberReposirory = vIPMemberReposirory;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods
        public async Task<Result> JoinVIPMembersAsync(ClientSideJoinVIPMembersViewModel model)
        {
            if (model == null)
            {
                return Result.Failure(ErrorMessages.NullValue);
            }

            VIPMember vIPMember = new()
            {
                VIPPlanId = model.VIPPlanId,
                UserId = model.UserId,
                MembershipStartDate = model.MembershipStartDate,
                MembershipEndDate = model.MembershipEndDate,
                CreatedDateOnUtc = DateTime.UtcNow,
            };

            await _vIPMemberReposirory.InsertAsync(vIPMember);
            await _vIPMemberReposirory.SaveAsync();

            JoinVIPMembersEvent joinVIPMembersEvent = new(
                vIPMember.UserId,
                vIPMember.VIPPlanId,
                vIPMember.MembershipStartDate,
                vIPMember.MembershipEndDate
                );

            await _mediatorHandler.PublishEvent(joinVIPMembersEvent);

            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        public async Task<Result<ClientSideVIPUserInformationViewModel>> GetVIPUserInformation(int userId)
        {
            var vIPMembers = await _vIPMemberReposirory.LastOrDefaultAsync(
                              filter: v => v.UserId == userId && v.MembershipEndDate > DateTime.UtcNow,
                              orderBy: v => v.Id
                          );


            var isVIPUser = false;
            DateTime membershipEndDate = DateTime.UtcNow;

            if (vIPMembers is not null)
            {
                isVIPUser = true;
                membershipEndDate = vIPMembers.MembershipEndDate;
            }

            ClientSideVIPUserInformationViewModel vIPUserInformationViewModel = new ClientSideVIPUserInformationViewModel()
            {
                IsVIPUser = isVIPUser,
                MembershipEndDate = membershipEndDate,
            };

            return vIPUserInformationViewModel;
        }

        public async Task<Result<bool>> IsUserVIPMemberAsync(int userId)
        {
            if (userId <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }
            var userIsVIPMember = await _vIPMemberReposirory.AnyAsync(v => v.UserId == userId && v.MembershipEndDate > DateTime.UtcNow);

            if (!userIsVIPMember) return false;

            return true;


        }

        public async Task<Result> ExpirationVIPAccountAsync(int userId)
        {
            if (userId <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }
            var vIPMember = await _vIPMemberReposirory.LastOrDefaultAsync(
                                         filter: v => v.UserId == userId && v.MembershipEndDate > DateTime.UtcNow,
                                         orderBy: v => v.Id
                                     );

            vIPMember.MembershipEndDate = DateTime.UtcNow;

            _vIPMemberReposirory.Update(vIPMember);
            await _vIPMemberReposirory.SaveAsync();

            return Result.Success(SuccessMessages.SuccessfullyDone);

        }

        #endregion

    }
}
