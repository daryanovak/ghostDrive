using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
 
namespace GhostDrive.Web.TechSupport
{
    public class TechSupportHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
