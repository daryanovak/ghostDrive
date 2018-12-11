using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Application.Interfaces;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;

namespace GhostDrive.Application.Files.Commands.Delete
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, CommandResult>
    {
        private readonly GhostDriveDbContext _context;
        private readonly IFileService _fileService;

        public DeleteFileCommandHandler(GhostDriveDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<CommandResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FindAsync(request.FileId);
            if (file == null)
            {
                return CommandResult.Fail(CommandErrors.FileNotFound);
            }
            _fileService.DeleteFile(file.LocalName);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync(cancellationToken);
            return CommandResult.Success;
        }
    }
}
