// <copyright file="IAddressValidationResultModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressValidationResultModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers
{
    using Models;

    /// <summary>Interface for address validation model.</summary>
    public interface IAddressValidationResultModel
    {
        /// <summary>Gets or sets the identifier of the account contact.</summary>
        /// <value>The identifier of the account contact.</value>
        int? AccountContactID { get; set; }

        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        int? ContactID { get; set; }

        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets a value indicating whether this IAddressValidationResultModel is valid.</summary>
        /// <value>True if this IAddressValidationResultModel is valid, false if not.</value>
        bool IsValid { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        string? Message { get; set; }

        /// <summary>Gets or sets the validated address.</summary>
        /// <value>The validated address.</value>
        IAddressModel? ValidatedAddress { get; set; }

        /// <summary>Gets or sets source address.</summary>
        /// <value>The source address.</value>
        IAddressModel? SourceAddress { get; set; }

        /// <summary>Gets or sets the merged address.</summary>
        /// <value>The merged address.</value>
        IAddressModel? MergedAddress { get; set; }

        /// <summary>With success.</summary>
        /// <param name="validatedAddress">The validated address.</param>
        /// <param name="message">         The message.</param>
        /// <returns>An IAddressValidationResultModel.</returns>
        IAddressValidationResultModel WithSuccess(IAddressModel validatedAddress, string? message = null);

        /// <summary>With failure.</summary>
        /// <param name="message">The message.</param>
        /// <returns>An IAddressValidationResultModel.</returns>
        IAddressValidationResultModel WithFailure(string? message = null);
    }
}
