using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Commands.Update
{
    public class UpdateFileCommand : IRequest<CommandResult>
    {
        public int FileId { get; set; }

        public string Tags { get; set; }
    }
}
