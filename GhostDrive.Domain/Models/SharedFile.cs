namespace GhostDrive.Domain.Models
{
    public class SharedFile
    {
        public SharedFile(int fileId, int userId)
        {
            FileId = fileId;
            UserId = userId;
        }

        public int FileId { get; set; }

        public virtual File File { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
