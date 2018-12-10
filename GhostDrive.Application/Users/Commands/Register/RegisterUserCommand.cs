using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Users.Commands.Register
{
    public class RegisterUserCommand : IRequest<CommandResult<ClaimsIdentity>>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}

