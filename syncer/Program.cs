using syncer;

namespace HelloWorld
{
    class Program
    {

        static async Task Main()
        {
            while (true)
            {
                List<Task> tasks = new List<Task> { Users.Firms(), Users.Firms_SelfEmployed(), Projects.DrxSync() };
                await Task.WhenAll(tasks);
                await Projects.Sync();
                Console.WriteLine(DateTime.Now + "   Sleep for hour");
                Thread.Sleep(3600000);
            }
        }
    }
}