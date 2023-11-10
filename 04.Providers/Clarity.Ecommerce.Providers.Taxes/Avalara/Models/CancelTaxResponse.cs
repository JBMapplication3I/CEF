// <copyright file="CancelTaxResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cancel tax response class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a cancel tax response.</summary>
    [Serializable]
    public class CancelTaxResponse
    {
        /// <summary>Gets or sets the cancel tax result.</summary>
        /// <value>The cancel tax result.</value>
        public CancelTaxResult? CancelTaxResult { get; set; }

        /// <summary>Gets or sets the result code.</summary>
        /// <value>The result code.</value>
        public SeverityLevel ResultCode { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public Message[] Messages { get; set; } = Array.Empty<Message>();
    }
}
