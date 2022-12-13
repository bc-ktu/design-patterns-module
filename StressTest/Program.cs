

using System.Diagnostics;

namespace HelloWorld;

public static class Program
{
    static void Main(string[] args)
    {
        ProcessStartInfo ServerStartInfo = new ProcessStartInfo();
        ServerStartInfo.CreateNoWindow = false;
        ServerStartInfo.UseShellExecute = false;
        ServerStartInfo.FileName = "..\\..\\..\\..\\Server\\bin\\Debug\\net6.0-windows\\Server.exe";
        ServerStartInfo.WindowStyle = ProcessWindowStyle.Normal;
        ServerStartInfo.Arguments = "";

        Process serverProcess = Process.Start(ServerStartInfo);
        Thread.Sleep(3000);

        Console.WriteLine("Starting Client");

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.CreateNoWindow = false;
        startInfo.UseShellExecute = false;
        startInfo.FileName = "..\\..\\..\\..\\OOP_Bomberman_client_graphics_v1\\bin\\Debug\\net6.0-windows\\client_graphics.exe";
        startInfo.WindowStyle = ProcessWindowStyle.Normal;
        startInfo.Arguments = "";

        Process proc = Process.Start(startInfo);
        //Console.WriteLine(proc.HasExited);
        //Thread.Sleep(3000);
        //Console.WriteLine(proc.HasExited);
        //proc.Close();

        Console.WriteLine("Nope");
    }
}



