// <copyright file="BaseContext`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base context` 1 class</summary>
namespace Microsoft.Owin.Security.Provider
{
    /// <summary>Base class used for certain event contexts.</summary>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    public abstract class BaseContext<TOptions>
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Provider.BaseContext{TOptions}"/} class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        protected BaseContext(IOwinContext context, TOptions options)
        {
            OwinContext = context;
            Options = options;
        }

        /// <summary>Gets options for controlling the operation.</summary>
        /// <value>The options.</value>
        public TOptions Options
        {
            get;
        }

        /// <summary>Gets a context for the owin.</summary>
        /// <value>The owin context.</value>
        public IOwinContext OwinContext
        {
            get;
        }

        /// <summary>Gets the request.</summary>
        /// <value>The request.</value>
        public IOwinRequest Request => OwinContext.Request;

        /// <summary>Gets the response.</summary>
        /// <value>The response.</value>
        public IOwinResponse Response => OwinContext.Response;
    }
}
