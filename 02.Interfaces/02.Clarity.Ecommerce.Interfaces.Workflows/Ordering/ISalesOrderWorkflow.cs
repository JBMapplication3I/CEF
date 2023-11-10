// <copyright file="ISalesOrderWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesOrderWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Providers.Taxes;

    public partial interface ISalesOrderWorkflow
    {
        /// <summary>Gets by payoneer order identifier.</summary>
        /// <param name="payoneerOrderID">   Identifier for the payoneer order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by payoneer order identifier.</returns>
        Task<ISalesOrderModel?> GetByPayoneerOrderIDAsync(long payoneerOrderID, string? contextProfileName);

        /// <summary>Gets the sales orders distinct products for accounts in this collection.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process the sales orders distinct products for
        /// accounts in this collection.</returns>
        Task<IEnumerable<int?>> GetSalesOrdersDistinctProductsForAccountAsync(int accountID, string? contextProfileName);

        /// <summary>Gets the sales order only if the supplied AccountID exists on the order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountIds">        List of identifiers for the accounts.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{ISalesOrderModel}.</returns>
        Task<ISalesOrderModel> SecureSalesOrderAsync(int id, List<int> accountIds, string? contextProfileName);

        /// <summary>Confirm order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ConfirmOrderAsync(int id, string? contextProfileName);

        /// <summary>re-pend an order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> PendingOrderAsync(int id, string? contextProfileName);

        /// <summary>Hold order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> HoldOrderAsync(int id, string? contextProfileName);

        /// <summary>Back order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> BackOrderOrderAsync(int id, string? contextProfileName);

        /*
        /// <summary>Splits sales order into sub orders based on item statuses.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="sendEmail">         True to send email.</param>
        /// <returns>A CEFActionResponse{ISalesOrderModel[]}.</returns>
        Task<CEFActionResponse<ISalesOrderModel[]>> SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(
            int id,
            string? contextProfileName,
            bool sendEmail = true);
        */

        /// <summary>Adds a payment to order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> AddPaymentToOrderAsync(int id, IPaymentModel payment, string? contextProfileName);

        /// <summary>Capture payment for order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CapturePaymentForOrderAsync(int id, string? contextProfileName);

        /// <summary>Creates pick ticket for order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new pick ticket for order.</returns>
        Task<CEFActionResponse<List<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>>> CreatePickTicketForOrderAsync(
            int id,
            string? contextProfileName);

        /// <summary>Drop ship order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{IPurchaseOrderModel}.</returns>
        Task<CEFActionResponse<IPurchaseOrderModel>> DropShipOrderAsync(int id, string? contextProfileName);

        /// <summary>Ready for pickup order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReadyForPickupOrderAsync(int id, string? contextProfileName);

        /// <summary>Ship order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ShipOrderAsync(int id, string? contextProfileName);

        /// <summary>Complete order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CompleteOrderAsync(int id, string? contextProfileName);

        /// <summary>Void order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="taxesProvider">       The tax provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> VoidOrderAsync(int id, ITaxesProviderBase? taxesProvider, string? contextProfileName);

        /// <summary>Mark items in stock.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="guid">              Unique identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> MarkItemsInStockAsync(int id, Guid guid, string? contextProfileName);

        /// <summary>Mark items not in stock.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="guid">              Unique identifier.</param>
        /// <param name="taxesProvider">       The tax provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> MarkItemsNotInStockAsync(
            int id,
            Guid guid,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Gets sales order by user and event.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="calendarEventID">   Identifier for the calendar event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales order by user and event.</returns>
        Task<CEFActionResponse<ISalesOrderModel?>> GetSalesOrderByUserAndEventAsync(
            int userID,
            int calendarEventID,
            string? contextProfileName);

        /// <summary>Updates the status of sales order.</summary>
        /// <param name="statusUpdate">      The status update.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="salesOrderID">      Identifier for the sales order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UpdateStatusOfSalesOrderAsync(
            string statusUpdate,
            int productID,
            int quantity,
            int salesOrderID,
            string? contextProfileName);

        /// <summary>Gets on demand subscriptions by user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The on demand subscriptions by user.</returns>
        Task<(List<ISubscriptionModel> results, int totalPages, int totalCount)> GetOnDemandSubscriptionsByUserAsync(
            int userID,
            ISubscriptionSearchModel search,
            string? contextProfileName);

        /// <summary>Gets subscription by sales order.</summary>
        /// <param name="salesOrderID">      Identifier for the sales order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The subscription by sales group.</returns>
        Task<ISubscriptionModel?> GetSubscriptionBySalesOrder(
            int salesOrderID,
            string? contextProfileName);

        /// <summary>Gets subscription history by sub identifier.</summary>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The subscription history by sub identifier.</returns>
        Task<(List<ISubscriptionHistoryModel> results, int totalPages, int totalCount)> GetSubscriptionHistoryBySubID(
            int subscriptionID,
            ISubscriptionHistorySearchModel search,
            string? contextProfileName);

        /// <summary>Refill on demand subscription.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="subscriptionID">    Identifier for the subscription.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> RefillOnDemandSubscriptionAsync(
            int userID,
            int subscriptionID,
            string? contextProfileName);

        /// <summary>Edits the sales order for store admin.</summary>
        /// <param name="salesOrderId">                The sales order identifier.</param>
        /// <param name="productId">                The product identifier.</param>
        /// <param name="factoryContext">                The pricing factory context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{ISalesOrderModel}.</returns>
        Task<CEFActionResponse> EditSalesOrderAsync(
            int salesOrderId,
            int productId,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName);

        /// <summary>Cancels the subscription for the user.</summary>
        /// <param name="salesOrderID">      Identifier for the sales order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> CancelSubscriptionAsync(
            int salesOrderID,
            string? contextProfileName);
    }
}
