// <copyright file="ILogger.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILogger interface</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface for logger.</summary>
    public interface ILogger
    {
        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, string? contextProfileName);

        /// <summary>Logs error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogErrorAsync(string name, string? message, string? contextProfileName);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        true to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, bool forceEmail, string? contextProfileName);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="data">              The data.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, string data, string? contextProfileName);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, Exception ex, string? contextProfileName);

        /// <summary>Logs error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogErrorAsync(string name, string? message, Exception ex, string? contextProfileName);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, Exception ex, string? data, string? contextProfileName);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        true to force email.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, bool forceEmail, Exception ex, string data, string? contextProfileName);

        /// <summary>Logs error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        True to force email.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogErrorAsync(string name, string message, bool forceEmail, Exception? ex, string data, string? contextProfileName);

        /// <summary>Logs an information.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogInformation(string name, string message, string? contextProfileName);

        /// <summary>Logs information.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogInformationAsync(string name, string message, string? contextProfileName);

        /// <summary>Logs an information.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        true to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogInformation(string name, string message, bool forceEmail, string? contextProfileName);

        /// <summary>Logs information.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        True to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogInformationAsync(string name, string message, bool forceEmail, string? contextProfileName);

        /// <summary>Logs a warning.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogWarning(string name, string message, string? contextProfileName);

        /// <summary>Logs a warning.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogWarningAsync(string name, string message, string? contextProfileName);

        /// <summary>Logs a warning.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        true to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogWarning(string name, string message, bool forceEmail, string? contextProfileName);

        /// <summary>Logs a warning.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="forceEmail">        true to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogWarningAsync(string name, string message, bool forceEmail, string? contextProfileName);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="data">              The data.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="args">              A variable-length parameters list containing arguments.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, string data, string? contextProfileName, params object[] args);

        /// <summary>Logs an error.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="args">              A variable-length parameters list containing arguments.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogError(string name, string message, Exception ex, string data, string? contextProfileName, params object[] args);

        /// <summary>Logs an information.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="args">              A variable-length parameters list containing arguments.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogInformation(string name, string message, string? contextProfileName, params object[] args);

        /// <summary>Logs a warning.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="args">              A variable-length parameters list containing arguments.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Guid LogWarning(string name, string message, string? contextProfileName, params object[] args);

        /// <summary>Logs a warning.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="args">              A variable-length parameters list containing arguments.</param>
        /// <returns>The Guid Log Identifier for the database entry, or default(Guid) if not logging to database.</returns>
        Task<Guid> LogWarningAsync(string name, string message, string? contextProfileName, params object[] args);
    }
}
