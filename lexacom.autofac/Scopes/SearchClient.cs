using System.Threading.Tasks;

namespace Lexacom.Autofac.Scopes
{
    public class SearchClient
    {
        public readonly IHttpClientFactory _factory;

        public SearchClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<string> SearchAsync(string engine, string query)
        {
            var client = _factory.GetInstance(engine);
            var response = client.GetAsync($"/search?q={query}").Result;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}