namespace GhostDrive.Domain.Models
{
    public class File : BaseEntity
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Extension { get; set; }

        public int SizeBytes { get; set; }
    }
}
