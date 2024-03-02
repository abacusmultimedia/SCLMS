using Sclms.Persistence.Modles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Sclms.Persistence.Context
{
    public class DrillingDBContext : IdentityDbContext<AppUsers, IdentityRole, string>
    {


        public DrillingDBContext(DbContextOptions<DrillingDBContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVersion> ProductVersions { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}

