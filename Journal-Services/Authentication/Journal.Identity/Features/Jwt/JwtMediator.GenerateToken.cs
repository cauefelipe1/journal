using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JetBrains.Annotations;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Journal.Identity.Features.Jwt;

public abstract partial class JwtMediator
{
    public class GenerateJwtTokenQuery : IRequest<UserLoginResult>
    {
        public AppUserModel UserModel { get; }

        public GenerateJwtTokenQuery(AppUserModel userModel) => UserModel = userModel;
    }

    [UsedImplicitly]
    public class GenerateJwtTokenHandler : IRequestHandler<GenerateJwtTokenQuery, UserLoginResult>
    {
        private readonly AppAuthSettings _authSettings;
        private readonly IMediator _mediator;

        public GenerateJwtTokenHandler(AppAuthSettings authSettings, IMediator mediator)
        {
            _authSettings = authSettings;
            _mediator = mediator;
        }

        public async Task<UserLoginResult> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
        {
            var userModel = request.UserModel;
            string jwtId = Guid.NewGuid().ToString();

            var refreshTokenTask = _mediator.Send(new CreateRefreshTokenQuery(jwtId, userModel.Id), cancellationToken);

            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.JwtSecret);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, request.UserModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, jwtId),
                    new Claim(Constants.JWT_USER_ID_CLAIM, userModel.Id),
                    new Claim(Constants.JWT_USER_SECONDARY_ID_CLAIM, userModel.SecondaryId.ToString())
                }),
                Expires = DateTime.UtcNow.Add(_authSettings.JwtLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            var refreshToken = await refreshTokenTask;

            return new UserLoginResult
            {
                Token = token,
                RefreshToken = refreshToken.Token
            };
        }
    }
}