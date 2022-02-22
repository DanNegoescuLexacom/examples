using System;
using System.Collections.Generic;
using System.Text;
using Lexacom.Autofac.Targets;

namespace Lexacom.Autofac.Logging
{
    internal class Logger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine($"[{level}]\t{DateTime.Now}\t{message}");
        }
    }

    internal class LoggerEx : ILogger
    {
        private readonly IEnumerable<ITarget> _targets;

        public LoggerEx(IEnumerable<ITarget> targets)
        {
            _targets = targets;
        } 
        
        public void Log(LogLevel level, string message)
        {
            var messageText = $"[{level}]\t{DateTime.Now}\t{message}";

            foreach (var target in _targets)
            {
                target.WriteMessage(messageText);
            }
        }
    }
}