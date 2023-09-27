using Dapper;
using Journal.Identity.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Identity.Features.Jwt;

/// <summary>
/// <see cref="IJwtRepository"/> implementation that handles the JWT objects in the database.
/// </summary>
public class JwtRepository : IJwtRepository
{
    private readonly IdentityDatabaseContext _dbContext;

    public JwtRepository(IdentityDatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public RefreshTokenDTO? GetRefreshToken(string refreshToken)
    {
        var token =
            _dbContext.RefreshToken
            .Where(r => r.Token == refreshToken)
            .AsNoTracking()
            .FirstOrDefault();

        return token;
    }

    /// <inheritdoc/>
    public void UpdateRefreshToken(RefreshTokenDTO dto)
    {
        _dbContext.Update(dto);
        _dbContext.SaveChanges();
    }

    /// <inheritdoc/>
    public void CreateRefreshToken(RefreshTokenDTO dto)
    {
        _dbContext.Add(dto);
        _dbContext.SaveChanges();
    }

    public void InvalidateRefreshTokenByUserId(string userId)
    {
        using (var con = _dbContext.GetConnection())
        {
            const string SQL = @"
                update 
                    auth.refresh_token
                set
                    invalidated = true
                where 
                    used = false
                    and user_id = @UserId";

            con.Open();

            using (var t = con.BeginTransaction())
            {
                try
                {
                    var p = new { UserId = userId };

                    con.Execute(SQL, p, commandTimeout: 30);

                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw;
                }
            }
        }
    }
}