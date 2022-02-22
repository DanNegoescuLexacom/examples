namespace Lexacom.Autofac.Generics
{
    public class PrintMessage : IMessage
    {
        public PrintMessage(string message)
        {
            Message = message;
        }
        
        public string Message { get; }
    }
}