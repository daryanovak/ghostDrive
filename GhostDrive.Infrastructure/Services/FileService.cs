using System;
using System.IO;
using System.Threading.Tasks;
using GhostDrive.Application.Interfaces;
using System.Threading;

namespace GhostDrive.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _driveFolder;

        public FileService(string driveFolder)
        {
            _driveFolder = driveFolder;
        }

        public async Task<bool> SaveFile(Stream stream, string fileName, CancellationToken cancellationToken)
        {
            try
            {
                var directoryPath = GetDirectoryPath();
                Directory.CreateDirectory(directoryPath);
                var path = Path.Combine(directoryPath, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream, cancellationToken);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MemoryStream> GetFile(string fileName, CancellationToken cancellationToken)
        {
            var path = GetFilePath(fileName);

            try
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory, cancellationToken);
                }
                memory.Position = 0;

                return memory;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeleteFile(string fileName)
        {
            var path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _driveFolder, fileName);
        }

        private string GetDirectoryPath()
        {
            return GetFilePath(string.Empty);
        }
    }
}
