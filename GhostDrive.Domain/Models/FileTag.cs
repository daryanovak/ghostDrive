namespace GhostDrive.Domain.Models
{
    public class FileTag
    {
        public int FileId { get; set; }

        public virtual File File { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
