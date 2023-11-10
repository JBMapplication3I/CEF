// <copyright file="IAddressValidationRequestModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressValidationRequestModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers
{
    using Models;

    /// <summary>Interface for address validation request model.</summary>
    public interface IAddressValidationRequestModel
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

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        IAddressModel? Address { get; set; }
    }
}
