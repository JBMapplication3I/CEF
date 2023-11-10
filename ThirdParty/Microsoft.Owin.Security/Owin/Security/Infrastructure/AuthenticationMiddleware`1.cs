// <copyright file="AuthenticationMiddleware`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication middleware` 1 class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    /// <summary>An authentication middleware.</summary>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="OwinMiddleware"/>
    /// <seealso cref="OwinMiddleware"/>
    public abstract class AuthenticationMiddleware<TOptions> : OwinMiddleware
        where TOptions : AuthenticationOptions
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Infrastructure.AuthenticationMiddleware{TOptions}"/} class.</summary>
        /// <param name="next">   The next.</param>
        /// <param name="options">Options for controlling the operation.</param>
        protected AuthenticationMiddleware(OwinMiddleware next, TOptions options) : base(next)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>Gets or sets options for controlling the operation.</summary>
        /// <value>The options.</value>
        public TOptions Options
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override async Task Invoke(IOwinContext context)
        {
            var authenticationHandler = CreateHandler();
            await authenticationHandler.Initialize(Options, context);
            if (!await authenticationHandler.InvokeAsync())
            {
                await Next.Invoke(context);
            }
            await authenticationHandler.TeardownAsync();
        }

        /// <summary>Handler, called when the create.</summary>
        /// <returns>The new handler.</returns>
        protected abstract AuthenticationHandler<TOptions> CreateHandler();
    }
}
