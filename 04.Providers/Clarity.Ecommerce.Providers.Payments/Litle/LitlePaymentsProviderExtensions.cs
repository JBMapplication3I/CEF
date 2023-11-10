// <copyright file="LitlePaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the litle payments provider extensions class</summary>
#if !NET5_0_OR_GREATER // Litle doesn't have .net 5.0+ builds (alternative available)
namespace Clarity.Ecommerce.Providers.Payments.LitleShip
{
    using Interfaces.Providers.Payments;
    using Litle.Sdk;
    using Utilities;

    /// <summary>A litle payments provider extensions.</summary>
    public static class LitlePaymentsProviderExtensions
    {
        /// <summary>A string extension method that converts a credit card to a payment type.</summary>
        /// <param name="creditCard">The credit card to act on.</param>
        /// <returns>credit card as a methodOfPaymentTypeEnum.</returns>
        public static methodOfPaymentTypeEnum ToPaymentType(this string creditCard)
        {
            if (string.IsNullOrWhiteSpace(creditCard) || creditCard.Length < 3)
            {
                return methodOfPaymentTypeEnum.Item;
            }
            switch (int.Parse(creditCard[..2]))
            {
                case 34:
                case 37:
                {
                    return methodOfPaymentTypeEnum.AX; // American Express
                }
                case 36:
                {
                    return methodOfPaymentTypeEnum.DC; // Diners Club
                }
                case 38:
                {
                    return methodOfPaymentTypeEnum.BL; // Carte Blanche
                }
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                {
                    return methodOfPaymentTypeEnum.MC; // Carte Blanche
                }
                default:
                {
                    switch (int.Parse(creditCard[..4]))
                    {
                        case 2014:
                        case 2149:
                        {
                            return methodOfPaymentTypeEnum.EC; // EnRoute
                        }
                        case 2131:
                        case 1800:
                        {
                            return methodOfPaymentTypeEnum.JC; // JCB
                        }
                        case 6011:
                        {
                            return methodOfPaymentTypeEnum.DI; // Discover
                        }
                        default:
                        {
                            switch (int.Parse(creditCard[..3]))
                            {
                                case 300:
                                case 301:
                                case 302:
                                case 303:
                                case 304:
                                case 305:
                                {
                                    return methodOfPaymentTypeEnum.DC; // American Diners Club
                                }
                                default:
                                {
                                    switch (int.Parse(creditCard[..1]))
                                    {
                                        case 3:
                                        {
                                            return methodOfPaymentTypeEnum.JC; // JCB
                                        }
                                        case 4:
                                        {
                                            return methodOfPaymentTypeEnum.VI; // Visa
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            return methodOfPaymentTypeEnum.Item;
        }

        /// <summary>A refundReversalResponse extension method that converts a response to a payment response.</summary>
        /// <param name="response">The response to act on.</param>
        /// <returns>response as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this authorizationResponse response)
        {
            Contract.RequiresNotNull(response);
            var amount = decimal.Parse(!string.IsNullOrWhiteSpace(response.approvedAmount) ? response.approvedAmount : "0");
            return new PaymentResponse
            {
                Amount = amount,
                Approved = amount > 0,
                AuthorizationCode = response.authCode,
                ResponseCode = response.authorizationResponseSubCode,
                TransactionID = response.litleTxnId.ToString(),
            };
        }

        /// <summary>A refundReversalResponse extension method that converts a response to a payment response.</summary>
        /// <param name="response">The response to act on.</param>
        /// <returns>response as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this captureResponse response)
        {
            Contract.RequiresNotNull(response);
            return new PaymentResponse
            {
                Amount = 0,
                Approved = true,
                AuthorizationCode = string.Empty,
                ResponseCode = string.Empty,
                TransactionID = response.litleTxnId.ToString(),
            };
        }

        /// <summary>A refundReversalResponse extension method that converts a response to a payment response.</summary>
        /// <param name="response">The response to act on.</param>
        /// <returns>response as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this refundReversalResponse response)
        {
            Contract.RequiresNotNull(response);
            return new PaymentResponse
            {
                Amount = 0,
                Approved = true,
                AuthorizationCode = string.Empty,
                ResponseCode = string.Empty,
                TransactionID = response.litleTxnId,
            };
        }

        /// <summary>A refundReversalResponse extension method that converts a response to a payment response.</summary>
        /// <param name="transactionID">Identifier for the transaction.</param>
        /// <param name="amount">       The amount.</param>
        /// <returns>response as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(string transactionID, decimal amount)
        {
            return new PaymentResponse
            {
                Amount = amount,
                TransactionID = transactionID,
            };
        }
    }
}
#endif
