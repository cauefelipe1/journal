using JetBrains.Annotations;
using Journal.Identity.Features.Jwt;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

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

        public UserLoginHandler(
            UserManager<AppUserModel> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<UserLoginResult> Handle(UserLoginQuery query, CancellationToken cancellationToken)
        {
            //TODO: Translate it
            const string ERROR_MESSAGE = "User or password invalid.";

            UserLoginResult GetLoginFailedResult() => new() { Errors = new[] { ERROR_MESSAGE } };

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