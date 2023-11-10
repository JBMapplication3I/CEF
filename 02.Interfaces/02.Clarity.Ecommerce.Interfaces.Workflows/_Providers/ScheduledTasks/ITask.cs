// <copyright file="ITask.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITask interface</summary>
namespace Clarity.Ecommerce.Interfaces.Tasks
{
    using System.Threading.Tasks;
    using Hangfire;
    using Models;

    /// <summary>Interface for task.</summary>
    public interface ITask
    {
        /// <summary>Gets the configuration key.</summary>
        /// <value>The configuration key.</value>
        string ConfigurationKey { get; }

        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        IScheduledJobConfigurationModel Configuration { get; }

        /// <summary>Gets the task cron setting.</summary>
        /// <value>The task cron setting.</value>
        string TaskCronSetting { get; }

        /// <summary>Processes the logic for the task to execute.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        Task ProcessAsync(IJobCancellationToken? cancellationToken);
    }
}
