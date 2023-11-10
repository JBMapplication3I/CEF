// <copyright file="ILogger.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILogger interface</summary>
namespace Microsoft.Owin.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>A generic interface for logging.</summary>
    public interface ILogger
    {
        /// <summary>Aggregates most logging patterns to a single method.  This must be compatible with the Func
        /// representation in the OWIN environment. To check IsEnabled call WriteCore with only TraceEventType and check
        /// the return value, no event will be written.</summary>
        /// <param name="eventType">.</param>
        /// <param name="eventId">  .</param>
        /// <param name="state">    .</param>
        /// <param name="exception">.</param>
        /// <param name="formatter">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        bool WriteCore(
            TraceEventType eventType,
            int eventId,
            object state,
            Exception exception,
            Func<object, Exception, string> formatter);
    }
}
