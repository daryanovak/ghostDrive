using System;
using System.Security.Cryptography;
using System.Text;
using GhostDrive.Application.Interfaces;

namespace GhostDrive.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private const int SaltSize = 256;

        private readonly SHA256 _hashAlgorithm;
        private readonly RandomNumberGenerator _randomGenerator;

        public AccountService(SHA256 hashAlgorithm, RandomNumberGenerator randomGenerator)
        {
            _hashAlgorithm = hashAlgorithm;
            _randomGenerator = randomGenerator;
        }

        public string GenerateSalt()
        {
            var salt = new byte[SaltSize];
            _randomGenerator.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public string GetHash(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(string.Concat(password, salt));
            var hash = _hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
