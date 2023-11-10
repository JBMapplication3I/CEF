// <copyright file="ActionsService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice actions service class</summary>
#nullable enable
#pragma warning disable CA1822
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services
{
    using System;
    using System.Threading.Tasks;
    using Emails;
    using Endpoints;
    using Models;
    using Service;
    using Utilities;

    /// <summary>A sales invoice actions service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SalesInvoiceActionsService : ClarityEcommerceServiceBase
    {
        /// <summary>Post handler for the <see cref="PaySingleInvoiceByID"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public virtual async Task<object?> Post(PaySingleInvoiceByID request)
        {
            Contract.RequiresNotNull(request.Payment?.Amount);
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            var payInvoiceResult = await provider.PaySingleByIDAsync(
                    id: request.InvoiceID,
                    payment: request.Payment!,
                    billing: request.Billing,
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (!payInvoiceResult.ActionSucceeded)
            {
                return payInvoiceResult;
            }
            try
            {
                var queueEmailResult = await new SalesInvoicesPaymentReceivedToCustomerEmail().QueueAsync(
                        to: null,
                        contextProfileName: null,
                        parameters: new()
                        {
                            ["salesInvoice"] = Contract.RequiresNotNull(
                                await Workflows.SalesInvoices.GetAsync(
                                    request.InvoiceID,
                                    contextProfileName: null)
                                .ConfigureAwait(false)),
                        })
                    .ConfigureAwait(false);
                if (!queueEmailResult.ActionSucceeded)
                {
                    // Pass, but tell it the issues with sending the email
                    return CEFAR.PassingCEFAR(queueEmailResult.Messages.ToArray());
                }
            }
            catch (Exception ex)
            {
                // Pass, but tell it the issues with sending the email
                return CEFAR.PassingCEFAR(ex.Message);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Post handler for the <see cref="PayMultipleInvoicesByAmounts"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public virtual async Task<object?> Post(PayMultipleInvoicesByAmounts request)
        {
            Contract.RequiresNotNull(request.Payment?.Amount);
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            var payInvoicesResult = await provider.PayMultipleByAmountsAsync(
                    amounts: request.Amounts,
                    payment: request.Payment!,
                    billing: request.Billing,
                    userID: CurrentUserIDOrThrow401,
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (!payInvoicesResult.ActionSucceeded)
            {
                return payInvoicesResult;
            }
            try
            {
                var queueEmailResult = await new SalesInvoicesMultipleInvoicesPaymentRecievedNotificationToCustomerEmail()
                    .QueueAsync(
                        to: null,
                        contextProfileName: null,
                        parameters: new() { ["amounts"] = request.Amounts!, })
                    .ConfigureAwait(false);
                if (!queueEmailResult.ActionSucceeded)
                {
                    // Pass, but tell it the issues with sending the email
                    return CEFAR.PassingCEFAR(queueEmailResult.Messages.ToArray());
                }
            }
            catch (Exception ex)
            {
                // Pass, but tell it the issues with sending the email
                return CEFAR.PassingCEFAR(ex.Message);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Patch handler for the <see cref="CreateInvoiceForSalesOrder"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_SalesInvoiceModel}"/>.</returns>
        public async Task<object?> Patch(CreateInvoiceForSalesOrder request)
        {
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            return await provider.CreateInvoiceForOrderAsync(
                    id: request.ID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Patch handler for the <see cref="SetSalesInvoiceAsPaid"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public virtual async Task<object?> Patch(SetSalesInvoiceAsPaid request)
        {
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            return await provider.SetRecordAsPaidAsync(
                    id: Contract.RequiresValidID(request.ID),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Patch handler for the <see cref="SetSalesInvoiceAsPartiallyPaid"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public virtual async Task<object?> Patch(SetSalesInvoiceAsPartiallyPaid request)
        {
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            return await provider.SetRecordAsPartiallyPaidAsync(
                    id: Contract.RequiresValidID(request.ID),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Patch handler for the <see cref="SetSalesInvoiceAsUnpaid"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public virtual async Task<object?> Patch(SetSalesInvoiceAsUnpaid request)
        {
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            return await provider.SetRecordAsUnpaidAsync(
                    id: Contract.RequiresValidID(request.ID),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Patch handler for the <see cref="SetSalesInvoiceAsVoided"/> request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public virtual async Task<object?> Patch(SetSalesInvoiceAsVoided request)
        {
            var provider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName: null);
            if (provider is null)
            {
                return null;
            }
            return await provider.SetRecordAsVoidedAsync(
                    id: Contract.RequiresValidID(request.ID),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }
    }
}
