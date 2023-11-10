// <copyright file="EndpointContext`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the endpoint context` 1 class</summary>
namespace Microsoft.Owin.Security.Provider
{
    /// <summary>Base class used for certain event contexts.</summary>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseContext{TOptions}"/>
    /// <seealso cref="BaseContext{TOptions}"/>
    public abstract class EndpointContext<TOptions> : BaseContext<TOptions>
    {
        /// <summary>Creates an instance of this context.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        protected EndpointContext(IOwinContext context, TOptions options) : base(context, options) { }

        /// <summary>True if the request should not be processed further by other components.</summary>
        /// <value>True if this EndpointContext{TOptions} is request completed, false if not.</value>
        public bool IsRequestCompleted
        {
            get;
            private set;
        }

        /// <summary>Prevents the request from being processed further by other components. IsRequestCompleted becomes
        /// true after calling.</summary>
        public void RequestCompleted()
        {
            IsRequestCompleted = true;
        }
    }
}
