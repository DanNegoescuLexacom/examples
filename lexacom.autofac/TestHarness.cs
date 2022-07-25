using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Autofac;
using Lexacom.Autofac.Generics;
using Lexacom.Autofac.Logging;
using Lexacom.Autofac.Modules;
using Lexacom.Autofac.Targets;
using Lexacom.Autofac.Scopes;
using System.Diagnostics;

namespace Lexacom.Autofac
{
    public static class TestHarness
    {
        ///<summary>Performs basic registration of a concrete impl and a consuming service</summary>
        public static void BasicRegistration()
        {
            Console.WriteLine("Basic Registration");
            Console.WriteLine();

            var cb = new ContainerBuilder();
            cb.Register(c => new Logger()).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();

            Console.WriteLine();
        }

        public static void AdvancedRegistration()
        {
            Console.WriteLine("Advanced Registration");
            Console.WriteLine();
            var cb = new ContainerBuilder();
            cb.Register(c => new LoggerEx(c.Resolve<IEnumerable<ITarget>>())).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            cb.Register(c => new ConsoleTarget()).As<ITarget>();

            // Last registration always wins (FileTarget overrides ConsoleTarget)
            //cb.Register(C => new FileTarget(@"d:\scratch\log.txt")).As<ITarget>();

            // unless we preserve existing defaults...
            cb.Register(C => new FileTarget(@"d:\scratch\log.txt")).As<ITarget>().PreserveExistingDefaults();

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var targets = scope.Resolve<IEnumerable<ITarget>>();
            foreach (var target in targets)
            {
                Console.WriteLine("Target: " + target.GetType().Name);
            }

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();

            Console.WriteLine();
        }

        public static void AssemblyScanningI()
        {
            Console.WriteLine("Assembly Scanning (I)");
            Console.WriteLine();

            var cb = new ContainerBuilder();
            cb.Register(c => new LoggerEx(c.Resolve<IEnumerable<ITarget>>())).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            //register all ITargets in the current assembly
            //cb.RegisterAssemblyTypes(typeof(ITarget).Assembly)
            //    .Where(t => t.IsAssignableTo<ITarget>())
            //    .AsImplementedInterfaces();

            // register all ITarget types except FileTarget (supplying custom parameters)
            cb.RegisterAssemblyTypes(typeof(ITarget).Assembly)
                .Except<FileTarget>(e => e.WithParameter(new TypedParameter(typeof(string), @"d:\scratch\log.txt")))
                .AsImplementedInterfaces();

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();

            Console.WriteLine();
        }

        public static async Task AssemblyScanningII()
        {
            Console.WriteLine("Assembly Scanning (II)");
            Console.WriteLine();

            var cb = new ContainerBuilder();
            cb.Register(c => new LoggerEx(c.Resolve<IEnumerable<ITarget>>())).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            // where do we register the HashService?
            cb.Register(c => new HashService());

            //register all IMessageHandler types in the current assembly
            cb.RegisterAssemblyTypes(typeof(IMessageHandler<>).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>)))
                .AsImplementedInterfaces();

            // register our message handler resolver (ILifetimeScope is implicitly registered)
            cb.Register(c => new MessageHandlerResolver(c.Resolve<ILifetimeScope>()));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var resolver = scope.Resolve<MessageHandlerResolver>();

            await resolver.InvokeHandlerAsync(new PrintMessage("Hello World"));
            await resolver.InvokeHandlerAsync(new HashMessage("Hello World"));

            Console.WriteLine();
        }

        public static void ModuleRegistration()
        {
            Console.WriteLine("Module Registration");
            Console.WriteLine();

            var cb = new ContainerBuilder();
            cb.Register(c => new LoggerEx(c.Resolve<IEnumerable<ITarget>>())).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            // Can construct settings directly
            var settings = new TargetsModuleSettings { FilePath = @"d:\scratch\log.txt" };
            
            // Or can use IConfiguration to get settings from appsettings.json
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            //cb.RegisterModule(TargetsModule.FromConfiguration(configuration));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();

            Console.WriteLine();
        }

        public static async Task ScopeManagementI()
        {
            Console.WriteLine("Scope Management (I)");
            Console.WriteLine();

            var uris = new Dictionary<string, Uri> 
            { 
                {"Google", new Uri("https://www.google.com")}, 
                {"Bing", new Uri("https://www.bing.com") }
            };

            var cb = new ContainerBuilder();
            cb.Register(c => new ManualHttpClientFactory(uris)).SingleInstance();
            cb.Register(c => new SearchClient(c.Resolve<ManualHttpClientFactory>())).InstancePerLifetimeScope();

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var searchClient = scope.Resolve<SearchClient>();
            var response = await searchClient.SearchAsync("Bing", "Hello World");
            Console.WriteLine(response);

            // different SearchClient instance from child scope (same HttpClientFactory)
            using var childScope = scope.BeginLifetimeScope();
            var childSearchClient = childScope.Resolve<SearchClient>();
            Debug.Assert(childSearchClient.GetHashCode() != searchClient.GetHashCode());
            Debug.Assert(childSearchClient._factory.GetHashCode() == searchClient._factory.GetHashCode());

            // different SearchClient instance from peer scope (still same HttpClientFactory)
            using var peerScope = container.BeginLifetimeScope();
            var peerSearchClient = childScope.Resolve<SearchClient>();
            Debug.Assert(peerSearchClient.GetHashCode() != searchClient.GetHashCode());
            Debug.Assert(peerSearchClient._factory.GetHashCode() == searchClient._factory.GetHashCode());

            Console.WriteLine();
        }

        public static async Task ScopeManagementII()
        {
            Console.WriteLine("Scope Management (II)");
            Console.WriteLine();

            var uris = new Dictionary<string, Uri> 
            { 
                {"Google", new Uri("https://www.google.com")}, 
                {"Bing", new Uri("https://www.bing.com") }
            };

            var cb = new ContainerBuilder();
            cb.Register(c => new AutofacHttpClientFactory(c.Resolve<ILifetimeScope>())).As<IHttpClientFactory>().SingleInstance();
            cb.Register(c => new SearchClient(c.Resolve<IHttpClientFactory>())).InstancePerLifetimeScope();
            cb.Register(c => new Uri("https://www.bing.com")).Named<Uri>("Bing");
            cb.Register(c => new Uri("https://www.google.com")).Named<Uri>("Google");

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var searchClient = scope.Resolve<SearchClient>();
            var response = await searchClient.SearchAsync("Google", "Hello World");
            Console.WriteLine(response);

            // same SearchClient instance from same scope
            var searchClient2 = scope.Resolve<SearchClient>();
            Debug.Assert(searchClient2.GetHashCode() == searchClient.GetHashCode());

            // different SearchClient instance from child scope (same HttpClientFactory)
            using var childScope = scope.BeginLifetimeScope();
            var childSearchClient = childScope.Resolve<SearchClient>();
            Debug.Assert(childSearchClient.GetHashCode() != searchClient.GetHashCode());
            Debug.Assert(childSearchClient._factory.GetHashCode() == searchClient._factory.GetHashCode());

            // different SearchClient instance from peer scope (still same HttpClientFactory)
            using var peerScope = container.BeginLifetimeScope();
            var peerSearchClient = childScope.Resolve<SearchClient>();
            Debug.Assert(peerSearchClient.GetHashCode() != searchClient.GetHashCode());
            Debug.Assert(peerSearchClient._factory.GetHashCode() == searchClient._factory.GetHashCode());

            Console.WriteLine();
        }
    }
}