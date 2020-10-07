using System;
using HomeTask4.Infrastructure.Extensions;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HomeTask4.Core.Controllers;

/// <summary>
/// A skeleton for the Home Task 4 in AltexSoft Lab 2020
/// For more details how to organize configuration, logging and dependency injections in console app
/// watch https://www.youtube.com/watch?v=GAOCe-2nXqc
///
/// For more information about General Host
/// read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
///
/// For more information about Logging
/// read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1
///
/// For more information about Dependency Injection
/// read https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1
/// </summary>
namespace HomeTask4.Cmd
{
    public class Program
    {

        public static void Main(string[] args)
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
                    manager.ShowMainMenu();
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
                   services.AddScoped<IController, Controller>(sp => new Controller(sp.GetRequiredService<IUnitOfWork>()));
                   services.AddTransient<Manager>();
               })
               .ConfigureLogging(config =>
               {
                   config.ClearProviders();
               });
    }
}
