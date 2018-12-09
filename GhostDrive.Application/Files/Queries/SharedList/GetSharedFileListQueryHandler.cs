using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Queries.SharedList
{
    public class GetSharedFileListQueryHandler : IRequestHandler<GetSharedFileListQuery, IEnumerable<FileDto>>
    {
        private readonly GhostDriveDbContext _context;

        public GetSharedFileListQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FileDto>> Handle(GetSharedFileListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(user => user.Login.Equals(request.UserLogin))
                .SelectMany(user => user.SharedFiles)
                .Select(sharedFile => sharedFile.File)
                .Select(FileDto.Projection)
                .OrderByDescending(p => p.UploadDate)
                .ToListAsync(cancellationToken);
        }
    }
}
