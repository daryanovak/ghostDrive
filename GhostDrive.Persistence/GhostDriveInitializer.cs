using System;
using System.Collections.Generic;
using System.Linq;
using GhostDrive.Domain.Enums;
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

            if (context.Users.Any())
            {
                return;
            }

            SeedUsers(context);
        }

        public void SeedUsers(GhostDriveDbContext context)
        {
            var users = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Select(role => new User
            {
                Login = role.ToString().ToLower(),
                Password = "wJGQUmoCLzk5bXlTm0ZmFp6mByBh3vlLf+q19Rp+RC8=",
                PasswordSalt = "Od+ES7bdQXfOSAx9Dc0ZooSk/iDv1ATZdp6CqilHlgXCDxMyF31xYBmY7pAT+JW5HzlIkzAJDPt8tQhqMYdObrwVk0RjENhCCPTJgKzELrt91w4UHPcvXD45Lr9GP2OQFy8fUvgxkwREM4XqTVsUQx0jbSj0hcvyPl9VvpX8AuOc5Mw2Sz3u99BdmaIhabeHBTvA8q5TuT7EsXL9M0jfSHYFJd7PfjyxF8arD19x33CiR4t8y7SrFZQ/uliMPm0sVnxcHwcgMQfYOZeKOPJSnJtal/sc8Opu6H8jmFlMFCUWUP+NE5NW4GapXvG0Wrfg9uYvvakXjhJcPe8t98//cg==",
                Role = role,
                RegistrationDate = DateTime.Now
            });

            context.Users.AddRange(users);

            context.SaveChanges();
        }

        public IEnumerable<File> GetFiles()
        {
            return Enumerable.Range(1, 10).Select(index =>
                new File
                {
                    Name = $"File_{index}",
                    Extension = "txt",
                    LocalName = "",
                    SizeBytes = 1024,
                    UploadDate = DateTime.Now
                });
        }
    }
}
