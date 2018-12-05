using System.Threading.Tasks;
using GhostDrive.Application.Files.Queries.List;
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
    }
}
