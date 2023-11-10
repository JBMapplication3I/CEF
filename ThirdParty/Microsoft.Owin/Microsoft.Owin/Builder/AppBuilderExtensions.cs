// <copyright file="AppBuilderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application builder extensions class</summary>
namespace Microsoft.Owin.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Extension methods for IAppBuilder.</summary>
    public static class AppBuilderExtensions
    {
        /// <summary>Adds converters for adapting between disparate application signatures.</summary>
        /// <param name="builder">   .</param>
        /// <param name="conversion">.</param>
        public static void AddSignatureConversion(this global::Owin.IAppBuilder builder, Delegate conversion)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (builder.Properties.TryGetValue("builder.AddSignatureConversion", out var obj))
            {
                if (obj is Action<Delegate> action)
                {
                    action(conversion);
                    return;
                }
            }
            throw new MissingMethodException(builder.GetType().FullName, "AddSignatureConversion");
        }

        /// <summary>Adds converters for adapting between disparate application signatures.</summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="builder">   .</param>
        /// <param name="conversion">.</param>
        public static void AddSignatureConversion<T1, T2>(this global::Owin.IAppBuilder builder, Func<T1, T2> conversion)
        {
            builder.AddSignatureConversion(conversion);
        }

        /// <summary>The Build is called at the point when all of the middleware should be chained together. May be
        /// called to build pipeline branches.</summary>
        /// <param name="builder">.</param>
        /// <returns>The request processing entry point for this section of the pipeline.</returns>
        public static Func<IDictionary<string, object>, Task> Build(this global::Owin.IAppBuilder builder)
        {
            return builder.Build<Func<IDictionary<string, object>, Task>>();
        }

        /// <summary>The Build is called at the point when all of the middleware should be chained together. May be
        /// called to build pipeline branches.</summary>
        /// <typeparam name="TApp">The application signature.</typeparam>
        /// <param name="builder">.</param>
        /// <returns>The request processing entry point for this section of the pipeline.</returns>
        public static TApp Build<TApp>(this global::Owin.IAppBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return (TApp)builder.Build(typeof(TApp));
        }
    }
}
