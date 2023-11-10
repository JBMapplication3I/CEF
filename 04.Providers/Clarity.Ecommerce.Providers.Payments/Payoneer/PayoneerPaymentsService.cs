// <copyright file="PayoneerPaymentsService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Payoneer payments service class</summary>
// ReSharper disable UnusedAutoPropertyAccessor.Global, UnusedMember.Global, UnusedParameter.Global
#pragma warning disable 1584, 1591, 1658
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using Newtonsoft.Json;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A create a payoneer account for current store.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin,
     Authenticate,
     Route("/Payments/CurrentStore/CreateAPayoneerAccount", "POST", Summary = "")]
    public class CreateAPayoneerAccountForCurrentStore : IReturn<CEFActionResponse<string>>
    {
    }

    /// <summary>A create a payoneer account for current user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/CurrentUser/CreateAPayoneerAccount", "POST", Summary = "")]
    public class CreateAPayoneerAccountForCurrentUser : IReturn<CEFActionResponse<string>>
    {
    }

    /// <summary>An assign payoneer account user for current store.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/CurrentStore/AssignPayoneerAccountUser", "GET", Summary = "")]
    public class AssignPayoneerAccountUserForCurrentStore : IReturn<CEFActionResponse>
    {
    }

    /// <summary>An assign payoneer account user for current user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/CurrentUser/AssignPayoneerAccountUser", "GET", Summary = "")]
    public class AssignPayoneerAccountUserForCurrentUser : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A get authed URL for store owner to set up payout accounts for current store.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin,
     Authenticate,
     Route("/Payments/CurrentStore/GetPayoneerAuthedURLForStoreOwnerToSetUpPayoutAccounts", "GET", Summary = "")]
    public class GetAuthedURLForStoreOwnerToSetUpPayoutAccountsForCurrentStore : IReturn<CEFActionResponse<string>>
    {
    }

    /// <summary>A get authed URL for buyer to set up payment accounts.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/CurrentStore/GetPayoneerAuthedURLForBuyerToSetUpPaymentAccounts", "GET", Summary = "")]
    public class GetAuthedURLForBuyerToSetUpPaymentAccounts : IReturn<CEFActionResponse<string>>
    {
    }

    /* Note: This happens during Checkout process
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/CreateAPayoneerEscrowOrderToFacilitateATransactionForGoods/CurrentCart", "GET", Summary = "")]
    public class CreateAnEscrowOrderToFacilitateATransactionForGoods : IReturn<CEFActionResponse> { }*/

    /// <summary>A get payment instructions URL for escrow order.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/GetPaymentInstructionsUrlForEscrowOrder/{OrderID}/{PayoneerAccountID}/{PayoneerCustomerID}", "POST", Summary = "")]
    public class GetPaymentInstructionsUrlForEscrowOrder : IReturn<CEFActionResponse<string>>
    {
        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [ApiMember(Name = "OrderID", DataType = "int", ParameterType = "path", IsRequired = true)]
        public int OrderID { get; set; }

        /// <summary>Gets or sets the identifier of the payoneer account.</summary>
        /// <value>The identifier of the payoneer account.</value>
        [ApiMember(Name = "PayoneerAccountID", DataType = "long?", ParameterType = "path", IsRequired = false)]
        public long? PayoneerAccountID { get; set; }

        /// <summary>Gets or sets the identifier of the payoneer customer.</summary>
        /// <value>The identifier of the payoneer customer.</value>
        [ApiMember(Name = "PayoneerCustomerID", DataType = "long?", ParameterType = "path", IsRequired = false)]
        public long? PayoneerCustomerID { get; set; }
    }

    /// <summary>A get authed URL for store owner to add a tracking number to the escrow order.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin,
     Authenticate,
     Route("/Payments/GetPayoneerAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder/OrderID/{OrderID}", "GET", Summary = "")]
    public class GetAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder : IReturn<CEFActionResponse<string>>
    {
        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [ApiMember(Name = "OrderID", DataType = "int", ParameterType = "path", IsRequired = true)]
        public int OrderID { get; set; }
    }

    /// <summary>A get authed URL to release funds for escrow order.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{string}}"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin,
     Authenticate,
     Route("/Payments/GetPayoneerAuthedURLToReleaseFundsForEscrowOrder/OrderID/{OrderID}", "GET", Summary = "")]
    public class GetAuthedURLToReleaseFundsForEscrowOrder : IReturn<CEFActionResponse<string>>
    {
        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [ApiMember(Name = "OrderID", DataType = "int", ParameterType = "path", IsRequired = true)]
        public int OrderID { get; set; }
    }

    /// <summary>A Payoneer order event webhook return.</summary>
    /// <seealso cref="IReturnVoid"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Payments/Payoneer/OrderEventWebhooks", "POST", Summary = "")]
    public class PayoneerOrderEventWebhookReturn : IReturnVoid
    {
        /// <summary>Gets or sets the API key.</summary>
        /// <value>The API key.</value>
        [JsonProperty("api_key"), DataMember(Name = "api_key"), ApiMember(Name = "api_key", DataType = "ApiKeyWebhookReturn", ParameterType = "body", IsRequired = false)]
        public ApiKeyWebhookReturn? APIKey { get; set; }

        /// <summary>Gets or sets the event.</summary>
        /// <value>The event.</value>
        [JsonProperty("event"), DataMember(Name = "event"), ApiMember(Name = "event", DataType = "OrderEventWebhookReturn", ParameterType = "body", IsRequired = false)]
        public OrderEventWebhookReturn? Event { get; set; }

        /// <summary>Gets or sets the order.</summary>
        /// <value>The order.</value>
        [JsonProperty("order"), DataMember(Name = "order"), ApiMember(Name = "order", DataType = "OrderWebhookReturn", ParameterType = "body", IsRequired = false)]
        public OrderWebhookReturn? Order { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        [JsonProperty("account"), DataMember(Name = "account"), ApiMember(Name = "account", DataType = "AccountWebhookReturn", ParameterType = "body", IsRequired = false)]
        public AccountWebhookReturn? Account { get; set; }
    }

    /// <summary>A Payoneer payments service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class PayoneerPaymentsService : ClarityEcommerceServiceBase
    {
        /// <summary>Post this message.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(CreateAPayoneerAccountForCurrentStore _)
        {
            var currentStoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            var store = await Workflows.Stores.GetAsync(currentStoreID, contextProfileName: null).ConfigureAwait(false);
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var provider = new PayoneerPaymentsProvider();
            if (!provider.CreateAPayoneerAccount(store, user))
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The store object will now have new attribute values
            await Workflows.Stores.UpdateAsync(store!, contextProfileName: null).ConfigureAwait(false);
            var acctID = user.SerializableAttributes!["Payoneer-Account-ID"].Value;
            var userID = user.SerializableAttributes["Payoneer-User-ID"].Value;
            var url = user.SerializableAttributes["Payoneer-Account-URL"].Value;
            return new CEFActionResponse<string>($"{acctID}|{userID}|{url}", true);
        }

        /// <summary>Post this message.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(CreateAPayoneerAccountForCurrentUser _)
        {
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var provider = new PayoneerPaymentsProvider();
            if (!provider.CreateAPayoneerAccount(null, user))
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The user object will now have new attribute values
            await Workflows.Users.UpdateAsync(user, contextProfileName: null).ConfigureAwait(false);
            var acctID = user.SerializableAttributes!["Payoneer-Account-ID"].Value;
            var url = user.SerializableAttributes["Payoneer-Account-URL"].Value;
            var userID = user.SerializableAttributes["Payoneer-User-ID"].Value;
            return new CEFActionResponse<string>($"{acctID}|{userID}|{url}", true);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="_">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(AssignPayoneerAccountUserForCurrentStore _)
        {
            var currentStoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            var store = (await Workflows.Stores.GetAsync(currentStoreID, contextProfileName: null).ConfigureAwait(false))!;
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var provider = new PayoneerPaymentsProvider();
            if (!provider.AssignAccountUser(store, user))
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse(false);
            }
            // The user object will now have new attribute values
            await Workflows.Users.UpdateAsync(user, contextProfileName: null).ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="_">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(AssignPayoneerAccountUserForCurrentUser _)
        {
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var provider = new PayoneerPaymentsProvider();
            if (!provider.AssignAccountUser(null, user))
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse(false);
            }
            // The user object will now have new attribute values
            await Workflows.Users.UpdateAsync(user, contextProfileName: null).ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="_">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(GetAuthedURLForStoreOwnerToSetUpPayoutAccountsForCurrentStore _)
        {
            var currentStoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            var store = await Workflows.Stores.GetAsync(currentStoreID, contextProfileName: null).ConfigureAwait(false);
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var provider = new PayoneerPaymentsProvider();
            var result = provider.GetAnAuthenticatedURLForStoreOwnersToSetUpPayoutAccountsForCurrentStore(store, user);
            if (result == null)
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The user object will now have new attribute values
            await Workflows.Users.UpdateAsync(user, contextProfileName: null).ConfigureAwait(false);
            return new CEFActionResponse<string>(result, true);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="_">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(GetAuthedURLForBuyerToSetUpPaymentAccounts _)
        {
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var provider = new PayoneerPaymentsProvider();
            var result = provider.GetAnAuthenticatedURLForStoreOwnersToSetUpPayoutAccountsForCurrentStore(null, user);
            if (result == null)
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The user object will now have new attribute values
            await Workflows.Users.UpdateAsync(user, contextProfileName: null).ConfigureAwait(false);
            return new CEFActionResponse<string>(result, true);
        }

        /*public CEFActionResponse Get(CreateAnEscrowOrderToFacilitateATransactionForGoods request)
        {
            PickupCartCookie();
            var buyer = CurrentUserOrThrow401();
            var currentCart = Workflows.Carts.Get(
                SessionCartGuid, PricingFactoryContext, "Cart", GetTaxProvider(), buyer.ID);
            if (!currentCart.SalesItems.Any())
            {
                return CEFAR.FailingCEFAR("No items in cart.");
            }
            var itemWithStore = currentCart.SalesItems
                .FirstOrDefault(x => x.StoreID.HasValue && x.StoreID > 0
                    || x.StoreProduct != null && x.StoreProduct.StoreID > 0);
            if (itemWithStore == null)
            {
                return CEFAR.FailingCEFAR("No items in cart that have a store assigned.");
            }
            var store = await Workflows.Stores.GetAsync(itemWithStore.StoreID.HasValue && itemWithStore.StoreID > 0
                ? itemWithStore.StoreID.Value
                : itemWithStore.StoreProduct.StoreID);
            var sellerID = store.StoreUsers
                .FirstOrDefault(x => x.Active
                    && Workflows.Authentication.GetRolesForUser(x.UserID)
                        .Any(y => y.Name == "CEF Store Administrator"))?.UserID;
            if (!sellerID.HasValue || sellerID.Value <= 0 || sellerID.Value == int.MaxValue)
            {
                return CEFAR.FailingCEFAR("Could not locate the administrator user for the store assigned.");
            }
            var seller = Workflows.Users.Get(sellerID.Value);
            if (seller == null)
            {
                return CEFAR.FailingCEFAR("No items in cart that have a store assigned.");
            }
            if (!PayoneerPaymentsProvider.CreateAnEscrowOrderToFacilitateATransactionForGoods(
                    store, seller, buyer, currentCart))
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse(false);
            }
            // The cart object will now have new attribute values
            Workflows.Carts.Upda te(currentCart);
            return CEFAR.PassingCEFAR();
        }*/

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(GetPaymentInstructionsUrlForEscrowOrder request)
        {
            var buyer = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var order = await Workflows.SalesOrders.GetAsync(request.OrderID, contextProfileName: null).ConfigureAwait(false);
            Contract.RequiresValidID(order!.StoreID);
            var provider = new PayoneerPaymentsProvider();
            var result = provider.GetAnAuthenticatedURLForUsersToBePresentedWithPaymentInstructions(
                buyer: buyer,
                salesCollection: order,
                payoneerAccountID: Contract.CheckValidID(request.PayoneerAccountID) ? request.PayoneerAccountID : null,
                payoneerCustomerID: Contract.CheckValidID(request.PayoneerCustomerID) ? request.PayoneerCustomerID : null);
            if (result == null)
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The user object will now have new attribute values
            await Workflows.Users.UpdateAsync(buyer, contextProfileName: null).ConfigureAwait(false);
            // The order object will now have new attribute values
            await Workflows.SalesOrders.UpdateAsync(order, contextProfileName: null).ConfigureAwait(false);
            return new CEFActionResponse<string>(result, true);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(GetAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder request)
        {
            var currentStoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            var store = (await Workflows.Stores.GetAsync(currentStoreID, contextProfileName: null).ConfigureAwait(false))!;
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var order = (await Workflows.SalesOrders.GetAsync(request.OrderID, contextProfileName: null).ConfigureAwait(false))!;
            var provider = new PayoneerPaymentsProvider();
            var result = provider.GetAnAuthenticatedURLForStoreOwnersToAddATrackingNumberToTheEscrowOrder(
                store: store,
                user: user,
                order: order);
            if (result == null)
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The sales order object will now have new attribute values
            await Workflows.SalesOrders.UpdateAsync(order, contextProfileName: null).ConfigureAwait(false);
            return new CEFActionResponse<string>(result, true);
        }

        /// <summary>Gets a task{object} using the given request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(GetAuthedURLToReleaseFundsForEscrowOrder request)
        {
            var buyer = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var order = (await Workflows.SalesOrders.GetAsync(request.OrderID, contextProfileName: null).ConfigureAwait(false))!;
            var provider = new PayoneerPaymentsProvider();
            var result = provider.GetAnAuthenticatedReleaseFundsURLForAnEscrowOrder(
                user: buyer,
                order: order);
            if (result == null)
            {
                // TODO: Log an error or throw?
                return new CEFActionResponse<string>(false);
            }
            // The sales order object will now have new attribute values
            await Workflows.SalesOrders.UpdateAsync(order, contextProfileName: null).ConfigureAwait(false);
            return new CEFActionResponse<string>(result, true);
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        public async Task Post(PayoneerOrderEventWebhookReturn request)
        {
            // Ensure the Payoneer provider has loaded
            // ReSharper disable once UnusedVariable
            System.Diagnostics.Debug.WriteLine(Request.GetRawBody());
            var eventMessage = string.Empty;
            eventMessage += "====== Raw Body START ========================================\r\n"
                          + Request.GetRawBody() + "\r\n"
                          + "====== Raw Body END ==========================================\r\n"
                          + "====== Parsed Body START =====================================\r\n"
                          + request.ToJson() + "\r\n"
                          + "====== Parsed Body END =======================================\r\n";
            await Workflows.EventLogs.AddEventAsync(
                    message: eventMessage,
                    name: "PayoneerOrderEventWebhookReturn",
                    customKey: $"{request.Order?.OrderID ?? request.Account?.AccountID}",
                    dataID: null,
                    contextProfileName: null)
                .ConfigureAwait(false);
            System.Diagnostics.Debug.WriteLine(eventMessage);
            if (request.Order != null)
            {
                try
                {
                    await PayoneerPaymentsProvider.HandleWebhookEventAsync(
                            workflows: Workflows,
                            apiKeyReturn: request.APIKey!,
                            orderEventReturn: request.Event!,
                            orderReturn: request.Order, contextProfileName: null)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    var message = eventMessage;
                    var inner = ex;
                    while (inner != null)
                    {
                        message += "====== Exception START =====================================\r\n"
                                 + new { inner.Message, inner.Data, inner.StackTrace }.ToJson() + "\r\n"
                                 + "====== Exception END =======================================\r\n";
                        inner = inner.InnerException;
                    }
                    await Workflows.EventLogs.AddEventAsync(
                            message,
                            "PayoneerOrderEventWebhookReturn.Failed",
                            request.Order.OrderID?.ToString(),
                            null,
                            contextProfileName: null)
                        .ConfigureAwait(false);
                    throw;
                }
            }
            // ReSharper disable once InvertIf
            else if (request.Account != null)
            {
                await Workflows.EventLogs.AddEventAsync(
                        message: Request.GetRawBody(),
                        name: "PayoneerOrderEventWebhookReturn.Ignored",
                        customKey: request.Account.AccountID?.ToString(),
                        dataID: null,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                // TODO: Process Account events
            }
        }
    }

    /// <summary>A Payoneer payments feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class PayoneerPaymentsFeature : IPlugin
    {
        /// <summary>Registers this PayoneerPaymentsFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
