using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Application.Models;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.UserLogin, cancellationToken);
            if (user == null)
            {
                return CommandResult.Fail(CommandErrors.UserNotExists);
            }

            // Add Logic for SharedLinks

            return CommandResult.Success;
        }
    }
}
