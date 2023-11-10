// <copyright file="IdentityFactoryOptions`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the identity factory options` 1 class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using Microsoft.Owin.Security.DataProtection;

    /// <summary>Configuration options for a IdentityFactoryMiddleware.</summary>
    /// <typeparam name="T">.</typeparam>
    public class IdentityFactoryOptions<T>
        where T : IDisposable
    {
        /// <summary>Used to configure the data protection provider.</summary>
        /// <value>The data protection provider.</value>
        public IDataProtectionProvider DataProtectionProvider
        {
            get;
            set;
        }

        /// <summary>Provider used to Create and Dispose objects.</summary>
        /// <value>The provider.</value>
        public IIdentityFactoryProvider<T> Provider
        {
            get;
            set;
        }
    }
}
