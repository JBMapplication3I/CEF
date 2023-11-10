// <copyright file="SharedSalesReturns.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shared sales returns class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;

    /// <summary>A shared sales returns.</summary>
    internal static class SharedSalesReturns
    {
        /// <summary>The workflows.</summary>
        private static readonly IWorkflowsController Workflows
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <summary>Standard return replacements.</summary>
        /// <param name="salesReturn">          The sales return.</param>
        /// <param name="salesReturnItem">      The sales return item.</param>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        internal static async Task StandardReturnReplacementsAsync(
            ISalesReturnModel salesReturn,
            ISalesItemBaseModel salesReturnItem,
            Dictionary<string, string?> replacementDictionary)
        {
            await SharedSalesOrders.StandardSalesCollectionReplacementsAsync(
                    salesReturn,
                    salesReturn.SalesItems!,
                    replacementDictionary)
                .ConfigureAwait(false);
            // Sales Return specific replacements
            replacementDictionary["{{PurchaseOrderID}}"] = salesReturn.PurchaseOrderNumber ?? string.Empty;
            replacementDictionary["{{TaxTransactionID}}"] = salesReturn.TaxTransactionID ?? string.Empty;
            replacementDictionary["{{ProductSeoUrl}}"] = salesReturnItem.ProductSeoUrl;
            replacementDictionary["{{ProductName}}"] = salesReturnItem.ProductName;
            replacementDictionary["{{Quantity}}"] = salesReturnItem.Quantity.ToString("#,###.####");
            replacementDictionary["{{RmaNumber}}"] = salesReturnItem.CustomKey;
            replacementDictionary["{{RestockingFee}}"] = salesReturnItem.RestockingFeeAmount.HasValue
                ? salesReturnItem.RestockingFeeAmount.Value.ToString("C2")
                : string.Empty;
        }

        /// <summary>Sends the sales return notification.</summary>
        /// <param name="salesReturn">          The sales Return.</param>
        /// <param name="emailSettings">        The email settings.</param>
        /// <param name="isInternal">           True if this is internal.</param>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{Int32}"/>.</returns>
        internal static async Task<CEFActionResponse<int>> FormatAndQueueSalesReturnNotificationAsync(
            ISalesReturnModel salesReturn,
            IEmailSettings emailSettings,
            bool isInternal,
            Dictionary<string, string?> replacementDictionary,
            string? contextProfileName)
        {
            var result = new CEFActionResponse<int>();
            foreach (var salesReturnSalesItem in salesReturn.SalesItems!)
            {
                await StandardReturnReplacementsAsync(
                        salesReturn,
                        salesReturnSalesItem,
                        replacementDictionary)
                    .ConfigureAwait(false);
                result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                        email: isInternal ? emailSettings.To : salesReturn.BillingContact?.Email1 ?? salesReturn.User?.Email,
                        replacementDictionary: replacementDictionary,
                        emailSettings: emailSettings,
                        attachmentPath: null,
                        attachmentType: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
                }
            }
            return result;
        }
    }
}
