// <copyright file="Startup.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the startup class</summary>
[assembly: Microsoft.Owin.OwinStartup(typeof(Clarity.Ecommerce.Scheduler.Startup))]

namespace Clarity.Ecommerce.Scheduler
{
    using Owin;

    /// <summary>A startup.</summary>
    public class Startup
    {
        /// <summary>Gets the application.</summary>
        /// <value>The application.</value>
        public static IAppBuilder App { get; private set; } = null!;

        /// <summary>Configurations the given application.</summary>
        /// <param name="app">The application.</param>
        // ReSharper disable once UnusedMember.Global
        public void Configuration(IAppBuilder app)
        {
            App = app;
            app.CreatePerOwinContext(() => new AppBuilderProvider(app));
            var logger = new Logger();
            logger.LogInformation("Scheduler", "Owin Startup initializing Hangfire Dashboard Start", true, null);
            // Start the server
            HangfireBootstrapper.Instance.Start(app, "Startup.Configuration");
            logger.LogInformation("Scheduler", "Owin Startup initializing Hangfire Dashboard Finished", true, null);
        }
    }
}
