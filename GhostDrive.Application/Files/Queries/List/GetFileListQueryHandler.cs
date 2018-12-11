using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Application.Models.ViewModels;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Queries.List
{
    public class GetFileListQueryHandler : IRequestHandler<GetFileListQuery, FilesViewModel>
    {
        private readonly GhostDriveDbContext _context;

        public GetFileListQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<FilesViewModel> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            var files = _context.Files.Where(file => file.User.Login.Equals(request.UserLogin));
            var tags = await files.SelectMany(f => f.Tags).Select(t => t.Tag.Name).ToListAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Tag))
            {
                files = files.Where(file => file.Tags.Any(t => t.Tag.Name == request.Tag));
            }

            var fileDtos = await files.Select(FileDto.Projection)
                .OrderByDescending(p => p.UploadDate)
                .ToListAsync(cancellationToken);

            return new FilesViewModel(fileDtos, tags);
        }
    }
}
