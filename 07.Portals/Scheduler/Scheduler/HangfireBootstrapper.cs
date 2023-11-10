// <copyright file="HangfireBootstrapper.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire bootstrapper class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Hosting;
    using Hangfire;
    using Hangfire.Dashboard;
    using JSConfigs;
    using Microsoft.AspNet.Identity.Owin;
    using Owin;

    /// <summary>A hangfire bootstrapper.</summary>
    /// <seealso cref="IRegisteredObject"/>
    public class HangfireBootstrapper : IRegisteredObject
    {
        /// <summary>The instance.</summary>
        public static readonly HangfireBootstrapper Instance = new();

        /// <summary>Prevents a default instance of the <see cref="HangfireBootstrapper"/> class from being created.</summary>
        private HangfireBootstrapper()
        {
        }

        /// <summary>Gets or sets the application.</summary>
        /// <value>The application.</value>
        private IAppBuilder App { get; set; } = null!;

        /// <summary>Starts this HangfireBootstrapper.</summary>
        /// <param name="app">     The application.</param>
        /// <param name="caller">The function calling this one.</param>
        public void Start(IAppBuilder app, string caller)
        {
            var logger = new Logger();
            logger.LogInformation("Scheduler.HangfireBootstrapper", caller + " is initializing Hangfire", true, null);
            App ??= app;
            try
            {
                App ??= HttpContext.Current.GetOwinContext().Get<AppBuilderProvider>().Get();
            }
            catch
            {
                // Do Nothing
            }
            CEFConfigDictionary.Load();
            HostingEnvironment.RegisterObject(this);
            GlobalConfiguration.Configuration.UseSqlServerStorage("ClarityEcommerceEntities");
            var serverOptions = new BackgroundJobServerOptions
            {
                ServerName = $"{CEFConfigDictionary.SchedulerNodeName}.{Environment.MachineName}",
                WorkerCount = CEFConfigDictionary.SchedulerWorkerCount,
                ServerTimeout = TimeSpan.FromSeconds(CEFConfigDictionary.SchedulerServerTimeoutInSeconds),
                HeartbeatInterval = TimeSpan.FromSeconds(CEFConfigDictionary.SchedulerHeartBeatIntervalInSeconds),
                SchedulePollingInterval = TimeSpan.FromSeconds(CEFConfigDictionary.SchedulerSchedulePollingIntervalInSeconds),
                ServerCheckInterval = TimeSpan.FromSeconds(CEFConfigDictionary.SchedulerServerCheckIntervalInSeconds),
                ShutdownTimeout = TimeSpan.FromSeconds(CEFConfigDictionary.SchedulerShutdownTimeout),
                Queues = (CEFConfigDictionary.SchedulerQueues ?? "DEFAULT").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
            };
            var dashboardOptions = new DashboardOptions
            {
                AppPath = null,
                StatsPollingInterval = CEFConfigDictionary.SchedulerDashboardStatsPollingIntervalInSeconds,
                Authorization = new List<IDashboardAuthorizationFilter>
                {
                    new AllowEveryoneDashboardAuthorizationFilter(),
                },
            };
            App!
                .UseHangfireServer(serverOptions, JobStorage.Current)
                .UseHangfireDashboard("/Dashboard", dashboardOptions, JobStorage.Current);
            HangfireTaskInitializer.InitializeTaskSet();
        }

        /// <summary>Stops this HangfireBootstrapper.</summary>
        public void Stop()
        {
            HostingEnvironment.UnregisterObject(this);
        }

        /// <inheritdoc/>
        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}
