// <copyright file="BNGVoidTransaction.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bng void transaction class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG.Transactions
{
    /// <summary>A bng void transaction.</summary>
    /// <seealso cref="BNGTransaction"/>
    public class BNGVoidTransaction : BNGTransaction
    {
        /// <summary>The type.</summary>
        private const string StrType = "void";

        /// <summary>Initializes a new instance of the <see cref="BNGVoidTransaction"/> class.</summary>
        /// <param name="transactionID">The transactionID.</param>
        public BNGVoidTransaction(string transactionID)
        {
            Type = StrType;
            TransactionID = transactionID;
        }
    }
}
