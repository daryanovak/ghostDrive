using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GhostDrive.Application.Interfaces;
using GhostDrive.Infrastructure.Models;
using Newtonsoft.Json;

namespace GhostDrive.Infrastructure.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private const string FirebaseShortLinkApiEndpoint = "https://firebasedynamiclinks.googleapis.com/v1/shortLinks?key=AIzaSyD_RCXRgiwwHUN5CvmtLmuObHIptIYq5o0";
        private const string FirebaseShortLinkDomain = "https://gdr.page.link";

        public async Task<string> GetShortLink(string longLink)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(FirebaseShortLinkApiEndpoint, GetShortLinkOptions(longLink));
                return await ParseResponse(result);
            }
        }

        private static HttpContent GetShortLinkOptions(string longLink)
        {
            var options = new
            {
                longDynamicLink = $"{FirebaseShortLinkDomain}/?link={longLink}",
                suffix = new
                {
                    option = "SHORT"
                }
            };
            var stringOptions = JsonConvert.SerializeObject(options);
            return new StringContent(stringOptions, Encoding.UTF8, "application/json");
        }

        private static async Task<string> ParseResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var stringResponse = await response.Content.ReadAsStringAsync();
            var shortLinkResponse = JsonConvert.DeserializeObject<ShortLinkResponse>(stringResponse);
            return shortLinkResponse.ShortLink;
        }
    }
}
