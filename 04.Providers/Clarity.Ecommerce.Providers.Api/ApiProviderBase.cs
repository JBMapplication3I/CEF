// <copyright file="ApiProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API provider base class</summary>
namespace Clarity.Ecommerce.Providers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers.Api;
    using Models;

    /// <summary>An API provider.</summary>
    /// <seealso cref="ProviderBase"/>
    public abstract class ApiProviderBase : ProviderBase, IApiProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Api;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string?>> PostAsync(
            string url,
            string verb,
            Dictionary<string, string> customHeaders,
            Dictionary<string, string> parameters,
            int? timeout,
            string body,
            string contentType,
            string? contextProfileName);
    }
}
