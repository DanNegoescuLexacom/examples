using Microsoft.Extensions.Configuration;

namespace Lexacom.Autofac.Modules
{
    public class TargetsModuleSettings
    {
        public string FilePath { get; set; }

        public static TargetsModuleSettings FromConfiguration(IConfiguration configuration)
        {
            return new TargetsModuleSettings
            {
                FilePath = configuration["FilePath"]
            };
        }
    }
}