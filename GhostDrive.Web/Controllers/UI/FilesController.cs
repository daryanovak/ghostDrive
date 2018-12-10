using System.Threading.Tasks;
using GhostDrive.Application.Files.Queries.Get;
using GhostDrive.Application.Files.Queries.List;
using GhostDrive.Application.Files.Queries.SharedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Controllers.UI
{
    public class FilesController : BaseController
    {
        [Authorize]
        [Route("{tag?}")]
        public async Task<IActionResult> Index(string tag)
        {
            var files = await Mediator.Send(new GetFileListQuery(CurrentUser, tag));
            RouteData.Values.Remove("tag");
            return View(files);
        }

        [Authorize]
        public async Task<IActionResult> Shared()
        {
            var files = await Mediator.Send(new GetSharedFileListQuery(CurrentUser));
            return View(files);
        }

        [Authorize]
        [Route("{id}/{key?}")]
        public async Task<IActionResult> Details(int id, string key)
        {
            var file = await Mediator.Send(new GetFileQuery {Id = id, ShortLinkKey = key, ActorName = CurrentUser});
            if (file == null)
            {
                return NotFound();
            }
            return View(file);
        }
    }
}
