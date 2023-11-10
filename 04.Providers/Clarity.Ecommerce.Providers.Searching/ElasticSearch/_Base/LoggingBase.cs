// <copyright file="LoggingBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the logging base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal abstract class LoggingBase
    {
        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected virtual ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Log an informational note to the system log.</summary>
        /// <param name="body">              The body.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="callerMemberName">  Name of the caller member.</param>
        /// <returns>A Task.</returns>
        protected virtual Task Log(string body, string? contextProfileName, [CallerMemberName] string? callerMemberName = null)
        {
            return Logger.LogInformationAsync($"{GetType().Name}.{callerMemberName}", body, contextProfileName);
        }

        /// <summary>Logs an error to the system log.</summary>
        /// <param name="body">              The body.</param>
        /// <param name="ex">                The exception.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="callerMemberName">  Name of the caller member.</param>
        /// <returns>A Task.</returns>
        protected virtual Task Error(string body, Exception ex, string? contextProfileName, [CallerMemberName] string? callerMemberName = null)
        {
            return Logger.LogErrorAsync($"{GetType().Name}.{callerMemberName}", body, ex, contextProfileName);
        }
    }
}
