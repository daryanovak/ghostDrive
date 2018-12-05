using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<CommandResult<ClaimsIdentity>>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
