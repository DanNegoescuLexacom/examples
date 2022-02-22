using System.Threading.Tasks;
using Autofac;

namespace Lexacom.Autofac.Generics 
{
    public class MessageHandlerResolver
    {
        private ILifetimeScope _lifetimeScope;

        public MessageHandlerResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task InvokeHandlerAsync(IMessage message)
        {
            var baseHandlerType = typeof(IMessageHandler<>);
            var genericHandlerType = baseHandlerType.MakeGenericType(message.GetType());

            using (var transactionScope = _lifetimeScope.BeginLifetimeScope("messaging_scope"))
            {
                var handler = transactionScope.Resolve(genericHandlerType);

                //Handle the message
                var methodInfo = genericHandlerType.GetMethod(nameof(IMessageHandler<IMessage>.HandleAsync));
                await (Task) methodInfo.Invoke(handler, new[] {message});
            }
        }
    }
}