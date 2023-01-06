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
    public class UserLoginQuery : IRequest<UserLoginResult>
    {
        public UserLoginInput Input { get; }

        public UserLoginQuery(UserLoginInput input) => Input = input;
    }

    [UsedImplicitly]
    public class UserLoginHandler : IRequestHandler<UserLoginQuery, UserLoginResult>
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Translations> _l10n;

        public UserLoginHandler(
            UserManager<AppUserModel> userManager,
            IMediator mediator,
            IStringLocalizer<Translations> l10n)
        {
            _userManager = userManager;
            _mediator = mediator;
            _l10n = l10n;
        }

        public async Task<UserLoginResult> Handle(UserLoginQuery query, CancellationToken cancellationToken)
        {
            string errorMessage = _l10n["LoginUsernameOrPasswordErrorMessage"];

            UserLoginResult GetLoginFailedResult() => new() { Errors = new[] { errorMessage } };

            var userInput = query.Input;

            var user = await _userManager.FindByEmailAsync(userInput.Email);

            if (user is null)
            {
                return GetLoginFailedResult();
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, userInput.Password);

            if (!isPasswordValid)
            {
                return GetLoginFailedResult();
            }

            var result = await _mediator.Send(new JwtMediator.GenerateJwtTokenQuery(user), cancellationToken);

            return result;
        }
    }
}