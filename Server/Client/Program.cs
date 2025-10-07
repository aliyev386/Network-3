using System.Diagnostics;
using System.Net.Sockets;

namespace Client;
class Program
{
    public static string Options(string[] options)
    {
        int selectedIndex = 0;
        ConsoleKey key;
        do
        {
            Console.Clear();
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-> " + options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("   " + options[i]);
                }
            }
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % options.Length;
            }

        } while (key != ConsoleKey.Enter);
        Process.Start(options[selectedIndex]);
        return options[selectedIndex];
    }

    public static void Main()
    {
        var client = new TcpClient("127.0.0.1", 27001);

        var stream = client.GetStream();

        var bw = new BinaryWriter(stream);
        var br = new BinaryReader(stream);
        string[] options = { "Taskmgr.exe", "explorer.exe", "chrome.exe", "notepad.exe", "mspaint" };

        while (true)
        {
            Console.WriteLine("Client to Message....");
            bw.Write(Options(options));
            Console.WriteLine($"Server answer: {br.ReadString()}");
        }
        // Taskmgr.exe
        // explorer.exe
        // chrome.exe
        // WINWORD.EXE
        // notepad.exe
        // mspaint



        //   var prosesName = Process.Start("mspaint");
        //
        //   Console.WriteLine(prosesName.ProcessName);
        //   Thread.Sleep(3000);
        //
        //   prosesName.Kill();
        //
        //   Console.WriteLine("Completed Kill");

    }
}