// <copyright file="StandardQueriesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard queries provider class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A standard sales return queries provider.</summary>
    /// <seealso cref="SalesReturnQueriesProviderBase"/>
    public class StandardSalesReturnQueriesProvider : SalesReturnQueriesProviderBase
    {
        /// <summary>The valid statuses to initiate returns.</summary>
        private static readonly string[] ValidStatusesToInitiateReturns = { "Shipped", "Shipped from Vendor", "Completed" };

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSalesReturnQueriesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ValidateSalesOrderReadyForReturnAsync(
            int salesOrderID,
            string? contextProfileName,
            bool isBackendOverride = false)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var order = await context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(salesOrderID))
                .Select(x => new
                {
                    StatusKey = x.Status!.CustomKey,
                    x.CreatedDate,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (order == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Could not retrieve the sales order");
            }
            return await ValidateSalesOrderReadyForReturnAsync(
                    order.StatusKey!,
                    order.CreatedDate,
                    isBackendOverride,
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ValidateSalesOrderReadyForReturnAsync(
            string salesOrderStatusKey,
            DateTime salesOrderCreatedDate,
            bool isBackendOverride,
            string? contextProfileName)
        {
            if (isBackendOverride)
            {
                // Overridden to force allow
                return Task.FromResult(CEFAR.PassingCEFAR());
            }
            // Check for valid Sales Order Status
            if (!ValidStatusesToInitiateReturns.Contains(Contract.RequiresValidKey(salesOrderStatusKey), StringComparer.OrdinalIgnoreCase))
            {
                return Task.FromResult(CEFAR.FailingCEFAR("ERROR! The sales order is not in the appropriate status"));
            }
            // If config value is missing or set to 0, we skip the test
            if (CEFConfigDictionary.ReturnsValidityPeriodInDays.HasValue
                && CEFConfigDictionary.ReturnsValidityPeriodInDays != 0
                && salesOrderCreatedDate.AddDays(CEFConfigDictionary.ReturnsValidityPeriodInDays.Value) < DateExtensions.GenDateTime)
            {
                return Task.FromResult(CEFAR.FailingCEFAR("ERROR! The return capability has expired"));
            }
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ValidateSalesReturnAsync(
            ISalesReturnModel @return,
            string? contextProfileName,
            bool isBackendOverride = false)
        {
            @return.SalesItems = @return.SalesItems?.Where(x => x != null).ToList();
            if (@return.SalesItems == null || @return.SalesItems.Count == 0)
            {
                return CEFAR.FailingCEFAR("ERROR! There is no item to return.");
            }
            if (@return.SalesItems.Any(x => x.TotalQuantity <= 0))
            {
                return CEFAR.FailingCEFAR("ERROR! The quantity to return must be set.");
            }
            int salesOrderID;
            if (@return.AssociatedSalesOrders?.Count > 0)
            {
                salesOrderID = @return.AssociatedSalesOrders[0].SlaveID;
            }
            else
            {
                if (@return.SalesOrderIDs?.Count <= 0)
                {
                    return CEFAR.FailingCEFAR("ERROR! There is no related sales order");
                }
                salesOrderID = @return.SalesOrderIDs![0];
            }
            var salesOrderModel = (await Workflows.SalesOrders.GetAsync(salesOrderID, contextProfileName).ConfigureAwait(false))!;
            var tmpResponse = await ValidateSalesOrderReadyForReturnAsync(
                    salesOrderModel.StatusKey!,
                    salesOrderModel.CreatedDate,
                    isBackendOverride,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!tmpResponse.ActionSucceeded)
            {
                return tmpResponse;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var salesOrderIDsChecked = new List<int>();
            foreach (var salesReturnItemModel in @return.SalesItems)
            {
                var salesItem = salesOrderModel.SalesItems!.FirstOrDefault(
                    x => x.ProductID == salesReturnItemModel.ProductID
                        && !salesOrderIDsChecked.Contains(x.ID));
                if (salesItem == null)
                {
                    return CEFAR.FailingCEFAR("ERROR! There is no related Order Item");
                }
                var totalReturnItemQuantity = salesReturnItemModel.TotalQuantity;
                var totalOriginalItemQuantity = salesItem.TotalQuantity;
                if (totalReturnItemQuantity > totalOriginalItemQuantity)
                {
                    return CEFAR.FailingCEFAR("ERROR! The quantity to return is greater than the quantity ordered");
                }
                var eligible = await context.Products
                    .AsNoTracking()
                    .FilterByID(salesItem.ProductID)
                    .Select(x => x.IsEligibleForReturn)
                    .SingleAsync();
                if (!isBackendOverride && !eligible)
                {
                    return CEFAR.FailingCEFAR("ERROR! The product is not eligible for return");
                }
                // Quantity to return must be set and lower than quantity ordered minus quantity already returned (if any)
                var previousSalesReturns = context.SalesReturnSalesOrders
                    .FilterByActive(true)
                    .Where(x => x.SlaveID == salesOrderModel.ID
                        && x.MasterID != @return.ID)
                    .Select(x => x.MasterID);
                var previouslyReturnedQuantity = await context.SalesReturnItems
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.ProductID == salesItem.ProductID && previousSalesReturns.Contains(x.MasterID))
                    .Select(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m))
                    .DefaultIfEmpty(0m)
                    .SumAsync();
                if (totalReturnItemQuantity + previouslyReturnedQuantity > totalOriginalItemQuantity)
                {
                    return CEFAR.FailingCEFAR("ERROR! The quantity to return is greater than the quantity ordered");
                }
                salesOrderIDsChecked.Add(salesItem.ID);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<ISalesReturnModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName)
        {
            var model = await Workflows.SalesReturns.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckNotNull(model)
                && accountIDs.Exists(x => x == model!.AccountID)
                && model!.Active)
            {
                return model;
            }
            throw HttpError.Unauthorized("Unauthorized to view this SalesReturn");
        }
    }
}
