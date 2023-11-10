// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.Logging.AppBuilderLoggerExtensions
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>Logging extension methods for IAppBuilder.</summary>
    public static class AppBuilderLoggerExtensions
    {
        /// <summary>Creates a new ILogger instance from the server.LoggerFactory in the Properties collection.</summary>
        /// <param name="app"> .</param>
        /// <param name="name">.</param>
        /// <returns>The new logger.</returns>
        public static ILogger CreateLogger(this global::Owin.IAppBuilder app, string name)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return (app.GetLoggerFactory() ?? LoggerFactory.Default).Create(name);
        }

        /// <summary>Creates a new ILogger instance from the server.LoggerFactory in the Properties collection.</summary>
        /// <param name="app">      .</param>
        /// <param name="component">.</param>
        /// <returns>The new logger.</returns>
        public static ILogger CreateLogger(this global::Owin.IAppBuilder app, Type component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            return app.CreateLogger(component.FullName);
        }

        /// <summary>Creates a new ILogger instance from the server.LoggerFactory in the Properties collection.</summary>
        /// <typeparam name="TType">.</typeparam>
        /// <param name="app">.</param>
        /// <returns>The new logger.</returns>
        public static ILogger CreateLogger<TType>(this global::Owin.IAppBuilder app)
        {
            return app.CreateLogger(typeof(TType));
        }

        /// <summary>Retrieves the server.LoggerFactory from the Properties collection.</summary>
        /// <param name="app">.</param>
        /// <returns>The logger factory.</returns>
        public static ILoggerFactory GetLoggerFactory(this global::Owin.IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.Properties.TryGetValue("server.LoggerFactory", out var obj)
                && obj is Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> create
                    ? new WrapLoggerFactory(create)
                    : (ILoggerFactory)null;
        }

        /// <summary>Sets the server.LoggerFactory in the Properties collection.</summary>
        /// <param name="app">          .</param>
        /// <param name="loggerFactory">.</param>
        public static void SetLoggerFactory(this global::Owin.IAppBuilder app, ILoggerFactory loggerFactory)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.Properties["server.LoggerFactory"] =
                (Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>>)(name
                    => loggerFactory.Create(name).WriteCore);
        }

        /// <summary>A wrap logger factory.</summary>
        /// <seealso cref="ILoggerFactory"/>
        /// <seealso cref="ILoggerFactory"/>
        private class WrapLoggerFactory : ILoggerFactory
        {
            /// <summary>The create.</summary>
            private readonly
                Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> _create;

            /// <summary>Initializes a new instance of the
            /// <see cref="WrapLoggerFactory" /> class.</summary>
            /// <param name="create">The create.</param>
            public WrapLoggerFactory(
                Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> create)
            {
                _create = create ?? throw new ArgumentNullException(nameof(create));
            }

            /// <inheritdoc/>
            public ILogger Create(string name)
            {
                return new WrappingLogger(_create(name));
            }
        }

        /// <summary>A wrapping logger.</summary>
        /// <seealso cref="ILogger"/>
        /// <seealso cref="ILogger"/>
        private class WrappingLogger : ILogger
        {
            /// <summary>The write.</summary>
            private readonly Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> _write;

            /// <summary>Initializes a new instance of the
            /// <see cref="WrappingLogger" /> class.</summary>
            /// <param name="write">The write.</param>
            public WrappingLogger(
                Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> write)
            {
                _write = write ?? throw new ArgumentNullException(nameof(write));
            }

            /// <inheritdoc/>
            public bool WriteCore(
                TraceEventType eventType,
                int eventId,
                object state,
                Exception exception,
                Func<object, Exception, string> message)
            {
                return _write(eventType, eventId, state, exception, message);
            }
        }
    }
}
