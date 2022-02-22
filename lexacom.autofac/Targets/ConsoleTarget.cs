using System;

namespace Lexacom.Autofac.Targets
{
    // an implementation of ITarget that writes to the console
    public class ConsoleTarget : ITarget
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

}