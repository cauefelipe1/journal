namespace Journal.Identity.Features.Jwt;

public class RefreshTokenDTO
{
    public string JwtToken { get; set; } = default!;

    public string JwtId { get; set; } = default!;

    public DateTime CreationDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool Used { get; set; }

    public bool Invalidated { get; set; }

    public string UserId { get; set; } = default!;
}