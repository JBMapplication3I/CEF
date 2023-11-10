// <copyright file="GetTaxRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get tax request class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>(Serializable)a get tax request.</summary>
    [Serializable]
    public class GetTaxRequest
    {
        #region Required for tax calculation
        /// <summary>Gets or sets the document date.</summary>
        /// <value>The document date.</value>
        public string? DocDate { get; set; }

        /// <summary>Gets or sets the customer code.</summary>
        /// <value>The customer code.</value>
        public string? CustomerCode { get; set; }

        /// <summary>Gets or sets the addresses.</summary>
        /// <value>The addresses.</value>
        public Address[]? Addresses { get; set; }

        /// <summary>Gets or sets the lines.</summary>
        /// <value>The lines.</value>
        public Line[]? Lines { get; set; }
        #endregion

        #region Best Practice for tax calculation
        /// <summary>Gets or sets the client.</summary>
        /// <value>The client.</value>
        public string? Client { get; set; }

        /// <summary>Gets or sets the document code.</summary>
        /// <value>The document code.</value>
        public string? DocCode { get; set; }

        /// <summary>Gets or sets the type of the document.</summary>
        /// <value>The type of the document.</value>
        public DocType DocType { get; set; }

        /// <summary>Gets or sets the company code.</summary>
        /// <value>The company code.</value>
        public string? CompanyCode { get; set; }

        /// <summary>Gets or sets a value indicating whether the commit.</summary>
        /// <value>True if commit, false if not.</value>
        public bool Commit { get; set; }

        /// <summary>Gets or sets the detail level.</summary>
        /// <value>The detail level.</value>
        public DetailLevel DetailLevel { get; set; }
        #endregion

        #region Use where appropriate to the situation
        /// <summary>Gets or sets the type of the customer usage.</summary>
        /// <value>The type of the customer usage.</value>
        public string? CustomerUsageType { get; set; }

        /// <summary>Gets or sets the exemption no.</summary>
        /// <value>The exemption no.</value>
        public string? ExemptionNo { get; set; }

        /// <summary>Gets or sets the discount.</summary>
        /// <value>The discount.</value>
        public decimal Discount { get; set; }

        /// <summary>Gets or sets the business identification no.</summary>
        /// <value>The business identification no.</value>
        public string? BusinessIdentificationNo { get; set; }

        /// <summary>Gets or sets the tax override.</summary>
        /// <value>The tax override.</value>
        public TaxOverrideDef? TaxOverride { get; set; }

        /// <summary>Gets or sets the currency code.</summary>
        /// <value>The currency code.</value>
        public string? CurrencyCode { get; set; }
        #endregion

        #region Optional
        /// <summary>Gets or sets the purchase order no.</summary>
        /// <value>The purchase order no.</value>
        public string? PurchaseOrderNo { get; set; }

        /// <summary>Gets or sets the payment date.</summary>
        /// <value>The payment date.</value>
        public string? PaymentDate { get; set; }

        /// <summary>Gets or sets the position lane code.</summary>
        /// <value>The position lane code.</value>
        public string? PosLaneCode { get; set; }

        /// <summary>Gets or sets the reference code.</summary>
        /// <value>The reference code.</value>
        public string? ReferenceCode { get; set; }

        /// <summary>Gets or sets the location code.</summary>
        /// <value>The location code.</value>
        public string? LocationCode { get; set; }

        /// <summary>Convert this GetTaxRequest into a string representation.</summary>
        /// <returns>A string that represents this GetTaxRequest.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
        #endregion
    }
}
