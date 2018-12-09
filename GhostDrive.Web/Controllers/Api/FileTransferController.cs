using System.Threading.Tasks;
using GhostDrive.Application.Files.Commands.Delete;
using GhostDrive.Application.Files.Commands.Share;
using GhostDrive.Application.Files.Commands.Upload;
using GhostDrive.Application.Files.Queries.Download;
using GhostDrive.Application.Files.Queries.ShortLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GhostDrive.Web.Controllers.Api
{
    public class FileTransferController : ApiBaseController
    {
        private readonly IStringLocalizer<FileTransferController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedLocalizer;

        public FileTransferController(
            IStringLocalizer<FileTransferController> localizer,
            IStringLocalizer<SharedResources> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        [HttpPost]
        [Authorize]
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

        [HttpGet]
        public async Task<string> GetShortLink(int id)
        {
            var fileDetailsLink = $"{Request.Scheme}://{Request.Host}{Url.Action("Details", "Files")}";
            var shortLink = await Mediator.Send(new GetShortLinkQuery(id, fileDetailsLink));
            return shortLink ?? _localizer["ShortLinkError"];
        }

        [HttpPost]
        public async Task<IActionResult> ShareFile([FromBody] ShareFileCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(_sharedLocalizer[result.FailureReason].Value);
        }

        private RedirectToActionResult RedirectToFilesPage()
        {
            return RedirectToAction("Index", "Files");
        }
    }
}
