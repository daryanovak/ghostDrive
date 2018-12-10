using System.Security.Claims;

namespace GhostDrive.Application.Interfaces
{
    public interface IAccountService
    {
        string GenerateSalt();

        string GetHash(string password, string salt);

        ClaimsIdentity GetClaimsIdentity(string login, string role);
    }
}
