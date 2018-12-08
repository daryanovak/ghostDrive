using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Users.Queries.List
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable<UserDto>>
    {
        private readonly GhostDriveDbContext _context;

        public GetUserListQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Select(UserDto.Projection)
                .OrderByDescending(p => p.RegistrationDate)
                .ToListAsync(cancellationToken);
        }
    }
}
