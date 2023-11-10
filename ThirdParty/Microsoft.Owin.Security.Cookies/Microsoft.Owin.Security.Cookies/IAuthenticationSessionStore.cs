// <copyright file="IAuthenticationSessionStore.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuthenticationSessionStore interface</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System.Threading.Tasks;

    /// <summary>Interface for authentication session store.</summary>
    public interface IAuthenticationSessionStore
    {
        /// <summary>Removes the asynchronous described by key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>A Task.</returns>
        Task RemoveAsync(string key);

        /// <summary>Renew asynchronous.</summary>
        /// <param name="key">   The key.</param>
        /// <param name="ticket">The ticket.</param>
        /// <returns>A Task.</returns>
        Task RenewAsync(string key, AuthenticationTicket ticket);

        /// <summary>Retrieves an asynchronous.</summary>
        /// <param name="key">The key.</param>
        /// <returns>A Task{AuthenticationTicket}</returns>
        Task<AuthenticationTicket> RetrieveAsync(string key);

        /// <summary>Stores an asynchronous.</summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>A Task{string}</returns>
        Task<string> StoreAsync(AuthenticationTicket ticket);
    }
}
