using System;
using Autofac;

namespace lexacom.autofac
{
    internal class Program
    {
        private static void Main(string[] args)
        {
             
            var cb = new ContainerBuilder();
            // create named registrations for specific implementations
            cb.Register(c => new GoogleHostUriProvider()).Named<IHostUriProvider>("Google");
            cb.Register(c => new BingHostUriProvider()).Named<IHostUriProvider>("Bing");

            // resolve those registrations where specifically required
            cb.Register(c => new GoogleMessageGenerator(c.ResolveNamed<IHostUriProvider>("Google")));
            cb.Register(c => new BingMessageGenerator(c.ResolveNamed<IHostUriProvider>("Bing")));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var bing = scope.Resolve<BingMessageGenerator>();
            // will print Bing URI due to injected named BingHostUriProvider
            bing.DoSomething();

            var google = scope.Resolve<GoogleMessageGenerator>();
            // will print Google URI due to injected named GoogleHostUriProvider
            google.DoSomething();

        }
    }

    internal class GoogleMessageGenerator
    {
        private readonly IHostUriProvider _hostProvider;

        public GoogleMessageGenerator(IHostUriProvider hostProvider)
        {
            _hostProvider = hostProvider;
        }

        public void DoSomething()
        {
            Console.WriteLine($"Go here for Google: {_hostProvider.GetUri()}");
        }
    }

    internal class BingMessageGenerator
    {
        private readonly IHostUriProvider _hostProvider;

        public BingMessageGenerator(IHostUriProvider hostProvider)
        {
            _hostProvider = hostProvider;
        }

        public void DoSomething()
        {
            Console.WriteLine($"Go here for Bing: {_hostProvider.GetUri()}");
        }
    }

    internal interface IHostUriProvider
    {
        Uri GetUri();
    }

    internal class GoogleHostUriProvider : IHostUriProvider
    {
        public Uri GetUri()
        {
            return new Uri("https://www.google.com");
        }
    }

    internal class BingHostUriProvider : IHostUriProvider
    {
        public Uri GetUri()
        {
            return new Uri("https://www.bing.com");
        }
    }
}
