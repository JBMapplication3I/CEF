// <copyright file="Program.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the program class</summary>
namespace Clarity.Ecommerce.UI.XPortal.Server
{
    using System.Threading.Tasks;
    // using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    // using Microsoft.Extensions.Logging;
    using ServiceStack;

    /// <summary>A program.</summary>
    public class Program
    {
        /// <summary>Main entry-point for this application.</summary>
        /// <param name="args">Array of command-line argument strings.</param>
        /// <returns>Exit-code for the process - 0 for success, else an error code.</returns>
        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }

        /// <summary>Creates host builder.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The new host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            /* NOTE: This causes it to load the settings into the providers list twice
             * It appears to be automatic so this block is not necessary
            .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder)
                => configurationBuilder
                    .AddJsonFile(
                        "appsettings.json",
                        optional: true,
                        reloadOnChange: true)
                    .AddJsonFile(
                        $"appsettings.{hostBuilderContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true,
                        reloadOnChange: true)
                    .AddEnvironmentVariables())
            */
            /*
            .ConfigureLogging((hostBuilderContext, loggingBuilder)
                => loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging")))
            */
            .ConfigureWebHostDefaults(webHostBuilder
                => webHostBuilder.UseModularStartup<Startup, StartupActivator>());
    }
}
