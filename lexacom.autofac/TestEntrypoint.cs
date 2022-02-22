using System;
using Lexacom.Autofac.Logging;

namespace Lexacom.Autofac
{
    public class TestEntrypoint
    {
        private readonly ILogger _logger;

        public TestEntrypoint(ILogger logger)
        {
            _logger = logger;
        }

        public void ExecuteTest()
        {
            _logger.Log(LogLevel.Info, "We're doing something...");
            _logger.Log(LogLevel.Warn, "Uh oh. It's going wrong...");
            _logger.Log(LogLevel.Error, "There was an error!");
            Console.ReadKey();
        }
    }
}