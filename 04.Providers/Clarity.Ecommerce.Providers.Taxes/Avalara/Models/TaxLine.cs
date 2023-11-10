// <copyright file="TaxLine.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax line class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a tax line.</summary>
    [Serializable]
    public class TaxLine // Result object
    {
        /// <summary>Gets or sets the line no.</summary>
        /// <value>The line no.</value>
        public string? LineNo { get; set; }

        /// <summary>Gets or sets the tax code.</summary>
        /// <value>The tax code.</value>
        public string? TaxCode { get; set; }

        /// <summary>Gets or sets a value indicating whether the taxability.</summary>
        /// <value>True if taxability, false if not.</value>
        public bool Taxability { get; set; }

        /// <summary>Gets or sets the taxable.</summary>
        /// <value>The taxable.</value>
        public decimal Taxable { get; set; }

        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        public decimal Rate { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        public decimal Tax { get; set; }

        /// <summary>Gets or sets the discount.</summary>
        /// <value>The discount.</value>
        public decimal Discount { get; set; }

        /// <summary>Gets or sets the tax calculated.</summary>
        /// <value>The tax calculated.</value>
        public decimal TaxCalculated { get; set; }

        /// <summary>Gets or sets the exemption.</summary>
        /// <value>The exemption.</value>
        public decimal Exemption { get; set; }

        /// <summary>Gets or sets the tax details.</summary>
        /// <value>The tax details.</value>
        public TaxDetail[]? TaxDetails { get; set; }
    }
}
