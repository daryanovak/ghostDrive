using System;
using System.Linq;
using System.Linq.Expressions;
using GhostDrive.Domain.Models;

namespace GhostDrive.Application.Models
{
    public class FileDto : BaseDto
    {
        public string Name { get; set; }

        public DateTime UploadDate { get; set; }

        public long SizeBytes { get; set; }

        public string UserName { get; set; }

        public string Tags { get; set; }

        public bool IsReadonly(string user) => !UserName.Equals(user);

        public static Expression<Func<File, FileDto>> Projection
        {
            get
            {
                return file => new FileDto
                {
                    Id = file.Id,
                    Name = $"{file.Name}.{file.Extension}",
                    SizeBytes = file.SizeBytes,
                    UploadDate = file.UploadDate,
                    UserName = file.User.Login,
                    Tags = string.Join(' ', file.Tags.Select(x => x.Tag.Name))
                };
            }
        }

        public static FileDto Create(File file)
        {
            return Projection.Compile().Invoke(file);
        }
    }
}
