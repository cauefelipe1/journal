using JetBrains.Annotations;
using Journal.Identity.Features.Jwt;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Journal.Identity.Features.User;

public partial class UserMediator
{
    public class UserRegistrationQuery : IRequest<UserRegistrationResult>
    {
        public AppUserRegistrationInput Input { get; }

        public UserRegistrationQuery(AppUserRegistrationInput input) => Input = input;
    }

    [UsedImplicitly]
    public class UserRegistrationHandler : IRequestHandler<UserRegistrationQuery, UserRegistrationResult>
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IMediator _mediator;

        public UserRegistrationHandler(
            UserManager<AppUserModel> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<UserRegistrationResult> Handle(UserRegistrationQuery query, CancellationToken cancellationToken)
        {
            var userInput = query.Input;

            var existingUser = await _userManager.FindByEmailAsync(userInput.Email);

            if (existingUser is not null)
            {
                //TODO: Set a proper result and message (also translate it)
                throw new Exception("The user already exists.");
            }

            var userModel = new AppUserModel
            {
                Email = userInput.Email,
                UserName = userInput.UserName
            };

            var userCreationResult = await _userManager.CreateAsync(userModel, userInput.Password);

            var result = await HandleUserCreationResult(userCreationResult, userModel);

            return result;
        }

        private async Task<UserRegistrationResult> HandleUserCreationResult(IdentityResult identityResult, AppUserModel appUserModel)
        {
            var result = new UserRegistrationResult
            {
                Success = identityResult.Succeeded
            };

            if (!result.Success)
            {
                result.Errors = identityResult.Errors.Select(error => error.Description);
            }
            else
            {
                var userLogin = await _mediator.Send(new JwtMediator.GenerateJwtTokenQuery(appUserModel));
                result.Token = userLogin.Token;
            }

            return result;
        }
    }
}