using System;
using System.Threading.Tasks;

namespace Lexacom.Autofac.Generics
{
    public class PrintMessageHandler : IMessageHandler<PrintMessage>
    {
        public Task HandleAsync(PrintMessage message)
        {
            Console.WriteLine(message.Message);
            return Task.CompletedTask;
        }
    }
}