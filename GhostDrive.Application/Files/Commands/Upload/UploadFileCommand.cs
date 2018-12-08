using System.IO;
using GhostDrive.Application.Models;
using MediatR;

namespace GhostDrive.Application.Files.Commands.Upload
{
    public class UploadFileCommand : IRequest<CommandResult>
    {
        public Stream FileStream { get; set; }

        public string FileName { get; set; }

        public string FileDetailsEndpoint { get; set; }

        public long SizeBytes { get; set; }

        public string ContentType { get; set; }

        public string UserName { get; set; }
    }
}
