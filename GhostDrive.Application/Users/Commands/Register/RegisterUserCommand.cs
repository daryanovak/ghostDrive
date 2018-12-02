using System.ComponentModel.DataAnnotations;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Users.Commands.Register
{
    public class RegisterUserCommand : IRequest<CommandResult>
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

