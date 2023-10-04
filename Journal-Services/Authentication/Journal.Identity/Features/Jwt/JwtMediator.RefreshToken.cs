using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JetBrains.Annotations;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Journal.Identity.Features.Jwt;

public abstract partial class JwtMediator
{
    #region Create
    public class CreateRefreshTokenQuery : IRequest<RefreshTokenDTO>
    {
        public string JwtId { get; }
        public string UserId { get; }

        public CreateRefreshTokenQuery(string jwtId, string userId)
        {
            JwtId = jwtId;
            UserId = userId;
        }
    }

    [UsedImplicitly]
    public class CreateRefreshTokenHandler : IRequestHandler<CreateRefreshTokenQuery, RefreshTokenDTO>
    {
        private readonly IJwtRepository _repo;

        public CreateRefreshTokenHandler(IJwtRepository repo) => _repo = repo;

        public Task<RefreshTokenDTO> Handle(CreateRefreshTokenQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var refreshTokenDTO = new RefreshTokenDTO
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = request.JwtId,
                UserId = request.UserId,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMonths(6), //TODO: Get this from config
                Invalidated = false,
                Used = false
            };

            _repo.CreateRefreshToken(refreshTokenDTO);

            return refreshTokenDTO;

        });
    }
    #endregion Create

    #region Refresh
    public class RefreshTokenQuery : IRequest<UserLoginResult>
    {
        public RefreshTokenInput Input { get; }

        public RefreshTokenQuery(RefreshTokenInput input) => Input = input;
    }

    [UsedImplicitly]
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, UserLoginResult>
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IJwtRepository _repo;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IMediator _mediator;

        public RefreshTokenHandler(
            TokenValidationParameters tokenValidationParameters,
            IJwtRepository repo,
            UserManager<AppUserModel> userManager,
            IMediator mediator)
        {
            _tokenValidationParameters = tokenValidationParameters;
            _repo = repo;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<UserLoginResult> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var input = request.Input;

            var principal = GetPrincipalFromToken(input.Token);

            var (refreshToken, _) = ValidateToken(principal, input);

            if (refreshToken is not null)
            {
                refreshToken.Used = true;
                _repo.UpdateRefreshToken(refreshToken);

                string userId = principal!.Claims.Single(c =>
                    string.Equals(c.Type, Constants.JWT_USER_ID_CLAIM, StringComparison.InvariantCultureIgnoreCase)).Value;

                var userModel = await _userManager.FindByIdAsync(userId);

                var userLoginResult = await _mediator.Send(new GenerateJwtTokenQuery(userModel), cancellationToken);

                return userLoginResult;
            }

            return new UserLoginResult
            {
                Errors = new [] { "Invalid token" }
            };
        }

        private (RefreshTokenDTO? refreshToken, string? error) ValidateToken(ClaimsPrincipal? principal, RefreshTokenInput input)
        {
            if (principal is null)
                return (null, "Invalid token.");

            string expiryClaim = principal.Claims.Single(c =>
                string.Equals(c.Type, JwtRegisteredClaimNames.Exp, StringComparison.InvariantCultureIgnoreCase)).Value;

            long expiryDateLong = long.Parse(expiryClaim);
            DateTime expireDateUtc = DateTime.UnixEpoch.AddSeconds(expiryDateLong);

            if (expireDateUtc > DateTime.UtcNow)
                return (null, "The token is not expired yet.");

            string jtiValue = principal.Claims.Single(c =>
                string.Equals(c.Type, JwtRegisteredClaimNames.Jti, StringComparison.InvariantCultureIgnoreCase)).Value;

            var refreshToken = _repo.GetRefreshToken(input.RefreshToken);

            if (refreshToken is null)
                return (null, "Refresh token does not exist.");

            if (DateTime.UtcNow > refreshToken.ExpirationDate)
                return (null, "The refresh token token is expired.");

            if (refreshToken.Invalidated)
                return (null, "The refresh token is invalidated.");

            if (refreshToken.Used)
                return (null, "The refresh token has been used.");

            if (!string.Equals(refreshToken.JwtId, jtiValue))
                return (null, "The refresh token does not match the JWT.");

            return (refreshToken, null);

        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var localValidationParameters = _tokenValidationParameters.Clone();
                localValidationParameters.ValidateLifetime = false;

                var principal = tokenHandler.ValidateToken(token, localValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) =>
            (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);

    }
    #endregion Refresh

    #region Invalidate

    [UsedImplicitly]
    public class InvalidateRefreshTokenCommand : IRequest<bool>
    {
        public string UserId { get; }

        public InvalidateRefreshTokenCommand(string userId)
        {
            UserId = userId;
        }
    }

    [UsedImplicitly]
    public class InvalidateRefreshTokenHandler : IRequestHandler<InvalidateRefreshTokenCommand, bool>
    {
        private readonly IJwtRepository _repo;

        public InvalidateRefreshTokenHandler(IJwtRepository repo) => _repo = repo;

        public Task<bool> Handle(InvalidateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _repo.InvalidateRefreshTokenByUserId(request.UserId);

            return Task.FromResult(true);
        }
    }
    #endregion Invalidate
}