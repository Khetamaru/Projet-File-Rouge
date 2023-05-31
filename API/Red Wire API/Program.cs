using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace Red_Wire_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Set Stratup Parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("Démarage du serveur.");
                    Setup setup = SetupCacheFile.Start();

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    webBuilder.UseSetting("http_port", setup.HTTP_PORT);
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(setup.HTTP_URL + setup.HTTP_PORT + "/");
                    Console.WriteLine("Lancement du serveur sur : " + setup.HTTP_URL + setup.HTTP_PORT + "/");
                });
    }
}
