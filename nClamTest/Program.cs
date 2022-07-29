// See https://aka.ms/new-console-template for more information
using nClam;

while (true)
{
    Console.WriteLine("Specify a file path to scan for viruses: ");
    Console.Write("> ");
    var path = Console.ReadLine();

    if (path == null) continue;
    if (path?.ToUpperInvariant() == "EXIT")
    {
        break;
    }

    var clam = new ClamClient("localhost", 3310);
    var result = await clam.SendAndScanFileAsync(path);
    Console.WriteLine(result.ToString());
}
