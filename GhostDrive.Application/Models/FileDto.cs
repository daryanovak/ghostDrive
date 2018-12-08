using System;
using System.Linq.Expressions;
using GhostDrive.Domain.Models;

namespace GhostDrive.Application.Models
{
    public class FileDto : BaseDto
    {
        public string Name { get; set; }

        public DateTime UploadDate { get; set; }

        public long SizeBytes { get; set; } 

        public static Expression<Func<File, FileDto>> Projection
        {
            get
            {
                return file => new FileDto
                {
                    Id = file.Id,
                    Name = $"{file.Name}.{file.Extension}",
                    SizeBytes = file.SizeBytes,
                    UploadDate = file.UploadDate
                };
            }
        }

        public static FileDto Create(File file)
        {
            return Projection.Compile().Invoke(file);
        }
    }
}
