namespace Journal.Identity.Features.Jwt;

/// <summary>
/// Defines the methods to handle the JWT token feature.
/// </summary>
public interface IJwtRepository
{
    /// <summary>
    /// Gets a refresh token from the storage.
    /// </summary>
    /// <param name="refreshToken">THe refresh token.</param>
    /// <returns>An instance of <see cref="RefreshTokenDTO"/></returns>
    RefreshTokenDTO? GetRefreshToken(string refreshToken);

    /// <summary>
    /// Updates a refresh token instance in the storage.
    /// </summary>
    /// <param name="dto">The <see cref="RefreshTokenDTO"/> instance to be updated.</param>
    void UpdateRefreshToken(RefreshTokenDTO dto);

    /// <summary>
    /// Saves a refresh token instance in the storage.
    /// </summary>
    /// <param name="dto">The <see cref="RefreshTokenDTO"/> instance to be saved.</param>
    void CreateRefreshToken(RefreshTokenDTO dto);
}