using GhostDrive.Application.Models.ViewModels;
using MediatR;

namespace GhostDrive.Application.Files.Queries.List
{
    public class GetFileListQuery : IRequest<FilesViewModel>
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
