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
        Console.Clear();
        string[] options2 = { "Run", "Kill" };
        int selectedIndex2 = 0;
        ConsoleKey key2;
        do
        {
            Console.Clear();
            for (int i = 0; i < options2.Length; i++)
            {
                if (i == selectedIndex2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-> " + options2[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("   " + options2[i]);
                }
            }
            key2 = Console.ReadKey(true).Key;

            if (key2 == ConsoleKey.UpArrow)
            {
                selectedIndex2 = (selectedIndex2 == 0) ? options2.Length - 1 : selectedIndex2 - 1;
            }
            else if (key2 == ConsoleKey.DownArrow)
            {
                selectedIndex2 = (selectedIndex2 + 1) % options2.Length;
            }

        } while (key2 != ConsoleKey.Enter);

        if (selectedIndex2 == 0)
        {
            Process.Start(options[selectedIndex]);
            return "Runned: " + options[selectedIndex];
        }
        else if (selectedIndex2 == 1)
        {
            foreach (var process in Process.GetProcessesByName(options[selectedIndex].Replace(".exe", "")))
            {
                process.Kill();
            }
            return "Killed: " + options[selectedIndex];
        }
        else
        {
            return "Error";
        }


    }

    public static void Main()
    {
        var client = new TcpClient("127.0.0.1", 27001);

        var stream = client.GetStream();

        var bw = new BinaryWriter(stream);
        var br = new BinaryReader(stream);
        string[] ProcList = { "Taskmgr.exe", "explorer.exe", "chrome.exe", "notepad.exe", "mspaint" };

        while (true)
        {
            Console.WriteLine("Client to Message....");
            bw.Write($"{Options(ProcList)}");
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