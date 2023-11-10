// <copyright file="LoggerFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the logger factory class</summary>
namespace Microsoft.Owin.Logging
{
    /// <summary>Provides a default ILoggerFactory.</summary>
    public static class LoggerFactory
    {
        /// <summary>Initializes static members of the Microsoft.Owin.Logging.LoggerFactory class.</summary>
        static LoggerFactory()
        {
            Default = new DiagnosticsLoggerFactory();
        }

        /// <summary>Provides a default ILoggerFactory based on System.Diagnostics.TraceSorce.</summary>
        /// <value>The default.</value>
        public static ILoggerFactory Default
        {
            get;
            set;
        }
    }
}
