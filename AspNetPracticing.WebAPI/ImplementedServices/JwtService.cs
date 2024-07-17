using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Identity;
using AspNetPracticing.WebAPI.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetPracticing.WebAPI.ImplementedServices
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponse CreateJwtToken(ApplicationUser user)
        {
            // 5:00 --> 5:10 ends after 10 minutes
            var ExpTime = DateTime.UtcNow.AddMinutes
                (Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[] {
                // Sub => Identity
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
                // Jti => Jwt Unique ID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                // Iat => Time of token generation
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), 
                // NameIdentifier => Unique Identifier of the user [Email]
                new Claim(ClaimTypes.NameIdentifier, user.Email!), 
                // Sub => Identity
                new Claim(ClaimTypes.Name, user.UserName!), 
            };

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            //---->   Hash the sceret key   <-------

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
                // Apply the Hask Type => HmacSha256
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //---->   Hash the sceret key   <-------

            JwtSecurityToken GeneratorToken = new JwtSecurityToken
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: ExpTime,
                signingCredentials: credentials
            );

            // Create Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(GeneratorToken);

            return new AuthResponse 
            { 
                Token = token,
                Email = user.Email ?? "",
                PersonName = user.UserName ?? "",
                Expiration = ExpTime
            };
        }
    }
}
