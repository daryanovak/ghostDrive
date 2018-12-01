using System.Linq;
using GhostDrive.Domain.Models;

namespace GhostDrive.Persistence
{
    public class GhostDriveInitializer
    {
        public static void Initialize(GhostDriveDbContext context)
        {
            var initializer = new GhostDriveInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(GhostDriveDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Files.Any())
            {
                return;
            }

            SeedFiles(context);
        }

        public void SeedFiles(GhostDriveDbContext context)
        {
            var files = Enumerable.Range(1, 10).Select(index =>
                new File
                {
                    //Id = index,
                    Name = $"File_{index}",
                    Extension = "txt",
                    Location = "",
                    SizeBytes = 1024
                });

            context.Files.AddRange(files);

            context.SaveChanges();
        }
    }
}
