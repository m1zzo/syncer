using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syncer
{
    internal class Users
    {
        static readonly HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(5) };
        internal static async Task Firms()
        {
            try
            {
                Console.WriteLine(DateTime.Now + "   Syncing firms");
                HttpResponseMessage response = await client.GetAsync("http://api.billing.tizh.ru/sync/pirs_firm_all");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(DateTime.Now + "   Syncing firms complete");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(DateTime.Now + "   Syncing firms Exception Caught!");
                Console.WriteLine(DateTime.Now + "   Message :{0} ", e.Message);
            }
        }
        internal static async Task Firms_SelfEmployed()
        {
            try
            {
                Console.WriteLine(DateTime.Now + "   Syncing selfEmployed");
                HttpResponseMessage response = await client.GetAsync("http://api.billing.tizh.ru/sync/pirs_selfemployed_all");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(DateTime.Now + "   Syncing selfEmployed complete");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(DateTime.Now + "   Syncing selfEmployed Exception  Caught!");
                Console.WriteLine(DateTime.Now + "   Message :{0} ", e.Message);
            }
        }
    }
}
