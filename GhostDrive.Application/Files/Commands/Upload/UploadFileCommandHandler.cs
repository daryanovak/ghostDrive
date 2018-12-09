using System;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Application.Interfaces;
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
        private readonly IFileService _fileService;
        private readonly IDateTime _dateTime;

        public UploadFileCommandHandler(
            GhostDriveDbContext context,
            IFileService fileService,
            IDateTime dateTime)
        {
            _context = context;
            _fileService = fileService;
            _dateTime = dateTime;
        }

        public async Task<CommandResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.UserName, cancellationToken);
            if (user == null)
            {
                return CommandResult.Fail(CommandErrors.UserNotExists);
            }

            var localName = Guid.NewGuid().ToString();
            var saveResult = await _fileService.SaveFile(request.FileStream, localName, cancellationToken);
            if (!saveResult)
            {
                return CommandResult.Fail(CommandErrors.FileSave);
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
