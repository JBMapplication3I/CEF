// <copyright file="SchedulerApplicationPreload.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scheduler application preload class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System.Web.Hosting;

    /// <summary>An application preload.</summary>
    /// <seealso cref="IProcessHostPreloadClient"/>
    public class SchedulerApplicationPreload : IProcessHostPreloadClient
    {
        /// <inheritdoc/>
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start(Startup.App, "SchedulerApplicationPreload.Preload");
        }
    }
}
