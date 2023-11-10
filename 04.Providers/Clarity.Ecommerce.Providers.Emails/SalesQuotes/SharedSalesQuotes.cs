// <copyright file="SharedSalesQuotes.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shared sales quotes class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Utilities;

    /// <summary>A shared sales quotes.</summary>
    internal static class SharedSalesQuotes
    {
        /// <summary>Standard quote replacements.</summary>
        /// <param name="quote">                The quote.</param>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <returns>A Task{Dictionary{string,string}}.</returns>
        internal static Task StandardQuoteReplacementsAsync(
            ISalesQuoteModel quote,
            Dictionary<string, string?> replacementDictionary)
        {
            return SharedSalesOrders.StandardSalesCollectionReplacementsAsync(
                quote,
                quote.SalesItems!,
                replacementDictionary);
        }

        /// <summary>Gets order customer email to use.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="whichEmailTemplate">The which email template.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The order customer email to use.</returns>
        internal static async Task<CEFActionResponse<string?>> GetQuoteCustomerEmailToUseAsync(
            ISalesQuoteModel quote,
            string whichEmailTemplate,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var userEmail = await context.Users
                .AsNoTracking()
                .FilterByID(quote.UserID)
                .Select(x => x.Email)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckValidKey(userEmail))
            {
                return userEmail.WrapInPassingCEFAR();
            }
            if (!Contract.CheckAnyValidID(
                quote.SalesGroupAsRequestMasterID,
                quote.SalesGroupAsRequestSubID,
                quote.SalesGroupAsResponseMasterID,
                quote.SalesGroupAsResponseSubID))
            {
                return CEFAR.FailingCEFAR<string?>(
                    $"ERROR! Unable to send Customer order ${whichEmailTemplate} notification as there is no email"
                    + " address to use in the order and it doesn't belong to a sales group to check");
            }
            var shippingEmail = await context.SalesGroups
                .AsNoTracking()
                .FilterByID(
                    (quote.SalesGroupAsRequestMasterID
                        ?? quote.SalesGroupAsRequestSubID
                        ?? quote.SalesGroupAsResponseMasterID
                        ?? quote.SalesGroupAsResponseSubID)!
                    .Value)
                .Where(x => x.BillingContact != null)
                .Select(x => x.BillingContact!.Email1)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckValidKey(shippingEmail))
            {
                return shippingEmail.WrapInPassingCEFAR();
            }
            return CEFAR.FailingCEFAR<string?>(
                $"ERROR! Unable to send Customer order ${whichEmailTemplate} notification as there is no email"
                + " address to use in the order or the sales group it belongs to");
        }
    }
}
