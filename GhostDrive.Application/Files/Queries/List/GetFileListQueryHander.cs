﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Queries.List
{
    public class GetFileListQueryHander : IRequestHandler<GetFileListQuery, IEnumerable<FileDto>>
    {
        private readonly GhostDriveDbContext _context;

        public GetFileListQueryHander(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FileDto>> Handle(GetFileListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Files
                .Select(FileDto.Projection)
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
