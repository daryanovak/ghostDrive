using System.Threading.Tasks;
using GhostDrive.Infrastructure.Services;
using Xunit;

namespace GhostDrive.Application.Tests.Services
{
    public class ShortLinkServiceTests
    {
        [Theory]
        [InlineData("https:///localhost", true)]
        [InlineData("https://localhost/Files/Details", false)]
        public async Task GetShortLink(string longLink, bool isNull)
        {
            var subject = new ShortLinkService();

            var result = await subject.GetShortLink(longLink);

            Assert.True(isNull && result == null || !isNull && result != null);
        }
    }
}
