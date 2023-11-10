// <copyright file="ICEFUserAuthRepository.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICEFUserAuthRepository interface</summary>
namespace ServiceStack.Auth
{
    /// <summary>Interface for CEF user authentication repository.</summary>
    /// <seealso cref="IUserAuthRepository"/>
    public interface ICEFUserAuthRepository : IUserAuthRepository
    {
        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="userId">  Identifier for the user.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        bool TryAuthenticate(
            string userName,
            string password,
            out string? userId);
    }
}
