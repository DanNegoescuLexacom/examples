namespace Lexacom.Autofac 
{
    public class ConsoleTarget : ITarget
    {
        public void WriteMessage(string text)
        {
            Console.WriteLine(text);
        }
    }
}