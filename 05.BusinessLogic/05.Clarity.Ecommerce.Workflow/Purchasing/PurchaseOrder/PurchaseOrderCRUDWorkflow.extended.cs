// <copyright file="PurchaseOrderCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class PurchaseOrderWorkflow
    {
        /// <inheritdoc/>
        public async Task CheckoutAsync(int cartId, string? contextProfileName, List<int>? vendors = null)
        {
            var cart = await Workflows.Carts.GetAsync(cartId, contextProfileName).ConfigureAwait(false);
            var vendorLists = SeparateItemsByVendor(cart!.SalesItems!);
            var timestamp = DateExtensions.GenDateTime;
            foreach (var vendor in vendorLists)
            {
                if (vendors?.Contains(vendor.Key) == false)
                {
                    continue;
                }
                var po = ConvertToPurchaseOrder(vendor.Key, vendor.Value, timestamp);
                if (po == null)
                {
                    continue;
                }
                RemoveFromCart(vendor.Value, timestamp, contextProfileName);
                await CreateAsync(po, contextProfileName).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<IPurchaseOrderModel> CreateFromSalesOrderAsync(
            ISalesOrderModel order,
            bool isDropShip,
            string? contextProfileName)
        {
            var purchaseOrder = RegistryLoaderWrapper.GetInstance<IPurchaseOrder>(contextProfileName);
            // Base
            purchaseOrder.ID = 0;
            var timestamp = DateExtensions.GenDateTime;
            var subIterator = 0;
            var key = $"POForOrder-{order.ID}";
            while ((await CheckExistsAsync(key, contextProfileName).ConfigureAwait(false)).HasValue)
            {
                subIterator++;
                key = $"POForOrder-{order.ID}.{subIterator}";
            }
            purchaseOrder.CustomKey = key;
            purchaseOrder.CreatedDate = timestamp;
            purchaseOrder.UpdatedDate = null;
            purchaseOrder.Active = true;
            // IHaveJsonAttributesBase
            purchaseOrder.JsonAttributes = order.SerializableAttributes?.SerializeAttributesDictionary();
            // IHaveANullableTypeBase<PurchaseOrderType>
            purchaseOrder.TypeID = await Workflows.PurchaseOrderTypes.ResolveWithAutoGenerateToIDAsync(
                null, "Generated from Order", "Generated from Order", "Generated from Order", null, contextProfileName)
                .ConfigureAwait(false);
            // IHaveANullableStatusBase<PurchaseOrderStatus>
            purchaseOrder.StatusID = await Workflows.PurchaseOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                null, "Pending", "Pending", "Pending", null, contextProfileName)
                .ConfigureAwait(false);
            // IHaveANullableStateBase<PurchaseOrderState>
            purchaseOrder.StateID = await Workflows.PurchaseOrderStates.ResolveWithAutoGenerateToIDAsync(
                null, "WORK", "Open", "Open", null, contextProfileName)
                .ConfigureAwait(false);
            // IAmFilterableByNullableStore
            purchaseOrder.StoreID = order.StoreID;
            // ISalesCollectionBase
            purchaseOrder.Total = 0;
            purchaseOrder.SubtotalDiscounts = 0;
            purchaseOrder.SubtotalFees = 0;
            purchaseOrder.SubtotalHandling = 0;
            purchaseOrder.SubtotalItems = 0;
            purchaseOrder.SubtotalShipping = 0;
            purchaseOrder.SubtotalTaxes = 0;
            purchaseOrder.ShippingSameAsBilling = false;
            if (isDropShip)
            {
                purchaseOrder.ShippingContactID = order.ShippingSameAsBilling ?? false
                    ? order.BillingContactID
                    : order.ShippingContactID;
            }
            purchaseOrder.SalesItems = order.SalesItems!
                .Where(x => x.Active && x.TotalQuantity > 0)
                .Select(x => new PurchaseOrderItem
                {
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = x.CustomKey,
                    JsonAttributes = x.SerializableAttributes.SerializeAttributesDictionary(),
                    Name = x.Name,
                    Description = x.Description,
                    ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                    Quantity = x.Quantity,
                    QuantityBackOrdered = x.QuantityBackOrdered,
                    QuantityPreSold = x.QuantityPreSold,
                    Sku = x.Sku,
                    UnitOfMeasure = x.UnitOfMeasure,
                    ProductID = x.ProductID,
                    OriginalCurrencyID = x.OriginalCurrencyID,
                    SellingCurrencyID = x.SellingCurrencyID,
                    UnitCorePrice = x.UnitCorePrice,
                    UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                    UnitSoldPrice = x.UnitSoldPrice,
                    UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                })
                .ToList();
            // */
            // Associate to the Order
            purchaseOrder.AssociatedSalesOrders!.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                CustomKey = order.CustomKey + "|" + purchaseOrder.CustomKey,
                // ReSharper disable once PossibleInvalidOperationException
                MasterID = order.ID,
            });
            // Save and Return
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.PurchaseOrders.Add((PurchaseOrder)purchaseOrder);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    throw new EntityException("Unable to save purchase order created from sales order");
                }
            }
            return purchaseOrder.CreatePurchaseOrderModelFromEntityFull(contextProfileName)!;
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IPurchaseOrder entity,
            IPurchaseOrderModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdatePurchaseOrderFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            entity.EstimatedReceiptDate = model.ActualReceiptDate;
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <summary>Separate items by vendor.</summary>
        /// <param name="collection">The collection.</param>
        /// <returns>A list of.</returns>
        // ReSharper disable UnusedParameter.Local
        private static Dictionary<int, List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>> SeparateItemsByVendor(
            IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> collection)
        // ReSharper restore UnusedParameter.Local
        {
            throw new NotImplementedException();
            ////var vendorLists = new Dictionary<int, List<ISalesItemBaseModel>>();
            ////foreach (var item in collection.Where(x => x.Active && x.VendorProductID.HasValue && x.StatusID == 1))
            ////{
            ////    // ReSharper disable once AssignNullToNotNullAttribute, PossibleInvalidOperationException
            ////    if (!vendorLists.TryGetValue(item.VendorProductID.Value, out var salesItems))
            ////    {
            ////        vendorLists[item.VendorProductID.Value] = salesItems = new List<ISalesItemBaseModel>();
            ////    }
            ////    salesItems.Add(item);
            ////}
            ////return vendorLists;
        }

        /// <summary>Removes from cart.</summary>
        /// <param name="salesItems">        The sales items.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static void RemoveFromCart(
            IEnumerable<ISalesItemBaseModel> salesItems,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var cartItem in salesItems
                .Select(ci => context.CartItems.FirstOrDefault(x => x.ID == ci.ID))
                .Where(ci => ci != null))
            {
                cartItem!.Active = false;
                cartItem.UpdatedDate = timestamp;
            }
        }

        /// <summary>Converts this PurchaseOrderWorkflow to a purchase order.</summary>
        /// <param name="vendorID">  Identifier for the vendor.</param>
        /// <param name="salesItems">The sales items.</param>
        /// <param name="timestamp"> The timestamp Date/Time.</param>
        /// <returns>The given data converted to a purchase order.</returns>
        private static IPurchaseOrderModel ConvertToPurchaseOrder(
            int vendorID,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            DateTime timestamp)
        {
            var total = salesItems.Sum(x => x.UnitCorePrice);
            return new PurchaseOrderModel
            {
                Active = true,
                CreatedDate = timestamp,
                SalesItems = salesItems
                    .Select(x =>
                    {
                        var q = new SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            UpdatedDate = timestamp,
                            ProductID = x.ProductID,
                            Name = x.ProductName,
                            Sku = x.ProductKey,
                            ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                            Quantity = x.Quantity,
                            QuantityBackOrdered = x.QuantityBackOrdered ?? 0m,
                            QuantityPreSold = x.QuantityPreSold ?? 0m,
                            UnitCorePrice = x.UnitCorePrice,
                            UnitSoldPrice = x.UnitCorePrice,
                            UnitOfMeasure = x.UnitOfMeasure,
                        };
                        // TODO: Attributes
                        return q;
                    })
                    .ToList(),
                Totals = new()
                {
                    SubTotal = total,
                },
                VendorID = vendorID,
            };
        }
    }
}
