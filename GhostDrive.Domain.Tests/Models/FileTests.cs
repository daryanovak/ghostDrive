using GhostDrive.Domain.Models;
using Xunit;

namespace GhostDrive.Domain.Tests.Models
{
    public class FileTests
    {
        [Fact]
        public void ShouldHaveCorrectFullName()
        {
            var file = new File
            {
                Name = "Name",
                Extension = "txt"
            };

            Assert.Equal("Name.txt", file.FullName);
        }
    }
}
