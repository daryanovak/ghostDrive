using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Tags.Queries
{
    public class GetTagListQueryHandler : IRequestHandler<GetTagListQuery, IEnumerable<string>>
    {
        private readonly GhostDriveDbContext _context;

        public GetTagListQueryHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetTagListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Files.Where(file => file.User.Login.Equals(request.UserLogin))
                .SelectMany(f => f.Tags)
                .Select(t => t.Tag.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
