using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Persistence;
using MediatR;

namespace GhostDrive.Application.Files.Queries.Download
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileModel>
    {
        private readonly GhostDriveDbContext _context;

        public DownloadFileQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<DownloadFileModel> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _context.Files.FindAsync(request.Id);
                if (file == null)
                {
                    return null;
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(),
                                        ApplicationConstants.DriveFolder,
                                        file.LocalName);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory, cancellationToken);
                }
                memory.Position = 0;
                return new DownloadFileModel
                {
                    Stream = memory,
                    Name = file.FullName,
                    ContentType = file.ContentType
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
