using Microsoft.AspNetCore.Identity;

namespace Journal.Identity.Models.User;

public class AppUserModel : IdentityUser
{
    public int SecondaryId { get; set; }
}

public class AppUserRegistrationInput
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}