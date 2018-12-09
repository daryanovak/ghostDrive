using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Interfaces;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResult>
    {
        private readonly GhostDriveDbContext _context;
        private readonly IFileService _fileService;

        public DeleteUserCommandHandler(GhostDriveDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<CommandResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(u => u.SharedFiles).Include(u => u.Files)
                .SingleAsync(u => u.Id == request.UserId, cancellationToken);

            foreach (var file in user.Files)
            {
                _fileService.DeleteFile(file.LocalName);
            }

            user.SharedFiles.Clear();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return CommandResult.Success;
        }
    }
}
