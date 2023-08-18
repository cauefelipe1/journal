using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;

namespace Journal.Identity.Models.User;

[UsedImplicitly]
public class Role : IdentityRole
{
    public long SecondaryId { get; set; }
}