// <copyright file="SalesOrderService.StateChanges.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order service class</summary>
#nullable enable
#pragma warning disable 1584, SA1118 // Parameter should not span multiple lines
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A confirm sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Confirm"),
        Route("/Ordering/SalesOrder/Confirm/{ID}", "PATCH",
            Summary = "The order items each have sufficient stock and will be allocated against their stock (reducing"
                + " each). The order status will be set to 'Confirmed'. An email notification will be sent to the"
                + " customer.")]
    public class ConfirmSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>Revert to pending for a sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Pending"),
        Route("/Ordering/SalesOrder/Pending/{ID}", "PATCH",
            Summary = "The order items each have sufficient stock and will be allocated against their stock (reducing"
                + " each). The order status will be set to 'Confirmed'. An email notification will be sent to the"
                + " customer.")]
    public class PendingSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A confirm sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Hold"),
        Route("/Ordering/SalesOrder/Hold/{ID}", "PATCH",
            Summary = "The order items each have sufficient stock and will be allocated against their stock (reducing"
                + " each). The order status will be set to 'On Hold'. An email notification will be sent to the"
                + " customer.")]
    public class HoldSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A backorder sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Backorder"),
        Route("/Ordering/SalesOrder/Backorder/{ID}", "PATCH",
            Summary = "The order items do not have sufficient stock. The order status will be set to 'Backordered'. An"
                + " email notification will be sent to the customer. A Purchase Order should be created and reference"
                + " this order by an Inventory Manager to refill stock.")]
    public class BackorderSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>An add payment to sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.AddPayment"),
        Route("/Ordering/SalesOrder/AddPayment", "PATCH",
            Summary = "Take payment information from the customer and perform an Authorize and/or Capture from the Payment"
                + " Provider. If the total of all payments is less than the Balance Due, the status will be set to"
                + "'Partial Payment Received', otherwise 'Full Payment Received'. An email notification will be sent to"
                + " the customer.")]
    public class AddPaymentToSalesOrder : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int ID { get; set; }

        /// <summary>Gets or sets the payment.</summary>
        /// <value>The payment.</value>
        [ApiMember(Name = nameof(Payment), DataType = "PaymentModel", ParameterType = "body", IsRequired = true,
            Description = "The Payment information. This will only be Authorized and not Captured with the Payment Provider")]
        public PaymentModel Payment { get; set; } = null!;
    }

    /// <summary>A capture payment for sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.AddPayment"),
        Route("/Ordering/SalesOrder/CapturePayment/{ID}", "PATCH",
            Summary = "Captures a pre-authorized payment on the order. No status change will occur.")]
    public class CapturePaymentForSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A create pick ticket for sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{List{SalesItemBaseModel}}}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.CreatePickTicket"),
        Route("/Ordering/SalesOrder/CreatePickTicket/{ID}", "PATCH",
            Summary = "Creates a printable Pick Ticket for the Warehouse to locate products for the order. The order will"
                + " be set to the 'Processing' status.")]
    public class CreatePickTicketForSalesOrder
        : ImplementsIDBase,
            IReturn<CEFActionResponse<List<SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>>>>
    {
    }

    /// <summary>A drop ship sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{PurchaseOrderModel}}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.DropShip"),
        Route("/Ordering/SalesOrder/DropShip/{ID}", "PATCH",
            Summary = "A Purchase Order will be created with this order's line items where a Vendor can be selected that"
                + " allows Drop Shipping. The order will be set to 'Shipped from Vendor' status. An email notification"
                + " will be sent to the customer.")]
    public class DropShipSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse<PurchaseOrderModel>>
    {
    }

    /// <summary>A ship sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Ship"),
        Route("/Ordering/SalesOrder/Ship/{ID}", "PATCH",
            Summary = "The order will be set to 'Shipped' status. An email notification will be sent to the customer.")]
    public class ShipSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A ready for pickup sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.ReadyForPickup"),
        Route("/Ordering/SalesOrder/ReadyForPickup/{ID}", "PATCH",
            Summary = "The order will be set to 'Ready For Pickup' status. An email notification will be sent to the customer.")]
    public class ReadyForPickupSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A complete sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Complete"),
        Route("/Ordering/SalesOrder/Complete/{ID}", "PATCH",
            Summary = "The order will be set to 'Completed' status, no further modifications will be allowed. An email "
                + "notification will be sent to the customer.")]
    public class CompleteSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A void sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Void"),
        Route("/Ordering/SalesOrder/Void/{ID}", "PATCH",
            Summary = "Void the order. It will no longer be processed and will be visible on the Completed Orders view. "
                + "An email notification will be sent to the customer.")]
    public class VoidSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>The sales order items in stock.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Ordering/SalesOrder/InStock/{ID}/{Guid}", "PATCH",
            Summary = "Void the order. It will no longer be processed and will be visible on the Completed Orders view. "
                + "An email notification will be sent to the customer.")]
    public class MarkSalesOrderItemsInStock : ImplementsIDBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets a unique identifier.</summary>
        /// <value>The identifier of the unique.</value>
        [ApiMember(Name = nameof(Guid), DataType = "Guid", ParameterType = "path", IsRequired = true)]
        public Guid Guid { get; set; }
    }

    /// <summary>The sales order items not in stock.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Ordering/SalesOrder/NotInStock/{ID}/{Guid}", "PATCH",
            Summary = "Void the order. It will no longer be processed and will be visible on the Completed Orders view. "
                + "An email notification will be sent to the customer.")]
    public class MarkSalesOrderItemsNotInStock : ImplementsIDBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets a unique identifier.</summary>
        /// <value>The identifier of the unique.</value>
        [ApiMember(Name = nameof(Guid), DataType = "Guid", ParameterType = "path", IsRequired = true)]
        public Guid Guid { get; set; }
    }

    /// <summary>Updating status of sales order.</summary>
    /// <seealso cref="ServiceStack.IReturn{Clarity.Ecommerce.Models.CEFActionResponse}"/>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate, RequiredPermission("Ordering.SalesOrder.View", "Storefront.UserDashboard.SalesOrders.View"),
        Route("/Ordering/SalesOrder/PendingSalesOrders/{SalesOrderID}/Update", "PATCH",
            Summary = "Update status of order. Doctor can approve, deny or modify.")]
    public class UpdateStatusOfSalesOrder : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the status update.</summary>
        /// <value>The status update.</value>
        [ApiMember(Name = nameof(StatusUpdate), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string StatusUpdate { get; set; } = null!;

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        [ApiMember(Name = nameof(SalesOrderID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int SalesOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int ProductID { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [ApiMember(Name = nameof(Quantity), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int Quantity { get; set; }
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Patch(ConfirmSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.ConfirmOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(PendingSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.PendingOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(HoldSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.HoldOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(BackorderSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.BackOrderOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(AddPaymentToSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.AddPaymentToOrderAsync(request.ID, request.Payment, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(CapturePaymentForSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.CapturePaymentForOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(CreatePickTicketForSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.CreatePickTicketForOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(DropShipSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.DropShipOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(ShipSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.ShipOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(ReadyForPickupSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.ReadyForPickupOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(CompleteSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.CompleteOrderAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(VoidSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.VoidOrderAsync(
                    id: request.ID,
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(MarkSalesOrderItemsInStock request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.MarkItemsInStockAsync(
                    id: request.ID,
                    guid: request.Guid,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(MarkSalesOrderItemsNotInStock request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
            return await Workflows.SalesOrders.MarkItemsNotInStockAsync(
                    id: request.ID,
                    guid: request.Guid,
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UpdateStatusOfSalesOrder request)
        {
            await ThrowIfNoRightsToRecordSalesOrderAsync(request.SalesOrderID).ConfigureAwait(false);
            return await Workflows.SalesOrders.UpdateStatusOfSalesOrderAsync(
                    statusUpdate: request.StatusUpdate,
                    productID: request.ProductID,
                    quantity: request.Quantity,
                    salesOrderID: request.SalesOrderID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
