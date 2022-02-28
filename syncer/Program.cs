using syncer;
using System.Diagnostics;

namespace HelloWorld
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        private const uint ENABLE_EXTENDED_FLAGS = 0x0080;
        static async Task Main()
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            SetConsoleMode(handle, ENABLE_EXTENDED_FLAGS);
            while (true)
            {
                List<Task> tasks = new List<Task> { Users.Firms(), Users.Firms_SelfEmployed()/*, Projects.DrxSync()*/ };
                await Task.WhenAll(tasks);
                await Projects.Sync();
                Console.WriteLine(DateTime.Now + "   Sleep for hour");
                Thread.Sleep(3600000);
            }
        }
    }
}