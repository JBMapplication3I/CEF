// <copyright file="VinLookupProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the VinLookup provider base class</summary>
namespace Clarity.Ecommerce.Providers.VinLookup
{
    using System.Threading.Tasks;
    using Interfaces.Providers.VinLookup;
    using Models;

    /// <summary>A VinLookup provider.</summary>
    /// <seealso cref="ProviderBase"/>
    public abstract class VinLookupProviderBase : ProviderBase, IVinLookupProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.VINLookup;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<bool>> ValidateVinAsync(string vinNumber, string? contextProfileName);
    }
}
