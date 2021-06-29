using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Adding database stuff and dependency injection
using MonarchAPI.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MonarchAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Creates the host builder, checks the db, then runs
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }


        //Method for initializing the database if it doesn't exist or just connecting to it.
        private static void CreateDbIfNotExists(IHost host) {

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MonarchContext>();
                    DbInitializer.Initialize(context);

                }
                catch (Exception ex) {

                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the Db.");
                
                }
            }
        
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
