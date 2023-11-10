// <copyright file="HistoricalAddressValidationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical address validation model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    /// <summary>A data Model for the historical address validation.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IHistoricalAddressValidationModel"/>
    public partial class HistoricalAddressValidationModel
    {
        /// <inheritdoc/>
        public string? Provider { get; set; }

        /// <inheritdoc/>
        public long? AddressHash { get; set; }

        /// <inheritdoc/>
        public DateTime OnDate { get; set; }

        /// <inheritdoc/>
        public bool IsValid { get; set; }

        /// <inheritdoc/>
        public string? SerializedRequest { get; set; }

        /// <inheritdoc/>
        public string? SerializedResponse { get; set; }
    }
}
