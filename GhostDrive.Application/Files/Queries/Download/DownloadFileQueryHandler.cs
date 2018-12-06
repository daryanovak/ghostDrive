using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Interfaces;
using GhostDrive.Persistence;
using MediatR;

namespace GhostDrive.Application.Files.Queries.Download
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileModel>
    {
        private readonly GhostDriveDbContext _context;
        private readonly IFileService _fileService;

        public DownloadFileQueryHandler(GhostDriveDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<DownloadFileModel> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FindAsync(request.Id);
            if (file == null)
            {
                return null;
            }

            var stream = await _fileService.GetFile(file.LocalName, cancellationToken);
            if (stream == null)
            {
                return null;
            }
            return new DownloadFileModel
            {
                Stream = stream,
                Name = file.FullName,
                ContentType = file.ContentType
            };

        }
    }
}
