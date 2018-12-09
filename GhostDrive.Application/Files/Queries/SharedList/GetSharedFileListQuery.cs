using System.Collections.Generic;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Queries.SharedList
{
    public class GetSharedFileListQuery : IRequest<IEnumerable<FileDto>>
    {
        public GetSharedFileListQuery(string userLogin)
        {
            UserLogin = userLogin;
        }

        public string UserLogin { get; set; }
    }
}
