// <copyright file="SignatureConversions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the signature conversions class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Builder;

    /// <summary>Adds adapters between <typeref name="Func{IDictionary{string,object}, Task}" /> and
    /// OwinMiddleware.</summary>
    public static class SignatureConversions
    {
        /// <summary>Adds adapters between <typeref name="Func{IDictionary{string,object}, Task}" /> and
        /// OwinMiddleware.</summary>
        /// <param name="app">.</param>
        public static void AddConversions(global::Owin.IAppBuilder app)
        {
            app.AddSignatureConversion(new Func<Func<IDictionary<string, object>, Task>, OwinMiddleware>(Conversion1));
            app.AddSignatureConversion(new Func<OwinMiddleware, Func<IDictionary<string, object>, Task>>(Conversion2));
        }

        /// <summary>Conversion 1.</summary>
        /// <param name="next">The next.</param>
        /// <returns>An OwinMiddleware.</returns>
        private static OwinMiddleware Conversion1(Func<IDictionary<string, object>, Task> next)
        {
            return new AppFuncTransition(next);
        }

        /// <summary>Conversion 2.</summary>
        /// <param name="next">The next.</param>
        /// <returns>A Func{IDictionary{string,object},Task}</returns>
        private static Func<IDictionary<string, object>, Task> Conversion2(OwinMiddleware next)
        {
            return new OwinMiddlewareTransition(next).Invoke;
        }
    }
}
