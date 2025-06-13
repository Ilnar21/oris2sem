using Microsoft.AspNetCore.Identity;

namespace HotMagazine.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
