// <copyright file="DiagnosticsLoggerFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the diagnostics logger factory class</summary>
namespace Microsoft.Owin.Logging
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;

    /// <summary>Provides an ILoggerFactory based on System.Diagnostics.TraceSource.</summary>
    /// <seealso cref="Microsoft.Owin.Logging.ILoggerFactory"/>
    /// <seealso cref="ILoggerFactory"/>
    public class DiagnosticsLoggerFactory : ILoggerFactory
    {
        /// <summary>Name of the root trace.</summary>
        private const string RootTraceName = "Microsoft.Owin";

        /// <summary>The root source switch.</summary>
        private readonly SourceSwitch _rootSourceSwitch;

        /// <summary>The root trace listener.</summary>
        private readonly TraceListener _rootTraceListener;

        /// <summary>The sources.</summary>
        private readonly ConcurrentDictionary<string, TraceSource> _sources =
            new(StringComparer.OrdinalIgnoreCase);

        /// <summary>Initializes a new instance of the <see cref="Microsoft.Owin.Logging.DiagnosticsLoggerFactory" />
        ///          class.</summary>
        /// <summary>Creates a factory named "Microsoft.Owin".</summary>
        public DiagnosticsLoggerFactory()
        {
            _rootSourceSwitch = new SourceSwitch("Microsoft.Owin");
            _rootTraceListener = null;
        }

        /// <summary>Initializes a new instance of the <see cref="Microsoft.Owin.Logging.DiagnosticsLoggerFactory" />
        /// class.</summary>
        /// <param name="rootSourceSwitch"> .</param>
        /// <param name="rootTraceListener">.</param>
        public DiagnosticsLoggerFactory(SourceSwitch rootSourceSwitch, TraceListener rootTraceListener)
        {
            _rootSourceSwitch = rootSourceSwitch ?? new SourceSwitch("Microsoft.Owin");
            _rootTraceListener = rootTraceListener;
        }

        /// <summary>Creates a new DiagnosticsLogger for the given component name.</summary>
        /// <param name="name">.</param>
        /// <returns>An ILogger.</returns>
        public ILogger Create(string name)
        {
            return new DiagnosticsLogger(GetOrAddTraceSource(name));
        }

        /// <summary>Query if 'traceSource' has default listeners.</summary>
        /// <param name="traceSource">The trace source.</param>
        /// <returns>True if default listeners, false if not.</returns>
        private static bool HasDefaultListeners(TraceSource traceSource)
        {
            if (traceSource.Listeners.Count != 1)
            {
                return false;
            }
            return traceSource.Listeners[0] is DefaultTraceListener;
        }

        /// <summary>Query if 'traceSource' has default switch.</summary>
        /// <param name="traceSource">The trace source.</param>
        /// <returns>True if default switch, false if not.</returns>
        private static bool HasDefaultSwitch(TraceSource traceSource)
        {
            if (string.IsNullOrEmpty(traceSource.Switch.DisplayName) != string.IsNullOrEmpty(traceSource.Name))
            {
                return false;
            }
            return traceSource.Switch.Level == SourceLevels.Off;
        }

        /// <summary>Parent source name.</summary>
        /// <param name="traceSourceName">Name of the trace source.</param>
        /// <returns>A string.</returns>
        private static string ParentSourceName(string traceSourceName)
        {
            var num = traceSourceName.LastIndexOf('.');
            if (num == -1)
            {
                return "Microsoft.Owin";
            }
            return traceSourceName.Substring(0, num);
        }

        /// <summary>Gets or add trace source.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The or add trace source.</returns>
        private TraceSource GetOrAddTraceSource(string name)
        {
            return _sources.GetOrAdd(name, InitializeTraceSource);
        }

        /// <summary>Initializes the trace source.</summary>
        /// <param name="traceSourceName">Name of the trace source.</param>
        /// <returns>A TraceSource.</returns>
        private TraceSource InitializeTraceSource(string traceSourceName)
        {
            var traceSource = new TraceSource(traceSourceName);
            if (traceSourceName != "Microsoft.Owin")
            {
                var str = ParentSourceName(traceSourceName);
                if (HasDefaultListeners(traceSource))
                {
                    var orAddTraceSource = GetOrAddTraceSource(str);
                    traceSource.Listeners.Clear();
                    traceSource.Listeners.AddRange(orAddTraceSource.Listeners);
                }
                if (HasDefaultSwitch(traceSource))
                {
                    traceSource.Switch = GetOrAddTraceSource(str).Switch;
                }
            }
            else
            {
                if (HasDefaultSwitch(traceSource))
                {
                    traceSource.Switch = _rootSourceSwitch;
                }
                if (_rootTraceListener != null)
                {
                    traceSource.Listeners.Add(_rootTraceListener);
                }
            }
            return traceSource;
        }
    }
}
