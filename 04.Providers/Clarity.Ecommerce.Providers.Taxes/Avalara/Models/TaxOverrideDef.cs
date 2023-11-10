// <copyright file="TaxOverrideDef.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax override definition class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a tax override definition.</summary>
    [Serializable]
    public class TaxOverrideDef // Allows tax date, amount, or exempt status to be overridden.
    {
        /// <summary>Gets or sets the type of the tax override.</summary>
        /// <value>The type of the tax override.</value>
        public string? TaxOverrideType { get; set; }

        /// <summary>Gets or sets the tax amount.</summary>
        /// <value>The tax amount.</value>
        public string? TaxAmount { get; set; }

        /// <summary>Gets or sets the tax date.</summary>
        /// <value>The tax date.</value>
        public string? TaxDate { get; set; }

        /// <summary>Gets or sets the reason.</summary>
        /// <value>The reason.</value>
        public string? Reason { get; set; }
    }
}
