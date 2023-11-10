// <copyright file="IDataProtector.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDataProtector interface</summary>
namespace Microsoft.Owin.Security.DataProtection
{
    /// <summary>Service used to protect and unprotect data.</summary>
    public interface IDataProtector
    {
        /// <summary>Called to protect user data.</summary>
        /// <param name="userData">The original data that must be protected.</param>
        /// <returns>A different byte array that may be unprotected or altered only by software that has access to the an
        /// identical IDataProtection service.</returns>
        byte[] Protect(byte[] userData);

        /// <summary>Called to unprotect user data.</summary>
        /// <param name="protectedData">The byte array returned by a call to Protect on an identical IDataProtection
        ///                             service.</param>
        /// <returns>The byte array identical to the original userData passed to Protect.</returns>
        byte[] Unprotect(byte[] protectedData);
    }
}
