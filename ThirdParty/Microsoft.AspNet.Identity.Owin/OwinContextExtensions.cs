// <copyright file="OwinContextExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the owin context extensions class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using Microsoft.Owin;

    /// <summary>Extension methods for OwinContext/&gt;</summary>
    public static class OwinContextExtensions
    {
        /// <summary>The identity key prefix.</summary>
        private static readonly string IdentityKeyPrefix;

        /// <summary>Initializes static members of the Microsoft.AspNet.Identity.Owin.OwinContextExtensions class.</summary>
        static OwinContextExtensions()
        {
            IdentityKeyPrefix = "AspNet.Identity.Owin:";
        }

        /// <summary>Retrieves an object from the OwinContext using a key based on the AssemblyQualified type name.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="context">.</param>
        /// <returns>A T.</returns>
        public static T Get<T>(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Get<T>(GetKey(typeof(T)));
        }

        /// <summary>Get the user manager from the context.</summary>
        /// <typeparam name="TManager">.</typeparam>
        /// <param name="context">.</param>
        /// <returns>The user manager.</returns>
        public static TManager GetUserManager<TManager>(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Get<TManager>();
        }

        /// <summary>Stores an object in the OwinContext using a key based on the AssemblyQualified type name.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="context">.</param>
        /// <param name="value">  .</param>
        /// <returns>An IOwinContext.</returns>
        public static IOwinContext Set<T>(this IOwinContext context, T value)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Set(GetKey(typeof(T)), value);
        }

        /// <summary>Gets a key.</summary>
        /// <param name="t">The Type to process.</param>
        /// <returns>The key.</returns>
        private static string GetKey(Type t)
        {
            return string.Concat(IdentityKeyPrefix, t.AssemblyQualifiedName);
        }
    }
}
