using JetBrains.Annotations;
using Journal.Identity.Features.Jwt;
using Journal.Identity.Models.User;
using Journal.Localization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Journal.Identity.Features.User;

public partial class UserMediator
{
    public class UserLogoutCommand : IRequest<bool>
    {
        public Guid UserId { get; }

        public UserLogoutCommand(Guid userId) => UserId = userId;
    }

    [UsedImplicitly]
    public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommand, bool>
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Translations> _l10n;

        public UserLogoutCommandHandler(
            UserManager<AppUserModel> userManager,
            IMediator mediator,
            IStringLocalizer<Translations> l10n)
        {
            _userManager = userManager;
            _mediator = mediator;
            _l10n = l10n;
        }

        public async Task<bool> Handle(UserLogoutCommand query, CancellationToken cancellationToken)
        {
            string strUserId = query.UserId.ToString();

            string errorMessage = _l10n["LoginUsernameOrPasswordErrorMessage"];

            UserLoginResult GetLoginFailedResult() => new() { Errors = new[] { errorMessage } };

            var user = await _userManager.FindByIdAsync(strUserId);

            if (user is null)
                return false;

            bool result = await _mediator.Send(new JwtMediator.InvalidateRefreshTokenCommand(strUserId), cancellationToken);

            return true;
        }
    }
}