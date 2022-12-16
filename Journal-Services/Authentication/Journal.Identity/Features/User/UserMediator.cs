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
    public class GetUserDataQuery : IRequest<UserData?>
    {
        public int UserId { get; }

        public GetUserDataQuery(int userId) => UserId = userId;
    }

    [UsedImplicitly]
    public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, UserData?>
    {
        private readonly UserManager<AppUserModel> _userManager;

        public GetUserDataHandler(UserManager<AppUserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserData?> Handle(GetUserDataQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindBySecondaryIdAsync(query.UserId);

            if (user is null)
                return null;

            var result = new UserData
            {
                UserId = user.SecondaryId,
                Email = user.NormalizedEmail.ToLower(),
                Name = "Dominic Toreto", //TODO: Add a field for that in the database.
                Nickname = "Dom", //TODO: Add a field for that in the database.
                UserType = UserType.Premium,
                Username = user.UserName,
                DisplayName = "Dominic"
            };

            return result;
        }
    }

}