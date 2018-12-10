using System;
using System.Collections.Generic;

namespace GhostDrive.Domain.Models
{
    public class File : BaseEntity
    {
        public File()
        {
            SharedFiles = new List<SharedFile>();
            Tags = new List<FileTag>();
        }

        public string Name { get; set; }

        public string LocalName { get; set; }

        public string Extension { get; set; }

        public string ShortLink { get; set; }

        public string ContentType { get; set; }

        public long SizeBytes { get; set; }

        public DateTime UploadDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string FullName => $"{Name}.{Extension}";

        public ICollection<SharedFile> SharedFiles { get; }

        public ICollection<FileTag> Tags { get; }
    }
}
