// <copyright file="ICEFAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICEFAuthProvider interface</summary>
namespace ServiceStack.Auth
{
    using System.Collections.Generic;
#if NET5_0_OR_GREATER
    using System.Threading;
    using System.Threading.Tasks;
#endif
    using Web;

    /// <summary>Interface for CEF authentication provider.</summary>
    /// <seealso cref="IAuthProvider"/>
    public interface ICEFAuthProvider : IAuthProvider
    {
#if NET5_0_OR_GREATER
        /// <summary>Executes the authenticated action.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="session">    The session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <param name="authInfo">   Information describing the authentication.</param>
        /// <param name="token">      The token.</param>
        /// <returns>An IHttpResult.</returns>
        Task<IHttpResult> OnAuthenticatedAsync(
            IServiceBase authService,
            IAuthSession session,
            IAuthTokens tokens,
            Dictionary<string, string> authInfo,
            CancellationToken token = default);

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="userName">   Name of the user.</param>
        /// <param name="password">   The password.</param>
        /// <param name="token">      The token.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        Task<bool> TryAuthenticateAsync(
            IServiceBase authService,
            string userName,
            string password,
            CancellationToken token = default);
#else
        /// <summary>Executes the authenticated action.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="session">    The session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <param name="authInfo">   Information describing the authentication.</param>
        /// <returns>An IHttpResult.</returns>
        IHttpResult? OnAuthenticated(
            IServiceBase authService,
            IAuthSession session,
            IAuthTokens? tokens,
            Dictionary<string, string>? authInfo);

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="userName">   Name of the user.</param>
        /// <param name="password">   The password.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        bool TryAuthenticate(
            IServiceBase authService,
            string userName,
            string password);
#endif
    }
}
