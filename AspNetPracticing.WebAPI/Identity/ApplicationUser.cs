using Microsoft.AspNetCore.Identity;

namespace AspNetPracticing.WebAPI.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Lastname { get; set; }
    }
}
