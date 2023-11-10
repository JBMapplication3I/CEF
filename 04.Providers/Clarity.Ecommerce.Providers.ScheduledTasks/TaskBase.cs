// <copyright file="TaskBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the task base class</summary>
// ReSharper disable VirtualMemberCallInConstructor
namespace Clarity.Ecommerce.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Hangfire;
    using Interfaces.Models;
    using Interfaces.Tasks;
    using Interfaces.Workflow;

    /// <summary>A base task.</summary>
    /// <seealso cref="ITask"/>
    public abstract class TaskBase : ITask
    {
        /// <summary>Initializes a new instance of the <see cref="TaskBase"/> class.</summary>
        protected TaskBase()
        {
            // ReSharper disable AsyncConverter.AsyncWait
            var task = Workflows.ScheduledJobConfigurations.GetAsync(ConfigurationKey, contextProfileName: null);
            task.Wait(10_000);
            Configuration = task.Result!;
            if (Configuration == null)
            {
                CreateAndStoreADefaultConfigurationAsync(null).Wait(10_000);
            }
            // ReSharper restore AsyncConverter.AsyncWait
#pragma warning disable CA2214 // Do not call overridable methods in constructors
            LoadSettingsAsync(null);
#pragma warning restore CA2214 // Do not call overridable methods in constructors
        }

        /// <inheritdoc/>
        // ReSharper disable once MemberInitializerValueIgnored
        public IScheduledJobConfigurationModel Configuration { get; private set; } = null!;

        /// <inheritdoc/>
        public string ConfigurationKey => GetType().Name;

        /// <inheritdoc/>
        public string TaskCronSetting { get; protected set; } = null!;

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        protected IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <inheritdoc/>
        public abstract Task ProcessAsync(IJobCancellationToken? cancellationToken);

        /// <summary>Creates a setting.</summary>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="key">               The key.</param>
        /// <param name="value">             The value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new setting.</returns>
        protected static IScheduledJobConfigurationSettingModel CreateSetting(
            DateTime timestamp,
            string key,
            string value,
            string? contextProfileName)
        {
            var model = RegistryLoaderWrapper.GetInstance<IScheduledJobConfigurationSettingModel>(contextProfileName);
            model.Slave = RegistryLoaderWrapper.GetInstance<ISettingModel>(contextProfileName);
            model.Slave.Type = RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName);
            model.Active = model.Slave.Active = model.Slave.Type.Active = true;
            model.CreatedDate = model.Slave.CreatedDate = model.Slave.Type.CreatedDate = timestamp;
            model.Slave.Type.CustomKey = key;
            model.Slave.Type.Name = key;
            model.Slave.Type.DisplayName = key;
            model.Slave.Value = value;
            return model;
        }

        /// <summary>Creates a cron schedule setting with the standard naming convention.</summary>
        /// <param name="timestamp">         Timestamp for creation of this set of schedule settings, so they're all the
        ///                                  same.</param>
        /// <param name="cronString">        A cron string which describes how this task should be scheduled. See
        ///                                  crontab.guru fo reference.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new setting.</returns>
        protected IScheduledJobConfigurationSettingModel CreateCronScheduleSetting(
            DateTime? timestamp,
            string cronString,
            string? contextProfileName)
            => CreateSetting(
                timestamp ?? DateTime.Now,
                $"Process {ConfigurationKey}: Cron Schedule",
                cronString,
                contextProfileName);

        /// <summary>Loads the settings.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The settings.</returns>
        protected abstract Task LoadSettingsAsync(string? contextProfileName);

        /// <summary>Loads the settings.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="defaultCron">       The default cron.</param>
        /// <returns>The settings.</returns>
        protected async Task LoadSettingsAsync(string? contextProfileName, string defaultCron)
        {
            TaskCronSetting = await GetSettingValueOrCreateDefaultAsync(
                    Configuration.ScheduledJobConfigurationSettings!,
                    $"Process {ConfigurationKey}: Cron Schedule",
                    defaultCron,
                    contextProfileName)
                .ConfigureAwait(false)
                ?? "0 0 * * *";
        }

        /// <summary>Default settings.</summary>
        /// <param name="timestamp">  The timestamp Date/Time.</param>
        /// <param name="description">The description.</param>
        /// <returns>A List{IScheduledJobConfigurationSettingModel}.</returns>
        protected abstract List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description);

        /// <summary>Gets setting value or create default.</summary>
        /// <param name="settings">          Options for controlling the operation.</param>
        /// <param name="name">              The name.</param>
        /// <param name="defaultValue">      The default value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The setting value.</returns>
        protected async Task<string?> GetSettingValueOrCreateDefaultAsync(
            IEnumerable<IScheduledJobConfigurationSettingModel> settings,
            string name,
            string defaultValue,
            string? contextProfileName)
        {
            if (settings == null)
            {
                return null;
            }
            var value = settings.FirstOrDefault(x => x.Active && x.Slave!.Active && x.Slave.TypeName == name)?.Slave!.Value;
            if (value != null)
            {
                return value;
            }
            Configuration.ScheduledJobConfigurationSettings!.Add(
                CreateSetting(DateExtensions.GenDateTime, name, defaultValue, contextProfileName));
            await Workflows.ScheduledJobConfigurations.UpdateAsync(Configuration, contextProfileName).ConfigureAwait(false);
            return defaultValue;
        }

        /// <summary>Gets active task jobs count.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The active task jobs count.</returns>
        protected int GetActiveTaskJobsCount(string? contextProfileName)
        {
            return GetActiveTaskJobs(contextProfileName).Count;
        }

        /// <summary>Creates and store a default configuration.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new and store a default configuration.</returns>
        private async Task CreateAndStoreADefaultConfigurationAsync(string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            var model = RegistryLoaderWrapper.GetInstance<IScheduledJobConfigurationModel>(contextProfileName);
            model.Active = true;
            model.CreatedDate = timestamp;
            model.CustomKey = ConfigurationKey;
            model.Name = ConfigurationKey;
            model.ScheduledJobConfigurationSettings = DefaultSettings(timestamp, out var desc);
            model.Description = desc;
            var createResponse = await Workflows.ScheduledJobConfigurations.CreateAsync(model, contextProfileName).ConfigureAwait(false);
            Configuration = (await Workflows.ScheduledJobConfigurations.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false))!;
        }

        /// <summary>Gets active task jobs.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The active task jobs.</returns>
        private List<HangfireJob> GetActiveTaskJobs(string? contextProfileName)
        {
            return GetTaskJobs(
                contextProfileName,
                // ReSharper disable once StyleCop.SA1118
                new[]
                {
                    HangFireJobState.Enqueued,
                    HangFireJobState.Scheduled,
                    HangFireJobState.Processing,
                });
        }

        /// <summary>Gets task jobs.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="jobStates">         List of states of the jobs.</param>
        /// <returns>The task jobs.</returns>
        private List<HangfireJob> GetTaskJobs(string? contextProfileName, HangFireJobState[]? jobStates = null)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var query = context.HangfireJobs.Where(j => j.InvocationData!.Contains(ConfigurationKey));
                if (jobStates != null)
                {
                    var stateNames = jobStates.Select(s => s.ToString()).Distinct().ToList();
                    query = query.Where(j => stateNames.Contains(j.StateName!));
                }
                var jobs = query.ToList();
                return jobs;
            }
            catch (Exception ex)
            {
                Logger.LogError(ConfigurationKey, ex.Message, ex, contextProfileName);
                throw;
            }
        }
    }
}
