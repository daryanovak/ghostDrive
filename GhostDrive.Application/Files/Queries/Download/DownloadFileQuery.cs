using MediatR;

namespace GhostDrive.Application.Files.Queries.Download
{
    public class DownloadFileQuery : IRequest<DownloadFileModel>
    {
        public int Id { get; set; }
    }
}
