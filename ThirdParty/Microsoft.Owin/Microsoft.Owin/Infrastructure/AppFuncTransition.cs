// <copyright file="AppFuncTransition.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application function transition class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Converts between an OwinMiddlware and an
    /// <typeref name="Func&lt;IDictionary&lt;string,object&gt;, Task&gt;" />.</summary>
    /// <seealso cref="OwinMiddleware"/>
    /// <seealso cref="OwinMiddleware"/>
    internal sealed class AppFuncTransition : OwinMiddleware
    {
        /// <summary>The next.</summary>
        private readonly Func<IDictionary<string, object>, Task> _next;

        /// <summary>Initializes a new instance of the <see cref="AppFuncTransition" />
        /// class.</summary>
        /// <param name="next">.</param>
        public AppFuncTransition(Func<IDictionary<string, object>, Task> next) : base(null)
        {
            _next = next;
        }

        /// <summary>Executes the given operation on a different thread, and waits for the result.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public override Task Invoke(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return _next(context.Environment);
        }
    }
}
