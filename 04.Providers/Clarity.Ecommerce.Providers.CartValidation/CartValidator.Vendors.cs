// <copyright file="CartValidator.Vendors.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Models;
    using Utilities;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Process the vendor with prepaid freight.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="summary">           The summary.</param>
        /// <param name="response">          The response.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual async Task<bool> ProcessVendorWithPrepaidFreightAsync(
            ICartModel cart,
            Summary summary,
            CEFActionResponse response,
            string? contextProfileName)
        {
            var needsToBeMet = false;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                foreach (var cartItem in cart.SalesItems!.Where(c => c.ProductID.HasValue))
                {
                    if (!await context.VendorProducts
                            .AsNoTracking()
                            .FilterByActive(true)
                            .AnyAsync(x => x.Active && x.Slave!.Active && x.SlaveID == cartItem.ProductID!.Value)
                            .ConfigureAwait(false))
                    {
                        continue;
                    }
                    needsToBeMet = true;
                }
            }
            if (!needsToBeMet)
            {
                return false;
            } // Move on to next Summary
            var attrs = summary.JsonAttributes.DeserializeAttributesDictionary();
            if (attrs.ContainsKey("Prepaid-Freight-Other")
                && attrs.TryGetValue("Prepaid-Freight-Other", out var value)
                && bool.TryParse(value.Value, out var parsedValue)
                && parsedValue)
            {
                if (attrs.ContainsKey("Prepaid-Freight-Other-Message")
                    && attrs.TryGetValue("Prepaid-Freight-Other-Message", out var messageAttr)
                    && Contract.CheckValidKey(messageAttr.Value))
                {
                    response.Messages.Add("WARNING! " + messageAttr.Value);
                    return false;
                }
                response.Messages.Add("WARNING! This Vendor may have Prepaid Freight Options. Please contact the Vendor directly for options.");
                return false;
            }
            return false;
        }

        /// <summary>Process the vendor with minimums.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The vendor.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessVendorWithMinimumsAsync(
            ICartModel cart,
            Summary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            var temp1 = await ProcessVendorWithPrepaidFreightAsync(cart, summary, response, contextProfileName).ConfigureAwait(false);
            var temp2 = await ProcessSummaryWithMinimumDollarAsync<Vendor, Product, VendorProduct, StoreProduct>(cart, summary, nameof(Vendor), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp3 = await ProcessSummaryWithMinimumQuantityAsync<Vendor, Product, VendorProduct, StoreProduct>(cart, summary, nameof(Vendor), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp4 = await ProcessSummaryWithMinimumDollarFreeShippingAsync<Vendor, Product, VendorProduct, StoreProduct>(cart, summary, nameof(Vendor), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp5 = await ProcessSummaryWithMinimumQuantityFreeShippingAsync<Vendor, Product, VendorProduct, StoreProduct>(cart, summary, nameof(Vendor), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            return temp1 || temp2 || temp3 || temp4 || temp5;
        }

        /// <summary>Process the vendors with minimums.</summary>
        /// <param name="response">             The response.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual Task<bool> ProcessVendorsWithMinimumsAsync(
            CEFActionResponse response,
            ICartModel cart,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            return ProcessRecordsWithMinimumsAsync<Vendor, VendorProduct>(
                response,
                cart,
                pricingFactoryContext,
                Config!.DoVendorRestrictionsByMinMax,
                ProcessVendorWithMinimumsAsync,
                contextProfileName);
        }
    }
}
