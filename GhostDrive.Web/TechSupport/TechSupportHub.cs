using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
 
namespace GhostDrive.Web.TechSupport
{
    public class TechSupportHub : Hub
    {
        public async Task SendMessage(string message, string fromUserName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, fromUserName);
        }
    }
}
