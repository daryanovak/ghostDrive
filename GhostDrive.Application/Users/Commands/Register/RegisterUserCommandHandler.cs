using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GhostDrive.Application.Interfaces;
using GhostDrive.Application.Models;
using GhostDrive.Domain.Models;
using GhostDrive.Persistence;
using GhostDrive.Common;
using GhostDrive.Domain.Enums;

namespace GhostDrive.Application.Users.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CommandResult>
    {
        private readonly GhostDriveDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IDateTime _dateTime;

        public RegisterUserCommandHandler(
            GhostDriveDbContext context,
            IAccountService accountService,
            IDateTime dateTime)
        {
            _context = context;
            _accountService = accountService;
            _dateTime = dateTime;
        }

        public async Task<CommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Login == request.Login, cancellationToken))
            {
                return CommandResult.Fail("UserAlreadyExists");
            }
            if (!request.Password.Equals(request.ConfirmPassword))
            {
                return CommandResult.Fail("PasswordsAreNotEqual");
            }

            var passwordSalt = _accountService.GenerateSalt();
            var passwordHash = _accountService.GetHash(request.Password, passwordSalt);

            var entity = new User
            {
                Login = request.Login,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                RegistrationDate = _dateTime.Now,
                Role = UserRole.Common
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return CommandResult.Success;
        }
    }
}
