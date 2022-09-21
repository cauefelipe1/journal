namespace Journal.Identity.Models.Registration;

public class UserRegistrationResult
{
    public bool Success { get; set; }

    public string Token { get; set; } = string.Empty;

    public IEnumerable<string>? Errors { get; set; }
}