// <copyright file="IProxyProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProxyProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Proxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <inheritdoc/>
    public interface IProxyProviderBase : IProviderBase
    {
        /// <summary>Creates web proxy.</summary>
        /// <returns>The new web proxy.</returns>
        Task<CEFActionResponse<IWebProxy>> CreateWebProxyAsync();
    }
}
