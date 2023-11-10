// <copyright file="Tax.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Oracle.Models
{
    /// <summary>A tax.</summary>
    public class Tax
    {
        /// <summary>Gets or sets the county.</summary>
        /// <value>The county.</value>
        public string? County { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        public string? City { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        public string? State { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        public string? PostalCode { get; set; }

        /// <summary>Gets or sets the tax rate.</summary>
        /// <value>The tax rate.</value>
        public decimal TaxRate { get; set; }

        /// <summary>Gets or sets the tax amount.</summary>
        /// <value>The tax amount.</value>
        public decimal TaxAmount { get; set; }
    }
}
