using AspNetPracticing.WebAPI.ImplementedServices;
using AspNetPracticing.WebAPI.ServiceContracts;

namespace AspNetPracticing.WebAPI.ProgramServices
{
    public static class AddJwtService
    {
        public static WebApplicationBuilder AddAlljwtService(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IJwtService, JwtService>();

            return builder;
        }
    }
}
