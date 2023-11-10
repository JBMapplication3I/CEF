// <copyright file="QueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries provider base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.SalesInvoiceHandlers.Queries;
    using Models;

    /// <summary>The sales invoice queries provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesInvoiceQueriesProviderBase"/>
    public abstract class SalesInvoiceQueriesProviderBase : ProviderBase, ISalesInvoiceQueriesProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesInvoiceQueriesHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ISalesInvoiceModel>> GetRecordByUserAndEventAsync(
            int userID,
            int calendarEventID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<IEnumerable<ISalesInvoiceModel>> GetRecordsPerEventAndNotStatusAsync(
            string eventKey,
            string statusKey,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<ISalesInvoiceModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<List<ISalesInvoice>> GetRecordsForFinalPaymentReminderAsync(
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<List<ISalesInvoice>> GetRecordsPastDueForPaymentReminderAsync(
            string? contextProfileName);
    }
}
