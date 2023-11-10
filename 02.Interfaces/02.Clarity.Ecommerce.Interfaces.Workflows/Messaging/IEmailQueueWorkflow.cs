// <copyright file="IEmailQueueWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEmailQueueWorkflow interface</summary>
// ReSharper disable InconsistentNaming, InvalidXmlDocComment
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for email queue workflow.</summary>
    public partial interface IEmailQueueWorkflow
    {
        /// <summary>Adds an email to queue.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{int}}.</returns>
        Task<CEFActionResponse<int>> AddEmailToQueueAsync(IEmailQueueModel model, string? contextProfileName);

        /// <summary>Sends an email.</summary>
        /// <param name="email">                The email.</param>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <param name="emailSettings">        The email settings.</param>
        /// <param name="attachmentPath">       Full path name of the attachment file.</param>
        /// <param name="attachmentType">       Type of the attachment.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{int}}.</returns>
        Task<CEFActionResponse<int>> FormatAndQueueEmailAsync(
            string? email,
            Dictionary<string, string?> replacementDictionary,
            IEmailSettings emailSettings,
            IReadOnlyCollection<string?>? attachmentPath,
            Enums.FileEntityType? attachmentType,
            string? contextProfileName);

        /// <summary>Generates a result.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The result, with modification if failed.</returns>
        Task<CEFActionResponse<int>> GenerateResultAsync(CEFActionResponse<int> result);

        /// <summary>Dequeue email.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        Task<bool> DequeueEmailAsync(int id, string? contextProfileName);

        /// <summary>Dequeue email.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        Task<bool> DequeueEmailAsync(string key, string? contextProfileName);

#if FALSE
        /// <summary>Sends an invite code email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="fromUser">          from user.</param>
        /// <param name="inviteCode">        The invite code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueInviteCodeEmailAsync(string email, IUserModel fromUser, string inviteCode, string? contextProfileName);

        /// <summary>Sends a forgot password email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="username">          The username.</param>
        /// <param name="resetToken">        The reset token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueForgotPasswordEmailAsync(string email, string username, string resetToken, string? contextProfileName);

        /// <summary>Sends an account validation email.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="passwordResetToken">The password reset token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        Task<CEFActionResponse<int>> QueueAccountValidationEmailAsync(ICreateLiteAccountAndSendValidationEmail model, string passwordResetToken, string? contextProfileName);

        /// <summary>Sends an account validation email no reset.</summary>
        /// <param name="userModel">         The user model.</param>
        /// <param name="token">             The token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueAccountValidationEmailNoResetAsync(IUserModel userModel, string token, string? contextProfileName);

        /// <summary>Sends a product now in stock notification email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="productName">       Name of the product.</param>
        /// <param name="productSeoUrl">     URL of the product seo.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueProductNowInStockNotificationEmailAsync(string email, string productName, string productSeoUrl, string? contextProfileName);

        /// <summary>Queue product removed from cart notification email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{Int32}}.</returns>
        Task<CEFActionResponse<int>> QueueProductRemovedFromCartNotificationEmailAsync(string email, IProductModel product, string? contextProfileName);

        /// <summary>Queue product in cart notification email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{Int32}}.</returns>
        Task<CEFActionResponse<int>> QueueProductInCartNotificationEmailAsync(string email, IProductModel product, string? contextProfileName);

        /// <summary>Queue sales order customer notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="cc">                The Cc.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_Customer_ReceiptAsync(ISalesOrderModel order, string cc, string? contextProfileName);

        /// <summary>Queue sales order customer notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_Forward_Customer_ReceiptAsync(ISalesOrderModel order, string email, string? contextProfileName);

        /// <summary>Sends the sales order customer notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_Customer_Submitted_NormalAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order customer store pickup notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_Customer_Submitted_StorePickupAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Queue warehouse shipping information.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="orderID">           Identifier for the order.</param>
        /// <param name="regionCode">        The region code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Warehouse_Shipping_InformationAsync(ICartModel cart, int orderID, string regionCode, string? contextProfileName);

        /// <summary>Queue sales order back office notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_BackOffice_Submitted_NormalAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Queue sales order back office store pickup notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_BackOfficeStore_Submitted_StorePickupAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Queue sales order back office store notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> Queue_Notifications_SalesOrders_BackOfficeStore_Submitted_NormalAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order confirmed notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderConfirmedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Queue sales order updated notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderUpdatedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order on hold notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderOnHoldNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order off of on hold notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderOffHoldNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order back order notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderBackOrderNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order split notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderSplitNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order invoice notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="invoice">           The invoice.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderInvoiceNotificationAsync(ISalesOrderModel order, ISalesInvoiceModel invoice, string? contextProfileName);

        /// <summary>Sends the sales order partial payment notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderPartialPaymentNotificationAsync(ISalesOrderModel order, IPaymentModel payment, string? contextProfileName);

        /// <summary>Sends the sales order full payment notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderFullPaymentNotificationAsync(ISalesOrderModel order, IPaymentModel payment, string? contextProfileName);

        /// <summary>Sends the sales order processing notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderProcessingNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order drop ship notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="purchaseOrder">     The purchase order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderDropShipNotificationAsync(ISalesOrderModel order, IPurchaseOrderModel purchaseOrder, string? contextProfileName);

        /// <summary>Sends the sales order shipped notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderShippedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order ready for pickup notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderReadyForPickupNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order completed notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderCompletedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order voided notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesOrderVoidedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales invoice voided notification.</summary>
        /// <param name="invoice">           The invoice.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesInvoiceVoidedNotificationToCustomerAsync(ISalesInvoiceModel invoice, string? contextProfileName);

        /// <summary>Sends a new message waiting notification to recipients.</summary>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{CEFActionResponse{Int32}}.</returns>
        Task<List<CEFActionResponse<int>>> QueueNewMessageWaitingNotificationToRecipientsAsync(IMessageModel message, string? contextProfileName);

        /// <summary>Sends a user account completed registration user notification.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueUserAccountCompletedRegistrationUserNotificationAsync(IUserModel user, string? contextProfileName);

        /// <summary>Sends a user account completed registration back office notification.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueUserAccountCompletedRegistrationBackOfficeNotificationAsync(IUserModel user, string? contextProfileName);

        /// <summary>Sends a user account updated back office notification.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueUserAccountProfileUpdatedNotificationAsync(IUserModel user, string? contextProfileName);

        /// <summary>Sends a seller account completed registration seller notification.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="store">             The store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSellerAccountCompletedRegistrationSellerNotificationAsync(IUserModel user, IStoreModel store, string? contextProfileName);

        /// <summary>Sends a seller account completed registration back office notification.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="store">             The store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSellerAccountCompletedRegistrationBackOfficeNotificationAsync(IUserModel user, IStoreModel store, string? contextProfileName);

        /// <summary>Sends a user account approved notification.</summary>
        /// <param name="userModel">         The user model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueUserAccountApprovedNotificationAsync(IUserModel userModel, string? contextProfileName);

        /// <summary>Sends the sales return confirmed notification.</summary>
        /// <param name="return">The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnConfirmedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return shipped notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnShippedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return completed notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnCompletedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return voided notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnVoidedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return rejected notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnRejectedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return validated notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnValidatedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return cancelled notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnCancelledNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return refunded notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnRefundedNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Sends the sales return payment sent notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> SendSalesReturnPaymentSentNotificationAsync(ISalesReturnModel @return, IPaymentModel payment, string? contextProfileName);

        /// <summary>Queue sales return internal notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesReturnInternalNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Queue sales return customer notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesReturnCustomerNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Queue sales return internal store notification.</summary>
        /// <param name="return">            The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesReturnInternalStoreNotificationAsync(ISalesReturnModel @return, string? contextProfileName);

        /// <summary>Queue sales quote rfq submitted to user email.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesQuoteRFQSubmittedToUserEmailAsync(ISalesQuoteModel quote, string? contextProfileName);

        /// <summary>Queue Cart Items Submitted To Email.</summary>
        /// <param name="cartItems">         The cart items.</param>
        /// <param name="userModel">         The user model.</param>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueCartItemsSubmittedToEmailAsync(IEnumerable<ISalesItemBaseModel> cartItems, IUserModel userModel, string email, string? contextProfileName);

        /// <summary>Queue sales quote rfq submitted to store back office email.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesQuoteRFQSubmittedToStoreBackOfficeEmailAsync(ISalesQuoteModel quote, string? contextProfileName);

        /// <summary>Queue sales quote rfq submitted to back office email.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesQuoteRFQSubmittedToBackOfficeEmailAsync(ISalesQuoteModel quote, string? contextProfileName);

        /// <summary>Queue conversation copy to user email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="messageContents">   The message contents.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueConversationCopyToUserEmailAsync(string email, string messageContents, string? contextProfileName);

        /// <summary>Queue generic email.</summary>
        /// <param name="queueModel">        The queue model.</param>
        /// <param name="replacements">      The replacements.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueGenericEmailAsync(IEmailQueueModel queueModel, Dictionary<string, string> replacements, string? contextProfileName);

        /// <summary>Queue weekly event report group leader.</summary>
        /// <param name="evtModel">          The event model.</param>
        /// <param name="userModel">         The user model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueWeeklyEventReportGroupLeaderAsync(ICalendarEventModel evtModel, IUserModel userModel, string? contextProfileName);

        /// <summary>Queue CalendarEvents Assistance Request notifications to back office.</summary>
        /// <param name="email">             Email of user making the request.</param>
        /// <param name="fromDate">          Request From Date.</param>
        /// <param name="toDate">            Request To Date.</param>
        /// <param name="requestValues">     Request Values.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{Int32}.</returns>
        Task<CEFActionResponse<int>> QueueCalendarEventsAssistanceRequestNotificationsToBackOfficeAsync(string email, DateTime? fromDate, DateTime? toDate, Dictionary<string, string> requestValues, string? contextProfileName);

        /// <summary>Queue package changed notifications to back office.</summary>
        /// <param name="evtModel">          The event model.</param>
        /// <param name="userModel">         The user model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueuePackageChangedNotificationsToBackOfficeAsync(ICalendarEventModel evtModel, IUserModel userModel, string? contextProfileName);

        /// <summary>Queue new registration notifications to group leader.</summary>
        /// <param name="evtModel">          The event model.</param>
        /// <param name="groupLeader">       The group leader.</param>
        /// <param name="newRegisteredUser"> The new registered user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueNewRegistrationNotificationsToGroupLeaderAsync(ICalendarEventModel evtModel, IUserModel groupLeader, IUserModel newRegisteredUser, string? contextProfileName);

        /// <summary>Queue event maximum capacity to group leader.</summary>
        /// <param name="evtModel">          The event model.</param>
        /// <param name="groupLeader">       The group leader.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueEventMaxCapacityToGroupLeaderAsync(ICalendarEventModel evtModel, IUserModel groupLeader, string? contextProfileName);

        /// <summary>Queue passenger cancellation to back office.</summary>
        /// <param name="evtModel">          The event model.</param>
        /// <param name="userModel">         The user model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueuePassengerCancellationToBackOfficeAsync(ICalendarEventModel evtModel, IUserModel userModel, string? contextProfileName);

        /// <summary>Queue passenger cancellation group leader.</summary>
        /// <param name="evtModel">          The event model.</param>
        /// <param name="groupLeader">       The group leader.</param>
        /// <param name="newRegisteredUser"> The new registered user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueuePassengerCancellationGroupLeaderAsync(ICalendarEventModel evtModel, IUserModel groupLeader, IUserModel newRegisteredUser, string? contextProfileName);

        /// <summary>Queue last payment reminder.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesInvoicesLastPaymentReminderNotificationToCustomerAsync(IUserModel user, string? contextProfileName);

        /// <summary>Queue invoice payment.</summary>
        /// <param name="invoice">           The invoice.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSalesInvoicesPaymentNotificationToCustomerAsync(ISalesInvoiceModel invoice, string? contextProfileName);

        /// <summary>Queue multiple invoices payment.</summary>
        /// <param name="amounts">           The amounts.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse> QueueMultipleInvoicesPaymentAsync(Dictionary<int, decimal> amounts, string? contextProfileName);

        /// <summary>Queue payment past due.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueuePaymentPastDueAsync(IUserModel user, string? contextProfileName);

        /// <summary>Queue passport overdue.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="date">              The date Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueuePassportOverdueAsync(IUserModel user, DateTime date, string? contextProfileName);
#endif

#if SPLIT_TEMPLATES
        #region Split Template Emails
        /// <summary>Invitation to Join Site Notice to User (Invite Token from Settings).</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailInvitationWithStaticTokenAsync(string email, string? contextProfileName);

        /// <summary>Invitation to Join Site Notice to User (Generated Invite Token from Workflow).</summary>
        /// <param name="email">             The email.</param>
        /// <param name="fromUser">          from user.</param>
        /// <param name="inviteCode">        The invite code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailInviteCodeAsync(string email, IUserModel fromUser, string inviteCode, string? contextProfileName);

        /// <summary>User Registration without Verification or Forced PasswordReset.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewUserRegistrationToUserAsync(IUserModel user, string? contextProfileName);

        /// <summary>User Registration with Email Verification but no Forced Password Reset.</summary>
        /// <param name="userModel">         The user model.</param>
        /// <param name="validationToken">   The validation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewUserRegistrationEmailValidationToUserAsync(IUserModel userModel, string validationToken, string? contextProfileName);

        /// <summary>User Registration with Email Verification and Forced Password Reset.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="passwordResetToken">The password reset token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewUserRegistrationEmailValidationAndForcedPasswordResetToUserAsync(ICreateLiteAccountAndSendValidationEmail model, string passwordResetToken, string? contextProfileName);

        /// <summary>Back Office Approved User Registration Notice to User.</summary>
        /// <param name="user">              The user model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailBackOfficeApprovedUserNotificationToUserAsync(IUserModel user, string? contextProfileName);

        /// <summary>User password reset token request.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="username">          The username.</param>
        /// <param name="resetToken">        The reset token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailUserPasswordResetTokenRequestAsync(string email, string username, string resetToken, string? contextProfileName);

        /// <summary>User forgot username request.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="username">          The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailUserForgotUsernameRequestAsync(string email, string username, string? contextProfileName);

        /// <summary>New seller registration to seller.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="store">             The store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSellerRegistrationToSellerAsync(IUserModel user, IStoreModel store, string? contextProfileName);

        /// <summary>New seller registration to back office.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="store">             The store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSellerRegistrationToBackOfficeAsync(IUserModel user, IStoreModel store, string? contextProfileName);

        /// <summary>New Message is Waiting Notice to Recipient User(s) (with and without message body content).</summary>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{CEFActionResponse{Int32}}.</returns>
        Task<List<CEFActionResponse<int>>> QueueSplitTemplateEmailNewMessageWaitingNotificationToRecipientsAsync(IMessageModel message, string? contextProfileName);

        /// <summary>Product is now in stock notification to User.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailProductNowInStockNotificationAsync(string email, IProductModel product, string? contextProfileName);

        /// <summary>Queue sales order customer receipt notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="cc">                The Cc.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderCustomerReceiptNotificationAsync(ISalesOrderModel order, string cc, string? contextProfileName);

        /// <summary>Queue sales order back office notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSalesOrderNotificationToBackOfficeAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Queue sales order back office store notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSalesOrderNotificationToBackOfficeStoreAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order confirmed notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderConfirmedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order back order notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderBackOrderNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order split notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderSplitNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order invoice notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="invoice">           The invoice.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderInvoiceNotificationAsync(ISalesOrderModel order, ISalesInvoiceModel invoice, string? contextProfileName);

        /// <summary>Sends the sales order partial payment notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderPartialPaymentNotificationAsync(ISalesOrderModel order, IPaymentModel payment, string? contextProfileName);

        /// <summary>Sends the sales order full payment notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderFullPaymentNotificationAsync(ISalesOrderModel order, IPaymentModel payment, string? contextProfileName);

        /// <summary>Sends the sales order processing notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderProcessingNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order drop ship notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="purchaseOrder">     The purchase order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderDropShipNotificationAsync(ISalesOrderModel order, IPurchaseOrderModel purchaseOrder, string? contextProfileName);

        /// <summary>Sends the sales order shipped notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderShippedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order ready for pickup notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderReadyForPickupNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order completed notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderCompletedNotificationAsync(ISalesOrderModel order, string? contextProfileName);

        /// <summary>Sends the sales order voided notification.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}" />.</returns>
        Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderVoidedNotificationAsync(ISalesOrderModel order, string? contextProfileName);
        #endregion
#endif
    }
}
