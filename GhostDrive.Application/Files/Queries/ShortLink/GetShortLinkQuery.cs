using MediatR;

namespace GhostDrive.Application.Files.Queries.ShortLink
{
    public class GetShortLinkQuery : IRequest<string>
    {
        public GetShortLinkQuery(int fileId, string endpoint)
        {
            FileId = fileId;
            Endpoint = endpoint;
        }

        public int FileId { get; set; }

        public string Endpoint { get; set; }
    }
}
