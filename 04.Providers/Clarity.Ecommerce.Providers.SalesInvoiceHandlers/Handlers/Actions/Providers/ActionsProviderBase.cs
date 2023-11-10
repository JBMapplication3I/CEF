// <copyright file="ActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice actions provider base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesInvoiceHandlers.Actions;
    using Models;

    /// <summary>The sales invoice actions provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesInvoiceActionsProviderBase"/>
    public abstract class SalesInvoiceActionsProviderBase : ProviderBase, ISalesInvoiceActionsProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesInvoiceActionsHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ISalesInvoiceModel>> CreateSalesInvoiceFromSalesOrderAsync(
            ISalesOrderModel salesOrder,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> AutoPaySalesInvoiceAsync(
            int? userID,
            int? invoiceID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ISalesInvoiceModel>> CreateInvoiceForOrderAsync(
            int id,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> AddPaymentAsync(
            ISalesInvoiceModel salesInvoice,
            IPaymentModel payment,
            decimal? originalPayment,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> PaySingleByIDAsync(
            int id,
            IPaymentModel payment,
            IContactModel? billing,
            string? contextProfileName,
            bool useWalletToken = false,
            int? userID = null);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> AssignSalesGroupAsync(
            int salesInvoiceID,
            int salesGroupID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> PayMultipleByAmountsAsync(
            Dictionary<int, decimal> amounts,
            IPaymentModel payment,
            IContactModel? billing,
            int userID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsPaidAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsUnpaidAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsPartiallyPaidAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsVoidedAsync(int id, string? contextProfileName);
    }
}
