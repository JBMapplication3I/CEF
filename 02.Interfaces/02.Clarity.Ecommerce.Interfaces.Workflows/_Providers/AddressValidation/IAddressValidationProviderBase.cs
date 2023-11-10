// <copyright file="IAddressValidationProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressValidationProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.AddressValidation
{
    using System.Threading.Tasks;

    /// <summary>Interface for address validation provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IAddressValidationProviderBase : IProviderBase
    {
        /// <summary>Validates the address described by addressModel.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IAddressValidationResultModel.</returns>
        Task<IAddressValidationResultModel> ValidateAddressAsync(
            IAddressValidationRequestModel request,
            string? contextProfileName);
    }
}
