// <copyright file="BaseContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base context class</summary>
namespace Microsoft.Owin.Security.Provider
{
    /// <summary>A base context.</summary>
    public abstract class BaseContext
    {
        /// <summary>Initializes a new instance of the <see cref="BaseContext" />
        /// class.</summary>
        /// <param name="context">The context.</param>
        protected BaseContext(IOwinContext context)
        {
            OwinContext = context;
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
