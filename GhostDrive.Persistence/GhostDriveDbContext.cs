using Microsoft.EntityFrameworkCore;
using GhostDrive.Domain.Models;
using GhostDrive.Persistence.Extensions;

namespace GhostDrive.Persistence
{
    public class GhostDriveDbContext : DbContext
    {
        public GhostDriveDbContext(DbContextOptions<GhostDriveDbContext> options)
            : base(options)
        {
        }

        public DbSet<File> Files { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
