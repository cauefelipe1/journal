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
            const string ERROR_MESSAGE = "User or password invalid.";
            var userInput = query.Input;

            var user = await _userManager.FindByEmailAsync(userInput.Email);

            if (user is null)
            {
                //TODO: Set a proper result and message (also translate it)
                throw new Exception(ERROR_MESSAGE);
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, userInput.Password);

            if (!isPasswordValid)
            {
                //TODO: Set a proper result and message (also translate it)
                throw new Exception(ERROR_MESSAGE);
            }

            var result = await GetLoginResult(user);

            return result;
        }

        private async Task<UserLoginResult> GetLoginResult(AppUserModel appUserModel)
        {
            string token = await _mediator.Send(new JwtMediator.GenerateJwtTokenQuery(appUserModel));

            var result = new UserLoginResult
            {
                Token = token
            };

            return result;
        }
    }
}