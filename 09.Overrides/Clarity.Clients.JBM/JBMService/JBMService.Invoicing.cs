// <copyright file="JBMService.Invoicing.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Models;
    using Ecommerce.Service;
    using Ecommerce.Utilities;
    using ServiceStack;

    public partial class JBMService : ClarityEcommerceServiceBase
    {
        public async Task<CEFActionResponse> Post(CreateInvoices request)
        {
            if (!Contract.CheckNotNull(request.Items) || !Contract.CheckNotEmpty(request.Items))
            {
                return CEFAR.PassingCEFAR();
            }
            foreach (var i in request.Items!)
            {
                try
                {
                    var fusionLines = await GetResponseAsync<InvoiceLinesResponse>(
                    resource: $"{JBMConfig.JBMSalesAPI}/receivablesInvoices/{i.CustomerTransactionId}/child/receivablesInvoiceLines")
                        .ConfigureAwait(false);
                    if (Contract.CheckNull(fusionLines))
                    {
                        continue;
                    }
                    var cefInvoice = await JBMWorkflow.MapToCEFInvoiceAsync(i, fusionLines!).ConfigureAwait(false);
                    if (Contract.CheckNull(cefInvoice))
                    {
                        return CEFAR.FailingCEFAR($"Error! Could not create CEF invoice for {i.TransactionNumber}");
                    }
                    using var context = RegistryLoaderWrapper.GetContext(null);
                    var res = await Workflows.SalesInvoices.ResolveWithAutoGenerateAsync(
                            byID: null,
                            byKey: cefInvoice!.CustomKey,
                            model: cefInvoice,
                            context: context)
                        .ConfigureAwait(false);
                    if (Contract.CheckNotNull(cefInvoice.AssociatedSalesOrders))
                    {
                        cefInvoice.AssociatedSalesOrders.First().SlaveID = res.Result!.ID;
                        await Workflows.SalesOrderSalesInvoices.UpsertAsync(
                                model: cefInvoice.AssociatedSalesOrders.First(),
                                contextProfileName: ServiceContextProfileName)
                            .ConfigureAwait(false);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return CEFAR.PassingCEFAR();
        }
    }
}