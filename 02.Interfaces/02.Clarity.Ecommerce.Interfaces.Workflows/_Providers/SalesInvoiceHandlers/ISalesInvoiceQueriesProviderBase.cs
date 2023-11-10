// <copyright file="ISalesInvoiceQueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesInvoiceQueriesProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SalesInvoiceHandlers.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for sales invoice queries provider.</summary>
    public interface ISalesInvoiceQueriesProviderBase : IProviderBase
    {
        /// <summary>Gets sales invoice by user and event.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="calendarEventID">   Identifier for the calendar event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales invoice by user and event.</returns>
        Task<CEFActionResponse<ISalesInvoiceModel>> GetRecordByUserAndEventAsync(
            int userID,
            int calendarEventID,
            string? contextProfileName);

        /// <summary>Gets sales invoices per event and not status.</summary>
        /// <param name="eventKey">          The event key.</param>
        /// <param name="statusKey">         The status key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales invoices per event and not status.</returns>
        Task<IEnumerable<ISalesInvoiceModel>> GetRecordsPerEventAndNotStatusAsync(
            string eventKey,
            string statusKey,
            string? contextProfileName);

        /// <summary>Gets the sales invoice only if the supplied AccountID exists on the record.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountIDs">        List of identifiers for the accounts.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Sales Invoice requested that is confirmed by the supplied account id.</returns>
        Task<ISalesInvoiceModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);

        /// <summary>Get SalesInvoices For Final Payment Reminder.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales invoices for final payment reminder.</returns>
        Task<List<ISalesInvoice>> GetRecordsForFinalPaymentReminderAsync(string? contextProfileName);

        /// <summary>Get past due SalesInvoices For Final Payment Reminder.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The past due sales invoices for payment reminder.</returns>
        Task<List<ISalesInvoice>> GetRecordsPastDueForPaymentReminderAsync(string? contextProfileName);
    }
}
