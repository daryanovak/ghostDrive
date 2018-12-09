using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Application.Models;
using GhostDrive.Domain.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Commands.Share
{
    public class ShareFileCommandHandler : IRequestHandler<ShareFileCommand, CommandResult>
    {
        private readonly GhostDriveDbContext _context;

        public ShareFileCommandHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(ShareFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(u => u.SharedFiles)
                .FirstOrDefaultAsync(u => u.Login == request.UserLogin, cancellationToken);
            if (user == null)
            {
                return CommandResult.Fail(CommandErrors.UserNotExists);
            }
            if (user.SharedFiles.Any(x => x.FileId == request.FileId))
            {
                return CommandResult.Fail(CommandErrors.AlreadyShared);
            }
            var fileOwnerId = await _context.Files.Where(f => f.Id == request.FileId).Select(f => f.UserId)
                .FirstAsync(cancellationToken);
            if (fileOwnerId == user.Id)
            {
                return CommandResult.Fail(CommandErrors.UserIsOwner);
            }

            user.SharedFiles.Add(new SharedFile(request.FileId, user.Id));
            await _context.SaveChangesAsync(cancellationToken);

            return CommandResult.Success;
        }
    }
}
