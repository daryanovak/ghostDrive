using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Queries.Get
{
    public class GetFileQuery : IRequest<FileDto>
    {
        public int Id { get; set; }

        public string ShortLinkKey { get; set; }

        public string ActorName { get; set; }
    }
}
