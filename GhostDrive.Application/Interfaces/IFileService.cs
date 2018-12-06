using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GhostDrive.Application.Interfaces
{
    public interface IFileService
    {
        Task<bool> SaveFile(Stream stream, string fileName, CancellationToken cancellationToken);

        Task<MemoryStream> GetFile(string fileName, CancellationToken cancellationToken);

        void DeleteFile(string fileName);
    }
}
