// <copyright file="SignInStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sign in status class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    /// <summary>Possible results from a sign in attempt.</summary>
    public enum SignInStatus
    {
        /// <summary>
        ///     Sign in was successful
        /// </summary>
        Success,

        /// <summary>
        ///     User is locked out
        /// </summary>
        LockedOut,

        /// <summary>
        ///     Sign in requires addition verification (i.e. two factor)
        /// </summary>
        RequiresVerification,

        /// <summary>
        ///     Sign in failed
        /// </summary>
        Failure,
    }
}
