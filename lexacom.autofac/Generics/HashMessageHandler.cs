using System;
using System.Text;
using System.Threading.Tasks;

namespace Lexacom.Autofac.Generics
{
    public class HashMessageHandler : IMessageHandler<HashMessage>
    {
        private readonly HashService _hashService;
        public HashMessageHandler(HashService hashService)
        {
            _hashService = hashService;
        }

        public Task HandleAsync(HashMessage message)
        {
            var hash = _hashService.Hash(Encoding.UTF8.GetBytes(message.Message));
            Console.WriteLine("Hash: " + Convert.ToBase64String(hash));
            return Task.CompletedTask;
        }
    }
}