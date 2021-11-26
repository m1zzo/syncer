using Newtonsoft.Json;
using syncer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncer
{
    internal class Projects
    {
        static readonly HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(5) };
        internal static async Task DrxSync()
        {
            try
            {
                Console.WriteLine(DateTime.Now + "   Syncing from DRX");
                HttpResponseMessage response = await client.GetAsync("http://api.billing.tizh.ru/sync/DirectumProjects");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(DateTime.Now + "   Syncing from DRX complete");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(DateTime.Now + "   Syncing from DRX Exception Caught!");
                Console.WriteLine(DateTime.Now + "   Message :{0} ", e.Message);
            }
        }
        internal static async Task<List<Entity>> Get_Pirs_Projects()
        {
            try
            {
                Console.WriteLine(DateTime.Now + "   Getting projects to sync");
                HttpResponseMessage response = await client.GetAsync("http://api.billing.tizh.ru/sync/Pirs_Projects");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<Entity> entities = JsonConvert.DeserializeObject<List<Entity>>(responseBody) ?? throw new ArgumentException("Got nothing to sync!");
                Console.WriteLine(DateTime.Now + "   Got " + entities.Count + " projects to sync");
                return entities;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(DateTime.Now + "   Exception Caught!");
                Console.WriteLine(DateTime.Now + "   Message :{0} ", e.Message);
                throw new ArgumentException(DateTime.Now + "   Got nothing to sync!");
            }
        }
        internal static async Task Pirs_Project(int PirsId)
        {
            try
            {
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), "http://api.billing.tizh.ru/sync/Pirs_Project?PirsId=" + PirsId);
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(DateTime.Now + "   Sync project " + PirsId  + " Exception Caught!");
                Console.WriteLine(DateTime.Now + "   Message :{0} ", e.Message);
            }
        }
        internal static async Task Sync()
        {
            List<Entity> projects = await Get_Pirs_Projects();
            List<Task> tasks = new List<Task>();
            Parallel.ForEach(projects, project =>
            {
                tasks.Add(Pirs_Project(project.PirsId));
            });
            Console.WriteLine(DateTime.Now + "   Staring to sync projects");
            await Task.WhenAll(tasks);
            Console.WriteLine(DateTime.Now +  "   Synced!");
        }
    }
}
