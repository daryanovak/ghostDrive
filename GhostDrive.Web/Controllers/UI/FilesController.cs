using System.Threading.Tasks;
using GhostDrive.Application.Files.Commands.Delete;
using GhostDrive.Application.Files.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Controllers.UI
{
    public class FilesController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var files = await Mediator.Send(new GetFileListQuery());
            return View(files);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFileCommand(id));
            return RedirectToAction("Index");
        }
    }
}
