using System;
using System.Collections.Generic;
using GhostDrive.Domain.Enums;

namespace GhostDrive.Domain.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Files = new List<File>();
            SharedFiles = new List<SharedFile>();
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public UserRole Role { get; set; }

        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<File> Files { get; }

        public virtual ICollection<SharedFile> SharedFiles { get; }
    }
}
