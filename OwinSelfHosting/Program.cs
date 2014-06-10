using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://localhost:7766/"))
            {
                Parallel.For(0, 1000, new ParallelOptions{MaxDegreeOfParallelism = 8}, async index => {
                    var client = new HttpClient(new HttpClientHandler()
                    {
                        UseDefaultCredentials = true
                    }) { BaseAddress = new Uri("http://localhost:7766/") };
                    var result = await client.GetStringAsync("api/test");
                    Console.WriteLine(result.Trim('"'));
                });
                
                



                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();
            }
        }
    }
}
