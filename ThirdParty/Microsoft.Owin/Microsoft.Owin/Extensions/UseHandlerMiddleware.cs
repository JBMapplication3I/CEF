// <copyright file="UseHandlerMiddleware.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the use handler middleware class</summary>
namespace Microsoft.Owin.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Represents a middleware for executing in-line function middleware.</summary>
    public class UseHandlerMiddleware
    {
        /// <summary>The handler.</summary>
        private readonly Func<IOwinContext, Task> _handler;

        /// <summary>The next.</summary>
        private readonly Func<IDictionary<string, object>, Task> _next;

        /// <summary>Initializes a new instance of the <see cref="UseHandlerMiddleware" />
        /// class.</summary>
        /// <param name="next">   The pointer to next middleware.</param>
        /// <param name="handler">A function that handles all requests.</param>
        public UseHandlerMiddleware(Func<IDictionary<string, object>, Task> next, Func<IOwinContext, Task> handler)
        {
            _next = next;
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>Initializes a new instance of the <see cref="UseHandlerMiddleware" />
        /// class.</summary>
        /// <param name="next">   The pointer to next middleware.</param>
        /// <param name="handler">A function that handles the request or calls the given next function.</param>
        public UseHandlerMiddleware(
            Func<IDictionary<string, object>, Task> next,
            Func<IOwinContext, Func<Task>, Task> handler)
        {
            var useHandlerMiddleware = this;
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }
            _next = next;
            _handler = context => handler(context, () => useHandlerMiddleware._next(context.Environment));
        }

        /// <summary>Invokes the handler for processing the request.</summary>
        /// <param name="environment">The OWIN context.</param>
        /// <returns>The <see cref="Task" /> object that represents the request operation.</returns>
        public Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext owinContext = new OwinContext(environment);
            return _handler(owinContext);
        }
    }
}
