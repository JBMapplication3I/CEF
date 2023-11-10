// <copyright file="ISecureDataFormat`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISecureDataFormat`1 interface</summary>
namespace Microsoft.Owin.Security
{
    /// <summary>Interface for secure data format.</summary>
    /// <typeparam name="TData">Type of the data.</typeparam>
    public interface ISecureDataFormat<TData>
    {
        /// <summary>Protects the given data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>A string.</returns>
        string Protect(TData data);

        /// <summary>Unprotects.</summary>
        /// <param name="protectedText">The protected text.</param>
        /// <returns>A TData.</returns>
        TData Unprotect(string protectedText);
    }
}
