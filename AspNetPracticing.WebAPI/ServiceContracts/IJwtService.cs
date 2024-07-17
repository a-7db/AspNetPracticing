using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Identity;

namespace AspNetPracticing.WebAPI.ServiceContracts
{
    public interface IJwtService
    {
        AuthResponse CreateJwtToken(ApplicationUser user);
    }
}
