// <copyright file="NoProxyProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the No Proxy provider class</summary>
namespace Clarity.Ecommerce.Providers.Proxy.NoProxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Models;
    using Proxy;

    /// <inheritdoc/>
    public class NoProxyProvider : ProxyProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => NoProxyProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<IWebProxy>> CreateWebProxyAsync()
        {
            IWebProxy proxyObject = new WebProxy();
            return Task.FromResult(proxyObject.WrapInPassingCEFAR())!;
        }
    }
}
