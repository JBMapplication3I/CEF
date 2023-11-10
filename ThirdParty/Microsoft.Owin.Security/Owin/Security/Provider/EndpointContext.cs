// <copyright file="EndpointContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the endpoint context class</summary>
namespace Microsoft.Owin.Security.Provider
{
    /// <summary>An endpoint context.</summary>
    /// <seealso cref="BaseContext"/>
    /// <seealso cref="BaseContext"/>
    public abstract class EndpointContext : BaseContext
    {
        /// <summary>Initializes a new instance of the <see cref="EndpointContext" />
        /// class.</summary>
        /// <param name="context">The context.</param>
        protected EndpointContext(IOwinContext context) : base(context) { }

        /// <summary>Gets a value indicating whether this EndpointContext is request completed.</summary>
        /// <value>True if this EndpointContext is request completed, false if not.</value>
        public bool IsRequestCompleted
        {
            get;
            private set;
        }

        /// <summary>Request completed.</summary>
        public void RequestCompleted()
        {
            IsRequestCompleted = true;
        }
    }
}
