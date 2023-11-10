// <copyright file="AuthenticationHandler`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication handler` 1 class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System.Threading.Tasks;

    /// <summary>Base class for the per-request work performed by most authentication middleware.</summary>
    /// <typeparam name="TOptions">Specifies which type for of AuthenticationOptions property.</typeparam>
    /// <seealso cref="AuthenticationHandler"/>
    /// <seealso cref="AuthenticationHandler"/>
    public abstract class AuthenticationHandler<TOptions> : AuthenticationHandler
        where TOptions : AuthenticationOptions
    {
        /// <summary>Gets options for controlling the operation.</summary>
        /// <value>The options.</value>
        protected TOptions Options
        {
            get;
            private set;
        }

        /// <summary>Initialize is called once per request to contextualize this instance with appropriate state.</summary>
        /// <param name="options">The original options passed by the application control behavior.</param>
        /// <param name="context">The utility object to observe the current request and response.</param>
        /// <returns>async completion.</returns>
        internal Task Initialize(TOptions options, IOwinContext context)
        {
            Options = options;
            return BaseInitializeAsync(options, context);
        }
    }
}
