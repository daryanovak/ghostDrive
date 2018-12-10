using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Queries.Get
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileDto>
    {
        private readonly GhostDriveDbContext _context;

        public GetFileQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.Files
                .Include(f => f.User)
                .Include(f => f.SharedFiles)
                .ThenInclude(l => l.User)
                .Include(f => f.Tags)
                .ThenInclude(t => t.Tag)
                .Where(f => f.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (file == null)
            {
                return null;
            }

            if (file.LocalName.Equals(request.ShortLinkKey))
            {
                return FileDto.Create(file);
            }

            if (file.User.Login != request.ActorName && file.SharedFiles.All(x => x.User.Login != request.ActorName))
            {
                return null;
            }

            return FileDto.Create(file);
        }
    }
}
