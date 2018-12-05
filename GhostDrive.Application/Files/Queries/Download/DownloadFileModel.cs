using System.IO;

namespace GhostDrive.Application.Files.Queries.Download
{
    public class DownloadFileModel
    {
        public MemoryStream Stream { get; set; }

        public string ContentType { get; set; }

        public string Name { get; set; }
    }
}
