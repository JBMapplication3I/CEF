﻿// <copyright file="PayeezyRefundRequestResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy refund request response class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy refund request response.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyRefundRequestResponse
    {
        /// <summary>Gets or sets the transaction status.</summary>
        /// <value>The transaction status.</value>
        public string? transaction_status { get; set; }

        /// <summary>Gets or sets the transaction tag.</summary>
        /// <value>The transaction tag.</value>
        public string? transaction_tag { get; set; }

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        public string? transaction_id { get; set; }

        /// <summary>Gets or sets the bank resp code.</summary>
        /// <value>The bank resp code.</value>
        public string? bank_resp_code { get; set; }

        /// <summary>Gets or sets the error.</summary>
        /// <value>The error.</value>
        public PayeezyApiError? Error { get; set; }
    }
}
