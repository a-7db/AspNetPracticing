using AspNetPracticing.WebAPI.Identity;
using AspNetPracticing.WebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AspNetPracticing.WebAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        { }

        public DbSet<City> City { get; set; }
    }
}
