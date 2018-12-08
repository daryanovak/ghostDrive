using System;
using System.Linq.Expressions;
using GhostDrive.Domain.Enums;
using GhostDrive.Domain.Models;

namespace GhostDrive.Application.Models
{
    public class UserDto : BaseDto
    {
        public string Login { get; set; }

        public UserRole Role { get; set; }

        public DateTime RegistrationDate { get; set; }

        public static Expression<Func<User, UserDto>> Projection
        {
            get
            {
                return user => new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    RegistrationDate = user.RegistrationDate,
                    Role = user.Role
                };
            }
        }

        public static UserDto Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}
