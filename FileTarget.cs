namespace Lexacom.Autofac 
{
    public class FileTarget : ITarget
    {
        public FileTarget(string path)
        {

        }
        
        public void WriteMessage(string text)
        {
            File.WriteAllText(
        }
    }
}