// <copyright file="PayeezyPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payeezy
{
    using System.Xml;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A payeezy payments provider extensions.</summary>
    public static class PayeezyPaymentsProviderExtensions
    {
        /// <summary>Converts a transactionResult to a payment response.</summary>
        /// <param name="transactionResult">The transaction result.</param>
        /// <returns>TransactionResult as an IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(XmlNode transactionResult)
        {
            Contract.RequiresNotNull(transactionResult);
            var retVal = new PaymentWalletResponse();
            var node = transactionResult.SelectSingleNode("Transaction_Approved");
            if (node != null)
            {
                retVal.Approved = node.InnerText == "true";
            }
            node = transactionResult.SelectSingleNode("TransarmorToken");
            if (node != null)
            {
                retVal.Token = node.InnerText;
            }
            node = transactionResult.SelectSingleNode("Authorization_Num");
            if (node != null)
            {
                retVal.ResponseCode = node.InnerText;
            }
            node = transactionResult.SelectSingleNode("CardType");
            if (node != null)
            {
                retVal.CardType = node.InnerText;
            }
            return retVal;
        }

        /// <summary>Converts values to a payment wallet response.</summary>
        /// <param name="approved">    True if approved.</param>
        /// <param name="responseCode">The response code.</param>
        /// <returns>TransactionResult as an IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(bool approved, string responseCode)
        {
            return new PaymentWalletResponse
            {
                Approved = approved,
                ResponseCode = responseCode,
            };
        }

        /// <summary>Converts a transactionResult to a payment response.</summary>
        /// <param name="transactionResult">The transaction result.</param>
        /// <returns>The given data converted to an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(XmlNode transactionResult)
        {
            Contract.RequiresNotNull(transactionResult);
            var retVal = new PaymentResponse();
            var node = transactionResult.SelectSingleNode("Transaction_Approved");
            if (node != null)
            {
                retVal.Approved = node.InnerText == "true";
            }
            node = transactionResult.SelectSingleNode("DollarAmount");
            if (node != null)
            {
                retVal.Amount = decimal.Parse(node.InnerText);
            }
            node = transactionResult.SelectSingleNode("Authorization_Num");
            if (node != null)
            {
                retVal.AuthorizationCode = node.InnerText;
            }
            node = transactionResult.SelectSingleNode("Transaction_Tag");
            if (node != null)
            {
                retVal.TransactionID = node.InnerText;
            }
            return retVal;
        }

        /// <summary>Converts values to a payment response.</summary>
        /// <param name="approved">    True if approved.</param>
        /// <param name="responseCode">The response code.</param>
        /// <returns>The given data converted to an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(bool approved, string responseCode)
        {
            return new PaymentResponse
            {
                Approved = approved,
                ResponseCode = responseCode,
            };
        }
    }
}
