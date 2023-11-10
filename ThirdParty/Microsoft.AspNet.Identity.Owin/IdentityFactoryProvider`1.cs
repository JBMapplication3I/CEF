// <copyright file="IdentityFactoryProvider`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the identity factory provider` 1 class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using Microsoft.Owin;

    /// <summary>Used to configure how the IdentityFactoryMiddleware will create an instance of the specified type
    /// for each OwinContext.</summary>
    /// <typeparam name="T">.</typeparam>
    /// <seealso cref="IIdentityFactoryProvider{T}"/>
    public class IdentityFactoryProvider<T> : IIdentityFactoryProvider<T>
        where T : class, IDisposable
    {
        /// <summary>Constructor.</summary>
        public IdentityFactoryProvider()
        {
            OnDispose = (options, instance) => { };
            OnCreate = (options, context) => default;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on create.</value>
        public Func<IdentityFactoryOptions<T>, IOwinContext, T> OnCreate
        {
            get;
            set;
        }

        /// <summary>A delegate assigned to this property will be invoked when the related method is called.</summary>
        /// <value>The on dispose.</value>
        public Action<IdentityFactoryOptions<T>, T> OnDispose
        {
            get;
            set;
        }

        /// <summary>Calls the OnCreate Delegate.</summary>
        /// <param name="options">.</param>
        /// <param name="context">.</param>
        /// <returns>A T.</returns>
        public virtual T Create(IdentityFactoryOptions<T> options, IOwinContext context)
        {
            return OnCreate(options, context);
        }

        /// <summary>Calls the OnDispose delegate.</summary>
        /// <param name="options"> .</param>
        /// <param name="instance">.</param>
        public virtual void Dispose(IdentityFactoryOptions<T> options, T instance)
        {
            OnDispose(options, instance);
        }
    }
}
