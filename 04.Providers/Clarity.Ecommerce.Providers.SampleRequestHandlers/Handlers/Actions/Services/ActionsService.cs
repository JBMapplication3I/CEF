// <copyright file="ActionsService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the actions service class</summary>
#pragma warning disable CA1822
#pragma warning disable CS1591, SA1600 // Missing XML comment for publicly visible type or member
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services
{
    using System.Threading.Tasks;
    using Endpoints;
    using Service;

    /// <summary>A sample request actions service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SampleRequestActionsService : ClarityEcommerceServiceBase
    {
        public async Task<object> Patch(SetSampleRequestAsConfirmed requestAsConfirmed)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .SetRecordAsConfirmedAsync(requestAsConfirmed.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSampleRequestAsBackordered requestAsBackordered)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .SetRecordAsBackorderedAsync(requestAsBackordered.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(CreateInvoiceForSampleRequest request)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .CreateInvoiceForAsync(request.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(AddPaymentToSampleRequest request)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .AddPaymentAsync(request.ID, request.Payment, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(CreatePickTicketForSampleRequest request)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .CreatePickTicketForAsync(request.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSampleRequestAsDropShipped request)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .SetRecordAsDropShippedAsync(request.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSampleRequestAsShipped requestAsShipped)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .SetRecordAsShippedAsync(requestAsShipped.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSampleRequestAsCompleted requestAsCompleted)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .SetRecordAsCompletedAsync(requestAsCompleted.ID, null)
                .ConfigureAwait(false);
        }

        public async Task<object> Patch(SetSampleRequestAsVoided requestAsVoided)
        {
            return await RegistryLoaderWrapper.GetSampleRequestActionsProvider(null)!
                .SetRecordAsVoidedAsync(requestAsVoided.ID, null)
                .ConfigureAwait(false);
        }
    }
}
