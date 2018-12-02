using System.ComponentModel.DataAnnotations;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<CommandResult>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
