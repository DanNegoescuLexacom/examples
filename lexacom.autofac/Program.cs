using System.Threading.Tasks;

namespace Lexacom.Autofac
{
    internal class Program
    {
        // Write a method that 

        private static async Task Main(string[] args)
        {
            TestHarness.BasicRegistration();
            TestHarness.AdvancedRegistration();
            TestHarness.AssemblyScanningI();
            await TestHarness.AssemblyScanningII();
            TestHarness.ModuleRegistration();
            await TestHarness.ScopeManagementI();
            await TestHarness.ScopeManagementII();
        }
    }
}
