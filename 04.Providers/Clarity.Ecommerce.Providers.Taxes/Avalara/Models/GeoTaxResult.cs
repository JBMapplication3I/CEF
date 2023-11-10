// <copyright file="GeoTaxResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the GEO tax result class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>Interface for GEO tax result.</summary>
    public interface IGeoTaxResult
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        decimal Tax { get; set; }

        /// <summary>Gets or sets the tax details.</summary>
        /// <value>The tax details.</value>
        TaxDetail[]? TaxDetails { get; set; }

        /// <summary>Gets or sets the result code.</summary>
        /// <value>The result code.</value>
        SeverityLevel ResultCode { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        Message[] Messages { get; set; }
    }

    /// <summary>(Serializable)encapsulates the result of a GEO tax.</summary>
    /// <seealso cref="IGeoTaxResult"/>
    [Serializable]
    public class GeoTaxResult : IGeoTaxResult // Result of tax/get verb GET
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        public decimal Rate { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        public decimal Tax { get; set; }

        /// <summary>Gets or sets the tax details.</summary>
        /// <value>The tax details.</value>
        public TaxDetail[]? TaxDetails { get; set; }

        /// <summary>Gets or sets the result code.</summary>
        /// <value>The result code.</value>
        public SeverityLevel ResultCode { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public Message[] Messages { get; set; } = Array.Empty<Message>();
    }
}
