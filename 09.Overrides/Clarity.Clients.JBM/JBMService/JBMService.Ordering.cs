// <copyright file="JBMService.Ordering.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Mapper;
    using Clarity.Ecommerce.Providers.Checkouts.TargetOrder;
    using Ecommerce;
    using Ecommerce.Interfaces.DataModel;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Models;
    using Ecommerce.Service;
    using Ecommerce.Utilities;
    using MoreLinq;
    using ServiceStack;
    using RestSharpSerializer = RestSharp.Serialization.Json.JsonSerializer;

    public partial class JBMService : ClarityEcommerceServiceBase
    {
        public async Task<CEFActionResponse?> Post(TryOrderAgain request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var salesGroup = (await context.SalesGroups.FirstOrDefaultAsync(x => x.ID == request.SalesGroupID).ConfigureAwait(false)).CreateSalesGroupModelFromEntityFull(ServiceContextProfileName);
            var res = await new JBMTargetOrderCheckoutProvider().BuildFusionSalesOrderAndSendAsync(
                    salesGroup!,
                    ServiceContextProfileName,
                    salesGroup!.SubSalesOrders.First().UserID!.Value,
                    salesGroup.AccountID)
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        public async Task<CEFActionResponse?> Post(UpdateOrder request)
        {
            if (Contract.CheckNull(request?.CompareDate))
            {
                return CEFAR.FailingCEFAR();
            }
            var hasMore = true;
            var orders = new List<FusionSalesOrder>();
            var offset = 0;
            while (hasMore)
            {
                var res = await GetResponseAsync<SalesOrderResponse>(
                        resource: JBMConfig.JBMFusionSalesOrderURLExtension,
                        queryParams: new Dictionary<string, string>
                        {
                            { "limit", "500" },
                            { "offset", $"{offset}" },
                            {
                                "q", $"LastUpdateDate>={request!.CompareDate!.Value.AddDays(JBMConfig.CompareDateReachBack ?? -30).ToString("yyyy-MM-dd")}" +
                                   $";CreationDate>={request!.CompareDate!.Value.AddDays(JBMConfig.CompareDateReachBack ?? -30).ToString("yyyy-MM-dd")}" +
                                   $";SourceTransactionSystem=ECOM"
                            },
                        },
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}")
                    .ConfigureAwait(false);
                orders.AddRange(res!.Items);
                offset = offset += 500;
                hasMore = res.HasMore!.Value;
            }
            offset = 0;
            var distinctOrders = orders.GroupBy(x => x.OrderNumber).Select(y => y.LastOrDefault()).ToList();
            foreach (var fusionOrder in distinctOrders)
            {
                if (Contract.CheckAnyInvalidKey(fusionOrder.OrderNumber))
                {
                    continue;
                }
                using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
                var matchingOrder = (await context.SalesOrders
                        .FilterByCustomKey(fusionOrder!.OrderNumber)
                        .Select(x => x.ID)
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .FirstOrDefault();
                if (Contract.CheckNull(matchingOrder))
                {
                    continue;
                }
                fusionOrder.Lines = (await GetResponseAsync<OrderLinesResponse>(
                        resource: $"{System.Web.HttpUtility.UrlEncode(fusionOrder.OrderKey)}/child/lines",
                        queryParams: new Dictionary<string, string> { { "limit", "100" } },
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}/{JBMConfig.JBMFusionSalesOrderURLExtension}")
                    .ConfigureAwait(false))?.Items;
                fusionOrder.Totals = await GetResponseAsync<FusionOrderTotals>(
                        resource: $"{System.Web.HttpUtility.UrlEncode(fusionOrder.OrderKey)}/child/totals",
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}/{JBMConfig.JBMFusionSalesOrderURLExtension}")
                    .ConfigureAwait(false);
                if (Contract.CheckEmpty(fusionOrder.Lines))
                {
                    await Logger.LogErrorAsync(
                    $"Failed to update order.",
                    $"Failed to get lines from Fusion for order: {fusionOrder.OrderNumber}\nSee JBMService.Ordering, Line: 47.",
                    ServiceContextProfileName);
                    continue;
                }
                if (Contract.CheckNull(fusionOrder.Totals))
                {
                    await Logger.LogErrorAsync(
                    $"Failed to update order.",
                    $"Failed to get totals from Fusion for order: {fusionOrder.OrderNumber}\nSee JBMService.Ordering, Line: 51.",
                    ServiceContextProfileName);
                    continue;
                }
                // Compares the order
                var res = await JBMWorkflow
                    .CompareAndUpdateOrder(fusionOrder, fusionOrder.OrderNumber!, ServiceContextProfileName)
                    .ConfigureAwait(false);
                if (Contract.CheckNull(res) || !res!.ActionSucceeded)
                {
                    if (res is null)
                    {
                        await Logger.LogErrorAsync(
                        $"Failed to update order.",
                        "An Error Occurred.",
                        ServiceContextProfileName);
                    }
                    else
                    {
                        await Logger.LogErrorAsync(
                        $"Failed to update order.",
                        $"{res.Messages.First()}",
                        ServiceContextProfileName);
                    }
                    continue;
                }
            }
            return CEFAR.PassingCEFAR();
        }

        public async Task<CEFActionResponse> Post(UpdateOrderStatus request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            List<ISalesOrderModel> ordersToUpdate = new();
            ordersToUpdate.Add(
                (await Workflows.SalesOrders.SearchAsync(
                    search: new SalesOrderSearchModel()
                    {
                        CustomKey = request.TransactionNumber,
                    },
                    asListing: true,
                    context: context)
                .ConfigureAwait(false)).results.FirstOrDefault());
            if (!ordersToUpdate.Any(x => x is not null))
            {
                return CEFAR.FailingCEFAR($"ERROR! No order could be found for Transaction: {request.TransactionNumber}");
            }
            ordersToUpdate.AddRange(
                (await Workflows.SalesOrders.SearchAsync(
                    search: new SalesOrderSearchModel()
                    {
                        SalesGroupAsSubID = ordersToUpdate.First().SalesGroupAsMasterID,
                    },
                    asListing: true,
                    context: context)
                .ConfigureAwait(false)).results);
            var responses = new List<CEFActionResponse>();
            foreach (var req in ordersToUpdate.Where(o => o is not null))
            {
                req.Status = null;
                req.StatusID = default;
                req.StatusKey = await MapFusionStatusToCEFStatus(request.StatusCode!, context).ConfigureAwait(false);
                responses.Add(await Workflows.SalesOrders.UpdateAsync(req, context));
            }
            if (responses.Any(x => !x.ActionSucceeded))
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not update the status of order {request.TransactionNumber}");
            }
            return CEFAR.PassingCEFAR();
        }

        private async Task<string> MapFusionStatusToCEFStatus(string fusionStatus, IClarityEcommerceEntities context)
        {
            var statuses = await context.SalesOrderStatuses.Select(x => x.CustomKey).ToListAsync();
            return fusionStatus switch
            {
                "Submitted" => "Confirmed",
                "Hold" => "On Hold",
                "Shipped" => "Shipped",
                "Closed" => "Completed",
                _ => "Pending",
            };
        }
    }
}