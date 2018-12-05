using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Application.Models;
using GhostDrive.Common;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Commands.Upload
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, CommandResult>
    {
        private const char ExtensionDelimiter = '.';

        private readonly GhostDriveDbContext _context;
        private readonly IDateTime _dateTime;

        public UploadFileCommandHandler(GhostDriveDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<CommandResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.UserName, cancellationToken);
            if (user == null)
            {
                return CommandResult.Fail("UserNotExists");
            }

            var localName = Guid.NewGuid().ToString();

            try
            {
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), ApplicationConstants.DriveFolder);
                Directory.CreateDirectory(directoryPath);
                var path = Path.Combine(directoryPath, localName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.FileStream.CopyToAsync(stream, cancellationToken);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

            var filename = request.FileName.Split(ExtensionDelimiter);

            var file = new Domain.Models.File
            {
                Name = filename[0],
                Extension = filename[1],
                LocalName = localName,
                ContentType = request.ContentType,
                SizeBytes = request.SizeBytes,
                UploadDate = _dateTime.Now,
                UserId = user.Id
                
            };
            _context.Files.Add(file);
            await _context.SaveChangesAsync(cancellationToken);
            return CommandResult.Success;
        }
    }
}
