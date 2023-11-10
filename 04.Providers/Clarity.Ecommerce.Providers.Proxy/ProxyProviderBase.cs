// <copyright file="ProxyProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Proxy provider base class</summary>
namespace Clarity.Ecommerce.Providers.Proxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Interfaces.Providers.Proxy;
    using Models;

    /// <summary>A Proxy provider.</summary>
    /// <seealso cref="ProviderBase"/>
    public abstract class ProxyProviderBase : ProviderBase, IProxyProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Proxy;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<IWebProxy>> CreateWebProxyAsync();
    }
}
