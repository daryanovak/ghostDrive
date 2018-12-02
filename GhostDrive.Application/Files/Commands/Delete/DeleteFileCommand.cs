using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Commands.Delete
{
    public class DeleteFileCommand : IRequest<CommandResult>
    {
        public DeleteFileCommand(int fileId)
        {
            FileId = fileId;
        }

        public int FileId { get; set; }
    }
}
