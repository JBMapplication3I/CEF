// <copyright file="Program.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the program class</summary>
namespace Clarity.Ecommerce.UI.MVCHost
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>A program.</summary>
    public static class Program
    {
        /// <summary>Main entry-point for this application.</summary>
        /// <param name="args">Array of command-line argument strings.</param>
        /// <returns>Exit-code for the process - 0 for success, else an error code.</returns>
        public static async Task Main(string[] args)
        {
            using var tokenSource = new CancellationTokenSource();
            await CreateWebHostBuilder(args).Build().RunAsync(tokenSource.Token).ConfigureAwait(false);
        }

        /// <summary>Creates web host builder.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The new web host builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventLog();
                })
                ////.UseKestrel()
                ////.UseContentRoot(Directory.GetCurrentDirectory())
                ////.UseSetting("detailedErrors", "true")
                ////.UseIISIntegration()
                .UseStartup<Startup>()
                .CaptureStartupErrors(true);
    }
}
