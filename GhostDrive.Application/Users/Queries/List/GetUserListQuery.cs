using System.Collections.Generic;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Users.Queries.List
{
    public class GetUserListQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
