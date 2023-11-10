// <copyright file="BNGRefundTransaction.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bng refund transaction class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG.Transactions
{
    /// <summary>A bng refund transaction.</summary>
    /// <seealso cref="BNGTransaction"/>
    public class BNGRefundTransaction : BNGTransaction
    {
        /// <summary>The type.</summary>
        private const string StrType = "auth";

        /// <summary>Initializes a new instance of the <see cref="BNGRefundTransaction"/> class.</summary>
        /// <param name="amount">       The amount.</param>
        /// <param name="transactionID">The transaction identifier.</param>
        public BNGRefundTransaction(string amount, string transactionID)
        {
            Type = StrType;
            TransactionID = transactionID;
            Amount = amount;
        }
    }
}
