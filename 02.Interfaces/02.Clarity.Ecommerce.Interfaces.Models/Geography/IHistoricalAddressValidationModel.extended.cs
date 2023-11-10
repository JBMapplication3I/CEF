// <copyright file="IHistoricalAddressValidationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHistoricalAddressValidationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for historical address validation model.</summary>
    public partial interface IHistoricalAddressValidationModel
    {
        /// <summary>Gets or sets the provider.</summary>
        /// <value>The provider.</value>
        string? Provider { get; set; }

        /// <summary>Gets or sets the address hash.</summary>
        /// <value>The address hash.</value>
        long? AddressHash { get; set; }

        /// <summary>Gets or sets the on date.</summary>
        /// <value>The on date.</value>
        DateTime OnDate { get; set; }

        /// <summary>Gets or sets a value indicating whether this IHistoricalAddressValidationModel is valid.</summary>
        /// <value>True if this IHistoricalAddressValidationModel is valid, false if not.</value>
        bool IsValid { get; set; }

        /// <summary>Gets or sets the serialized request.</summary>
        /// <value>The serialized request.</value>
        string? SerializedRequest { get; set; }

        /// <summary>Gets or sets the serialized response.</summary>
        /// <value>The serialized response.</value>
        string? SerializedResponse { get; set; }
    }
}
