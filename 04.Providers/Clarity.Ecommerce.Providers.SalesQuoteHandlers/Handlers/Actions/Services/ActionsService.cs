// <copyright file="ActionsService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the actions service class</summary>
#pragma warning disable CA1822
#pragma warning disable CS1591, SA1600 // Missing XML comment for publicly visible type or member
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services
{
    using System.Threading.Tasks;
    using Endpoints;
    using Models;
    using Service;

    /// <summary>A sales quote actions service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SalesQuoteActionsService : ClarityEcommerceServiceBase
    {
        public async Task<object> Post(SubmitRequestForQuoteForSingleProduct request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(
                    request.AccountID ?? CurrentAccountIDOrThrow401)
                .ConfigureAwait(false);
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SubmitRFQForSingleProductAsync(
                    request,
                    request.DoRecommendOtherSuppliers ?? false,
                    request.DoShareBusinessCardWithSupplier ?? false,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Post(SubmitRequestForQuoteForGenericProducts request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(
                    request.AccountID ?? CurrentAccountIDOrThrow401)
                .ConfigureAwait(false);
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SubmitRFQForGenericProductsAsync(
                    request,
                    request.DoShareBusinessCardWithSupplier ?? false,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Get(SetSalesQuotesAsExpired _)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SetRecordsToExpiredAsync(ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesQuoteAsInProcess request)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SetRecordAsInProcessAsync(request.ID, ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesQuoteAsProcessed request)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SetRecordAsProcessedAsync(request.ID, ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesQuoteAsApproved request)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SetRecordAsApprovedAsync(request.ID, ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesQuoteAsRejected request)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SetRecordAsRejectedAsync(request.ID, ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSalesQuoteAsVoided request)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .SetRecordAsVoidAsync(request.ID, ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(AwardSalesQuoteLineItem request)
        {
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(ServiceContextProfileName)!
                .AwardLineItemAsync(
                    request.OriginalItemID,
                    request.ResponseItemID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Post(ConvertQuoteToOrderForCurrentUser request)
        {
            // Verify the account is not on hold
            var checkoutResult1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (checkoutResult1 != null)
            {
                return CEFAR.FailingCEFAR<int>(checkoutResult1.ErrorMessage);
            }
            // Process the conversion
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(contextProfileName: ServiceContextProfileName)!
                .ConvertQuoteToOrderAsync(
                    request.ID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Post(ConvertSpecificQuoteToOrder request)
        {
            /* TODO: Verify the target account is not on hold (not the current account)
            var checkoutResult1 = await DoAccountOnHoldCheckAsync().ConfigureAwait(false);
            if (checkoutResult1 != null)
            {
                return CEFAR.FailingCEFAR<int>(checkoutResult1.ErrorMessage);
            }
            */
            // Process the conversion
            return await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(contextProfileName: ServiceContextProfileName)!
                .ConvertQuoteToOrderAsync(
                    request.ID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
