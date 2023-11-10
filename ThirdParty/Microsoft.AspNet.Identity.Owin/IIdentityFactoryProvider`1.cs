// <copyright file="IIdentityFactoryProvider`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IIdentityFactoryProvider`1 interface</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using Microsoft.Owin;

    /// <summary>Interface used to create objects per request.</summary>
    /// <typeparam name="T">.</typeparam>
    public interface IIdentityFactoryProvider<T>
        where T : IDisposable
    {
        /// <summary>Called once per request to create an object.</summary>
        /// <param name="options">.</param>
        /// <param name="context">.</param>
        /// <returns>A T.</returns>
        T Create(IdentityFactoryOptions<T> options, IOwinContext context);

        /// <summary>Called at the end of the request to dispose the object created.</summary>
        /// <param name="options"> .</param>
        /// <param name="instance">.</param>
        void Dispose(IdentityFactoryOptions<T> options, T instance);
    }
}
