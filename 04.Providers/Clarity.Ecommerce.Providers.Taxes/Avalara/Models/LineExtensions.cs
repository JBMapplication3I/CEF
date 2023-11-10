// <copyright file="LineExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the line extensions class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System.Linq;
    using Interfaces.Models;
    using Utilities;

    /// <summary>Extension methods to covert CEF items to Avalara items.</summary>
    public static class LineExtensions
    {
        /// <summary>Conversion function from CEF line item to Avalara Line item.</summary>
        /// <typeparam name="TIItemDiscountModel">Type of the ti item discount model.</typeparam>
        /// <param name="item">            Line item to convert.</param>
        /// <param name="originCode">      where is this item coming from.</param>
        /// <param name="destCode">        where is it going to.</param>
        /// <param name="discountsApplied">have discounts been applied to this item.</param>
        /// <returns>The given data converted to a Line.</returns>
        public static Line ToAvalaraLine<TIItemDiscountModel>(
                this ISalesItemBaseModel<TIItemDiscountModel> item,
                string originCode,
                string destCode,
                bool discountsApplied)
            where TIItemDiscountModel : IAppliedDiscountBaseModel
        {
            Contract.RequiresNotNull(item);
            // If there is a user provided tax code use it no matter what
            // if the product is non-taxable then put NT
            // If the product is taxable Avalara will handle it
            var taxCode = string.IsNullOrWhiteSpace(item.ProductTaxCode)
                ? !item.ProductIsTaxable ? "NT" : string.Empty
                : item.ProductTaxCode;
            // We need to send the price w/o discounts to avalara otherwise it will double discount since all
            // discounts are added to the document level
            var productDiscounts = item.Discounts!.Sum(x => x.DiscountTotal);
            return new()
            {
                LineNo = item.ProductID.ToString(),
                ItemCode = item.ProductKey,
                Description = item.ProductName,
                Qty = item.Quantity,
                Amount = item.ExtendedPrice + productDiscounts,
                TaxCode = taxCode,
                OriginCode = originCode,
                DestinationCode = destCode,
                Discounted = discountsApplied || productDiscounts > 0.00m,
            };
        }

        /// <summary>Conversion from shipping rate to avalara freight line item.</summary>
        /// <param name="item">      The item to act on.</param>
        /// <param name="originCode">The origin code.</param>
        /// <param name="destCode">  Destination code.</param>
        /// <returns>The given data converted to a Line.</returns>
        public static Line? ToAvalaraFreightLine(this IRateQuoteModel item, string originCode, string destCode)
        {
            if (item == null)
            {
                return null;
            }
            return new()
            {
                LineNo = "FR-" + item.ID,
                TaxCode = "FR000000",
                ItemCode = item.CustomKey,
                Description = item.Description,
                Qty = 1,
                Amount = item.Rate ?? 0m,
                OriginCode = originCode,
                DestinationCode = destCode,
            };
        }
    }
}
