// <copyright file="LoggerExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the logger extensions class</summary>
namespace Microsoft.Owin.Logging
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>ILogger extension methods for common scenarios.</summary>
    public static class LoggerExtensions
    {
        /// <summary>Message describing the.</summary>
        private static readonly Func<object, Exception, string> TheMessage;

        /// <summary>the message and error.</summary>
        private static readonly Func<object, Exception, string> TheMessageAndError;

        /// <summary>Initializes static members of the Microsoft.Owin.Logging.LoggerExtensions class.</summary>
        static LoggerExtensions()
        {
            TheMessage = (message, error) => (string)message;
            TheMessageAndError = (message, error) => string.Format(
                CultureInfo.CurrentCulture,
                "{0}\r\n{1}",
                message,
                error);
        }

        /// <summary>Checks if the given TraceEventType is enabled.</summary>
        /// <param name="logger">   .</param>
        /// <param name="eventType">.</param>
        /// <returns>True if enabled, false if not.</returns>
        public static bool IsEnabled(this ILogger logger, TraceEventType eventType)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            return logger.WriteCore(eventType, 0, null, null, null);
        }

        /// <summary>Writes a critical log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        public static void WriteCritical(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Critical, 0, message, null, TheMessage);
        }

        /// <summary>Writes a critical log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        /// <param name="error">  .</param>
        public static void WriteCritical(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Critical, 0, message, error, TheMessageAndError);
        }

        /// <summary>Writes an error log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        public static void WriteError(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Error, 0, message, null, TheMessage);
        }

        /// <summary>Writes an error log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        /// <param name="error">  .</param>
        public static void WriteError(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Error, 0, message, error, TheMessageAndError);
        }

        /// <summary>Writes an informational log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        public static void WriteInformation(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Information, 0, message, null, TheMessage);
        }

        /// <summary>Writes a verbose log message.</summary>
        /// <param name="logger">.</param>
        /// <param name="data">  .</param>
        public static void WriteVerbose(this ILogger logger, string data)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Verbose, 0, data, null, TheMessage);
        }

        /// <summary>Writes a warning log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        /// <param name="args">   .</param>
        public static void WriteWarning(this ILogger logger, string message, params string[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            object[] objArray = args;
            logger.WriteCore(
                TraceEventType.Warning,
                0,
                string.Format(CultureInfo.InvariantCulture, message, objArray),
                null,
                TheMessage);
        }

        /// <summary>Writes a warning log message.</summary>
        /// <param name="logger"> .</param>
        /// <param name="message">.</param>
        /// <param name="error">  .</param>
        public static void WriteWarning(this ILogger logger, string message, Exception error)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            logger.WriteCore(TraceEventType.Warning, 0, message, error, TheMessageAndError);
        }
    }
}
