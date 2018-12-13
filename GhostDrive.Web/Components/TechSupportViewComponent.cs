using System.Threading.Tasks;
using GhostDrive.Application.Tags.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GhostDrive.Web.Components
{
    public class TechSupport : ViewComponent
    {
        private readonly IMediator _mediator;

        public TechSupport(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var tags = await _mediator.Send(new GetTagListQuery(userName));
            return View("Default", User.Identity.Name);
        }
    }
}
