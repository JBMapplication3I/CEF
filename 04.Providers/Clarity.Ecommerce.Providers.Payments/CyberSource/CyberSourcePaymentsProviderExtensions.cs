// <copyright file="CyberSourcePaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cyber source payments provider extensions class</summary>
#if !NET5_0_OR_GREATER // Cybersource doesn't have .net 5.0+ builds
namespace Clarity.Ecommerce.Providers.Payments.CyberSource
{
    using System.Linq;
    using global::CyberSource.Clients.SoapServiceReference;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A cyber source payments provider extensions.</summary>
    public static class CyberSourcePaymentsProviderExtensions
    {
        /// <summary>Converts values to a payment wallet response.</summary>
        /// <param name="reasonCode">    The reason code.</param>
        /// <param name="subscriptionID">Identifier for the subscription.</param>
        /// <returns>The given data converted to an IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(string? reasonCode, string? subscriptionID)
        {
            return new PaymentWalletResponse
            {
                ResponseCode = reasonCode,
                Token = subscriptionID,
                Approved = !int.TryParse(reasonCode, out var reason) && reason == 100,
            };
        }

        /// <summary>Converts a reply to a subscription response.</summary>
        /// <param name="approved">    True if approved.</param>
        /// <param name="responseCode">The response code.</param>
        /// <returns>Reply as an IPaymentResponse.</returns>
        public static IPaymentResponse ToSubscriptionResponse(bool approved, string responseCode)
        {
            return new PaymentResponse
            {
                Approved = approved,
                ResponseCode = responseCode,
            };
        }

        /// <summary>Converts a reply to a subscription response.</summary>
        /// <param name="reasonCode">    The reason code.</param>
        /// <param name="subscriptionID">Identifier for the subscription.</param>
        /// <returns>Reply as an IPaymentResponse.</returns>
        public static IPaymentResponse ToSubscriptionResponse(string reasonCode, string subscriptionID)
        {
            return new PaymentResponse
            {
                ResponseCode = reasonCode,
                TransactionID = subscriptionID,
                Approved = !int.TryParse(reasonCode, out var reason) && reason == 100,
            };
        }

        /// <summary>Converts a reply to a subscription response.</summary>
        /// <param name="reply">The reply.</param>
        /// <returns>Reply as an IPaymentResponse.</returns>
        public static IPaymentResponse ToSubscriptionResponse(ReplyMessage reply)
        {
            Contract.RequiresNotNull(reply);
            var retVal = new PaymentResponse
            {
                Amount = 0,
            };
            switch (int.Parse(reply.reasonCode))
            {
                case 100: // Success
                {
                    retVal.Amount = !decimal.TryParse(reply.ccCaptureReply.amount, out var amt) ? 0 : amt;
                    retVal.Approved = true;
                    retVal.AuthorizationCode = reply.ccAuthReply.authorizationCode;
                    retVal.ResponseCode = reply.paySubscriptionCreateReply.reasonCode;
                    retVal.TransactionID = reply.paySubscriptionCreateReply.subscriptionID;
                    break;
                }
                case 101: // Missing field(s)
                {
                    retVal.Approved = false;
                    break;
                }
                case 102: // Invalid field(s)
                {
                    retVal.Approved = false;
                    break;
                }
                case 204: // Insufficient funds
                {
                    retVal.Approved = false;
                    break;
                }
                // Add more reason codes here that you need to handle specifically
            }
            return retVal;
        }

        /// <summary>Converts a reply to a payment response.</summary>
        /// <param name="approved">  True if approved.</param>
        /// <param name="reasonCode">The reason code.</param>
        /// <returns>Reply as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(bool approved, string reasonCode)
        {
            return new PaymentResponse
            {
                Approved = approved,
                ResponseCode = reasonCode,
            };
        }

        /// <summary>Converts a reply to a payment response.</summary>
        /// <param name="transactionID">Identifier for the transaction.</param>
        /// <param name="amount">       The amount.</param>
        /// <returns>Reply as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(string transactionID, decimal amount)
        {
            return new PaymentResponse
            {
                TransactionID = transactionID,
                Amount = amount,
            };
        }

        /// <summary>Converts a reply to a payment response.</summary>
        /// <param name="reply">The reply.</param>
        /// <returns>Reply as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(ReplyMessage reply)
        {
            Contract.RequiresNotNull(reply);
            var retVal = new PaymentResponse
            {
                Amount = 0,
            };
            var reasonCode = int.Parse(reply.reasonCode);
            switch (reasonCode)
            {
                case 100: // Success
                {
                    if (reply.ccCaptureReply != null)
                    {
                        // Credit card processing
                        retVal.Approved = true;
                        retVal.Amount = !decimal.TryParse(reply.ccCaptureReply.amount, out var amt) ? 0 : amt;
                        retVal.AuthorizationCode = reply.ccAuthReply.authorizationCode;
                        retVal.ResponseCode = reply.ccAuthReply.cavvResponseCode;
                        retVal.TransactionID = reply.ccAuthReply.transactionID;
                    }
                    else if (reply.ecDebitReply != null)
                    {
                        // ACH processing
                        retVal.Approved = true;
                        retVal.Amount = !decimal.TryParse(reply.ecDebitReply.amount, out var amt) ? 0 : amt;
                        retVal.AuthorizationCode = reply.ecDebitReply.reconciliationID;
                        retVal.ResponseCode = reply.ecDebitReply.processorResponse;
                        retVal.TransactionID = reply.ecDebitReply.processorTransactionID;
                    }
                    break;
                }
                case 101: // Missing field(s)
                {
                    retVal.Approved = false;
                    retVal.ResponseCode = "101 - Missing field(s)";
                    retVal.AuthorizationCode = reply.missingField
                        .DefaultIfEmpty("No fields named")
                        .Aggregate((c, n) => c + "\r\n" + n);
                    break;
                }
                case 102: // Invalid field(s)
                {
                    retVal.Approved = false;
                    retVal.ResponseCode = "102 - Invalid field(s)";
                    retVal.AuthorizationCode = reply.invalidField
                        .DefaultIfEmpty("No fields named")
                        .Aggregate((c, n) => c + "\r\n" + n);
                    break;
                }
                case 203: // Card declined by their bank
                {
                    retVal.Approved = false;
                    retVal.ResponseCode = "203 - Card Declined by issuing bank";
                    break;
                }
                case 204: // Insufficient funds
                {
                    retVal.Approved = false;
                    retVal.ResponseCode = "204 - Insufficient funds";
                    break;
                }
                // Add more reason codes here that you need to handle specifically
            }
            return retVal;
        }

        /// <summary>Query if 'payment' is ACH.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>True if ACH, false if not.</returns>
        internal static bool IsACH(this IProviderPayment payment)
        {
            return Contract.CheckAllValidKeys(
                payment.CardHolderName,
                payment.CardType,
                payment.AccountNumber,
                payment.RoutingNumber,
                payment.BankName);
        }
    }
}
#endif
