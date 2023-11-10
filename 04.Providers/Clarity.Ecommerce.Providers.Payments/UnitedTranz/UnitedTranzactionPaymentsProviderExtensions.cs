// <copyright file="UnitedTranzactionPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the united tranzaction payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.UnitedTranzaction
{
    using System.Xml;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>An united tranzaction payments provider extensions.</summary>
    public static class UnitedTranzactionPaymentsProviderExtensions
    {
        /// <summary>Converts a transactionResult to a payment wallet response.</summary>
        /// <param name="transactionResult">The transaction result.</param>
        /// <returns>TransactionResult as an IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(XmlNode? transactionResult)
        {
            Contract.RequiresNotNull(transactionResult);
            var retVal = new PaymentWalletResponse();
            var node = transactionResult!.SelectSingleNode("TRANSID");
            if (node != null)
            {
                retVal.ResponseCode = node.InnerText;
            }
            node = transactionResult.SelectSingleNode("STATUS");
            if (node != null)
            {
                retVal.Approved = node.InnerText == "A";
            }
            node = transactionResult.SelectSingleNode("TOKEN");
            if (node != null)
            {
                retVal.Token = node.InnerText;
            }
            return retVal;
        }

        /// <summary>Converts values to a payment wallet response.</summary>
        /// <param name="approved">True if approved.</param>
        /// <param name="message"> The message.</param>
        /// <returns>TransactionResult as an IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(bool approved, string message)
        {
            return new PaymentWalletResponse
            {
                Approved = approved,
                ResponseCode = message,
            };
        }

        /// <summary>Generates a response.</summary>
        /// <param name="transactionResult">The transaction result.</param>
        /// <returns>The response.</returns>
        public static IPaymentResponse ToPaymentResponse(XmlNode? transactionResult)
        {
            Contract.RequiresNotNull(transactionResult);
            var retVal = new PaymentResponse();
            var node = transactionResult!.SelectSingleNode("TRANSID");
            if (node != null)
            {
                retVal.TransactionID = node.InnerText;
            }
            node = transactionResult.SelectSingleNode("STATUS");
            if (node != null)
            {
                retVal.Approved = node.InnerText == "A";
            }
            return retVal;
        }

        /// <summary>Generates a response.</summary>
        /// <param name="approved">True if approved.</param>
        /// <param name="message"> The message.</param>
        /// <returns>The response.</returns>
        public static IPaymentResponse ToPaymentResponse(bool approved, string message)
        {
            return new PaymentResponse
            {
                Approved = approved,
                ResponseCode = message,
            };
        }
    }
}
