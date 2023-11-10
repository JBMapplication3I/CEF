// <copyright file="SalesInvoiceWithSalesItemsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate sales invoice sales items workflow base class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class SalesInvoiceWithSalesItemsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            ISalesItemBaseModel<IAppliedSalesInvoiceItemDiscountModel> model,
            ISalesInvoiceItem entity,
            IClarityEcommerceEntities context)
        {
            return await base.MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false)
                && entity.Active == model.Active
                && entity.Quantity == model.Quantity
                && entity.QuantityBackOrdered == model.QuantityBackOrdered
                && entity.QuantityPreSold == model.QuantityPreSold
                && entity.TotalQuantity == model.TotalQuantity
                && entity.UnitCorePrice == model.UnitCorePrice
                && entity.UnitCorePriceInSellingCurrency == model.UnitCorePriceInSellingCurrency
                && entity.UnitSoldPrice == model.UnitSoldPrice
                && entity.UnitSoldPriceInSellingCurrency == model.UnitSoldPriceInSellingCurrency;
        }
    }
}
