// <copyright file="Line.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the line class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a line.</summary>
    [Serializable]
    public class Line
    {
        /// <summary>Gets or sets the line no.</summary>
        /// <value>The line no.</value>
        public string? LineNo { get; set; } // Required

        /// <summary>Gets or sets destination code.</summary>
        /// <value>The destination code.</value>
        public string? DestinationCode { get; set; } // Required

        /// <summary>Gets or sets the origin code.</summary>
        /// <value>The origin code.</value>
        public string? OriginCode { get; set; } // Required

        /// <summary>Gets or sets the item code.</summary>
        /// <value>The item code.</value>
        public string? ItemCode { get; set; } // Required

        /// <summary>Gets or sets the qty.</summary>
        /// <value>The qty.</value>
        public decimal Qty { get; set; } // Required

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; } // Required

        /// <summary>Gets or sets the tax code.</summary>
        /// <value>The tax code.</value>
        public string? TaxCode { get; set; } // Best practice

        /// <summary>Gets or sets the type of the customer usage.</summary>
        /// <value>The type of the customer usage.</value>
        public string? CustomerUsageType { get; set; }

        /// <summary>Gets or sets the tax override.</summary>
        /// <value>The tax override.</value>
        public TaxOverrideDef? TaxOverride { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string? Description { get; set; } // Best Practice

        /// <summary>Gets or sets a value indicating whether the discounted.</summary>
        /// <value>True if discounted, false if not.</value>
        public bool Discounted { get; set; }

        /// <summary>Gets or sets a value indicating whether the tax included.</summary>
        /// <value>True if tax included, false if not.</value>
        public bool TaxIncluded { get; set; }

        /// <summary>Gets or sets the reference 1.</summary>
        /// <value>The reference 1.</value>
        public string? Ref1 { get; set; }

        /// <summary>Gets or sets the reference 2.</summary>
        /// <value>The reference 2.</value>
        public string? Ref2 { get; set; }
    }
}
