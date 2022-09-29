using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JetBrains.Annotations;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Journal.Identity.Features.Jwt;

[UsedImplicitly]
public class JwtMediator
{
    public class GenerateJwtTokenQuery : IRequest<string>
    {
        public AppUserModel UserModel { get; }

        public GenerateJwtTokenQuery(AppUserModel userModel) => UserModel = userModel;
    }

    [UsedImplicitly]
    public class GenerateJwtTokenHandler : IRequestHandler<GenerateJwtTokenQuery, string>
    {
        private readonly AppAuthSettings _authSettings;

        public GenerateJwtTokenHandler(AppAuthSettings authSettings) => _authSettings = authSettings;

        public Task<string> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, request.UserModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        });
    }
}