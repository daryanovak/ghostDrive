using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Commands.Share
{
    public class ShareFileCommand : IRequest<CommandResult>
    {
        public int FileId { get; set; }

        public string UserLogin { get; set; }
    }
}
