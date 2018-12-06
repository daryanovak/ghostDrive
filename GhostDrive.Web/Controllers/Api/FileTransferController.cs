using System.Threading.Tasks;
using GhostDrive.Application.Files.Commands.Delete;
using GhostDrive.Application.Files.Commands.Upload;
using GhostDrive.Application.Files.Queries.Download;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Controllers.Api
{
    public class FileTransferController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            await Mediator.Send(new UploadFileCommand
            {
                FileStream = file.OpenReadStream(),
                FileName = file.FileName,
                SizeBytes = file.Length,
                ContentType = file.ContentType,
                UserName = User.Identity.Name
            });

            return RedirectToFilesPage();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Download(int id)
        {
            var result = await Mediator.Send(new DownloadFileQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }

            return File(result.Stream, result.ContentType, result.Name);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFileCommand(id));
            return RedirectToFilesPage();
        }

        private RedirectToActionResult RedirectToFilesPage()
        {
            return RedirectToAction("Index", "Files");
        }
    }
}
