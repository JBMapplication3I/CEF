// <copyright file="DiagnosticsLogger.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the diagnostics logger class</summary>
namespace Microsoft.Owin.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>The diagnostics logger.</summary>
    /// <seealso cref="ILogger"/>
    /// <seealso cref="ILogger"/>
    internal class DiagnosticsLogger : ILogger
    {
        /// <summary>The trace source.</summary>
        private readonly TraceSource _traceSource;

        /// <summary>Initializes a new instance of the <see cref="DiagnosticsLogger" /> class.</summary>
        /// <param name="traceSource">The trace source.</param>
        public DiagnosticsLogger(TraceSource traceSource)
        {
            _traceSource = traceSource;
        }

        /// <inheritdoc/>
        public bool WriteCore(
            TraceEventType eventType,
            int eventId,
            object state,
            Exception exception,
            Func<object, Exception, string> formatter)
        {
            if (!_traceSource.Switch.ShouldTrace(eventType))
            {
                return false;
            }
            if (formatter != null)
            {
                _traceSource.TraceEvent(eventType, eventId, formatter(state, exception));
            }
            return true;
        }
    }
}
