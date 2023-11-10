// <copyright file="BNGPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the BNG payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments
{
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A BNG payments provider extensions.</summary>
    public static class BNGPaymentsProviderExtensions
    {
        /// <summary>Converts a string result to a payment response.</summary>
        /// <param name="result">The result.</param>
        /// <returns>Result as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(string result)
        {
            Contract.RequiresNotNull(result);
            var retVal = new PaymentResponse { Amount = 0 };
            var results = result.Split('&');
            if (results.Length < 2)
            {
                return retVal;
            }
            foreach (var val in results)
            {
                var vals = val.Split('=');
                if (vals.Length < 2)
                {
                    return retVal;
                }
                switch (vals[0])
                {
                    case "response":
                    {
                        retVal.Approved = vals[1] == "1";
                        break;
                    }
                    case "response_code":
                    {
                        retVal.ResponseCode = vals[1];
                        break;
                    }
                    case "authcode":
                    {
                        retVal.AuthorizationCode = vals[1];
                        break;
                    }
                    case "transactionID":
                    {
                        retVal.TransactionID = vals[1];
                        break;
                    }
                }
            }
            return retVal;
        }

        /// <summary>Converts a values to a payment response.</summary>
        /// <param name="transactionID">Identifier for the transaction.</param>
        /// <param name="amount">       The amount.</param>
        /// <returns>Result as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(string transactionID, decimal amount)
        {
            return new PaymentResponse
            {
                TransactionID = transactionID,
                Amount = amount,
            };
        }
    }
}
