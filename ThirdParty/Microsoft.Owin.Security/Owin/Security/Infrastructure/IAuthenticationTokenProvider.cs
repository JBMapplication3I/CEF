// <copyright file="IAuthenticationTokenProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuthenticationTokenProvider interface</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System.Threading.Tasks;

    /// <summary>Interface for authentication token provider.</summary>
    public interface IAuthenticationTokenProvider
    {
        /// <summary>Creates this IAuthenticationTokenProvider.</summary>
        /// <param name="context">The context.</param>
        void Create(AuthenticationTokenCreateContext context);

        /// <summary>Creates an asynchronous.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The new asynchronous.</returns>
        Task CreateAsync(AuthenticationTokenCreateContext context);

        /// <summary>Receives the given context.</summary>
        /// <param name="context">The context.</param>
        void Receive(AuthenticationTokenReceiveContext context);

        /// <summary>Receive asynchronous.</summary>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        Task ReceiveAsync(AuthenticationTokenReceiveContext context);
    }
}
