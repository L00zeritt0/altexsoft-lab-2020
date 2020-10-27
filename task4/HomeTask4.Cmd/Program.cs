using System;
using HomeTask4.Infrastructure.Extensions;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HomeTask4.Core.Controllers;
using System.Threading.Tasks;

namespace HomeTask4.Cmd
{
    public class Program
    {

        public async static Task Main(string[] args)
        {
            Manager manager;
            try
            {
                var host = CreateHostBuilder(args).Build();
                manager = host.Services.GetRequiredService<Manager>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }

            while (true)
            {
                try
                {
                    await manager.ShowMainMenu();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("The recipe didn't add. There is some problem!");
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// This method should be separate to support EF command-line tools in design time
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
        /// </summary>
        /// <param name="args"></param>
        /// <returns><see cref="IHostBuilder" /> hostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureServices((context, services) =>
               {
                   services.AddInfrastructure(context.Configuration.GetConnectionString("Default"));
                   services.AddScoped<IController, Controller>();
                   services.AddTransient<Manager>();
               })
               .ConfigureLogging(config =>
               {
                   config.ClearProviders();
               });
    }
}
