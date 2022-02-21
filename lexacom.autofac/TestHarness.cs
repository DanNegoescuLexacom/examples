using System.Collections.Generic;
using Autofac;

namespace Lexacom.Autofac
{
    public static class TestHarness
    {
        ///<summary>Performs basic registration of a concrete impl and a consuming service</summary>
        public static void BasicRegistration()
        {
            var cb = new ContainerBuilder();
            cb.Register(c => new Logger()).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();
        }

        public static void AdvancedRegistration()
        {
            var cb = new ContainerBuilder();
            cb.Register(c => new LoggerEx(c.Resolve<IEnumerable<ITarget>>())).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();
        }

        public static void ModuleRegistration()
        {
            var cb = new ContainerBuilder();
            cb.Register(c => new Logger()).As<ILogger>();
            cb.Register(c => new TestEntrypoint(c.Resolve<ILogger>()));

            using var container = cb.Build();
            using var scope = container.BeginLifetimeScope();

            var entrypoint = scope.Resolve<TestEntrypoint>();
            entrypoint.ExecuteTest();
        }
    }
}