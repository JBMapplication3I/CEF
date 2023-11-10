// <copyright file="SharedSalesInvoice.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shared sales invoice class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Utilities;

    /// <summary>A shared sales invoice.</summary>
    internal static class SharedSalesInvoice
    {
        /// <summary>Standard invoice replacements.</summary>
        /// <param name="invoice">               The invoice.</param>
        /// <param name="replacementsDictionary">Dictionary of replacements.</param>
        /// <returns>A Task.</returns>
        internal static Task StandardInvoiceReplacementsAsync(
            ISalesInvoiceModel invoice,
            Dictionary<string, string?> replacementsDictionary)
        {
            replacementsDictionary["{{BalanceDue}}"] = invoice.BalanceDue?.ToString("C2") ?? string.Empty;
            replacementsDictionary["{{DueDate}}"] = invoice.DueDate?.ToString("D") ?? string.Empty;
            return SharedSalesOrders.StandardSalesCollectionReplacementsAsync(
                invoice,
                invoice.SalesItems!,
                replacementsDictionary);
        }

        /// <summary>Gets order customer email to use.</summary>
        /// <param name="invoice">           The invoice.</param>
        /// <param name="whichEmailTemplate">The which email template.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The order customer email to use.</returns>
        internal static async Task<CEFActionResponse<string?>> GetInvoiceCustomerEmailToUseAsync(
            ISalesInvoiceModel invoice,
            string whichEmailTemplate,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (Contract.CheckValidID(invoice.UserID))
            {
                var userEmail = await context.Users
                    .AsNoTracking()
                    .FilterByID(invoice.UserID)
                    .Select(x => x.Email)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                if (Contract.CheckValidKey(userEmail))
                {
                    return userEmail.WrapInPassingCEFAR();
                }
            }
            if (!Contract.CheckValidID(invoice.SalesGroupID))
            {
                return CEFAR.FailingCEFAR<string?>(
                    $"ERROR! Unable to send Customer invoice ${whichEmailTemplate} notification as there is no email"
                    + " address to use in the invoice and it doesn't belong to a sales group to check");
            }
            var billingEmail = await context.SalesGroups
                .AsNoTracking()
                .FilterByID(invoice.SalesGroupID)
                .Where(x => x.BillingContact != null)
                .Select(x => x.BillingContact!.Email1)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckValidKey(billingEmail))
            {
                return billingEmail.WrapInPassingCEFAR();
            }
            return CEFAR.FailingCEFAR<string?>(
                $"ERROR! Unable to send Customer invoice ${whichEmailTemplate} notification as there is no email"
                + " address to use in the invoice or the sales group it belongs to");
        }
    }
}
