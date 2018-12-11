using System.Collections.Generic;

namespace GhostDrive.Application.Models.ViewModels
{
    public class FilesViewModel
    {
        public FilesViewModel(IEnumerable<FileDto> files, IEnumerable<string> tags)
        {
            Files = files;
            Tags = tags;
        }

        public IEnumerable<FileDto> Files { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
