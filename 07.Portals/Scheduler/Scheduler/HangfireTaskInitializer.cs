// <copyright file="HangfireTaskInitializer.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Hangfire task initializer class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Hangfire;
    using Hangfire.Common;
    using Interfaces.Tasks;
    using JSConfigs;
    using Mapper;
    using StructureMap.TypeRules;
    using Tasks.UpdateTranslationsFiles;

    /// <summary>A Hangfire starter.</summary>
    public static class HangfireTaskInitializer
    {
        /// <summary>Name of the constant ITask method 'Process'.</summary>
        private const string ConstITaskMethodName = nameof(ITask.ProcessAsync);

        private static string timeZoneSetting = null!;

        private static string TimeZoneSetting
        {
            get
            {
                if (string.IsNullOrWhiteSpace(timeZoneSetting))
                {
                    timeZoneSetting = CEFConfigDictionary.SchedulerTimeZone;
                }
                if (string.IsNullOrWhiteSpace(timeZoneSetting))
                {
                    timeZoneSetting = "UTC";
                }
                return timeZoneSetting;
            }
        }

        private static TimeZoneInfo TimeZoneInfo { get; } = GetTimeZoneInfo(TimeZoneSetting);

        /// <summary>Gets the specific tasks to load and isolate processing for testing.</summary>
        /// <value>The test tasks.</value>
        private static IEnumerable<string> EnabledTasks
        {
            get
            {
                var list = ConfigurationManager.AppSettings["Clarity.Scheduler.Tasks"];
                return !string.IsNullOrWhiteSpace(list)
                    ? list.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList()
                    : new();
            }
        }

        /// <summary>Initializes the task set.</summary>
        public static void InitializeTaskSet()
        {
            BaseModelMapper.Initialize();
            var tasks = new List<ITask>();
            var logger = new Logger();
            // Initialize all tasks in this DLL
            try
            {
                foreach (var type in GetTaskClasses())
                {
                    try
                    {
                        // This activation will generate default settings for the task in the settings table if they don't exist yet
                        tasks.Add((ITask)Activator.CreateInstance(type));
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        logger.LogError(
                            name: $"Scheduler.{nameof(InitializeTaskSet)}.{e.GetType().Name}",
                            message: e.LoaderExceptions.Aggregate(
                                "Loader Exceptions:",
                                (x, exception) => $"{x}\r\n{exception.Message}"),
                            contextProfileName: null);
                    }
                    catch (Exception ex1)
                    {
                        logger.LogError(
                            name: $"Scheduler.{nameof(InitializeTaskSet)}.{ex1.GetType().Name}",
                            message: "Task Constructor Exception",
                            ex: ex1,
                            contextProfileName: null);
                    }
                }
                // ReSharper disable once AsyncApostle.AsyncWait
                new UpdateTranslationsFilesTask().ProcessAsync(null).Wait(30_000);
            }
            catch (ReflectionTypeLoadException e)
            {
                logger.LogError(
                    name: $"Scheduler.{nameof(InitializeTaskSet)}.{e.GetType().Name}",
                    message: e.LoaderExceptions.Aggregate(
                        "Loader Exceptions:",
                        (x, exception) => $"{x}\r\n{exception.Message}"),
                    contextProfileName: null);
            }
            var manager = new RecurringJobManager(JobStorage.Current);
            logger.LogInformation(
                name: $"Scheduler.Initializing Tasks: {tasks.Count}",
                message: $"Initializing {tasks.Count} Tasks",
                contextProfileName: null);
            // Create tasks
            var count = 0;
            var failCount = 0;
            foreach (var task in tasks)
            {
                logger.LogInformation(
                    name: $"Scheduler.Initializing Tasks: {task.ConfigurationKey}",
                    message: $"Discovered Task: {task.ConfigurationKey}",
                    contextProfileName: null);
                try
                {
                    var typeOfTask = task.GetType();
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var job = new Job(typeOfTask, typeOfTask.GetMethod(ConstITaskMethodName), new object[1]);
                    logger.LogInformation(
                        name: $"Scheduler.Initializing Tasks: {task.ConfigurationKey}",
                        message: $"Adding Task '{task.ConfigurationKey}' to the Background Job Manager",
                        contextProfileName: null);
                    manager.AddOrUpdate(task.ConfigurationKey, job, task.TaskCronSetting, TimeZoneInfo);
                    count++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    logger.LogError($"Scheduler.Initializing Tasks: {task.ConfigurationKey}", ex.Message, ex, null);
                }
            }
            logger.LogInformation("Scheduler.Initializing Tasks", $"{count} Tasks Initialized", null);
            if (failCount > 0)
            {
                logger.LogWarning("Scheduler.Initializing Tasks.Failed", $"{failCount} Tasks failed to Initialize", null);
            }
        }

        /// <summary>Gets task classes.</summary>
        /// <returns>The task classes.</returns>
        private static ICollection<Type> GetTaskClasses()
        {
            return TaskClassesLoadedFromPlugins()
                .Union(TaskClassesLoadedFromClients())
                .Distinct()
                .ToList();
        }

        /// <summary>Enumerates service handler classes loaded from plugins in this collection.</summary>
        /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from plugins
        /// in this collection.</returns>
        private static IEnumerable<Type> TaskClassesLoadedFromPlugins()
        {
            return ServiceHandlerClassesLoadedFromPath(CEFConfigDictionary.PluginsPath);
        }

        /// <summary>Enumerates service handler classes loaded from clients in this collection.</summary>
        /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from clients
        /// in this collection.</returns>
        private static IEnumerable<Type> TaskClassesLoadedFromClients()
        {
            return ServiceHandlerClassesLoadedFromPath(CEFConfigDictionary.ClientsPath);
        }

        /// <summary>Enumerates service handler classes loaded from path in this collection.</summary>
        /// <param name="originalPath">Full pathname of the original file.</param>
        /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from path in
        /// this collection.</returns>
        private static IEnumerable<Type> ServiceHandlerClassesLoadedFromPath(string originalPath)
        {
            var location = string.IsNullOrWhiteSpace(originalPath)
                ? @"{CEF_RootPath}\"
                : originalPath;
            if (location.Contains("{CEF_RootPath}"))
            {
                if (location.Contains(@"{CEF_RootPath}\") && Globals.CEFRootPath.EndsWith("\\"))
                {
                    location = location.Replace(@"{CEF_RootPath}\", Globals.CEFRootPath);
                }
                else
                {
                    location = location.Replace("{CEF_RootPath}", Globals.CEFRootPath);
                }
            }
            return Directory.Exists(location)
                ? TaskClassesLoadedFromAssemblies(Directory.GetFiles(location, "Clarity.*.dll").Select(Assembly.LoadFrom))
                : new List<Type>();
        }

        /// <summary>Enumerates service handler classes loaded from assemblies in this collection.</summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from
        /// assemblies in this collection.</returns>
        private static IEnumerable<Type> TaskClassesLoadedFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(x => x.GetTypes()
                    .Where(c => c.IsClass
                        && !c.IsInterface
                        && !c.IsAbstract
                        && c.AllInterfaces().Any(i => typeof(ITask).IsAssignableFrom(i))
                        && IsTaskEnabled(c.Name)))
                .Distinct();
        }

        /// <summary>Verifies that the task is enabled.</summary>
        /// <param name="taskName">The type name of a task.</param>
        /// <returns>Returns true if a task is enabled, else false.</returns>
        private static bool IsTaskEnabled(string taskName)
        {
            var loweredEnabledTasks = EnabledTasks.Select(t => t.ToLower()).ToList();
            return !EnabledTasks.Any()
                || loweredEnabledTasks.Contains(taskName.ToLower())
                || string.Equals(taskName, "IndexElasticSearchAllKindsTask", StringComparison.CurrentCultureIgnoreCase)
                    && (loweredEnabledTasks.Contains("IndexElasticSearchProductsTask".ToLower())
                        || loweredEnabledTasks.Contains("IndexElasticSearchStoresTask".ToLower()));
        }

        /// <summary>Gets time zone information.</summary>
        /// <param name="toParse">to parse.</param>
        /// <returns>The time zone information.</returns>
        private static TimeZoneInfo GetTimeZoneInfo(string toParse)
        {
            return toParse.ToLower() switch
            {
                "local" => TimeZoneInfo.Local,
                "utc" => TimeZoneInfo.Utc,
                _ => TimeZoneInfo.FindSystemTimeZoneById(toParse),
            };
        }
    }
}
