using System;
using System.Net.Http;
using Autofac;

namespace Lexacom.Autofac.Scopes
{
    public class AutofacHttpClientFactory : IHttpClientFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacHttpClientFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public HttpClient GetInstance(string name)
        {
            if (!_lifetimeScope.IsRegisteredWithName<Uri>(name))
            {
                throw new ArgumentException($"No client with name {name}");
            }

            return new HttpClient 
            {
                BaseAddress = _lifetimeScope.ResolveNamed<Uri>(name) 
            };
        }
    }
}