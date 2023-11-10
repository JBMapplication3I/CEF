// <copyright file="BNGSaleTransaction.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bng sale transaction class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG.Transactions
{
    /// <summary>A BNG sale transaction.</summary>
    /// <seealso cref="BNGTransaction"/>
    public class BNGSaleTransaction : BNGTransaction
    {
        /// <summary>The type.</summary>
        private const string StrType = "sale";

        /// <summary>The payment.</summary>
        private const string StrPayment = "creditcard";

        /// <summary>Initializes a new instance of the <see cref="BNGSaleTransaction"/> class.</summary>
        /// <param name="amount">  The amount.</param>
        /// <param name="ccnumber">The ccnumber.</param>
        /// <param name="ccexp">   The ccexp.</param>
        /// <param name="cvv">     The cvv.</param>
        public BNGSaleTransaction(string amount, string ccnumber, string ccexp, string cvv)
        {
            Type = StrType;
            Payment = StrPayment;
            Amount = amount;
            CCNumber = ccnumber;
            CCExp = ccexp;
            CVV = cvv;
        }
    }
}
