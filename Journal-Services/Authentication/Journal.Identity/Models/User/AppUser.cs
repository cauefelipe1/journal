using Microsoft.AspNetCore.Identity;

namespace Journal.Identity.Models.User;

public class AppUserModel : IdentityUser
{
    [ProtectedPersonalData]
    public string Nickname { get; set; } = string.Empty;

    [ProtectedPersonalData]
    public string FullName { get; set; } = string.Empty;
}

public class AppUserRegistrationInput
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}