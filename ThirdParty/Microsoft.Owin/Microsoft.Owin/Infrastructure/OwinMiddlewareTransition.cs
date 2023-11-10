// <copyright file="OwinMiddlewareTransition.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the owin middleware transition class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Transitions between <typeref name="Func&lt;IDictionary&lt;string,object&gt;, Task&gt;" /> and
    /// OwinMiddleware.</summary>
    internal sealed class OwinMiddlewareTransition
    {
        /// <summary>The next.</summary>
        private readonly OwinMiddleware _next;

        /// <summary>Initializes a new instance of the
        /// <see cref="OwinMiddlewareTransition" /> class.</summary>
        /// <param name="next">.</param>
        public OwinMiddlewareTransition(OwinMiddleware next)
        {
            _next = next;
        }

        /// <summary>Executes the given operation on a different thread, and waits for the result.</summary>
        /// <param name="environment">OWIN environment dictionary which stores state information about the request,
        ///                           response and relevant server state.</param>
        /// <returns>A Task.</returns>
        public Task Invoke(IDictionary<string, object> environment)
        {
            return _next.Invoke(new OwinContext(environment));
        }
    }
}
