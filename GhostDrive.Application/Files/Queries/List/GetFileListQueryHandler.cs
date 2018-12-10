using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Queries.List
{
    public class GetFileListQueryHandler : IRequestHandler<GetFileListQuery, IEnumerable<FileDto>>
    {
        private readonly GhostDriveDbContext _context;

        public GetFileListQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FileDto>> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            var files = _context.Files.Where(file => file.User.Login.Equals(request.UserLogin));

            if (!string.IsNullOrEmpty(request.Tag))
            {
                files = files.Where(file => file.Tags.Any(t => t.Tag.Name == request.Tag));
            }

            return await files.Select(FileDto.Projection)
                .OrderByDescending(p => p.UploadDate)
                .ToListAsync(cancellationToken);
        }
    }
}
