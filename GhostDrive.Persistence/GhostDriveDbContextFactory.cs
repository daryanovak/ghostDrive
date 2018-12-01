using Microsoft.EntityFrameworkCore;
using GhostDrive.Persistence.Infrastructure;

namespace GhostDrive.Persistence
{
    public class GhostDriveDbContextFactory : DesignTimeDbContextFactoryBase<GhostDriveDbContext>
    {
        protected override GhostDriveDbContext CreateNewInstance(DbContextOptions<GhostDriveDbContext> options)
        {
            return new GhostDriveDbContext(options);
        }
    }
}
