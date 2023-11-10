// <copyright file="SurchargeService.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the surcharge service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Surcharges
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Surcharges;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>Gets called by the storefront during invoice payment to display the surcharge to the customer.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_Decimal}"/>
    [PublicAPI, UsedInStorefront, Route("/Payments/SurchargeForInvoices", Verbs = "POST")]
    public class GetSurchargeForInvoicePayment : IReturn<CEFActionResponse<decimal>>
    {
#pragma warning disable CS8618 // Only constructed by API calls
        /// <summary>Bank Identification Number for the card being used to pay.</summary>
        /// <value>The bin.</value>
        public string BIN { get; set; }

        /// <summary>ID of the contact making this payment.</summary>
        /// <value>The identifier of the billing contact.</value>
        public int BillingContactID { get; set; }

        /// <summary>IDs of all invoices involved in this payment.</summary>
        /// <value>The invoice i ds.</value>
        public HashSet<int> InvoiceIDs { get; set; }

        /// <summary>Total amount to be paid.</summary>
        /// <value>The total number of amount.</value>
        public decimal TotalAmount { get; set; }
#pragma warning restore CS8618
    }

    /// <inheritdoc cref="GetSurchargeForInvoicePayment"/>
    public class SurchargeProviderService : ClarityEcommerceServiceBase
    {
        /// <inheritdoc cref="GetSurchargeForInvoicePayment"/>
        public async Task<object?> Post(GetSurchargeForInvoicePayment request)
        {
            var provider = RegistryLoaderWrapper.GetSurchargeProvider(contextProfileName: null);
            if (provider is null)
            {
                return CEFAR.FailingCEFAR("ERROR! No surcharge provider configured");
            }
            var invoices = new List<ISalesInvoiceModel>();
            foreach (var i in request.InvoiceIDs)
            {
                invoices.Add((await Workflows.SalesInvoices.GetAsync(i, contextProfileName: null).ConfigureAwait(false))!);
            }
            return (await provider.CalculateSurchargeAsync(
                        new SurchargeDescriptor
                        {
#if NET5_0_OR_GREATER
                            ApplicableInvoices = invoices.ToSet(),
#else
                            ApplicableInvoices = invoices.ToHashSet(),
#endif
                            TotalAmount = request.TotalAmount,
                            BIN = request.BIN,
                            BillingContact = await Workflows.Contacts.GetAsync(
                                    request.BillingContactID,
                                    contextProfileName: null)
                                .ConfigureAwait(false),
                        },
                        contextProfileName: null)
                    .ConfigureAwait(false))
                .amount
                .WrapInPassingCEFAR();
        }
    }
}
