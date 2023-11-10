// <copyright file="PackagingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the packaging provider base class</summary>
namespace Clarity.Ecommerce.Providers.Packaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Packaging;
    using Models;

    /// <summary>A packaging provider.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IPackagingProviderBase"/>
    public abstract class PackagingProviderBase : ProviderBase, IPackagingProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Packaging;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<List<IProviderShipment>>> GetItemPackagesAsync(
            int cartID,
            string? contextProfileName);
    }
}
