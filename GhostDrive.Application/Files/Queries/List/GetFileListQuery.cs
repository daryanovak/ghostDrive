using System.Collections.Generic;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Queries.List
{
    public class GetFileListQuery : IRequest<IEnumerable<FileDto>>
    {
        public GetFileListQuery(string userLogin, string tag)
        {
            UserLogin = userLogin;
            Tag = tag;
        }

        public string UserLogin { get; set; }

        public string Tag { get; set; }
    }
}
