using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Interfaces;
using GhostDrive.Persistence;
using MediatR;

namespace GhostDrive.Application.Files.Queries.ShortLink
{
    public class GetShortLinkQueryHandler : IRequestHandler<GetShortLinkQuery, string>
    {
        private readonly GhostDriveDbContext _context;
        private readonly IShortLinkService _shortLinkService;

        public GetShortLinkQueryHandler(GhostDriveDbContext context, IShortLinkService shortLinkService)
        {
            _context = context;
            _shortLinkService = shortLinkService;
        }

        public async Task<string> Handle(GetShortLinkQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FindAsync(request.FileId);
            if (string.IsNullOrEmpty(file.ShortLink))
            {
                file.ShortLink = await _shortLinkService.GetShortLink($"{request.Endpoint}/{file.LocalName}");
                _context.Files.Update(file);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return file.ShortLink;
        }
    }
}
