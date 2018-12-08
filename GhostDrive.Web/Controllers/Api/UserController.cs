using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostDrive.Application.Users.Commands.Delete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Controllers.Api
{
    public class UserController : ApiBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCommand(id));
            return RedirectToAction("Index", "Admin");
        }
    }
}