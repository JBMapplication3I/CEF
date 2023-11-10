// <copyright file="ISalesInvoiceActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesInvoiceActionsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SalesInvoiceHandlers.Actions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for sales invoice actions provider.</summary>
    public interface ISalesInvoiceActionsProviderBase : IProviderBase
    {
        /// <summary>Creates a sales invoice from sales order.</summary>
        /// <param name="salesOrder">        The sales order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ISalesInvoiceModel}.</returns>
        Task<CEFActionResponse<ISalesInvoiceModel>> CreateSalesInvoiceFromSalesOrderAsync(
            ISalesOrderModel salesOrder,
            string? contextProfileName);

        /// <summary>Auto Pays a sales invoice.</summary>
        /// <param name="userID">               The userID.</param>
        /// <param name="invoiceID">            The invoiceID.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse<string>> AutoPaySalesInvoiceAsync(
            int? userID,
            int? invoiceID,
            string? contextProfileName);

        /// <summary>Creates invoice for order.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new invoice for order.</returns>
        Task<CEFActionResponse<ISalesInvoiceModel>> CreateInvoiceForOrderAsync(
            int id,
            string? contextProfileName);

        /// <summary>Adds a payment to sales invoice.</summary>
        /// <param name="salesInvoice">      The sales invoice.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="originalPayment">   The amount of the payment, before any upcharges or fees.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{string} with the transaction reference number.</returns>
        Task<CEFActionResponse<string>> AddPaymentAsync(
            ISalesInvoiceModel salesInvoice,
            IPaymentModel payment,
            decimal? originalPayment,
            string? contextProfileName);

        /// <summary>Pay sales invoice.</summary>
        /// <param name="id">                Identifier for the sales invoice.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="billing">           The billing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="useWalletToken">    True if using a wallet token.</param>
        /// <param name="userID">            The userID.</param>
        /// <returns>A CEFActionResponse{string} with the transaction reference number.</returns>
        Task<CEFActionResponse<string>> PaySingleByIDAsync(
            int id,
            IPaymentModel payment,
            IContactModel? billing,
            string? contextProfileName,
            bool useWalletToken = false,
            int? userID = null);

        /// <summary>Assign sales group.</summary>
        /// <param name="salesInvoiceID">    Identifier for the sales invoice.</param>
        /// <param name="salesGroupID">      Identifier for the sales group.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> AssignSalesGroupAsync(
            int salesInvoiceID,
            int salesGroupID,
            string? contextProfileName);

        /// <summary>Pay multiple sales invoices.</summary>
        /// <param name="amounts">           The amounts.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="billing">           The billing.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{string} containing the payment transaction number.</returns>
        Task<CEFActionResponse<string>> PayMultipleByAmountsAsync(
            Dictionary<int, decimal> amounts,
            IPaymentModel payment,
            IContactModel? billing,
            int userID,
            string? contextProfileName);

        /// <summary>Sets record as paid.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsPaidAsync(int id, string? contextProfileName);

        /// <summary>Sets record as unpaid.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsUnpaidAsync(int id, string? contextProfileName);

        /// <summary>Sets record as partially paid.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsPartiallyPaidAsync(int id, string? contextProfileName);

        /// <summary>Void sales invoice.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetRecordAsVoidedAsync(int id, string? contextProfileName);
    }
}
