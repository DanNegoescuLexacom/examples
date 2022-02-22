using Autofac;
using Lexacom.Autofac.Targets;
using Microsoft.Extensions.Configuration;

namespace Lexacom.Autofac.Modules
{

    public class TargetsModule : Module
    {
        private readonly TargetsModuleSettings _settings;

        public TargetsModule(TargetsModuleSettings settings)
        {
            _settings = settings;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ConsoleTarget()).As<ITarget>().PreserveExistingDefaults();
            builder.Register(C => new FileTarget(_settings.FilePath)).As<ITarget>().PreserveExistingDefaults();
            base.Load(builder);
        }

        public static TargetsModule FromConfiguration(IConfiguration configuration)
        {
            return new TargetsModule(TargetsModuleSettings.FromConfiguration(configuration));
        }
    }
}