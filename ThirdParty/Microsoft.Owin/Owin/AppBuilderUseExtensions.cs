// <copyright file="AppBuilderUseExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application builder use extensions class</summary>
namespace Owin
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;

    /// <summary>Extension methods for <see cref="IAppBuilder" />.</summary>
    public static class AppBuilderUseExtensions
    {
        /// <summary>Inserts into the OWIN pipeline a middleware which does not have a next middleware reference.</summary>
        /// <param name="app">    .</param>
        /// <param name="handler">An app that handles all requests.</param>
        public static void Run(this IAppBuilder app, Func<IOwinContext, Task> handler)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            app.Use<UseHandlerMiddleware>(handler);
        }

        /// <summary>Inserts a middleware into the OWIN pipeline.</summary>
        /// <typeparam name="T">The middleware type.</typeparam>
        /// <param name="app"> .</param>
        /// <param name="args">Any additional arguments for the middleware constructor.</param>
        /// <returns>An IAppBuilder.</returns>
        public static IAppBuilder Use<T>(this IAppBuilder app, params object[] args)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.Use(typeof(T), args);
        }

        /// <summary>Inserts a middleware into the OWIN pipeline.</summary>
        /// <param name="app">    .</param>
        /// <param name="handler">An app that handles the request or calls the given next Func.</param>
        /// <returns>An IAppBuilder.</returns>
        public static IAppBuilder Use(this IAppBuilder app, Func<IOwinContext, Func<Task>, Task> handler)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            return app.Use<UseHandlerMiddleware>(handler);
        }
    }
}
