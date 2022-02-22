namespace Lexacom.Autofac.Generics
{
    public class HashMessage : IMessage
    {
        public HashMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}