// <copyright file="ActionsService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the actions service class</summary>
#pragma warning disable CA1822
#pragma warning disable CS1591, SA1600 // Missing XML comment for publicly visible type or member
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Endpoints;
    using Models;
    using Service;

    /// <summary>A sales return actions service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SalesReturnActionsService : ClarityEcommerceServiceBase
    {
        public async Task<object> Patch(SetSalesReturnAsConfirmed request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsConfirmedAsync(
                    request.ID,
                    CurrentUserIDOrThrow401,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(AddPaymentToSalesReturn request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .AddPaymentToReturnAsync(
                    request.ID,
                    request.Payment,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesReturnAsShipped request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsShippedAsync(request.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesReturnAsCompleted request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsCompletedAsync(request.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesReturnAsVoided request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsVoidAsync(
                    request.ID,
                    await GetTaxProviderAsync().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesReturnAsRejected request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsRejectedAsync(
                    request.ID,
                    CurrentUserIDOrThrow401,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesReturnAsCancelled request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsCancelledAsync(
                    request.ID,
                    await CurrentUserOrThrow401Async().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesReturnAsRefunded request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!
                .SetRecordAsRefundedAsync(
                    request.ID,
                    CurrentUserIDOrThrow401,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Post(ManuallyRefundSalesReturn request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!.ManuallyRefundReturnAsync(
                    request,
                    CurrentUserIDOrThrow401,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Post(CreateSalesReturnFromStorefront request)
        {
            var rmaResponse = new CEFActionResponse<int?>();
            var actionsProvider = RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!;
            var queriesProvider = RegistryLoaderWrapper.GetSalesReturnQueriesProvider(null)!;
            var salesReturnResult = await queriesProvider.ValidateSalesReturnAsync(request, null).ConfigureAwait(false);
            if (!salesReturnResult.ActionSucceeded)
            {
                rmaResponse.Messages.AddRange(salesReturnResult.Messages);
                return rmaResponse;
            }
            var salesReturn = await actionsProvider.CreateStoreFrontSalesReturnAsync(
                    request,
                    await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
            if (salesReturn == null)
            {
                rmaResponse.Messages.Add(
                    $"There was an error creating the Sales Return for order id {request.SalesOrderIDs!.FirstOrDefault()}.");
                rmaResponse.Messages.AddRange(salesReturnResult.Messages);
                return rmaResponse;
            }
            rmaResponse.ActionSucceeded = true;
            rmaResponse.Result = salesReturn.ID;
            var emailResponse = await actionsProvider.SendRmaCreationEmailNotificationsAsync(
                    salesReturn,
                    request.SalesOrderIDs!.FirstOrDefault(),
                    null)
                .ConfigureAwait(false);
            if (!emailResponse.ActionSucceeded)
            {
                rmaResponse.Messages.AddRange(emailResponse.Messages);
            }
            return rmaResponse;
        }

        public async Task<object> Put(UpdateSalesReturnFromStorefront request)
        {
            var salesReturnResult = await RegistryLoaderWrapper.GetSalesReturnQueriesProvider(null)!.ValidateSalesReturnAsync(
                    request,
                    null,
                    true)
                .ConfigureAwait(false);
            if (!salesReturnResult.ActionSucceeded)
            {
                throw new ArgumentException(
                    salesReturnResult.Messages.Any() ? salesReturnResult.Messages.First() : string.Empty);
            }
            // TODO: Send email notifications?
            return await Workflows.SalesReturns.UpdateAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        public async Task<object> Post(CreateSalesReturnAsAdmin request)
        {
            var rmaResponse = new CEFActionResponse<int?>();
            var salesReturnResult = await RegistryLoaderWrapper.GetSalesReturnQueriesProvider(null)!.ValidateSalesReturnAsync(
                    request,
                    null,
                    true)
                .ConfigureAwait(false);
            if (!salesReturnResult.ActionSucceeded)
            {
                rmaResponse.Messages.AddRange(salesReturnResult.Messages);
                return rmaResponse;
            }
            var actionsProvider = RegistryLoaderWrapper.GetSalesReturnActionsProvider(null)!;
            var salesReturn = await actionsProvider.CreateStoreFrontSalesReturnAsync(
                    request,
                    await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
            if (salesReturn == null)
            {
                rmaResponse.Messages.Add(
                    $"There was an error creating the Sales Return for order id {request.SalesOrderIDs!.FirstOrDefault()}.");
                rmaResponse.Messages.AddRange(salesReturnResult.Messages);
                return rmaResponse;
            }
            rmaResponse.ActionSucceeded = true;
            rmaResponse.Result = salesReturn.ID;
            var emailResponse = await actionsProvider.SendRmaCreationEmailNotificationsAsync(
                    salesReturn,
                    request.SalesOrderIDs!.FirstOrDefault(),
                    null)
                .ConfigureAwait(false);
            if (!emailResponse.ActionSucceeded)
            {
                rmaResponse.Messages.AddRange(emailResponse.Messages);
            }
            return rmaResponse;
        }
    }
}
