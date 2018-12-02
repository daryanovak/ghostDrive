using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Domain.Models;
using GhostDrive.Persistence;
using MediatR;

namespace GhostDrive.Application.Files.Commands.Delete
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, CommandResult>
    {
        private readonly GhostDriveDbContext _context;

        public DeleteFileCommandHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = new File {Id = request.FileId};
            _context.Files.Attach(file);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync(cancellationToken);
            return CommandResult.Success;
        }
    }
}
