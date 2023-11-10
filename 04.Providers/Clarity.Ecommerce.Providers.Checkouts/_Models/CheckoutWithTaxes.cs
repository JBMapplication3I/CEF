// <copyright file="CheckoutWithTaxes.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout with taxes class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout with taxes.</summary>
    /// <seealso cref="ICheckoutWithTaxes"/>
    public class CheckoutWithTaxes : ICheckoutWithTaxes
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(VatID), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "Optional VAT ID number for European customers"), DefaultValue(null)]
        public string? VatID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TaxExemptionNumber), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "TaxExemptionNumber"), DefaultValue(null)]
        public string? TaxExemptionNumber { get; set; }
    }
}
