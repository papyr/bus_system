using TrackAChild.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrackAChild.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient client;
        private const string httpLink = "https://nominatim.openstreetmap.org/search?q=";
        private const string httpSuffix = "&format=json&addressdetails=1";

        public HttpService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("user-agent", "trackachild");
        }

        public async Task<string> SendGetRequest(string param)
        {
            using (var response = await client.GetAsync(httpLink + param + httpSuffix))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
