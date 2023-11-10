// <copyright file="TaxDetail.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax detail class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a tax detail.</summary>
    [Serializable]
    public class TaxDetail // Result object
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        public decimal Rate { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        public decimal Tax { get; set; }

        /// <summary>Gets or sets the taxable.</summary>
        /// <value>The taxable.</value>
        public decimal Taxable { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public string? Country { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        public string? Region { get; set; }

        /// <summary>Gets or sets the type of the juris.</summary>
        /// <value>The type of the juris.</value>
        public string? JurisType { get; set; }

        /// <summary>Gets or sets the name of the juris.</summary>
        /// <value>The name of the juris.</value>
        public string? JurisName { get; set; }

        /// <summary>Gets or sets the name of the tax.</summary>
        /// <value>The name of the tax.</value>
        public string? TaxName { get; set; }
    }
}
