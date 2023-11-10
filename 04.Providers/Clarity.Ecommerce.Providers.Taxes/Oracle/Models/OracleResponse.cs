// <copyright file="OracleResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle response class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.Oracle.Models
{
    using System.Collections.Generic;

    /// <summary>An oracle response.</summary>
    public class OracleResponse
    {
        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string? Message { get; set; }

        /// <summary>Gets or sets a value indicating whether this is success.</summary>
        /// <value>True if this is success, false if not.</value>
        public bool IsSuccess { get; set; }

        /// <summary>Gets or sets the origin tax.</summary>
        /// <value>The origin tax.</value>
        public List<OriginTax>? OriginTax { get; set; }

        /// <summary>Gets or sets the shipping tax.</summary>
        /// <value>The shipping tax.</value>
        public List<ShippingTax>? ShippingTax { get; set; }
    }
}
