// <copyright file="SalesOrderCRUDWorkflow.extended.Actions.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Providers.Emails;
    using Utilities;

    public partial class SalesOrderWorkflow
    {
        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ConfirmOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Confirmed", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            var model = entity.CreateSalesOrderModelFromEntityFull(contextProfileName);
            var emailResult = await new SalesOrderConfirmedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: context.ContextProfileName,
                    to: null,
                    parameters: new() { ["salesOrder"] = model, })
                .ConfigureAwait(false);
            if (!emailResult.ActionSucceeded)
            {
                return emailResult;
            }
            if (CEFConfigDictionary.SalesOrderOffHoldEnabled && entity.Status!.Name == "On Hold")
            {
                await new SalesOrderOffHoldNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = model, })
                    .ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> PendingOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Pending", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> HoldOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "On Hold", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            var emailResult = await new SalesOrderConfirmedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: context.ContextProfileName,
                    to: null,
                    parameters: new() { ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName), })
                .ConfigureAwait(false);
            return emailResult.ActionSucceeded
                ? CEFAR.PassingCEFAR()
                : emailResult;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> BackOrderOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var salesOrderEntityFull = entity.CreateSalesOrderModelFromEntityFull(contextProfileName);
            var purchaseOrder = await Workflows.PurchaseOrders.CreateFromSalesOrderAsync(
                    salesOrderEntityFull!,
                    false,
                    contextProfileName)
                .ConfigureAwait(false);
            var statusChange = await StatusChangeAsync(entity, "Backordered", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            var emailResult = await new SalesOrderBackOrderedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: context.ContextProfileName,
                    to: null,
                    parameters: new() { ["salesOrder"] = salesOrderEntityFull, })
                .ConfigureAwait(false);
            if (!emailResult.ActionSucceeded)
            {
                return emailResult.ChangeFailingCEFARType<IPurchaseOrderModel>();
            }
            if (CEFConfigDictionary.SalesOrderOffHoldEnabled && entity.Status!.Name == "On Hold")
            {
                await new SalesOrderOffHoldNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = salesOrderEntityFull, })
                    .ConfigureAwait(false);
            }
            return purchaseOrder.WrapInPassingCEFAR();
        }

        /*
        /// <inheritdoc/>
        public async Task<CEFActionResponse<ISalesOrderModel[]>> SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(
            int id,
            string? contextProfileName,
            bool sendEmail = true)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = context.SalesOrders.FilterByID(id).SingleOrDefault();
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<ISalesOrderModel[]>("ERROR! Could not find the Entity");
            }
            var splitCapableResult = ValidateSplitCapable(entity);
            if (!splitCapableResult.ActionSucceeded)
            {
                splitCapableResult.Messages.Add("ERROR! Order is not Split Capable");
                return splitCapableResult.ChangeFailingCEFARType<ISalesOrderModel[]>();
            }
            var timestamp = DateExtensions.GenDateTime;
            var subOrderForAvailable = CreateChildOrder(id, entity, timestamp, ".1", context);
            var subOrderForBackOrdered = CreateChildOrder(id, entity, timestamp, ".2", context);
            var processItemsResult = ProcessItems(entity, subOrderForAvailable, subOrderForBackOrdered, timestamp);
            if (!processItemsResult.ActionSucceeded)
            {
                processItemsResult.Messages.Add("ERROR! Couldn't process the Items");
                return processItemsResult.ChangeFailingCEFARType<ISalesOrderModel[]>();
            }
            if (subOrderForBackOrdered.SalesItems.Count == 0)
            {
                return CEFAR.FailingCEFAR<ISalesOrderModel[]>(
                    "ERROR! There were no items to be backordered, cannot split");
            }
            // entity.Children.Add(subOrderForAvailable);
            // entity.Children.Add(subOrderForBackOrdered);
            entity.UpdatedDate = timestamp;
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesOrderModel[]>("ERROR! Couldn't save the data");
            }
            entity = context.SalesOrders.FilterByID(id).Single();
            var statusChange = await StatusChangeAsync(entity, "Split", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange.ChangeFailingCEFARType<ISalesOrderModel[]>();
            }
            var stateChange = await StateChangeAsync(entity, "History", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange.ChangeFailingCEFARType<ISalesOrderModel[]>();
            }
            var retVal = new[]
            {
                subOrderForAvailable.CreateSalesOrderModelFromEntityFull(),
                subOrderForBackOrdered.CreateSalesOrderModelFromEntityFull()
            };
            // ReSharper disable once InvertIf
            if (sendEmail)
            {
                var emailResult = await Workflows.EmailQueues.QueueSalesOrderSplitNotificationAsync(
                        entity.CreateSalesOrderModelFromEntityFull(),
                        contextProfileName)
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    // Fail but still send the created sub orders
                    return retVal.WrapInFailingCEFAR(emailResult.Messages.ToArray());
                }
            }
            return retVal.WrapInPassingCEFAR();
        }
        */

        /// <inheritdoc/>
        public async Task<CEFActionResponse> AddPaymentToOrderAsync(
            int id,
            IPaymentModel payment,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(payment);
            if (payment.Amount is null or <= 0)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot add a payment without a positive amount.");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            // ReSharper disable once PossibleInvalidOperationException
            if (entity.BalanceDue < payment.Amount.Value)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot apply a payment for more than the Balance Due on the Order");
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            entity.BalanceDue -= payment.Amount.Value;
            CEFActionResponse<int> emailResult;
            if (entity.BalanceDue <= 0)
            {
                var statusChange = await StatusChangeAsync(entity, "Full Payment Received", context, contextProfileName).ConfigureAwait(false);
                if (!statusChange.ActionSucceeded)
                {
                    return statusChange;
                }
                emailResult = await new SalesOrderFullPaymentNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                            ["payment"] = payment,
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult;
                }
            }
            else
            {
                var statusChange = await StatusChangeAsync(entity, "Partial Payment Received", context, contextProfileName).ConfigureAwait(false);
                if (!statusChange.ActionSucceeded)
                {
                    return statusChange;
                }
                emailResult = await new SalesOrderPartialPaymentNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                            ["payment"] = payment,
                        })
                    .ConfigureAwait(false);
            }
            return emailResult.ActionSucceeded
                ? CEFAR.PassingCEFAR()
                : emailResult;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CapturePaymentForOrderAsync(int id, string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var order = await context.SalesOrders.FilterByID(id).SingleAsync().ConfigureAwait(false);
            ////Contract.RequiresValidKey(entity.PaymentTransactionID);
            if (order.Total <= 0)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot capture a payment for an order without a positive total");
            }
            var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (provider == null)
            {
                return CEFAR.FailingCEFAR("ERROR! No payment provider detected");
            }
            await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
            var payment = RegistryLoaderWrapper.GetInstance<IPaymentModel>(contextProfileName);
            payment.Amount = order.Total;
            var result = await provider.CaptureAsync(order.PaymentTransactionID!, payment, contextProfileName).ConfigureAwait(false);
            if (!result.Approved)
            {
                return CEFAR.FailingCEFAR("ERROR! Transaction not approved");
            }
            order.PaymentTransactionID = result.TransactionID;
            order.BalanceDue = Math.Max(0m, (order.BalanceDue ?? 0) - order.Total);
            // NOTE: This function will save the order
            var statusChange = await StatusChangeAsync(order, "Full Payment Received", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            // NOTE: This function will save the order
            var stateChange = await StateChangeAsync(order, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            var emailResult = await new SalesOrderFullPaymentNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: context.ContextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesOrder"] = order.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        ["payment"] = payment,
                    })
                .ConfigureAwait(false);
            return emailResult.ActionSucceeded
                ? CEFAR.PassingCEFAR()
                : emailResult;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>>> CreatePickTicketForOrderAsync(
            int id,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Processing", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange
                    .ChangeFailingCEFARType<List<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>>();
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange
                    .ChangeFailingCEFARType<List<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>>();
            }
            var result = entity.SalesItems!
                .Where(x => x.Active && x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m) > 0m)
                .Select(x => ModelMapperForSalesOrderItem.MapSalesOrderItemModelFromEntityLite(x, contextProfileName))
                .ToList()
                .WrapInPassingCEFAR();
            try
            {
                var emailResult = await new SalesOrderProcessingNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult
                        .ChangeFailingCEFARType<List<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>>();
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add(
                    $"Email: There was an error sending the email but the status and state changes succeeded. {ex.Message}");
            }
            return result!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IPurchaseOrderModel>> DropShipOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders
                .FilterByID(id)
                .SingleAsync()
                .ConfigureAwait(false);
            var purchaseOrder = await Workflows.PurchaseOrders.CreateFromSalesOrderAsync(
                    entity.CreateSalesOrderModelFromEntityFull(contextProfileName)!,
                    true,
                    contextProfileName)
                .ConfigureAwait(false);
            var statusChange = await StatusChangeAsync(entity, "Shipped from Vendor", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange.ChangeFailingCEFARType<IPurchaseOrderModel>();
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange.ChangeFailingCEFARType<IPurchaseOrderModel>();
            }
            try
            {
                var emailResult = await new SalesOrderDropShipNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult.ChangeFailingCEFARType<IPurchaseOrderModel>();
                }
            }
            catch (Exception ex)
            {
                return purchaseOrder.WrapInPassingCEFAR(
                    $"Email: There was an error sending the email but the status and state changes succeeded. {ex.Message}")!;
            }
            return purchaseOrder.WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ReadyForPickupOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Ready for Pickup", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            try
            {
                var emailResult = await new SalesOrderReadyForPickupNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult;
                }
            }
            catch (Exception ex)
            {
                return CEFAR.PassingCEFAR(
                    $"Email: There was an error sending the email but the status and state changes succeeded. {ex.Message}");
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ShipOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Shipped", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            try
            {
                var emailResult = await new SalesOrderShippedNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult;
                }
            }
            catch (Exception ex)
            {
                return CEFAR.PassingCEFAR(
                    $"Email: There was an error sending the email but the status and state changes succeeded. {ex.Message}");
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CompleteOrderAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Completed", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "History", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            try
            {
                var emailResult = await new SalesOrderCompletedNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult;
                }
            }
            catch (Exception ex)
            {
                return CEFAR.PassingCEFAR(
                    $"Email: There was an error sending the email but the status and state changes succeeded. {ex.Message}");
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> VoidOrderAsync(
            int id,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            var statusChange = await StatusChangeAsync(entity, "Void", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "History", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            if (taxesProvider != null)
            {
                await taxesProvider.VoidOrderAsync(entity.CreateSalesOrderModelFromEntityFull(contextProfileName)!, contextProfileName).ConfigureAwait(false);
            }
            try
            {
                var emailResult = await new SalesOrderVoidedNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: context.ContextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                        })
                    .ConfigureAwait(false);
                if (!emailResult.ActionSucceeded)
                {
                    return emailResult;
                }
            }
            catch (Exception ex)
            {
                return CEFAR.PassingCEFAR(
                    $"Email: There was an error sending the email but the status and state changes succeeded. {ex.Message}");
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> MarkItemsInStockAsync(
            int id,
            Guid check,
            string? contextProfileName)
        {
            if (check == default)
            {
                return CEFAR.FailingCEFAR("GUID Invalid");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders.FilterByID(id).SingleAsync();
            if (!entity.SerializableAttributes.TryGetValue("InStorePickupGuid", out var attribute))
            {
                return CEFAR.FailingCEFAR("SO GUID Invalid");
            }
            var guidValue = Guid.Parse(attribute.Value);
            if (guidValue != check)
            {
                return CEFAR.FailingCEFAR("GUID Do not match");
            }
            var statusChange = await StatusChangeAsync(entity, "Ready for Pickup", context, contextProfileName).ConfigureAwait(false);
            if (!statusChange.ActionSucceeded)
            {
                return statusChange;
            }
            var stateChange = await StateChangeAsync(entity, "Work", context, contextProfileName).ConfigureAwait(false);
            if (!stateChange.ActionSucceeded)
            {
                return stateChange;
            }
            var emailResult = await new SalesOrderReadyForPickupNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: context.ContextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesOrder"] = entity.CreateSalesOrderModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return emailResult.ActionSucceeded ? CEFAR.PassingCEFAR() : emailResult;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> MarkItemsNotInStockAsync(
            int id,
            Guid check,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            if (check == default)
            {
                return CEFAR.FailingCEFAR("GUID Invalid");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesOrders
                .AsNoTracking()
                .FilterByID(id)
                .Select(x => x.JsonAttributes)
                .SingleAsync();
            if (!entity.DeserializeAttributesDictionary().TryGetValue("InStorePickupGuid", out var attribute))
            {
                return CEFAR.FailingCEFAR("SO GUID Invalid");
            }
            var guidValue = Guid.Parse(attribute.Value);
            if (guidValue != check)
            {
                return CEFAR.FailingCEFAR("GUID Do not match");
            }
            return await VoidOrderAsync(id, taxesProvider, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Status change.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="status">            The status.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected async Task<CEFActionResponse> StatusChangeAsync(
            ISalesOrder entity,
            string status,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            if (entity.Status!.Name == status)
            {
                return CEFAR.PassingCEFAR($"INFO! No action taken as the order is already on status '{status}'");
            }
            var statusModel = RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName);
            statusModel.CustomKey = statusModel.Name = statusModel.DisplayName = status;
            try
            {
                entity.StatusID = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: null,
                        byName: null,
                        byDisplayName: null,
                        model: statusModel,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                entity.UpdatedDate = DateExtensions.GenDateTime;
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(
                    $"ERROR! Unable to save locate or generate the order status '{status}': {ex.Message}");
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR($"ERROR! Unable to save the new status '{status}' for the order.");
        }

        /// <summary>State change.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="state">             The state.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected async Task<CEFActionResponse> StateChangeAsync(
            ISalesOrder entity,
            string state,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            if (entity.State?.Name == state)
            {
                return CEFAR.PassingCEFAR($"INFO! No action taken as the order is already on state '{state}'");
            }
            var stateModel = RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName);
            stateModel.CustomKey = stateModel.Name = stateModel.DisplayName = state;
            try
            {
                entity.StateID = await Workflows.SalesOrderStates.ResolveWithAutoGenerateToIDAsync(
                        null,
                        null,
                        null,
                        null,
                        stateModel,
                        contextProfileName)
                    .ConfigureAwait(false);
                entity.UpdatedDate = DateExtensions.GenDateTime;
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(
                    $"ERROR! Unable to save locate or generate the order state '{state}': {ex.Message}");
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR($"ERROR! Unable to save the new state '{state}' for the order.");
        }

        private static SalesOrderItem ConvertToSalesOrderItem(
            string masterKey,
            DateTime timestamp,
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> model)
        {
            return new()
            {
                // IBase
                Active = true,
                CustomKey = Contract.CheckValidKey(model.CustomKey)
                    ? model.CustomKey
                    : Contract.CheckAllValidKeys(masterKey, model.Sku)
                        ? $"{masterKey}|{model.Sku}"
                        : null,
                CreatedDate = timestamp,
                JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary(),
                Hash = model.Hash,
                // INameableBase
                Name = model.Name ?? model.ProductName,
                Description = model.Description ?? model.ProductDescription,
                // ISalesItemBase
                UnitCorePrice = model.UnitCorePrice,
                UnitSoldPrice = model.UnitSoldPrice,
                UnitOfMeasure = model.UnitOfMeasure,
                Sku = model.Sku,
                ForceUniqueLineItemKey = model.ForceUniqueLineItemKey,
                Quantity = model.Quantity,
                QuantityBackOrdered = model.QuantityBackOrdered ?? 0m,
                QuantityPreSold = model.QuantityPreSold ?? 0m,
                UnitCorePriceInSellingCurrency = model.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = model.UnitSoldPriceInSellingCurrency,
                // Related Objects
                // Skipped : MasterID
                ProductID = model.ProductID,
                UserID = model.UserID,
                OriginalCurrencyID = null,
                SellingCurrencyID = null,
                // Associated Objects
                Discounts = model.Discounts
                    ?.Where(x => x.Active)
                    .Select(x => new AppliedSalesOrderItemDiscount
                    {
                        // IBase
                        Active = true,
                        CreatedDate = timestamp,
                        UpdatedDate = x.UpdatedDate,
                        CustomKey = x.CustomKey,
                        Hash = x.Hash,
                        JsonAttributes = x.SerializableAttributes.SerializeAttributesDictionary(),
                        // IAppliedDiscount
                        SlaveID = x.ID,
                        DiscountTotal = x.DiscountTotal,
                        ApplicationsUsed = x.ApplicationsUsed,
                    })
                    .ToList(),
                Notes = null, // TODO: Item Notes
                Targets = null, // TODO: Item Targets
            };
        }

        /*
        /// <summary>Validates the split capable described by entity.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static CEFActionResponse ValidateSplitCapable(SalesOrder entity)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Could not find the Entity");
            }
            if (!entity.Active)
            {
                return CEFAR.FailingCEFAR("ERROR! The order was not Active, cannot split");
            }
            if (!entity.SalesItems.Any(x => x.Active))
            {
                return CEFAR.FailingCEFAR("ERROR! The order did not have any active items, cannot split");
            }
            if (entity.SalesItems.Count(x => x.Active) == 1)
            {
                return CEFAR.FailingCEFAR("ERROR! The order only had one active line item, cannot split");
            }
            return CEFAR.PassingCEFAR();
        }
        */

        /*
        /// <summary>Creates child order.</summary>
        /// <param name="id">       The identifier.</param>
        /// <param name="entity">   The entity.</param>
        /// <param name="timeStamp">The time stamp Date/Time.</param>
        /// <param name="subKey">   The sub key.</param>
        /// <param name="context">  The context.</param>
        /// <returns>The new child order.</returns>
        private static SalesOrder CreateChildOrder(
            int id,
            ISalesOrder entity,
            DateTime timeStamp,
            string subKey,
            IClarityEcommerceEntities context)
        {
            return new SalesOrder
            {
                Active = true,
                CustomKey = (entity.CustomKey ?? entity.ID.ToString()) + subKey,
                CreatedDate = timeStamp,
                SalesItems = new HashSet<SalesOrderItem>(),
                AccountID = entity.AccountID,
                BillingContactID = entity.BillingContactID,
                ShippingSameAsBilling = entity.ShippingSameAsBilling,
                ShippingContactID = entity.ShippingContactID,
                OrderApprovedDate = entity.OrderApprovedDate,
                PurchaseOrderNumber = entity.PurchaseOrderNumber,
                ParentID = id,
                StatusID = entity.StatusID,
                StateID = entity.StateID,
                TypeID = context.SalesOrderTypes.AsNoTracking().FirstOrDefault(x => x.Name == "Sales Order Child")?.ID ?? 4
            };
        }
        */

        /*
        /// <summary>Process the items.</summary>
        /// <param name="entity">                The entity.</param>
        /// <param name="subOrderForAvailable">  The sub order for available.</param>
        /// <param name="subOrderForBackOrdered">The sub order for back ordered.</param>
        /// <param name="timeStamp">             The time stamp Date/Time.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static CEFActionResponse ProcessItems(
            ISalesOrder entity,
            ISalesOrder subOrderForAvailable,
            ISalesOrder subOrderForBackOrdered,
            DateTime timeStamp)
        {
            foreach (var originalItem in entity.SalesItems.Where(x => x.Active))
            {
                if (originalItem.QuantityBackOrdered > 0m)
                {
                    subOrderForBackOrdered.SalesItems.Add(CloneSalesOrderItem(originalItem, timeStamp));
                }
                else
                {
                    subOrderForAvailable.SalesItems.Add(CloneSalesOrderItem(originalItem, timeStamp));
                }
                originalItem.UpdatedDate = timeStamp;
            }
            return CEFAR.PassingCEFAR();
        }
        */

        /*
        /// <summary>Clone sales order item.</summary>
        /// <param name="originalItem">The original item.</param>
        /// <param name="timeStamp">   The time stamp Date/Time.</param>
        /// <returns>A SalesOrderItem.</returns>
        private static SalesOrderItem CloneSalesOrderItem(ISalesOrderItem originalItem, DateTime timeStamp)
        {
            return new SalesOrderItem
            {
                // Base Properties
                Active = true,
                CreatedDate = timeStamp,
                CustomKey = originalItem.CustomKey,
                UpdatedDate = null,
                JsonAttributes = originalItem.SerializableAttributes.SerializeAttributesDictionary(),
                // NameableBase Properties
                Name = originalItem.Name,
                Description = originalItem.Description,
                // Sales Item Properties
                UnitCorePrice = originalItem.UnitCorePrice,
                UnitSoldPrice = originalItem.UnitSoldPrice,
                UnitOfMeasure = originalItem.UnitOfMeasure,
                Quantity = originalItem.Quantity,
                QuantityBackOrdered = originalItem.QuantityBackOrdered ?? 0m,
                QuantityPreSold = originalItem.QuantityPreSold ?? 0m,
                Sku = originalItem.Sku,
                ForceUniqueLineItemKey = originalItem.ForceUniqueLineItemKey,
                // Related Objects
                ProductID = originalItem.ProductID,
                // Collections
                Discounts = originalItem.Discounts
                    ?.Where(y => y.Active)
                    .Select(y => new AppliedSalesOrderItemDiscount
                    {
                        Active = true,
                        CustomKey = y.CustomKey,
                        CreatedDate = timeStamp,
                        UpdatedDate = null,
                        SlaveID = y.SlaveID,
                        DiscountTotal = y.DiscountTotal,
                    })
                    .ToList()
            };
        }
        */

        /// <summary>Associate sales items.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AssociateSalesItemsAsync(
            ISalesOrderModel model,
            ISalesOrder entity,
            DateTime timestamp,
            string? contextProfileName)
        {
            // TODO@JTG: Use the new associate process so we don't wipe and re-add
            if (model.SalesItems == null)
            {
                return;
            }
            // Remove current Items
            foreach (var item in entity.SalesItems!)
            {
                item.UpdatedDate = timestamp;
                item.Active = false;
            }
            foreach (var item in model.SalesItems)
            {
                if (!item.Active)
                {
                    continue;
                }
                var productID = await Workflows.Products.ResolveToIDOptionalAsync(
                        item.ProductID,
                        item.ProductKey ?? item.Sku,
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (!Contract.CheckValidID(productID) && Contract.CheckValidKey(item.Sku))
                {
                    // Create a new invisible product to show legacy data. Product Managers can update the content later
                    item.ProductID = await Workflows.Products.CreateLegacyProductWithKeyAsync(
                            Contract.RequiresValidKey(item.ProductKey ?? item.Sku),
                            Contract.RequiresValidKey(item.ProductName ?? item.Name ?? item.Sku),
                            contextProfileName)
                        .ConfigureAwait(false);
                }
                entity.SalesItems.Add(ConvertToSalesOrderItem(model.CustomKey!, timestamp, item));
            }
        }
    }
}
