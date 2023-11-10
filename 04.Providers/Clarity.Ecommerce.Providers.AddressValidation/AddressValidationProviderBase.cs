// <copyright file="AddressValidationProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address validation provider base class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation
{
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.AddressValidation;

    /// <summary>The address validation provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IAddressValidationProviderBase"/>
    public abstract class AddressValidationProviderBase : ProviderBase, IAddressValidationProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.AddressValidation;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<IAddressValidationResultModel> ValidateAddressAsync(
            IAddressValidationRequestModel request, string? contextProfileName);
    }
}
