namespace GhostDrive.Application.Interfaces
{
    public interface IAccountService
    {
        string GenerateSalt();

        string GetHash(string password, string salt);
    }
}
