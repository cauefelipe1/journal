using JetBrains.Annotations;
using Journal.Domain.Models.User;
using Journal.Identity.Extensions;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Journal.Identity.Features.User;

[UsedImplicitly]
public partial class UserMediator
{
    public class GetUserDataQuery : IRequest<UserDataModel?>
    {
        public Guid? UserId { get; }

        public uint? UserSecondaryId { get; }

        public GetUserDataQuery(Guid userId) => UserId = userId;

        public GetUserDataQuery(uint userSecondaryId) => UserSecondaryId = userSecondaryId;
    }

    [UsedImplicitly]
    public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, UserDataModel?>
    {
        private readonly UserManager<AppUserModel> _userManager;

        public GetUserDataHandler(UserManager<AppUserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDataModel?> Handle(GetUserDataQuery query, CancellationToken cancellationToken)
        {
            AppUserModel? dto;

            if (query.UserId.HasValue)
                dto = await _userManager.FindByIdAsync(query.UserId.Value.ToString());

            else if (query.UserSecondaryId.HasValue)
                dto = await _userManager.FindBySecondaryIdAsync(query.UserSecondaryId.Value);
            else
                throw new ArgumentException("A valid User Id or Secondary Id must be informed.");

            if (dto is null)
                return null;

            var result = new UserDataModel
            {
                Id = Guid.Parse(dto.Id),
                SecondaryId = dto.SecondaryId,
                Email = dto.NormalizedEmail.ToLower(),
                UserType = UserType.Premium,
                Username = dto.UserName
            };

            return result;
        }
    }

}