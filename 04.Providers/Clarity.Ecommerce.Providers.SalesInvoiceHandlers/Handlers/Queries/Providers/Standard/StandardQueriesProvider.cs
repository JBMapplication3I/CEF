// <copyright file="StandardQueriesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard queries provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Standard
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A standard sales invoice queries provider.</summary>
    /// <seealso cref="SalesInvoiceQueriesProviderBase"/>
    public class StandardSalesInvoiceQueriesProvider : SalesInvoiceQueriesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSalesInvoiceQueriesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<ISalesInvoiceModel>> GetRecordByUserAndEventAsync(
            int userID,
            int calendarEventID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var possibleProductIDs = await context.CalendarEvents
                .FilterByID(calendarEventID)
                .SelectMany(x => x.Products!
                    .Where(y => y.Active && y.Slave!.Active)
                    .Select(y => y.SlaveID))
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
            if (possibleProductIDs.Count == 0)
            {
                return CEFAR.FailingCEFAR<ISalesInvoiceModel>();
            }
            var invoice = context.SalesInvoices
                .FilterByActive(true)
                .FilterSalesCollectionsByUserID<SalesInvoice,
                    SalesInvoiceStatus,
                    SalesInvoiceType,
                    SalesInvoiceItem,
                    AppliedSalesInvoiceDiscount,
                    SalesInvoiceState,
                    SalesInvoiceFile,
                    SalesInvoiceContact,
                    SalesInvoiceEvent,
                    // ReSharper disable once StyleCop.SA1110
                    SalesInvoiceEventType>(userID)
                .FilterSalesCollectionsByProductIDs<SalesInvoice,
                    SalesInvoiceStatus,
                    SalesInvoiceType,
                    SalesInvoiceItem,
                    AppliedSalesInvoiceDiscount,
                    SalesInvoiceState,
                    SalesInvoiceFile,
                    SalesInvoiceContact,
                    SalesInvoiceEvent,
                    SalesInvoiceEventType,
                    AppliedSalesInvoiceItemDiscount,
                    // ReSharper disable once StyleCop.SA1110
                    SalesInvoiceItemTarget>(possibleProductIDs)
                .OrderByDescending(x => x.ID)
                .Take(1)
                .SelectFirstFullSalesInvoiceAndMapToSalesInvoiceModel(contextProfileName);
            return invoice == null
                ? CEFAR.FailingCEFAR<ISalesInvoiceModel>()
                : invoice.WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ISalesInvoiceModel>> GetRecordsPerEventAndNotStatusAsync(
            string eventKey,
            string statusKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.SalesInvoices
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterSalesCollectionsByProductKey<SalesInvoice,
                        SalesInvoiceStatus,
                        SalesInvoiceType,
                        SalesInvoiceItem,
                        AppliedSalesInvoiceDiscount,
                        SalesInvoiceState,
                        SalesInvoiceFile,
                        SalesInvoiceContact,
                        SalesInvoiceEvent,
                        SalesInvoiceEventType,
                        AppliedSalesInvoiceItemDiscount,
                        // ReSharper disable once StyleCop.SA1110
                        SalesInvoiceItemTarget>(eventKey)
                    .FilterByExcludedStatusKey<SalesInvoice, SalesInvoiceStatus>(statusKey)
                    .SelectFullSalesInvoiceAndMapToSalesInvoiceModel(contextProfileName));
        }

        /// <inheritdoc/>
        public override async Task<ISalesInvoiceModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName)
        {
            var model = await Workflows.SalesInvoices.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (model is not { Active: true }
                || !Contract.CheckValidID(model.AccountID)
                || !accountIDs.Contains(model.AccountID!.Value))
            {
                throw HttpError.Unauthorized("Unauthorized to view this SalesInvoice");
            }
            return model;
        }

        /// <inheritdoc/>
        public override async Task<List<ISalesInvoice>> GetRecordsForFinalPaymentReminderAsync(
            string? contextProfileName)
        {
            // Get days before reminding
            var timestamp = DateExtensions.GenDateTime;
            // Get data
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.SalesInvoices
                .FilterByActive(true)
                .Where(x => x.BalanceDue > 0
                    && x.DueDate != null
                    && x.DueDate > timestamp
                    && DbFunctions.DiffDays(x.DueDate, timestamp)
                        <= CEFConfigDictionary.SendFinalStatementWithFinalPRandPDFInsertsAfterXDays
                    && x.BillingContact != null
                    && x.Status!.Name != "Paid"
                    && x.Status.Name != "Void")
                .ToListAsync<ISalesInvoice>()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<List<ISalesInvoice>> GetRecordsPastDueForPaymentReminderAsync(
            string? contextProfileName)
        {
            // Get data
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var timestamp = DateExtensions.GenDateTime;
            return await context.SalesInvoices
                .FilterByActive(true)
                .Where(x => x.BalanceDue > 0m
                    && x.DueDate != null
                    && x.DueDate <= timestamp
                    && x.BillingContact != null
                    && x.Status!.Name != "Paid"
                    && x.Status.Name != "Void")
                .ToListAsync<ISalesInvoice>()
                .ConfigureAwait(false);
        }
    }
}
