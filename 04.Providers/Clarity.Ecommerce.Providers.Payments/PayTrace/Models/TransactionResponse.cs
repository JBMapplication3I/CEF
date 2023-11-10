// <copyright file="TransactionResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace TransactionResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;

    /// <summary>Class for TransactionRequest.</summary>
    public class TransactionResponse : PayTraceBasicResponse, ITransactionResponse
    {
        /// <summary>Gets or sets the Transactions.</summary>
        /// <value>The Transactions.</value>
        [JsonProperty("transactions")]
        public Transaction[]? Transactions { get; set; }

        /// <inheritdoc/>
        ITransaction[]? ITransactionResponse.Transactions { get => Transactions; set => Transactions = (Transaction[]?)value; }
    }
}