using System.Threading.Tasks;

namespace GhostDrive.Application.Interfaces
{
    public interface IShortLinkService
    {
        Task<string> GetShortLink(string longLink);
    }
}
