using System;
using System.Threading.Tasks;

namespace Lexacom.Autofac
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine(TimeSpan.FromDays(365).ToString());
            //TestHarness.BasicRegistration();
            //TestHarness.AdvancedRegistration();
            //TestHarness.AssemblyScanningI();
            //await TestHarness.AssemblyScanningII();
            //TestHarness.ModuleRegistration();
            //await TestHarness.ScopeManagementI();
            //await TestHarness.ScopeManagementII();
        }
    }
}
