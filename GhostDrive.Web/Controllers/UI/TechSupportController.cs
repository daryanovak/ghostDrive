using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Controllers.UI
{
    public class TechSupportController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
