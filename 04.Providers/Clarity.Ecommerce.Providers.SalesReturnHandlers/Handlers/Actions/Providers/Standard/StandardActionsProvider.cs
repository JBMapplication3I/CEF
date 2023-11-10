// <copyright file="StandardActionsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard actions provider class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using DataModel;
    using Emails;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A standard sales return actions provider.</summary>
    /// <seealso cref="SalesReturnActionsProviderBase"/>
    public class StandardSalesReturnActionsProvider : SalesReturnActionsProviderBase
    {
        /// <summary>The generated sales return status IDs.</summary>
        private static readonly Dictionary<string, int> GeneratedSalesReturnStatusIDs = new();

        /// <summary>The generated note type IDs.</summary>
        private static readonly Dictionary<string, int> GeneratedNoteTypeIDs = new();

        /// <summary>Identifier for the return contact type.</summary>
        private static int? returnContactTypeID;

        /// <summary>Identifier for the billing contact type.</summary>
        private static int? billingContactTypeID;

        /// <summary>Identifier for the private note type.</summary>
        private static int? privateNoteTypeID;

        /// <summary>Identifier for the shipping contact type.</summary>
        private static int? shippingContactTypeID;

        /// <summary>Identifier for the confirmed status.</summary>
        private int? confirmedStatusID;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSalesReturnActionsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<ISalesReturnModel?> CreateStoreFrontSalesReturnAsync(
            ISalesReturnModel salesReturnModel,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            var salesOrderModels = (await Task.WhenAll(
                salesReturnModel.SalesOrderIDs!
                        .Select(x => Workflows.SalesOrders.GetAsync(x, contextProfileName)))
                    .ConfigureAwait(false))
                .Where(x => x != null)
                .ToList();
            // Assuming all validation tests passed, creating Sales Return and Sales Return Items
            var lstSalesReturnModel = new List<ISalesReturnModel>();
            if (CEFConfigDictionary.ReturnsAreSingleCreation)
            {
                foreach (var salesOrderModel in salesOrderModels)
                {
                    foreach (var salesItemBaseModel in salesReturnModel.SalesItems!)
                    {
                        salesReturnModel.SalesItems = new()
                        {
                            salesItemBaseModel,
                        };
                        lstSalesReturnModel.Add(
                            await CreateSalesReturnAsync(
                                salesReturn: salesReturnModel,
                                salesOrder: salesOrderModel!,
                                pricingFactoryContext: pricingFactoryContext,
                                isBackendOverride: false,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false));
                    }
                }
            }
            else
            {
                lstSalesReturnModel = (await Task.WhenAll(
                    salesOrderModels
                        .Select(x => CreateSalesReturnAsync(
                            salesReturn: salesReturnModel,
                            salesOrder: x!,
                            pricingFactoryContext: pricingFactoryContext,
                            isBackendOverride: false,
                            contextProfileName: contextProfileName)))
                        .ConfigureAwait(false))
                    .ToList();
            }
            return lstSalesReturnModel.Count > 0 ? lstSalesReturnModel[0] : null;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsRefundedAsync(
            int id,
            int userID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var response = await ValidateReturnForRefundAsync(id, context, contextProfileName).ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return response;
            }
            response.ActionSucceeded = false;
            var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (provider == null)
            {
                throw new InvalidOperationException(
                    "Could not load a Payment Provider. Are your provider settings in the web config correct?");
            }
            if (provider is not IRefundsProviderBase refundsProvider)
            {
                throw new InvalidOperationException("The currently selected provider does not support Refunds");
            }
            var salesOrder = response.Result!.AssociatedSalesOrders!.First().Slave;
            var associatedSalesInvoices = salesOrder!.AssociatedSalesInvoices?.FirstOrDefault();
            Payment? payment;
            if (associatedSalesInvoices?.Slave!.SalesInvoicePayments!.Any(x => x.Active) != true)
            {
                payment = context.Payments.SingleOrDefault(
                    x => x.TransactionNumber == salesOrder.PaymentTransactionID);
            }
            else
            {
                payment = associatedSalesInvoices.Slave!.SalesInvoicePayments!
                    .FirstOrDefault(x => x.Active)
                    ?.Slave;
            }
            if (payment == null)
            {
                response.Messages.Add("ERROR! No related payment!");
                return response;
            }
            var providerPayment = new PaymentModel
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                CardNumber = payment.Last4CardDigits,
                ExpirationMonth = payment.ExpirationMonth,
                ExpirationYear = payment.ExpirationYear,
                Amount = response.Result.RefundAmount,
            };
            var transaction = await refundsProvider.RefundAsync(
                    providerPayment,
                    salesOrder.PaymentTransactionID!,
                    response.Result.RefundAmount,
                    contextProfileName)
                .ConfigureAwait(false);
            if (transaction?.Approved != true)
            {
                response.Messages.Add("ERROR! Your transaction was not approved by the payment provider!");
                return response;
            }
            response.Result.RefundTransactionID = transaction.TransactionID;
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            var changeStatusResponse = await StatusChangeAsync(
                    entity: response.Result,
                    status: "Completed",
                    context: context,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!changeStatusResponse.ActionSucceeded)
            {
                return response;
            }
            var emailResult = await new SalesReturnsRefundedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = await Workflows.SalesReturns.GetAsync(response.Result.ID, contextProfileName).ConfigureAwait(false),
                    })
                .ConfigureAwait(false);
            if (!emailResult.ActionSucceeded)
            {
                return emailResult;
            }
            response.ActionSucceeded = true;
            return response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ManuallyRefundReturnAsync(
            ISalesReturnModel salesReturn,
            int userID,
            string? contextProfileName)
        {
            ISalesReturn? validatedSalesReturn = null;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (Contract.CheckValidID(salesReturn.ID))
            {
                var response = await ValidateReturnForRefundAsync(
                        salesReturn.ID,
                        context,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    return response;
                }
                validatedSalesReturn = response.Result;
            }
            if (salesReturn.SalesReturnPayments == null || salesReturn.SalesReturnPayments.Count == 0)
            {
                return CEFAR.FailingCEFAR("ERROR! payment information.");
            }
            if (validatedSalesReturn?.RefundAmount == null)
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentNullException(nameof(validatedSalesReturn.RefundAmount));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }
            var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (provider == null)
            {
                throw new InvalidOperationException(
                    "Could not load a Payment Provider. Are your provider settings in the web config correct?");
            }
            if (provider is not IRefundsProviderBase refundGateway)
            {
                throw new InvalidOperationException("The currently selected provider does not support Refunds");
            }
            var payment = salesReturn.SalesReturnPayments[0].Slave;
            var transaction = await refundGateway.RefundAsync(
                    payment: payment!,
                    transactionID: null,
                    amount: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!transaction.Approved)
            {
                return CEFAR.FailingCEFAR("ERROR! Your transaction was not approved by the payment provider!");
            }
            validatedSalesReturn.RefundTransactionID = transaction.TransactionID;
            validatedSalesReturn.User!.Wallets!.Add(new()
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                CreditCardNumber = payment!.CardNumber,
                ExpirationMonth = payment.ExpirationMonth,
                ExpirationYear = payment.ExpirationYear,
                CardType = payment.CardType,
                CardHolderName = payment.CardHolderName,
            });
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            var changeStatusResponse = await StatusChangeAsync(
                    validatedSalesReturn,
                    "Completed",
                    context,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!changeStatusResponse.ActionSucceeded)
            {
                return changeStatusResponse;
            }
            var emailResult = await new SalesReturnsRefundedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = validatedSalesReturn.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsConfirmedAsync(int id, int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = context.SalesReturns
                .Include(x => x.AssociatedSalesOrders!.Select(y => y.Slave))
                .SingleOrDefault(x => x.ID == id);
            if (@return == null)
            {
                return CEFAR.FailingCEFAR("Invalid return ID");
            }
            if (@return.AssociatedSalesOrders!.All(x => x.Slave!.PaymentTransactionID == null))
            {
                return CEFAR.FailingCEFAR("No payment found");
            }
            var previousStatus = @return.Status!.Name;
            var response = await StatusChangeAsync(@return, "Confirmed", context, contextProfileName).ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return response;
            }
            response = await AddStatusChangeNoteAsync(
                    entity: @return,
                    status: "Confirmed",
                    previousStatus: previousStatus!,
                    userID: userID,
                    context: context,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return response;
            }
            var emailResult = await new SalesReturnsConfirmedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> AddPaymentToReturnAsync(int id, IPaymentModel payment, string? contextProfileName)
        {
            Contract.Requires<ArgumentException>(
                payment.Amount is > 0,
                "Cannot add a payment without a positive amount.");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = await context.SalesReturns.FilterByID(id).FirstAsync();
            var response = await StatusChangeAsync(@return, "Full Payment Received", context, contextProfileName).ConfigureAwait(false);
            var emailResult = await new SalesReturnsPaymentSentNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                        ["payment"] = payment,
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsShippedAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = await context.SalesReturns.FilterByID(id).FirstAsync();
            var response = await StatusChangeAsync(@return, "Shipped", context, contextProfileName).ConfigureAwait(false);
            var emailResult = await new SalesReturnsShippedNotificationToBackOfficeEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsCompletedAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = await context.SalesReturns.FilterByID(id).FirstAsync();
            var response = await StatusChangeAsync(@return, "Completed", context, contextProfileName).ConfigureAwait(false);
            var emailResult = await new SalesReturnsCompletedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsVoidAsync(int id, ITaxesProviderBase? taxesProvider, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = await context.SalesReturns.FilterByID(id).FirstAsync();
            var response = await StatusChangeAsync(@return, "Void", context, contextProfileName).ConfigureAwait(false);
            if (taxesProvider is not null)
            {
                await taxesProvider.VoidReturnAsync(@return.CreateSalesReturnModelFromEntityFull(contextProfileName)!, contextProfileName).ConfigureAwait(false);
            }
            var emailResult = await new SalesReturnsVoidedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsRejectedAsync(int id, int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = await context.SalesReturns.FilterByID(id).FirstAsync();
            var previousStatus = @return.Status!.Name;
            var response = await StatusChangeAsync(@return, "Rejected", context, contextProfileName).ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return response;
            }
            response = await AddStatusChangeNoteAsync(
                    entity: @return,
                    status: "Rejected",
                    previousStatus: previousStatus!,
                    userID: userID,
                    context: context,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return response;
            }
            var emailResult = await new SalesReturnsRejectedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsCancelledAsync(int id, IUserModel currentUser, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var @return = await context.SalesReturns.FilterByID(id).FirstAsync();
            var salesOrder = @return.AssociatedSalesOrders!.First().Slave;
            if (salesOrder?.UserID != currentUser?.ID)
            {
                throw new ArgumentException("Invalid Return ID");
            }
            var response = await StatusChangeAsync(@return, "Canceled", context, contextProfileName).ConfigureAwait(false);
            var emailResult = await new SalesReturnsCancelledNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = @return.CreateSalesReturnModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return !emailResult.ActionSucceeded ? emailResult : response;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SendRmaCreationEmailNotificationsAsync(
            ISalesReturnModel salesReturn,
            int salesOrderId,
            string? contextProfileName)
        {
            var emailResponse = new CEFActionResponse();
            emailResponse.Messages ??= new();
            try
            {
                await new SalesReturnsSubmittedNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesReturn"] = salesReturn, })
                    .ConfigureAwait(false);
            }
            catch
            {
                emailResponse.ActionSucceeded = false;
                emailResponse.Messages.Add(
                    $"There was an error sending the customer sales order return confirmation for order id {salesOrderId}.");
            }
            try
            {
                await new SalesReturnsSubmittedNotificationToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesReturn"] = salesReturn, })
                    .ConfigureAwait(false);
            }
            catch
            {
                // emailResponse.ActionSucceeded = false;
                // emailResponse.Messages.Add(
                //     $"There was an error sending the back-office sales order return confirmation for order ID {salesOrderId}.");
            }
            try
            {
                await new SalesReturnsSubmittedNotificationToBackOfficeStoreEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesReturn"] = salesReturn, })
                    .ConfigureAwait(false);
            }
            catch
            {
                // emailResponse.ActionSucceeded = false;
                // emailResponse.Messages.Add(
                //     $"There was an error sending the store back-office sales order return confirmation for order ID {salesReturn.SalesOrderIDs?.FirstOrDefault()}.");
            }
            return emailResponse;
        }

        /// <summary>Searches for the first type.</summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>The found type.</returns>
        protected static string FindType(string cardNumber)
        {
            // https://www.regular-expressions.info/creditcard.html
            if (Regex.Match(cardNumber, "^4[0-9]{12}(?:[0-9]{3})?$").Success)
            {
                return "VISA";
            }
            if (Regex.Match(cardNumber, "^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
            {
                return "Mastercard";
            }
            if (Regex.Match(cardNumber, "^3[47][0-9]{13}$").Success)
            {
                return "American Express";
            }
            if (Regex.Match(cardNumber, "^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
            {
                return "Discover";
            }
            return "General";
        }

        /// <summary>Executes the replacements operation. This does a multi-pass replacement of the dictionary keys with
        /// their values in the source string.</summary>
        /// <param name="replacements">The replacements.</param>
        /// <param name="source">      Source for the.</param>
        /// <returns>A string.</returns>
        private static string DoReplacements(Dictionary<string, string> replacements, string source)
        {
            var result = source;
            var foundThings = true;
            while (foundThings)
            {
                foundThings = false;
                foreach (var kvp in replacements)
                {
                    while (result.Contains(kvp.Key))
                    {
                        result = result.Replace(kvp.Key, kvp.Value);
                        foundThings = true;
                    }
                }
            }
            return result;
        }

        private static async Task<decimal?> CalculateRestockingFeeAsync(
            ISalesItemBaseModel salesReturnItemModel,
            ISalesItemBaseModel salesOrderItemModel,
            string? contextProfileName)
        {
            decimal? retVal = 0m;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var rawFeeAmount = 0m;
            var rawFeePercent = 0m;
            var product = await GetProductRestockingValuesAsync(salesOrderItemModel.ProductID, context).ConfigureAwait(false);
            if (product != null)
            {
                if (!product.Value.productIsEligible)
                {
                    // Product isn't eligible for return
                    return 0m;
                }
                rawFeeAmount += product.Value.productFeeAmount ?? 0m;
                rawFeePercent += product.Value.productFeePercent ?? 0m;
            }
            var reason = await GetReasonRestockingValuesAsync(salesOrderItemModel.ProductID, context).ConfigureAwait(false);
            if (reason is { reasonIsApplicable: true })
            {
                rawFeeAmount += reason.Value.reasonFeeAmount ?? 0m;
                rawFeePercent += reason.Value.reasonFeePercent ?? 0m;
            }
            if (rawFeeAmount > 0)
            {
                retVal = rawFeeAmount;
            }
            else if (rawFeePercent > 0)
            {
                retVal = rawFeePercent
                    / 100
                    * (salesOrderItemModel.UnitSoldPrice ?? salesOrderItemModel.UnitCorePrice)
                    * salesReturnItemModel.TotalQuantity;
            }
            return retVal;
        }

        private static async Task<(bool productIsEligible, decimal? productFeeAmount, decimal? productFeePercent)?>
            GetProductRestockingValuesAsync(int? productID, IClarityEcommerceEntities context)
        {
            if (Contract.CheckInvalidID(productID))
            {
                // Doesn't exist
                return null;
            }
            var productReturnData = await context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .Select(x => new
                {
                    x.IsEligibleForReturn,
                    x.RestockingFeeAmount,
                    // x.RestockingFeeAmountCurrencyID, // TODO: MultiCurrency
                    x.RestockingFeePercent,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (productReturnData == null)
            {
                // Couldn't find it
                return null;
            }
            return (productReturnData.IsEligibleForReturn,
                productReturnData.RestockingFeeAmount,
                productReturnData.RestockingFeePercent);
        }

        private static async Task<(bool reasonIsApplicable, decimal? reasonFeeAmount, decimal? reasonFeePercent)?>
            GetReasonRestockingValuesAsync(int? reasonID, IClarityEcommerceEntities context)
        {
            if (Contract.CheckInvalidID(reasonID))
            {
                // Doesn't exist
                return null;
            }
            var returnReason = await context.SalesReturnReasons
                .AsNoTracking()
                .FilterByID(reasonID)
                .Select(x => new
                {
                    // TODO: Multi-currency
                    x.IsRestockingFeeApplicable,
                    x.RestockingFeePercent,
                    x.RestockingFeeAmount,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (returnReason == null)
            {
                // Couldn't find it
                return null;
            }
            return (returnReason.IsRestockingFeeApplicable,
                returnReason.RestockingFeeAmount,
                returnReason.RestockingFeePercent);
        }

        /// <summary>Creates a sales return order.</summary>
        /// <param name="salesReturn">          The sales return.</param>
        /// <param name="salesOrder">           The sales order.</param>
        /// <param name="pricingFactoryContext">The pricing factory context.</param>
        /// <param name="isBackendOverride">    The Is Backend Override.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A SalesOrderModel.</returns>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        private static async Task<ISalesReturnModel> CreateSalesReturnAsync(
            ISalesReturnModel salesReturn,
            ISalesOrderModel salesOrder,
#pragma warning disable IDE0060 // Remove unused parameter
            // ReSharper disable once UnusedParameter.Local
            IPricingFactoryContextModel pricingFactoryContext,
#pragma warning restore IDE0060 // Remove unused parameter
            bool isBackendOverride,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            Contact? billingContact = null;
            if (salesOrder.BillingContact != null)
            {
                billingContactTypeID ??= await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Billing",
                        byName: "Billing",
                        byDisplayName: "Billing",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                billingContact = (Contact?)salesOrder.BillingContact?.CreateContactEntity(timestamp, contextProfileName);
                billingContact!.TypeID = billingContactTypeID.Value;
                billingContact.Address = (Address?)salesOrder.BillingContact!.Address!.CreateAddressEntity(timestamp, contextProfileName);
            }
            Contact? shippingContact = null;
            if ((!salesOrder.ShippingSameAsBilling.HasValue || !salesOrder.ShippingSameAsBilling.Value)
                && salesOrder.ShippingContact != null)
            {
                shippingContactTypeID ??= await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Shipping",
                        byName: "Shipping",
                        byDisplayName: "Shipping",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                shippingContact = (Contact)salesOrder.ShippingContact.CreateContactEntity(timestamp, contextProfileName);
                shippingContact.TypeID = shippingContactTypeID.Value;
                shippingContact.Address = (Address)salesOrder.ShippingContact.Address!.CreateAddressEntity(timestamp, contextProfileName);
            }
            var newSalesReturn = new SalesReturn
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                JsonAttributes = salesOrder.SerializableAttributes.SerializeAttributesDictionary(),
                // ISalesCollection Properties
                ShippingSameAsBilling = salesOrder.ShippingSameAsBilling,
                BillingContact = billingContact,
                ShippingContact = shippingContact,
                PurchaseOrderNumber = salesOrder.PurchaseOrderNumber,
                // Related Objects
                UserID = salesOrder.UserID,
                AccountID = salesOrder.AccountID,
            };
            var dummy = new SalesReturnModel
            {
                State = new() { Name = "Work", CustomKey = "WORK", DisplayName = "Work" },
                Status = new() { Name = "Pending", CustomKey = "Pending", DisplayName = "Pending" },
                Type = new() { Name = "Web", CustomKey = "Web", DisplayName = "Web" },
            };
            await RelateRequiredStateAsync(newSalesReturn, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(newSalesReturn, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredTypeAsync(newSalesReturn, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.SalesReturns.Add(newSalesReturn);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            newSalesReturn.TaxTransactionID = salesOrder.TaxTransactionID;
            // Add sales Return Items
            var salesOrderIdsChecked = new List<int>();
            foreach (var salesItem in salesReturn.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                var salesOrderItemModel = salesOrder.SalesItems!.FirstOrDefault(
                    x => x.ProductID == salesItem.ProductID
                      && !salesOrderIdsChecked.Contains(x.ID));
                if (salesOrderItemModel == null)
                {
                    continue;
                }
                var salesItems = newSalesReturn.SalesItems!;
                salesItems.Add(await AddSalesReturnItemsAsync(
                        timestamp: timestamp,
                        newSalesReturn: newSalesReturn,
                        salesReturnItemModel: salesItem,
                        salesOrderItemModel: salesOrderItemModel,
                        isBackendOverride: isBackendOverride,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false));
                newSalesReturn.SalesItems = salesItems;
                salesOrderIdsChecked.Add(salesOrderItemModel.ID);
            }
            newSalesReturn.RefundAmount = 0;
            foreach (var item in newSalesReturn.SalesItems!)
            {
                if (item.UnitSoldPrice != null)
                {
                    newSalesReturn.RefundAmount += (item.UnitSoldPrice ?? item.UnitCorePrice) * item.TotalQuantity
                        - item.RestockingFeeAmount;
                }
            }
            if (CEFConfigDictionary.ReturnsAreSingleCreation)
            {
                newSalesReturn.CustomKey = newSalesReturn.SalesItems.First().CustomKey;
            }
            context.SalesReturnSalesOrders.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                SlaveID = salesOrder.ID,
                MasterID = newSalesReturn.ID,
            });
            // Transfer contacts
            foreach (var salesOrderContact in salesOrder.Contacts!)
            {
                newSalesReturn.Contacts ??= new HashSet<SalesReturnContact>();
                newSalesReturn.Contacts.Add(new()
                {
                    Active = salesOrderContact.Active,
                    CreatedDate = salesOrderContact.CreatedDate,
                    SlaveID = salesOrderContact.ContactID,
                });
            }
            var dummyReturnModel = new SalesReturnModel();
            if (dummyReturnModel.SalesReturnPayments != null)
            {
                await Workflows.SalesReturnWithSalesReturnPaymentsAssociation.AssociateObjectsAsync(
                        newSalesReturn,
                        dummyReturnModel,
                        timestamp,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(newSalesReturn, dummyReturnModel, contextProfileName).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(newSalesReturn.JsonAttributes))
            {
                newSalesReturn.JsonAttributes = "{}";
            }
            // TODO@BE: Add Payment to SalesOrderModel in Checkout
            if (dummyReturnModel.Notes != null)
            {
                if (Contract.CheckValidID(newSalesReturn.ID))
                {
                    foreach (var note in dummyReturnModel.Notes)
                    {
                        note.SalesReturnID = newSalesReturn.ID;
                    }
                }
                await Workflows.SalesReturnWithNotesAssociation.AssociateObjectsAsync(newSalesReturn, dummyReturnModel, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (salesReturn.Notes != null)
            {
                foreach (var note in salesReturn.Notes.Where(x => x != null))
                {
                    if (!GeneratedNoteTypeIDs.ContainsKey(note.TypeKey!))
                    {
                        GeneratedNoteTypeIDs[note.TypeKey!] = await Workflows.NoteTypes.ResolveToIDAsync(
                                byID: note.TypeID,
                                byKey: note.TypeKey,
                                byName: note.TypeName,
                                byDisplayName: note.TypeDisplayName,
                                model: note.Type,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
                    newSalesReturn.Notes!.Add(new()
                    {
                        Active = note.Active,
                        CreatedDate = note.CreatedDate,
                        TypeID = GeneratedNoteTypeIDs[note.TypeKey!],
                        CreatedByUserID = note.CreatedByUserID,
                        Note1 = note.Note1,
                    });
                }
            }
            if (CEFConfigDictionary.ReturnsAreSingleCreation)
            {
                privateNoteTypeID ??= await Workflows.NoteTypes.ResolveToIDAsync(
                        byID: null,
                        byKey: "Private Note",
                        byName: "Private Note",
                        byDisplayName: "Private Note",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                newSalesReturn.Notes!.Add(new()
                {
                    Active = true,
                    CreatedDate = timestamp,
                    TypeID = privateNoteTypeID.Value,
                    CreatedByUserID = newSalesReturn.UserID,
                    Note1 = salesReturn.SalesItems![0].Description,
                });
            }
            var returnContact = await RetrieveStoreReturnContactAsync(salesOrder, timestamp, contextProfileName).ConfigureAwait(false);
            if (returnContact != null)
            {
                newSalesReturn.Contacts ??= new HashSet<SalesReturnContact>();
                var tmpContact = (Contact)returnContact.CreateContactEntity(timestamp, contextProfileName);
                tmpContact.TypeID = returnContact.TypeID;
                tmpContact.Address = (Address)returnContact.Address!.CreateAddressEntity(timestamp, contextProfileName);
                newSalesReturn.Contacts.Add(new()
                {
                    Active = returnContact.Active,
                    CreatedDate = returnContact.CreatedDate,
                    Slave = tmpContact,
                });
            }
            newSalesReturn.SubtotalItems = newSalesReturn.SalesItems
                .Sum(x => x.TotalQuantity * (x.UnitSoldPrice ?? x.UnitCorePrice));
            newSalesReturn.SubtotalFees = 0m; // salesOrder.Totals.Fees; // TODO: As Negative of restocking fee?
            newSalesReturn.SubtotalShipping = 0m; // salesOrder.Totals.Shipping;
            newSalesReturn.SubtotalHandling = 0m; // salesOrder.Totals.Handling;
            newSalesReturn.SubtotalTaxes = 0m; // TODO: Run through tax provider
            newSalesReturn.SubtotalDiscounts = 0m; // TODO: Calc discounts against it
            newSalesReturn.Total = newSalesReturn.SubtotalItems
                + newSalesReturn.SubtotalItems
                + newSalesReturn.SubtotalFees
                + newSalesReturn.SubtotalShipping
                + newSalesReturn.SubtotalHandling
                + newSalesReturn.SubtotalTaxes
                - Math.Abs(newSalesReturn.SubtotalDiscounts);
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return (await Workflows.SalesReturns.GetAsync(newSalesReturn.ID, contextProfileName).ConfigureAwait(false))!;
        }

        /// <summary>Status change.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="status">            The status.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static async Task<CEFActionResponse> StatusChangeAsync(
            ISalesReturn entity,
            string status,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            if (entity.Status!.Name == status)
            {
                return CEFAR.FailingCEFAR($"INFO! No action taken as the return is already on status '{status}'");
            }
            if (!GeneratedSalesReturnStatusIDs.ContainsKey(status))
            {
                GeneratedSalesReturnStatusIDs[status] = await Workflows.SalesReturnStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: status,
                        byName: status,
                        byDisplayName: status,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (entity.StatusID == GeneratedSalesReturnStatusIDs[status])
            {
                // Save anyway, in case this is an in-memory change
                return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                    .BoolToCEFAR($"ERROR! Unable to save the new status '{status}' for the return.");
            }
            // Assign and then save
            entity.StatusID = GeneratedSalesReturnStatusIDs[status];
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR($"ERROR! Unable to save the new status '{status}' for the return.");
        }

        /// <summary>Adds the status change note.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="status">            The status.</param>
        /// <param name="previousStatus">    The previous status.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static async Task<CEFActionResponse> AddStatusChangeNoteAsync(
            IHaveNotesBase entity,
            string status,
            string previousStatus,
            int userID,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            privateNoteTypeID ??= await Workflows.NoteTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Private Note",
                    byName: "Private Note",
                    byDisplayName: "Private Note",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            entity.Notes!.Add(new()
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                TypeID = privateNoteTypeID.Value,
                CreatedByUserID = userID,
                Note1 = $"User ID: {userID} changed Status from {previousStatus} to {status}",
            });
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR($"ERROR! Unable to save the new status '{status}' for the return.");
        }

        /// <summary>Retrieves store return contact.</summary>
        /// <param name="salesOrder">        The sales order.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IContactModel.</returns>
        private static async Task<IContactModel?> RetrieveStoreReturnContactAsync(
            IAmFilterableByNullableStoreModel salesOrder,
            DateTime timestamp,
            string? contextProfileName)
        {
            if (salesOrder == null)
            {
                return null;
            }
            IContactModel? contactModel = null;
            // Retrieve Sales Order Store
            IStoreModel? storeModel = null;
            if (salesOrder.StoreID.HasValue)
            {
                storeModel = await Workflows.Stores.GetAsync(salesOrder.StoreID.Value, contextProfileName).ConfigureAwait(false);
            }
            storeModel ??= salesOrder.Store;
            // ReSharper disable once PossibleInvalidOperationException
            returnContactTypeID ??= await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Returning",
                    byName: "Returning",
                    byDisplayName: "Returning",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (storeModel != null)
            {
                // Retrieve Store Contact with Type 'Returning'
                if (storeModel.Contact?.TypeID == returnContactTypeID)
                {
                    contactModel = salesOrder.Store!.Contact;
                }
                else if (storeModel.StoreContacts != null)
                {
                    foreach (var storeContact in storeModel.StoreContacts.Where(x => x.Contact!.TypeID == returnContactTypeID))
                    {
                        contactModel = storeContact.Contact;
                        break;
                    }
                }
            }
            if (contactModel != null)
            {
                return contactModel;
            }
            // Create contact from configuration values
            if (!Contract.CheckAllValid(
                    CEFConfigDictionary.ReturnsDestinationAddressCity,
                    CEFConfigDictionary.ReturnsDestinationAddressStreet1,
                    CEFConfigDictionary.ReturnsDestinationAddressPostalCode))
            {
                throw new ArgumentException("ERROR! Return Destination Address is missing.");
            }
            var originCountryCode = !Contract.CheckValidKey(CEFConfigDictionary.ReturnsDestinationAddressCountryCode)
                ? "USA"
                : CEFConfigDictionary.ReturnsDestinationAddressCountryCode;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                contactModel = new ContactModel
                {
                    Active = true,
                    CreatedDate = timestamp,
                    TypeID = returnContactTypeID.Value,
                    Phone1 = CEFConfigDictionary.ReturnsDestinationAddressPhone,
                    Address = new()
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        Company = CEFConfigDictionary.ReturnsDestinationAddressCompany,
                        Street1 = CEFConfigDictionary.ReturnsDestinationAddressStreet1,
                        Street2 = CEFConfigDictionary.ReturnsDestinationAddressStreet2,
                        Street3 = CEFConfigDictionary.ReturnsDestinationAddressStreet3,
                        City = CEFConfigDictionary.ReturnsDestinationAddressCity,
                        RegionID = Contract.CheckValidKey(CEFConfigDictionary.ReturnsDestinationAddressRegionCode)
                            ? await context.Regions
                                .AsNoTracking()
                                .FilterByActive(true)
                                .FilterRegionsByCode(CEFConfigDictionary.ReturnsDestinationAddressRegionCode, true, false)
                                .Select(x => (int?)x.ID)
                                .SingleOrDefaultAsync()
                                .ConfigureAwait(false)
                            : null,
                        PostalCode = CEFConfigDictionary.ReturnsDestinationAddressPostalCode,
                        CountryID = await context.Countries
                            .AsNoTracking()
                            .FilterCountriesByCode(originCountryCode, true)
                            .Select(x => (int?)x.ID)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false),
                    },
                };
            }
            if (storeModel == null)
            {
                return contactModel;
            }
            // Add to Store Contacts
            storeModel.StoreContacts ??= new();
            var tmpStoreContacts = storeModel.StoreContacts;
            tmpStoreContacts.Add(new StoreContactModel
            {
                Active = true,
                CreatedDate = timestamp,
                Slave = (ContactModel)contactModel,
            });
            storeModel.StoreContacts = tmpStoreContacts;
            await Workflows.Stores.UpdateAsync(storeModel, contextProfileName).ConfigureAwait(false);
            return contactModel;
        }

        /// <summary>Adds the sales return items.</summary>
        /// <param name="timestamp">           The timestamp Date/Time.</param>
        /// <param name="newSalesReturn">      The new sales return.</param>
        /// <param name="salesReturnItemModel">The sales return item model.</param>
        /// <param name="salesOrderItemModel"> The sales order item model.</param>
        /// <param name="isBackendOverride">   True if this SalesReturnWorkflow is back-end override.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>A SalesReturnItem.</returns>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        private static async Task<SalesReturnItem> AddSalesReturnItemsAsync(
            DateTime timestamp,
            ISalesCollectionBase newSalesReturn,
            ISalesItemBaseModel<IAppliedSalesReturnItemDiscountModel> salesReturnItemModel,
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> salesOrderItemModel,
            bool isBackendOverride,
            string? contextProfileName)
        {
            var replacementDictionary = new Dictionary<string, string>
            {
                { "{{OrderID}}", salesOrderItemModel.MasterID.ToString() },
                { "{{ItemSku}}", salesOrderItemModel.ProductKey ?? salesOrderItemModel.Sku! },
            };
            // TODO@BE: if SKU is not set, look for the product's SKU
            var restockingFeeAmount = isBackendOverride
                ? salesReturnItemModel.RestockingFeeAmount
                : await CalculateRestockingFeeAsync(
                        salesReturnItemModel,
                        salesOrderItemModel,
                        contextProfileName)
                    .ConfigureAwait(false);
            var salesReturnItem = new SalesReturnItem
            {
                // Base Properties
                CustomKey = string.IsNullOrEmpty(CEFConfigDictionary.ReturnsNumberFormat)
                    ? null
                    : DoReplacements(replacementDictionary, CEFConfigDictionary.ReturnsNumberFormat),
                Active = true,
                CreatedDate = timestamp,
                // NameableBase Properties
                Name = salesOrderItemModel.ProductName ?? salesOrderItemModel.Name,
                Description = salesOrderItemModel.Description ?? salesOrderItemModel.ProductDescription,
                // SalesItemBase Properties
                Quantity = salesReturnItemModel.Quantity,
                ////QuantityBackOrdered = salesOrderItemModel.QuantityBackOrdered,
                ////QuantityPreSold = salesOrderItemModel.QuantityPreSold,
                Sku = salesOrderItemModel.ProductKey ?? salesOrderItemModel.Sku,
                ForceUniqueLineItemKey = salesOrderItemModel.ForceUniqueLineItemKey,
                UnitCorePrice = salesOrderItemModel.UnitCorePrice,
                UnitSoldPrice = salesOrderItemModel.UnitSoldPrice,
                UnitCorePriceInSellingCurrency = salesOrderItemModel.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = salesOrderItemModel.UnitSoldPriceInSellingCurrency,
                UnitOfMeasure = salesOrderItemModel.UnitOfMeasure,
                // TODO: Multi-currency Restocking Fee
                RestockingFeeAmount = restockingFeeAmount,
                // Related Objects
                MasterID = newSalesReturn?.ID ?? 0,
                ProductID = salesReturnItemModel.ProductID,
                OriginalCurrencyID = salesOrderItemModel.OriginalCurrencyID,
                SellingCurrencyID = salesOrderItemModel.SellingCurrencyID,
            };
            // Transfer shipment from cartItem to salesItem
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                    salesReturnItem,
                    salesOrderItemModel,
                    contextProfileName)
                .ConfigureAwait(false);
            if (salesReturnItemModel.Discounts != null && salesOrderItemModel.Discounts!.Count > 0)
            {
                salesReturnItem.Discounts ??= new List<AppliedSalesReturnItemDiscount>();
                foreach (var salesOrderItemDiscountModel in salesOrderItemModel.Discounts)
                {
                    salesReturnItem.Discounts.Add(new()
                    {
                        Active = salesOrderItemDiscountModel.Active,
                        CreatedDate = salesOrderItemDiscountModel.CreatedDate,
                        UpdatedDate = salesOrderItemDiscountModel.UpdatedDate,
                        CustomKey = salesOrderItemDiscountModel.CustomKey,
                        SlaveID = salesOrderItemDiscountModel.DiscountID,
                        DiscountTotal = salesOrderItemDiscountModel.DiscountTotal,
                        ApplicationsUsed = salesOrderItemDiscountModel.ApplicationsUsed,
                    });
                }
            }
            if (CEFConfigDictionary.ReturnsAreSingleCreation)
            {
                return salesReturnItem;
            }
            privateNoteTypeID ??= await Workflows.NoteTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Private Note",
                    byName: "Private Note",
                    byDisplayName: "Private Note",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            salesReturnItem.Notes!.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                TypeID = privateNoteTypeID.Value,
                CreatedByUserID = newSalesReturn?.UserID,
                Note1 = salesReturnItemModel.Description ?? "N/A",
            });
            return salesReturnItem;
        }

        /// <summary>Relate Required Status.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Status.</param>
        /// <param name="model">             The model that has a Required Status.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredStatusAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredStatusAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Status.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Status.</param>
        /// <param name="model">    The model that has a Required Status.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredStatusAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesReturnStatuses.ResolveWithAutoGenerateAsync(
                    byID: model.StatusID, // By Other ID
                    byKey: model.StatusKey, // By Flattened Other Key
                    byName: model.StatusName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Status,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StatusID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Status == null;
            if (resolved.Result == null && model.Status != null)
            {
                resolved.Result = model.Status;
            }
            var modelObjectIsNull = resolved == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable StatusID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StatusID == resolved!.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Status!.ID == resolved!.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Status!.UpdateSalesReturnStatusFromModel(resolved!.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StatusID = resolved!.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved!.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved!.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive StatusID to the SalesReturn entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.StatusID = resolved!.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StatusID = 0;
                entity.Status = (SalesReturnStatus)resolved!.Result!.CreateSalesReturnStatusEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Status to the SalesReturn entity");
        }

        /// <summary>Relate Required State.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required State.</param>
        /// <param name="model">             The model that has a Required State.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredStateAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredStateAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required State.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required State.</param>
        /// <param name="model">    The model that has a Required State.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredStateAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesReturnStates.ResolveWithAutoGenerateAsync(
                    byID: model.StateID, // By Other ID
                    byKey: model.StateKey, // By Flattened Other Key
                    byName: model.StateName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.State,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StateID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.State == null;
            if (resolved.Result == null && model.State != null)
            {
                resolved.Result = model.State;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable StateID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StateID == resolved!.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.State!.ID == resolved!.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.State!.UpdateSalesReturnStateFromModel(resolved!.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StateID = resolved!.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved!.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved!.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive StateID to the SalesReturn entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.StateID = resolved!.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StateID = 0;
                entity.State = (SalesReturnState)resolved!.Result!.CreateSalesReturnStateEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given State to the SalesReturn entity");
        }

        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Type.</param>
        /// <param name="model">             The model that has a Required Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredTypeAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredTypeAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Type.</param>
        /// <param name="model">    The model that has a Required Type.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredTypeAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesReturnTypes.ResolveWithAutoGenerateAsync(
                    byID: model.TypeID, // By Other ID
                    byKey: model.TypeKey, // By Flattened Other Key
                    byName: model.TypeName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Type,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.TypeID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Type == null;
            if (resolved.Result == null && model.Type != null)
            {
                resolved.Result = model.Type;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable TypeID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.TypeID == resolved!.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Type!.ID == resolved!.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Type!.UpdateSalesReturnTypeFromModel(resolved!.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.TypeID = resolved!.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved!.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved!.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive TypeID to the SalesReturn entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.TypeID = resolved!.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.TypeID = 0;
                entity.Type = (SalesReturnType)resolved!.Result!.CreateSalesReturnTypeEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Type to the SalesReturn entity");
        }

        /// <summary>Validates the return for refund.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private async Task<CEFActionResponse<SalesReturn>> ValidateReturnForRefundAsync(
            int id,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            confirmedStatusID ??= await Workflows.SalesReturnStatuses.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Confirmed",
                    "Confirmed",
                    "Confirmed",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            var salesReturn = await context.SalesReturns
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return salesReturn == null
                ? CEFAR.FailingCEFAR<SalesReturn>($"ERROR! Invalid sales return ID: {id}.")
                : salesReturn.StatusID != confirmedStatusID
                    ? CEFAR.FailingCEFAR<SalesReturn>($"ERROR! Invalid current status for sales return ID: {id}.")
                    : salesReturn.RefundAmount <= 0
                        ? CEFAR.FailingCEFAR<SalesReturn>($"ERROR! Invalid refund amount for sales return ID: {id}.")
                        : salesReturn.WrapInPassingCEFAR()!;
        }
    }
}
