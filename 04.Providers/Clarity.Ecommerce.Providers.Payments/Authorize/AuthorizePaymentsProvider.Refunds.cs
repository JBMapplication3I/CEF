// <copyright file="AuthorizePaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authorize payments provider class</summary>
#if !NET5_0_OR_GREATER // Authorize.NET doesn't have .net 5.0+ builds
namespace Clarity.Ecommerce.Providers.Payments.Authorize
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using AuthorizeNet;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using JSConfigs;
    using Utilities;

    /// <summary>An authorize payments provider.</summary>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class AuthorizePaymentsProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var transactionDetails = GetTransactionDetails(transactionID);
            if (transactionDetails == null)
            {
                return Task.FromResult<IPaymentResponse>(new PaymentResponse { Approved = false });
            }
#pragma warning disable CS0618 // Type or member is obsolete
            var request = new CreditRequest(
                transactionID,
                amount ?? payment.Amount!.Value,
                transactionDetails.cardNumber.Replace("XXXX", string.Empty))
            {
                ExpDate = transactionDetails.expirationDate,
            };
#pragma warning restore CS0618 // Type or member is obsolete
            var response = gateway!.Send(request);
            return Task.FromResult(response.ToPaymentResponse());
        }

        /// <summary>Voids an authorized payment before it is processed.</summary>
        /// <param name="transactionID">The ID of the transaction to be voided.</param>
        /// <param name="amount">The amount of the transaction to be voided.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A payment response indicating success or failure of the void attempt.</returns>
        public async Task<IPaymentResponse> VoidAsync(
            string transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.RequiresValidKey(transactionID, "ERROR! No transaction ID provided.");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var salesOrder = await context.SalesOrders
                .FilterByActive(true)
                .FilterSalesOrdersByPaymentTransactionID(transactionID, true, false)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            Contract.Requires<ArgumentException>(salesOrder.PaymentTransactionID == transactionID, "ERROR! Sales order not found.");
            Contract.RequiresValidKey(salesOrder.Status!.Name, "ERROR! No status name on sales order.");
            if (salesOrder.Status.Name != "Void" && salesOrder.Status.Name != "Completed")
            {
                switch (CEFConfigDictionary.PaymentsProviderMode)
                {
                    case Enums.PaymentProviderMode.Production:
                    {
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
                        break;
                    }
                    default:
                    {
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
                        break;
                    }
                }
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new()
                {
                    name = AuthorizePaymentsProviderConfig.Login,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = AuthorizePaymentsProviderConfig.TransactionKey,
                };
                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.voidTransaction.ToString(),
                    refTransId = transactionID,
                };
                var request = new createTransactionRequest { transactionRequest = transactionRequest };
                var controller = new createTransactionController(request);
                controller.Execute();
                var response = controller.GetApiResponse();
                return response.ToPaymentResponse(amount ?? 0m);
            }
            return new PaymentResponse()
            {
                Approved = false,
                Amount = amount ?? 0m,
                ResponseCode = "Order is already void or completed.",
                TransactionID = transactionID,
                AuthorizationCode = string.Empty,
            };
        }

        // Uses AuthNET API to get transaction details
        private static creditCardMaskedType? GetTransactionDetails(string? transactionID)
        {
            if (!Contract.CheckValidKey(transactionID))
            {
                return null;
            }
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = CEFConfigDictionary.PaymentsProviderMode switch
            {
                Enums.PaymentProviderMode.Production => AuthorizeNet.Environment.PRODUCTION,
                _ => ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX,
            };
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new()
            {
                name = AuthorizePaymentsProviderConfig.Login,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = AuthorizePaymentsProviderConfig.TransactionKey,
            };
            var transDetails = new getTransactionDetailsRequest
            {
                transId = transactionID,
            };
            var transDetailsController = new getTransactionDetailsController(transDetails);
            transDetailsController.Execute();
            var transDetailsResponse = transDetailsController.GetApiResponse();
            return transDetailsResponse.transaction.payment.Item is creditCardMaskedType maskedType
                ? maskedType
                : null;
        }
    }
}
#endif
