using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GhostDrive.Domain.Models
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            Files = new List<FileTag>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<FileTag> Files { get; }
    }
}
