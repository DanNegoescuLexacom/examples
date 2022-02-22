using System;
using System.IO;

namespace Lexacom.Autofac.Targets
{
    // an implementation of ITarget that appends to a file
    public class FileTarget : ITarget
    {
        private readonly string _filePath;

        public FileTarget(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteMessage(string message)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }
}