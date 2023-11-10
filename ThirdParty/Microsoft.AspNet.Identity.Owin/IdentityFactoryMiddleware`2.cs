// <copyright file="IdentityFactoryMiddleware`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the identity factory middleware` 2 class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Owin;

    /// <summary>OwinMiddleware that initializes an object for use in the OwinContext via the Get/Set generic
    /// extensions method.</summary>
    /// <typeparam name="TResult"> .</typeparam>
    /// <typeparam name="TOptions">.</typeparam>
    /// <seealso cref="OwinMiddleware"/>
    public class IdentityFactoryMiddleware<TResult, TOptions> : OwinMiddleware
        where TResult : IDisposable
        where TOptions : IdentityFactoryOptions<TResult>
    {
        /// <summary>Constructor.</summary>
        /// <param name="next">   The next middleware in the OWIN pipeline to invoke.</param>
        /// <param name="options">Configuration options for the middleware.</param>
        public IdentityFactoryMiddleware(OwinMiddleware next, TOptions options) : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.Provider == null)
            {
                throw new ArgumentNullException("options.Provider");
            }
            Options = options;
        }

        /// <summary>Configuration options.</summary>
        /// <value>The options.</value>
        public TOptions Options { get; }

        /// <summary>Create an object using the Options.Provider, storing it in the OwinContext and then disposes the
        /// object when finished.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public override async Task Invoke(IOwinContext context)
        {
            var factoryMiddleware = this;
            var instance = factoryMiddleware.Options.Provider.Create(factoryMiddleware.Options, context);
            try
            {
                context.Set(instance);
                if (factoryMiddleware.Next == null)
                {
                    return;
                }
                await factoryMiddleware.Next.Invoke(context);
            }
            finally
            {
                factoryMiddleware.Options.Provider.Dispose(factoryMiddleware.Options, instance);
            }
        }
    }
}
