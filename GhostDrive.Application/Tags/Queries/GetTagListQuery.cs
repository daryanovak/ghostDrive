using System.Collections.Generic;
using MediatR;

namespace GhostDrive.Application.Tags.Queries
{
    public class GetTagListQuery : IRequest<IEnumerable<string>>
    {
        public GetTagListQuery(string userLogin)
        {
            UserLogin = userLogin;
        }

        public string UserLogin { get; set; }
    }
}
