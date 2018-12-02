using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GhostDrive.Application.Interfaces;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;

namespace GhostDrive.Application.Users.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, CommandResult>
    {
        private readonly GhostDriveDbContext _context;
        private readonly IAccountService _accountService;

        public LoginUserCommandHandler(
            GhostDriveDbContext context,
            IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<CommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
            if (user == null)
            {
                return CommandResult.Fail("UserNotExists");
            }

            var passwordHash = _accountService.GetHash(request.Password, user.PasswordSalt);
            return !passwordHash.Equals(user.Password) ? CommandResult.Fail("IncorrectPassword") : CommandResult.Success;
        }
    }
}
