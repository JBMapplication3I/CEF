// <copyright file="ForwardProxyProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Forward Proxy provider class</summary>
namespace Clarity.Ecommerce.Providers.Proxy.ForwardProxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Models;
    using Proxy;

    /// <inheritdoc/>
    public class ForwardProxyProvider : ProxyProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => ForwardProxyProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<IWebProxy>> CreateWebProxyAsync()
        {
            IWebProxy proxyObject = new WebProxy(
                ForwardProxyProviderConfig.ForwardProxyURL!,
                ForwardProxyProviderConfig.ForwardProxyPort);
            return Task.FromResult(proxyObject.WrapInPassingCEFAR())!;
        }
    }
}
