using System.Net.Http;

namespace Lexacom.Autofac.Scopes
{
    public interface IHttpClientFactory
    {
        HttpClient GetInstance(string name);
    }
}