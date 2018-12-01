using System.Collections.Generic;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Queries.List
{
    public class GetFileListQuery : IRequest<IEnumerable<FileDto>>
    {
    }
}
