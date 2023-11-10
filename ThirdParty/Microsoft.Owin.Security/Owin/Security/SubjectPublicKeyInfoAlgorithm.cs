// <copyright file="SubjectPublicKeyInfoAlgorithm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subject public key information algorithm class</summary>
namespace Microsoft.Owin.Security
{
    /// <summary>The algorithm used to generate the subject public key information blob hashes.</summary>
    public enum SubjectPublicKeyInfoAlgorithm
    {
        /// <summary>An enum constant representing the sha 1 option.</summary>
        Sha1,

        /// <summary>An enum constant representing the sha 256 option.</summary>
        Sha256,
    }
}
