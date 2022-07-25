using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Red_Wire_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    webBuilder.UseSetting("http_port", "8086");
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("http://192.168.1.79:8086/");
                    webBuilder.UseUrls("http://localhost:8086/");
                });
    }
}
