using System.Threading.Tasks;

namespace Lexacom.Autofac.Generics
{
    public interface IMessageHandler<TMessage> where TMessage : IMessage
    {
        Task HandleAsync(TMessage message);
    }
}