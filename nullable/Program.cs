using Newtonsoft.Json;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var nullable = new Nullable();
MemoryStream? stream = null;
nullable.Save(stream);

public class Nullable 
{
    private readonly FileInfo _fileInfo;

    public Nullable()
    {}

    public Nullable(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
    }

    public void Save(Stream inStream)
    {
        using (var stream = File.OpenWrite(_fileInfo.FullName))
        {
            inStream.CopyTo(stream);
        }
    }
}