using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Journal.Identity.Models.Registration;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Journal.Identity.Feature.UserRegistration;

public class UserRegistrationMediator
{
    public class UserRegistrationQuery : IRequest<UserRegistrationResult>
    {
        public AppUserRegistrationInput Input { get; }

        public UserRegistrationQuery(AppUserRegistrationInput input) => Input = input;
    }

    public class UserRegistrationHandler : IRequestHandler<UserRegistrationQuery, UserRegistrationResult>
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly AppAuthSettings _authSettings;

        public UserRegistrationHandler(UserManager<AppUserModel> userManager, AppAuthSettings authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings;
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
                FullName = userInput.FullName,
                Nickname = userInput.FullName,
                Email = userInput.Email
            };

            var userCreationResult = await _userManager.CreateAsync(userModel, userInput.Password);

            var result = HandleUserCreationResult(userCreationResult, userModel);

            return result;
        }

        private UserRegistrationResult HandleUserCreationResult(IdentityResult identityResult, AppUserModel userModel)
        {
            var result = new UserRegistrationResult
            {
                Success = identityResult.Succeeded,
            };

            if (!result.Success)
            {
                result.Errors = identityResult.Errors.Select(error => error.Description);
            }
            else
            {
                result.Token = GenerateToken(userModel);
            }

            return result;
        }

        //TODO: Move it to a proper mediator class
        private string GenerateToken(AppUserModel userModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}