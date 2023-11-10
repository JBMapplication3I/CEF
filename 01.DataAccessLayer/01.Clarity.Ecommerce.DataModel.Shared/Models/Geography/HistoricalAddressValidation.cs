// <copyright file="HistoricalAddressValidation.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical address validation class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHistoricalAddressValidation : IBase
    {
        #region HistoricalAddressValidation Properties
        /// <summary>Gets or sets the address hash.</summary>
        /// <value>The address hash.</value>
        long? AddressHash { get; set; }

        /// <summary>Gets or sets the on date.</summary>
        /// <value>The on date.</value>
        DateTime OnDate { get; set; }

        /// <summary>Gets or sets a value indicating whether this IHistoricalAddressValidation is valid.</summary>
        /// <value>True if this IHistoricalAddressValidation is valid, false if not.</value>
        bool IsValid { get; set; }

        /// <summary>Gets or sets the provider.</summary>
        /// <value>The provider.</value>
        string? Provider { get; set; }

        /// <summary>Gets or sets the serialized request.</summary>
        /// <value>The serialized request.</value>
        string? SerializedRequest { get; set; }

        /// <summary>Gets or sets the serialized response.</summary>
        /// <value>The serialized response.</value>
        string? SerializedResponse { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;

    [SqlSchema("Geography", "HistoricalAddressValidation")]
    public class HistoricalAddressValidation : Base, IHistoricalAddressValidation
    {
        #region HistoricalAddressValidation Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public long? AddressHash { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime OnDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsValid { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), DefaultValue(null)]
        public string? Provider { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? SerializedRequest { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? SerializedResponse { get; set; }
        #endregion
    }
}
