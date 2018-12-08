using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<CommandResult>
    {
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
