// <copyright file="IDataProtectionProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDataProtectionProvider interface</summary>
namespace Microsoft.Owin.Security.DataProtection
{
    /// <summary>Factory used to create IDataProtection instances.</summary>
    public interface IDataProtectionProvider
    {
        /// <summary>Returns a new instance of IDataProtection for the provider.</summary>
        /// <param name="purposes">Additional entropy used to ensure protected data may only be unprotected for the
        ///                        correct purposes.</param>
        /// <returns>An instance of a data protection service.</returns>
        IDataProtector Create(params string[] purposes);
    }
}
