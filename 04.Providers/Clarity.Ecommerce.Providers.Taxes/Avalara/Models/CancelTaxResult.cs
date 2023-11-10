// <copyright file="CancelTaxResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cancel tax result class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)encapsulates the result of a cancel tax.</summary>
    [Serializable]
    public class CancelTaxResult
    {
        /// <summary>Gets or sets the result code.</summary>
        /// <value>The result code.</value>
        public SeverityLevel ResultCode { get; set; }

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        public string? TransactionId { get; set; }

        /// <summary>Gets or sets the identifier of the document.</summary>
        /// <value>The identifier of the document.</value>
        public string? DocId { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public Message[] Messages { get; set; } = Array.Empty<Message>();
    }
}
