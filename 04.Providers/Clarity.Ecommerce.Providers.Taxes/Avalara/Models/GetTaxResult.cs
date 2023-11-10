// <copyright file="GetTaxResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get tax result class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>(Serializable)encapsulates the result of a get tax.</summary>
    [Serializable]
    public class GetTaxResult // Result of tax/get verb POST
    {
        /// <summary>Gets or sets the document code.</summary>
        /// <value>The document code.</value>
        public string? DocCode { get; set; }

        /// <summary>Gets or sets the document date.</summary>
        /// <value>The document date.</value>
        public DateTime DocDate { get; set; }

        /// <summary>Gets or sets the Date/Time of the time stamp.</summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp { get; set; }

        /// <summary>Gets or sets the total number of amount.</summary>
        /// <value>The total number of amount.</value>
        public decimal TotalAmount { get; set; }

        /// <summary>Gets or sets the total number of discount.</summary>
        /// <value>The total number of discount.</value>
        public decimal TotalDiscount { get; set; }

        /// <summary>Gets or sets the total number of exemption.</summary>
        /// <value>The total number of exemption.</value>
        public decimal TotalExemption { get; set; }

        /// <summary>Gets or sets the total number of taxable.</summary>
        /// <value>The total number of taxable.</value>
        public decimal TotalTaxable { get; set; }

        /// <summary>Gets or sets the total number of tax.</summary>
        /// <value>The total number of tax.</value>
        public decimal TotalTax { get; set; }

        /// <summary>Gets or sets the total number of tax calculated.</summary>
        /// <value>The total number of tax calculated.</value>
        public decimal TotalTaxCalculated { get; set; }

        /// <summary>Gets or sets the tax date.</summary>
        /// <value>The tax date.</value>
        public DateTime TaxDate { get; set; }

        /// <summary>Gets or sets the tax lines.</summary>
        /// <value>The tax lines.</value>
        public TaxLine[]? TaxLines { get; set; }

        /// <summary>Gets or sets the tax summary.</summary>
        /// <value>The tax summary.</value>
        public TaxLine[]? TaxSummary { get; set; }

        /// <summary>Gets or sets the tax addresses.</summary>
        /// <value>The tax addresses.</value>
        public TaxAddress[]? TaxAddresses { get; set; }

        /// <summary>Gets or sets the result code.</summary>
        /// <value>The result code.</value>
        public SeverityLevel ResultCode { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public Message[] Messages { get; set; } = Array.Empty<Message>();

        /// <summary>Convert this GetTaxResult into a string representation.</summary>
        /// <returns>A string that represents this GetTaxResult.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
