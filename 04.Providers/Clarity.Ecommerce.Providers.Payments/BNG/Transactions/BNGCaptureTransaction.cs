// <copyright file="BNGCaptureTransaction.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the BNG capture transaction class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    /// <summary>A BNG capture transaction.</summary>
    /// <seealso cref="BNGTransaction"/>
    public class BNGCaptureTransaction : BNGTransaction
    {
        /// <summary>The type.</summary>
        private const string StrType = "capture";

        /// <summary>Initializes a new instance of the <see cref="BNGCaptureTransaction"/> class.</summary>
        /// <param name="amount">       The amount.</param>
        /// <param name="transactionID">The transactionID.</param>
        public BNGCaptureTransaction(string amount, string transactionID)
        {
            Type = StrType;
            Amount = amount;
            TransactionID = transactionID;
        }
    }
}
