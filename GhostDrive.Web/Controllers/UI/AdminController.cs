using System.Threading.Tasks;
using GhostDrive.Application.Users.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Controllers.UI
{
    public class AdminController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var users = await Mediator.Send(new GetUserListQuery());
            return View(users);
        }
    }
}