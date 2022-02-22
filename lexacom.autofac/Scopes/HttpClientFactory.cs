using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Lexacom.Autofac.Scopes
{

    public class ManualHttpClientFactory : IHttpClientFactory
    {
        private readonly Dictionary<string, Uri> _clients = new Dictionary<string, Uri>();

        public ManualHttpClientFactory(Dictionary<string, Uri> clients)
        {
            _clients = clients;
        }

        public HttpClient GetInstance(string name)
        {
            if (!_clients.ContainsKey(name))
            {
                throw new ArgumentException($"No client with name {name}");
            }

            return new HttpClient { BaseAddress = _clients[name] };
        }
    }
}