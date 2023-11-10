// <copyright file="PayoneerPaymentsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer payments provider class</summary>
// ReSharper disable RedundantStringInterpolation, StringLiteralTypo, StyleCop.SA1025, StyleCop.SA1123
// ReSharper disable StyleCop.SA1204
// ReSharper disable PossibleNullReferenceException
// ReSharper disable CommentTypo
// ReSharper disable RegionWithinTypeMemberBody
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Interfaces.Workflow;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Utilities;

    /// <summary>A payoneer payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public class PayoneerPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PayoneerPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets or sets the web client factory.</summary>
        /// <value>The web client factory.</value>
        public IWebClientFactory WebClientFactory { private get; set; } = new SystemWebClientFactory();

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets web client.</summary>
        /// <returns>The web client.</returns>
        private IWebClient GetWebClient()
        {
            var client = WebClientFactory.Create();
            client.BaseAddress = PayoneerPaymentsProviderConfig.GetURL;
            client.Headers = new()
            {
                ["Content-Type"] = "application/json",
                ["x-armorpayments-apikey"] = PayoneerPaymentsProviderConfig.APIKey,
                ["x-armorpayments-requesttimestamp"] = DateExtensions.GenDateTime.ToString("yyyy-MM-ddThh:mm:sszzz"),
            };
            return client;
        }

        /// <summary>Makes a signature.</summary>
        /// <param name="client">  The client.</param>
        /// <param name="method">  The method.</param>
        /// <param name="endpoint">The endpoint.</param>
#pragma warning disable SA1204 // Static elements should appear before instance elements
        private static void MakeSignature(IWebClient client, string method, string endpoint)
#pragma warning restore SA1204 // Static elements should appear before instance elements
        {
            using SHA512 shaM = new SHA512Managed();
            var hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(
                $"{PayoneerPaymentsProviderConfig.APISecret}:{method}:{endpoint}:{client.Headers["x-armorpayments-requesttimestamp"]}"));
            client.Headers["x-armorpayments-signature"] = ByteArrayToString(hash);
        }

        /// <summary>Byte array to string.</summary>
        /// <param name="ba">The ba.</param>
        /// <returns>A string.</returns>
        private static string ByteArrayToString(IReadOnlyCollection<byte> ba)
        {
            var hex = new StringBuilder(ba.Count * 2);
            foreach (var b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        /// <summary>Handles the web exception described by ex.</summary>
        /// <param name="ex">Details of the exception.</param>
        private static void HandleWebException(WebException ex)
        {
            // TODO: Log the error to the database
            using var failStream = ex.Response!.GetResponseStream();
            if (failStream == null)
            {
                return;
            }
            var failBuffer = new byte[failStream.Length];
            failStream.Read(failBuffer, 0, (int)failStream.Length);
            var failResultRaw = failBuffer.GetUTF8DecodedString()!;
            try
            {
                var failResult = JsonConvert.DeserializeObject<PayoneerError>(failResultRaw);
                foreach (var error in failResult!.Errors!)
                {
                    switch (error.Value)
                    {
                        case JArray:
                        {
                            break;
                        }
                        case string:
                        {
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Do Nothing
            }
        }

        #region Handle incoming Webhook Events
        /// <summary>Handles the webhook event.</summary>
        /// <param name="workflows">         The workflows.</param>
        /// <param name="apiKeyReturn">      The API key return.</param>
        /// <param name="orderEventReturn">  The order event return.</param>
        /// <param name="orderReturn">       The order return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CyclomaticComplexity
#pragma warning disable SA1202 // Elements should be ordered by access
        public static async Task HandleWebhookEventAsync(
#pragma warning restore SA1202 // Elements should be ordered by access
            IWorkflowsController workflows,
            ApiKeyWebhookReturn apiKeyReturn,
            OrderEventWebhookReturn orderEventReturn,
            OrderWebhookReturn orderReturn,
            string? contextProfileName)
        {
            Contract.RequiresValidID(orderEventReturn.OrderID, "No Payoneer Order ID, cannot process Order Webhook Event");
            var order = (await workflows.SalesOrders.GetByPayoneerOrderIDAsync(orderEventReturn.OrderID, contextProfileName).ConfigureAwait(false))!;
            Contract.RequiresNotNull(order, $"Unable to locate Order by Payoneer Order ID {orderEventReturn.OrderID}");
            var eventModel = RegistryLoaderWrapper.GetInstance<ISalesOrderEventModel>(contextProfileName);
            eventModel.Active = true;
            eventModel.CreatedDate = DateExtensions.GenDateTime;
            eventModel.CustomKey = "Payoneer:" + orderEventReturn.EventID;
            eventModel.Description = JsonConvert.SerializeObject(new object[] { apiKeyReturn, orderEventReturn, orderReturn });
            eventModel.OldRecordSerialized = JsonConvert.SerializeObject(order);
            eventModel.SerializableAttributes = new();
            eventModel.MasterID = order.ID;
            eventModel.TypeName = "Payoneer Webhook";
            UpsertPayoneerAttribute("Payoneer-Event-ID", eventModel.SerializableAttributes, orderEventReturn.EventID);
            UpsertPayoneerAttribute("Payoneer-Order-ID", eventModel.SerializableAttributes, orderEventReturn.OrderID);
            UpsertPayoneerAttribute("Payoneer-Account-ID", eventModel.SerializableAttributes, orderEventReturn.AccountID);
            UpsertPayoneerAttribute("Payoneer-Order-Event-Type", eventModel.SerializableAttributes, orderEventReturn.Type);
            UpsertPayoneerAttribute("Payoneer-Order-Event-URI", eventModel.SerializableAttributes, orderEventReturn.URI);
            UpsertPayoneerAttribute("Payoneer-Order-Event-Data", eventModel.SerializableAttributes, JsonConvert.SerializeObject(orderEventReturn));
            UpsertPayoneerAttribute("Payoneer-Order-Data", eventModel.SerializableAttributes, orderEventReturn.EventID);
            var updateOrder = false;
            var newPayoneerOrderStatus = Contract.CheckValidID(orderReturn.Status)
                ? (PayoneerOrderStatuses)orderReturn.Status!.Value
                : (PayoneerOrderStatuses?)null;
            if (newPayoneerOrderStatus != null)
            {
                var previousStatus = int.Parse(order.SerializableAttributes!["Payoneer-Order-Status-ID"].Value);
                if ((PayoneerOrderStatuses)previousStatus != newPayoneerOrderStatus)
                {
                    UpsertPayoneerAttribute("Payoneer-Order-Status-ID-Old", eventModel.SerializableAttributes, int.Parse(order.SerializableAttributes["Payoneer-Order-Status-ID"].Value));
                    UpsertPayoneerAttribute("Payoneer-Order-Status-ID-New", eventModel.SerializableAttributes, orderReturn.Status);
                    UpsertPayoneerAttribute("Payoneer-Order-Status-ID", order.SerializableAttributes, orderReturn.Status);
                    updateOrder = true;
                }
            }
            // ReSharper disable MultipleSpaces
            switch ((PayoneerOrderEventTypes)orderEventReturn.Type)
            {
                case PayoneerOrderEventTypes.Paid:
                case PayoneerOrderEventTypes.PaymentAdded:
                {
                    eventModel.Name = "Payoneer Escrow Order Received a Payment";
                    eventModel.OldBalanceDue = order.BalanceDue ?? order.Totals.Total;
                    eventModel.NewBalanceDue = order.BalanceDue = (orderReturn.Amount ?? order.Totals.Total) - (orderReturn.Balance ?? 0);
                    var salesOrderPaymentModel = RegistryLoaderWrapper.GetInstance<ISalesOrderPaymentModel>(contextProfileName);
                    var paymentModel = RegistryLoaderWrapper.GetInstance<IPaymentModel>(contextProfileName);
                    salesOrderPaymentModel.Active = paymentModel.Active = true;
                    salesOrderPaymentModel.CreatedDate = paymentModel.CreatedDate = orderEventReturn.Created;
                    paymentModel.BillingContactID = order.BillingContactID;
                    paymentModel.Amount = Math.Max(0m, eventModel.OldBalanceDue.Value - eventModel.NewBalanceDue.Value); // The difference was the amount paid
                    salesOrderPaymentModel.Slave = paymentModel;
                    order.SalesOrderPayments!.Add(salesOrderPaymentModel);
                    updateOrder = true;
                    switch (newPayoneerOrderStatus)
                    {
                        #region New/Sent
                        case PayoneerOrderStatuses.New when new[] { "Pending", "Confirmed", null }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Pending", "Confirmed", null }.Contains(order.StatusKey):
                        {
                            eventModel.OldStatusID = order.StatusID;
                            order.StatusID = 0;
                            order.Status = null;
                            order.StatusName = null;
                            order.StatusKey = "Partial Payment Received";
                            // TODO: eventModel.NewStatusID = order.StatusKey lookup
                            break;
                        }
                        case PayoneerOrderStatuses.New when new[] { "Partial Payment Received", "Backordered" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Partial Payment Received", "Backordered" }.Contains(order.StatusKey):
                        {
                            // Do Nothing
                            break;
                        }
                        case PayoneerOrderStatuses.New when new[] { "Full Payment Received" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Full Payment Received" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException(
                                "Invalid, this order has already received full payment and should not accept further payments.");
                        }
                        case PayoneerOrderStatuses.New when new[] { "Processing" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Processing" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order is already in processing.");
                        }
                        case PayoneerOrderStatuses.New when new[] { "Shipped from Vendor", "Shipped" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Shipped from Vendor", "Shipped" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has already shipped.");
                        }
                        case PayoneerOrderStatuses.New when new[] { "Completed" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Completed" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has already completed.");
                        }
                        case PayoneerOrderStatuses.New when new[] { "Void" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Void" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has been voided.");
                        }
                        case PayoneerOrderStatuses.New when new[] { "Split" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Sent when new[] { "Split" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException(
                                "Invalid, this order shouldn't receive payments as it has been split to separate orders and shouldn't be modified any further.");
                        }
                        case PayoneerOrderStatuses.New:
                        case PayoneerOrderStatuses.Sent:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                        #region Paid
                        case PayoneerOrderStatuses.Paid when new[] { "Pending", "Confirmed", "Partial Payment Received", null }.Contains(order.StatusKey):
                        {
                            eventModel.OldStatusID = order.StatusID;
                            order.StatusID = 0;
                            order.Status = null;
                            order.StatusName = null;
                            order.StatusKey = "Full Payment Received";
                            // TODO: eventModel.NewStatusID = order.StatusKey lookup
                            break;
                        }
                        case PayoneerOrderStatuses.Paid when new[] { "Full Payment Received", "Backordered" }.Contains(order.StatusKey):
                        {
                            // Do Nothing
                            break;
                        }
                        case PayoneerOrderStatuses.Paid when new[] { "Processing" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order is already in processing.");
                        }
                        case PayoneerOrderStatuses.Paid when new[] { "Shipped from Vendor", "Shipped" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has already shipped.");
                        }
                        case PayoneerOrderStatuses.Paid when new[] { "Completed" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has already completed.");
                        }
                        case PayoneerOrderStatuses.Paid when new[] { "Void" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has been voided.");
                        }
                        case PayoneerOrderStatuses.Paid when new[] { "Split" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException(
                                "Invalid, this order shouldn't receive payments as it has been split to separate orders and shouldn't be modified any further.");
                        }
                        case PayoneerOrderStatuses.Paid:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                        #region Other
                        case PayoneerOrderStatuses.Shipped:
                        case PayoneerOrderStatuses.Delivered:
                        case PayoneerOrderStatuses.Released:
                        case PayoneerOrderStatuses.PendingIncomplete:
                        case PayoneerOrderStatuses.Dispute:
                        case PayoneerOrderStatuses.Complete:
                        case PayoneerOrderStatuses.Arbitration:
                        case PayoneerOrderStatuses.MilestoneProgress:
                        case PayoneerOrderStatuses.MilestonePending:
                        case PayoneerOrderStatuses.ServiceProgress:
                        case PayoneerOrderStatuses.ServicePending:
                        case PayoneerOrderStatuses.Cancelled:
                        case PayoneerOrderStatuses.CancelledFinal:
                        case PayoneerOrderStatuses.Archive:
                        case null:
                        {
                            throw new InvalidOperationException("The Payoneer Order Status doesn't match with receiving a payment");
                        }
                        case PayoneerOrderStatuses.PendingError:
                        case PayoneerOrderStatuses.Error:
                        {
                            throw new WebException("There was an error in Payoneer");
                        }
                        default:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                    }
                    break;
                }
                case PayoneerOrderEventTypes.ShipmentAdded:
                case PayoneerOrderEventTypes.GoodsShipped:
                {
                    eventModel.Name = "Payoneer Escrow Order Goods were Shipped to Buyer";
                    order.ActualShipDate = orderReturn.StatusLastChange;
                    if (order.Shipment != null)
                    {
                        order.Shipment.ShipDate = orderEventReturn.Created;
                        // Default estimate is 5 days
                        order.Shipment.EstimatedDeliveryDate ??= order.Shipment.ShipDate.Value.AddDays(5);
                        // TODO: Add the shipment event
                    }
                    /*foreach (var salesItem in order.SalesItems)
                    {
#pragma warning disable 618
                        foreach (var shipment in salesItem.Shipments)
#pragma warning restore 618
                        {
                            shipment.Slave.ShipDate = orderEventReturn.Created;
                            if (shipment.Slave.EstimatedDeliveryDate == null)
                            {
                                // Default estimate is 5 days
                                shipment.Slave.EstimatedDeliveryDate = shipment.Slave.ShipDate.Value.AddDays(5);
                            }
                            // TODO: Add the shipment event
                        }
                    }*/
                    updateOrder = true;
                    switch (newPayoneerOrderStatus)
                    {
                        #region Shipped/Delivered
                        case PayoneerOrderStatuses.Shipped when new[] { "Pending", "Confirmed", "Backordered", "Partial Payment Received", "Full Payment Received", "Processing", null }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Pending", "Confirmed", "Backordered", "Partial Payment Received", "Full Payment Received", "Processing", null }.Contains(order.StatusKey):
                        {
                            eventModel.OldStatusID = order.StatusID;
                            order.StatusID = 0;
                            order.Status = null;
                            order.StatusName = null;
                            order.StatusKey = "Shipped";
                            // TODO: eventModel.NewStatusID = order.StatusKey lookup
                            break;
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Shipped From Vendor", "Shipped" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Shipped From Vendor", "Shipped" }.Contains(order.StatusKey):
                        {
                            // Do Nothing
                            break;
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Completed" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Completed" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has already completed.");
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Void" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Void" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has been voided.");
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Split" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Split" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException(
                                "Invalid, this order shouldn't be shipped as it has been split to separate orders and shouldn't be modified any further.");
                        }
                        case PayoneerOrderStatuses.Shipped:
                        case PayoneerOrderStatuses.Delivered:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                        #region Other
                        case PayoneerOrderStatuses.New:
                        case PayoneerOrderStatuses.Sent:
                        case PayoneerOrderStatuses.Paid:
                        case PayoneerOrderStatuses.Released:
                        case PayoneerOrderStatuses.PendingIncomplete:
                        case PayoneerOrderStatuses.Dispute:
                        case PayoneerOrderStatuses.Complete:
                        case PayoneerOrderStatuses.Cancelled:
                        case PayoneerOrderStatuses.Arbitration:
                        case PayoneerOrderStatuses.MilestoneProgress:
                        case PayoneerOrderStatuses.MilestonePending:
                        case PayoneerOrderStatuses.ServiceProgress:
                        case PayoneerOrderStatuses.ServicePending:
                        case PayoneerOrderStatuses.CancelledFinal:
                        case PayoneerOrderStatuses.Archive:
                        case null:
                        {
                            throw new InvalidOperationException("The Payoneer Order Status doesn't match up with a Goods Shipped event");
                        }
                        case PayoneerOrderStatuses.PendingError:
                        case PayoneerOrderStatuses.Error:
                        {
                            throw new WebException("There was an error in Payoneer");
                        }
                        default:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                    }
                    break;
                }
                case PayoneerOrderEventTypes.GoodsReceived:
                {
                    eventModel.Name = "Payoneer Escrow Order Goods were Delivered to Buyer";
                    if (order.Shipment != null)
                    {
                        order.Shipment.DateDelivered = orderEventReturn.Created;
                        // TODO: Add the shipment event
                    }
                    /*foreach (var salesItem in order.SalesItems)
                    {
#pragma warning disable 618
                        foreach (var shipment in salesItem.Shipments)
#pragma warning restore 618
                        {
                            shipment.Slave.DateDelivered = orderEventReturn.Created;
                            // TODO: Add the shipment event
                        }
                    }*/
                    updateOrder = true;
                    switch (newPayoneerOrderStatus)
                    {
                        #region Shipped/Delivered
                        case PayoneerOrderStatuses.Shipped when new[] { "Pending", "Confirmed", "Backordered", "Partial Payment Received", "Full Payment Received", "Processing", null }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Pending", "Confirmed", "Backordered", "Partial Payment Received", "Full Payment Received", "Processing", null }.Contains(order.StatusKey):
                        {
                            eventModel.OldStatusID = order.StatusID;
                            order.StatusID = 0;
                            order.Status = null;
                            order.StatusName = null;
                            order.StatusKey = "Shipped";
                            // TODO: eventModel.NewStatusID = order.StatusKey lookup
                            break;
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Shipped From Vendor", "Shipped" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Shipped From Vendor", "Shipped" }.Contains(order.StatusKey):
                        {
                            // Do Nothing
                            break;
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Completed" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Completed" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has already completed.");
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Void" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Void" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has been voided.");
                        }
                        case PayoneerOrderStatuses.Shipped when new[] { "Split" }.Contains(order.StatusKey):
                        case PayoneerOrderStatuses.Delivered when new[] { "Split" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException(
                                "Invalid, this order shouldn't be shipped as it has been split to separate orders and shouldn't be modified any further.");
                        }
                        case PayoneerOrderStatuses.Shipped:
                        case PayoneerOrderStatuses.Delivered:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                        #region Other
                        case PayoneerOrderStatuses.New:
                        case PayoneerOrderStatuses.Sent:
                        case PayoneerOrderStatuses.Paid:
                        case PayoneerOrderStatuses.Released:
                        case PayoneerOrderStatuses.PendingIncomplete:
                        case PayoneerOrderStatuses.Dispute:
                        case PayoneerOrderStatuses.Complete:
                        case PayoneerOrderStatuses.Cancelled:
                        case PayoneerOrderStatuses.Arbitration:
                        case PayoneerOrderStatuses.MilestoneProgress:
                        case PayoneerOrderStatuses.MilestonePending:
                        case PayoneerOrderStatuses.ServiceProgress:
                        case PayoneerOrderStatuses.ServicePending:
                        case PayoneerOrderStatuses.CancelledFinal:
                        case PayoneerOrderStatuses.Archive:
                        case null:
                        {
                            throw new InvalidOperationException("The Payoneer Order Status doesn't match up with a Goods Shipped event");
                        }
                        case PayoneerOrderStatuses.PendingError:
                        case PayoneerOrderStatuses.Error:
                        {
                            throw new WebException("There was an error in Payoneer");
                        }
                        default:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                    }
                    break;
                }
                case PayoneerOrderEventTypes.FundsReleased:
                {
                    eventModel.Name = "Payoneer Escrow Order Funds were Released by Buyer";
                    updateOrder = true;
                    switch (newPayoneerOrderStatus)
                    {
                        #region Shipped/Delivered
                        case PayoneerOrderStatuses.Released when new[] { "Pending", "Confirmed", "Backordered", "Partial Payment Received", "Full Payment Received", "Processing", "Shipped From Vendor", "Shipped", null }.Contains(order.StatusKey):
                        {
                            eventModel.OldStatusID = order.StatusID;
                            order.StatusID = 0;
                            order.Status = null;
                            order.StatusName = null;
                            order.StatusKey = "Completed";
                            // TODO: eventModel.NewStatusID = order.StatusKey lookup
                            break;
                        }
                        case PayoneerOrderStatuses.Released when new[] { "Completed" }.Contains(order.StatusKey):
                        {
                            // Do Nothing
                            break;
                        }
                        case PayoneerOrderStatuses.Released when new[] { "Void" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException("Invalid, this order has been voided.");
                        }
                        case PayoneerOrderStatuses.Released when new[] { "Split" }.Contains(order.StatusKey):
                        {
                            throw new InvalidOperationException(
                                "Invalid, this order shouldn't be funded as it has been split to separate orders and shouldn't be modified any further.");
                        }
                        case PayoneerOrderStatuses.Released:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                        #region Other
                        case PayoneerOrderStatuses.New:
                        case PayoneerOrderStatuses.Sent:
                        case PayoneerOrderStatuses.Paid:
                        case PayoneerOrderStatuses.Shipped:
                        case PayoneerOrderStatuses.Delivered:
                        case PayoneerOrderStatuses.PendingIncomplete:
                        case PayoneerOrderStatuses.Dispute:
                        case PayoneerOrderStatuses.Complete:
                        case PayoneerOrderStatuses.Cancelled:
                        case PayoneerOrderStatuses.Arbitration:
                        case PayoneerOrderStatuses.MilestoneProgress:
                        case PayoneerOrderStatuses.MilestonePending:
                        case PayoneerOrderStatuses.ServiceProgress:
                        case PayoneerOrderStatuses.ServicePending:
                        case PayoneerOrderStatuses.CancelledFinal:
                        case PayoneerOrderStatuses.Archive:
                        case null:
                        {
                            throw new InvalidOperationException("The Payoneer Order Status doesn't match up with a Goods Shipped event");
                        }
                        case PayoneerOrderStatuses.PendingError:
                        case PayoneerOrderStatuses.Error:
                        {
                            throw new WebException("There was an error in Payoneer");
                        }
                        default:
                        {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                            throw new ArgumentOutOfRangeException(nameof(newPayoneerOrderStatus));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                        }
                        #endregion
                    }
                    break;
                }
                case PayoneerOrderEventTypes.Cancelled:
                {
                    eventModel.Name = "Payoneer Escrow Order was Cancelled";
                    updateOrder = true;
                    if (order.StatusKey != "Void")
                    {
                        eventModel.OldStatusID = order.StatusID;
                        order.StatusID = 0;
                        order.Status = null;
                        order.StatusName = null;
                        order.StatusKey = "Void";
                        // TODO: eventModel.NewStatusID = order.StatusKey lookup
                    }
                    break;
                }
                // ReSharper disable RedundantCaseLabel
                /*case PayoneerOrderEventTypes.OrderCreate:
                case PayoneerOrderEventTypes.Sent:
                case PayoneerOrderEventTypes.Dispute:
                case PayoneerOrderEventTypes.Arbitration:
                case PayoneerOrderEventTypes.UploadDocument:
                case PayoneerOrderEventTypes.DisputeOffer:
                case PayoneerOrderEventTypes.OfferAccepted:
                case PayoneerOrderEventTypes.OfferCountered:
                case PayoneerOrderEventTypes.NoteAdded:
                case PayoneerOrderEventTypes.Insured:
                case PayoneerOrderEventTypes.MilestoneReached:
                case PayoneerOrderEventTypes.MilestoneReleased:
                case PayoneerOrderEventTypes.ServiceComplete:
                case PayoneerOrderEventTypes.BalanceXfrOut:
                case PayoneerOrderEventTypes.BalanceXfrIn:
                case PayoneerOrderEventTypes.DisbursementAdded:
                case PayoneerOrderEventTypes.Inspected:
                case PayoneerOrderEventTypes.ServiceCompletePartial:
                case PayoneerOrderEventTypes.PayerAdded:
                case PayoneerOrderEventTypes.PayerAccepted:
                case PayoneerOrderEventTypes.PayerRejected:
                case PayoneerOrderEventTypes.DomainTransferred:
                case PayoneerOrderEventTypes.PaymentFailed:
                case PayoneerOrderEventTypes.OrderUnderReview:
                case PayoneerOrderEventTypes.OrderPassedReview:
                case PayoneerOrderEventTypes.OrderFailedReview:
                case PayoneerOrderEventTypes.AgreementSent:
                case PayoneerOrderEventTypes.AgreementSigned:
                case PayoneerOrderEventTypes.AgreementConfirmed:
                case PayoneerOrderEventTypes.DomainDetailsGiven:
                case PayoneerOrderEventTypes.DomainXfrIn:
                case PayoneerOrderEventTypes.DomainXfrOut:
                case PayoneerOrderEventTypes.PaymentSeriesStart:
                case PayoneerOrderEventTypes.PaymentSeriesEnd:
                case PayoneerOrderEventTypes.PaymentPastDue:
                case PayoneerOrderEventTypes.CancelledInstallmentNonpayment:
                case PayoneerOrderEventTypes.MilestoneFunded:*/
                // ReSharper restore RedundantCaseLabel
                default:
                {
                    eventModel.Name = ((PayoneerOrderEventTypes)orderEventReturn.Type).ToString();
                    break;
                }
            }
            // ReSharper restore MultipleSpaces
            eventModel.NewRecordSerialized = JsonConvert.SerializeObject(order);
            await workflows.SalesOrderEvents.CreateAsync(eventModel, contextProfileName).ConfigureAwait(false);
            if (updateOrder)
            {
                await workflows.SalesOrders.UpdateAsync(order, contextProfileName).ConfigureAwait(false);
            }
        }
        #endregion

        #region 1. Create the user accounts (Action)
        /// <summary>Creates a payoneer account.</summary>
        /// <param name="store">The store.</param>
        /// <param name="user"> The user.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool CreateAPayoneerAccount(IStoreModel? store, IUserModel user)
        {
            long accountID;
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Accounts.html for the specific endpoint
            using (var client = GetWebClient())
            {
                MakeSignature(client, "POST", "/accounts");
                var dto = new CreateAccountDto
                {
                    AccountType = 1,
                    AgreedTerms = true,
                    EmailConfirmed = user.EmailConfirmed,
                    Company = store != null ? store.Name : user.Contact!.FullName,
                    Username = user.UserName,
                    UserEmail = user.Email,
                    UserPhone = user.PhoneNumber,
                    Address = store != null ? store.Contact!.Address!.Street1 : user.Contact!.Address!.Street1,
                    City = store != null ? store.Contact!.Address!.City : user.Contact!.Address!.City,
                    State = store != null ? store.Contact!.Address!.RegionName : user.Contact!.Address!.RegionName,
                    Zip = store != null ? store.Contact!.Address!.PostalCode : user.Contact!.Address!.PostalCode,
                    Country = store != null ? store.Contact!.Address!.CountryName : user.Contact!.Address!.CountryName,
                };
                string resultRaw;
                try
                {
                    resultRaw = client.UploadString("/accounts", JsonConvert.SerializeObject(dto));
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                    return false;
                }
                var result = JsonConvert.DeserializeObject<CreateAccountResult>(resultRaw);
                UpsertPayoneerAttribute("Payoneer-Account-ID", (store != null ? (IHaveJsonAttributesBaseModel)store : user).SerializableAttributes!, result!.AccountID);
                UpsertPayoneerAttribute("Payoneer-Account-URL", (store != null ? (IHaveJsonAttributesBaseModel)store : user).SerializableAttributes!, result.Uri);
                accountID = result.AccountID;
            }
            using (var client = GetWebClient())
            {
                var endpoint = $"/accounts/{accountID}/users";
                MakeSignature(client, "GET", endpoint);
                try
                {
                    var resultRaw = client.DownloadString(endpoint);
                    var result = JsonConvert.DeserializeObject<List<UserResult>>(resultRaw)!;
                    var userResult = result.First(x => x.Email == user.Email);
                    if (userResult == null)
                    {
                        return false;
                    }
                    UpsertPayoneerAttribute("Payoneer-User-ID", (store != null ? (IHaveJsonAttributesBaseModel)store : user).SerializableAttributes!, userResult.UserID);
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                    return false;
                }
            }
            return true;
        }

        /* Include this to notify to the end user the T&C for Payoneer Escrow for account creation
        <label>
            <input type="checkbox" id="armor_payments_terms" name="armor_payments_terms" />
            <!-- Replace 1052419 with your own Partner ID in this URL -->
            <span>I agree to the <a href="https://pay.payoneer.com/terms/1052419" target="_blank">Terms and Conditions</a> of Payoneer Escrow.</span>
        </label>
        */

        /// <summary>Gets the accounts users.</summary>
        /// <param name="store">The store.</param>
        /// <param name="user"> The user.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool AssignAccountUser(IStoreModel? store, IUserModel user)
        {
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Users.html for the specific endpoint
            using var client = GetWebClient();
            var endpoint = $"/accounts/{(store != null ? store.SerializableAttributes! : user.SerializableAttributes!)["Payoneer-Account-ID"].Value}/users";
            MakeSignature(client, "GET", endpoint);
            string resultRaw;
            try
            {
                resultRaw = client.DownloadString(endpoint);
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
                return false;
            }
            var results = JsonConvert.DeserializeObject<UserResult[]>(resultRaw)!;
            var email = user.Email;
            var specificUserResult = results.SingleOrDefault(x => x.Email == email);
            if (specificUserResult == null)
            {
                return false;
            } // TODO: Log an error?
            UpsertPayoneerAttribute("Payoneer-Order-Status-ID", user.SerializableAttributes!, specificUserResult.UserID);
            return true;
        }
        #endregion

        #region 2. Ask payee(s) to set up Payoneer Escrow and their payout preference (Action)
        /// <summary>Creates an authenticated URL for store owners.</summary>
        /// <param name="store">The store.</param>
        /// <param name="user"> The user.</param>
        /// <returns>an authenticated URL for store owners to set up payout accounts for current store.</returns>
        public string? GetAnAuthenticatedURLForStoreOwnersToSetUpPayoutAccountsForCurrentStore(IStoreModel? store, IUserModel user)
        {
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Authentications.html for the specific endpoint
            using var client = GetWebClient();
            var endpoint = $"/accounts/{(store != null ? store.SerializableAttributes : user.SerializableAttributes)!["Payoneer-Account-ID"].Value}"
                + $"/users/{user.SerializableAttributes!["Payoneer-User-ID"].Value}"
                + $"/authentications";
            MakeSignature(client, "POST", endpoint);
            var dto = new CreateAuthenticatedURLDto
            {
                Action = "create",
                Uri = $"/accounts/{(store != null ? store.SerializableAttributes : user.SerializableAttributes)!["Payoneer-Account-ID"].Value}/bankaccounts",
            };
            string resultRaw;
            try
            {
                var serialized = JsonConvert.SerializeObject(dto);
                resultRaw = client.UploadString(endpoint, serialized);
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
                return null;
            }
            var result = JsonConvert.DeserializeObject<CreateAuthenticatedURLReturn>(resultRaw);
            UpsertPayoneerAttribute(
                "Payoneer-Authenticated-URL-BankAccounts-Data",
                user.SerializableAttributes,
                JsonConvert.SerializeObject(result));
            return result!.Url;
        }

        /* Include this for store owners to set themselves up to receive payments after their account is created
        <!-- The Payoneer Escrow lightbox JS requires jQuery. If your site already includes jQuery, omit this line -->
        <script src="https://code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
        <!-- Sandbox environment? Use this script: -->
        <script src="https://sandbox.armorpayments.com/assets/js/modal.min.js" type="text/javascript"></script>
        <!-- Production environment? Use this script: -->
        <!-- <script src="https://{{ applicationUrl }}/assets/js/modal.min.js" type="text/javascript"></script> -->
        <script type="text/javascript">
            // Use the URL returned from the /authentications request
            var url = "https://{{ applicationUrl }}/accounts/290465/bankaccounts?action=create&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e7";
            armor.openModal(url);
        </script>
        */
        #endregion

        #region 3. Create the Order (Action)
        /// <summary>Validates the ready.</summary>
        /// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
        /// <param name="store">             The store.</param>
        /// <param name="seller">            The seller.</param>
        /// <param name="buyer">             The buyer.</param>
        /// <param name="payoneerAccountID"> Identifier for the payoneer account.</param>
        /// <param name="payoneerCustomerID">Identifier for the payoneer customer.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
#pragma warning disable SA1204 // Static elements should appear before instance elements
        public static bool ValidateReady(
#pragma warning restore SA1204 // Static elements should appear before instance elements
            IStoreModel? store,
            IUserModel seller,
            IUserModel buyer,
            long? payoneerAccountID,
            long? payoneerCustomerID)
        {
            Contract.RequiresNotNull(store?.SerializableAttributes);
            Contract.RequiresNotNull(seller?.SerializableAttributes);
            Contract.RequiresNotNull(buyer?.SerializableAttributes);
            // If they sent updated values, push them in
            if (UpsertPayoneerAttribute("Payoneer-Account-ID", buyer!.SerializableAttributes!, payoneerAccountID))
            {
                UpsertPayoneerAttribute("Payoneer-Account-URI", buyer.SerializableAttributes!, $"/accounts/{payoneerAccountID}");
                if (UpsertPayoneerAttribute("Payoneer-User-ID", buyer.SerializableAttributes!, payoneerCustomerID))
                {
                    UpsertPayoneerAttribute("Payoneer-User-URI", buyer.SerializableAttributes!, $"/accounts/{payoneerAccountID}/users/{payoneerCustomerID}");
                }
            }
            if (!store!.SerializableAttributes!.TryGetValue("Payoneer-Account-ID", out var accountID))
            {
                seller!.SerializableAttributes!.TryGetValue("Payoneer-Account-ID", out accountID);
            }
            if (accountID == null)
            {
                throw new InvalidOperationException("The Store Owner must set up Payoneer Account Information before items may be purchased with Payoneer");
            }
            return true;
        }

        /// <summary>Creates an escrow order to facilitate a transaction for goods.</summary>
        /// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
        /// <param name="store">             The store.</param>
        /// <param name="seller">            The seller.</param>
        /// <param name="buyer">             The buyer.</param>
        /// <param name="order">             The order.</param>
        /// <param name="cart">              The cart.</param>
        /// <param name="amount">            The amount.</param>
        /// <param name="payoneerAccountID"> Identifier for the payoneer account.</param>
        /// <param name="payoneerCustomerID">Identifier for the payoneer customer.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public CEFActionResponse CreateAnEscrowOrderToFacilitateATransactionForGoods(
            IStoreModel? store,
            IUserModel seller,
            IUserModel buyer,
            IHaveJsonAttributesBase order,
            IHaveJsonAttributesBaseModel cart,
            decimal amount,
            long? payoneerAccountID,
            long? payoneerCustomerID)
        {
            if (!ValidateReady(store, seller, buyer, payoneerAccountID, payoneerCustomerID))
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot validate that the order is ready for Payoneer");
            }
            if (!store!.SerializableAttributes!.TryGetValue("Payoneer-Account-ID", out var accountID))
            {
                seller.SerializableAttributes!.TryGetValue("Payoneer-Account-ID", out accountID);
            }
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Orders.html for the specific endpoint
            using var client = GetWebClient();
            var endpoint = $"/accounts/{accountID}/orders";
            MakeSignature(client, "POST", endpoint);
            var sellerUserID = long.Parse(seller.SerializableAttributes!["Payoneer-User-ID"].Value);
            var buyerUserID = long.Parse(buyer.SerializableAttributes!["Payoneer-User-ID"].Value);
            var dto = new CreateEscrowOrderDto
            {
                Type = 1,
                SellerUserID = sellerUserID,
                BuyerUserID = buyerUserID,
                Amount = amount,
                CreditCardTermsAccepted = true,
                ////PurchaseOrderNumber = order.PurchaseOrderNumber,
                ////InvoiceNumber = order.AssociatedSalesInvoices?.Select(x => x.SalesInvoiceID.ToString()).DefaultIfEmpty("").Aggregate((c, n) => c + "," + n),
                Summary = $"A purchase through {CEFConfigDictionary.CompanyName}",
            };
            string resultRaw;
            try
            {
                resultRaw = client.UploadString(endpoint, JsonConvert.SerializeObject(dto));
            }
            catch (WebException webex)
            {
                HandleWebException(webex);
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ////HandleWebException(ex);
                throw;
            }
            Contract.RequiresValidKey(resultRaw);
            var result = JsonConvert.DeserializeObject<CreateEscrowOrderReturn>(resultRaw);
            cart.SerializableAttributes ??= new();
            var orderAttributes = order.SerializableAttributes!; // Read Only, have to make a separate object to populate
            UpsertPayoneerAttribute("Payoneer-Order-ID", orderAttributes, result!.OrderID);
            UpsertPayoneerAttribute("Payoneer-Order-ID", cart.SerializableAttributes, result.OrderID);
            UpsertPayoneerAttribute("Payoneer-Order-Type-ID", orderAttributes, result.Type);
            UpsertPayoneerAttribute("Payoneer-Order-Type-ID", cart.SerializableAttributes, result.Type);
            UpsertPayoneerAttribute("Payoneer-Order-Status-ID", orderAttributes, result.Status);
            UpsertPayoneerAttribute("Payoneer-Order-Status-ID", cart.SerializableAttributes, result.Status);
            UpsertPayoneerAttribute("Payoneer-Order-Amount", orderAttributes, result.Amount);
            UpsertPayoneerAttribute("Payoneer-Order-Amount", cart.SerializableAttributes, result.Amount);
            UpsertPayoneerAttribute("Payoneer-Order-Balance", orderAttributes, result.Balance);
            UpsertPayoneerAttribute("Payoneer-Order-Balance", cart.SerializableAttributes, result.Balance);
            UpsertPayoneerAttribute("Payoneer-Order-Available-Balance", orderAttributes, result.AvailableBalance);
            UpsertPayoneerAttribute("Payoneer-Order-Available-Balance", cart.SerializableAttributes, result.AvailableBalance);
            UpsertPayoneerAttribute("Payoneer-Order-Status-Last-Changed", orderAttributes, result.StatusLastChanged);
            UpsertPayoneerAttribute("Payoneer-Order-Status-Last-Changed", cart.SerializableAttributes, result.StatusLastChanged);
            UpsertPayoneerAttribute("Payoneer-Order-URI", orderAttributes, result.Uri);
            UpsertPayoneerAttribute("Payoneer-Order-URI", cart.SerializableAttributes, result.Uri);
            order.JsonAttributes = orderAttributes.SerializeAttributesDictionary();
            return CEFAR.PassingCEFAR();
        }
        #endregion

        #region 4. Present payment instructions (Action)
        private static bool UpsertPayoneerAttribute<T>(string key, SerializableAttributesDictionary attributes, T value)
        {
            if (value == null)
            {
                return false;
            }
            string innerValue;
            // ReSharper disable BadControlBracesLineBreaks, MultipleStatementsOnOneLine, StyleCop.SA1107
            switch (value)
            {
                case int asInt:
                {
                    innerValue = asInt.ToString();
                    break;
                }
                case long asLong:
                {
                    innerValue = asLong.ToString();
                    break;
                }
                case decimal asDecimal:
                {
                    innerValue = asDecimal.ToString(CultureInfo.InvariantCulture);
                    break;
                }
                case double asDouble:
                {
                    innerValue = asDouble.ToString(CultureInfo.InvariantCulture);
                    break;
                }
                case float asFloat:
                {
                    innerValue = asFloat.ToString(CultureInfo.InvariantCulture);
                    break;
                }
                case string asString:
                {
                    if (!Contract.CheckValidKey(asString))
                    {
                        return false;
                    }
                    innerValue = asString;
                    break;
                }
                case DateTime asDateTime:
                {
                    if (!Contract.CheckValidDate(asDateTime))
                    {
                        return false;
                    }
                    innerValue = asDateTime.ToString("O");
                    break;
                }
                default:
                {
                    innerValue = JsonConvert.SerializeObject(value);
                    break;
                }
            }
            // ReSharper restore BadControlBracesLineBreaks, MultipleStatementsOnOneLine, StyleCop.SA1107
            if (attributes.ContainsKey(key))
            {
                attributes[key].Value = innerValue;
                return true;
            }
            attributes[key] = new()
            {
                Key = key,
                Value = innerValue,
            };
            return true;
        }

        /// <summary>Gets an authenticated URL for users to be presented with payment instructions.</summary>
        /// <param name="buyer">             The buyer.</param>
        /// <param name="salesCollection">   The sales collection (cart, order, etc).</param>
        /// <param name="payoneerAccountID"> Identifier for the payoneer account.</param>
        /// <param name="payoneerCustomerID">Identifier for the payoneer customer.</param>
        /// <returns>An authenticated URL for users to be presented with payment instructions.</returns>
#pragma warning disable SA1202 // Elements should be ordered by access
        public string? GetAnAuthenticatedURLForUsersToBePresentedWithPaymentInstructions(
#pragma warning restore SA1202 // Elements should be ordered by access
            IUserModel buyer,
            ISalesCollectionBaseModel salesCollection,
            long? payoneerAccountID,
            long? payoneerCustomerID)
        {
            // If they sent updated values, push them in
            if (UpsertPayoneerAttribute("Payoneer-Account-ID", buyer.SerializableAttributes!, payoneerAccountID))
            {
                UpsertPayoneerAttribute("Payoneer-Account-URI", buyer.SerializableAttributes!, $"/accounts/{payoneerAccountID}");
                if (UpsertPayoneerAttribute("Payoneer-User-ID", buyer.SerializableAttributes!, payoneerCustomerID))
                {
                    UpsertPayoneerAttribute("Payoneer-User-URI", buyer.SerializableAttributes!, $"/accounts/{payoneerAccountID}/users/{payoneerCustomerID}");
                }
            }
            Contract.Requires<InvalidOperationException>(
                buyer.SerializableAttributes!.ContainsKey("Payoneer-Account-URI"),
                "Payoneer Account data has not been set up properly for this buyer user");
            Contract.Requires<InvalidOperationException>(
                buyer.SerializableAttributes.ContainsKey("Payoneer-User-URI"),
                "Payoneer Customer data has not been set up properly for this buyer user");
            Contract.Requires<InvalidOperationException>(
                salesCollection.SerializableAttributes!.ContainsKey("Payoneer-Order-URI"),
                "Payoneer Order data has not been set up properly for this order");
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Orders.html for the specific endpoint
            using var client = GetWebClient();
            // ReSharper disable PossibleInvalidOperationException
            var endpoint = $"/accounts/{buyer.SerializableAttributes["Payoneer-Account-ID"].Value}"
                         + $"/users/{buyer.SerializableAttributes["Payoneer-User-ID"].Value}"
                         + $"/authentications";
            // ReSharper restore PossibleInvalidOperationException
            MakeSignature(client, "POST", endpoint);
            var dto = new CreateAuthenticatedURLDto
            {
                Action = "view",
                Uri = $"{salesCollection.SerializableAttributes["Payoneer-Order-URI"].Value}/paymentinstructions",
            };
            string resultRaw;
            try
            {
                resultRaw = client.UploadString(endpoint, JsonConvert.SerializeObject(dto));
            }
            catch (WebException webex)
            {
                HandleWebException(webex);
                return null;
            }
            catch (Exception ex)
            {
                ////HandleWebException(ex);
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
            var result = JsonConvert.DeserializeObject<CreateAuthenticatedURLReturn>(resultRaw);
            UpsertPayoneerAttribute("Payoneer-Authenticated-URL-PaymentIntructions-Data", salesCollection.SerializableAttributes, JsonConvert.SerializeObject(result));
            return result!.Url;
        }
        #endregion

        #region 6. Add shipping details (Action)
        /// <summary>Adds a shipping details to 'user'.</summary>
        /// <param name="store">The store.</param>
        /// <param name="user"> The user.</param>
        /// <param name="order">The order.</param>
        /// <returns>an authenticated URL for store owners to add a tracking number to the escrow order.</returns>
        public string? GetAnAuthenticatedURLForStoreOwnersToAddATrackingNumberToTheEscrowOrder(
            IStoreModel store,
            IUserModel user,
            ISalesOrderModel order)
        {
            Contract.Requires<InvalidOperationException>(
                store.SerializableAttributes!.ContainsKey("Payoneer-Account-URI"),
                "Payoneer Account data has not been set up properly for this store");
            Contract.Requires<InvalidOperationException>(
                user.SerializableAttributes!.ContainsKey("Payoneer-User-URI"),
                "Payoneer Customer data has not been set up properly for this seller user");
            Contract.Requires<InvalidOperationException>(
                order.SerializableAttributes!.ContainsKey("Payoneer-Order-URI"),
                "Payoneer Order data has not been set up properly for this order");
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Orders.html for the specific endpoint
            using var client = GetWebClient();
            var endpoint = $"/accounts/{store.SerializableAttributes["Payoneer-Account-ID"].Value}"
                         + $"/users/{user.SerializableAttributes["Payoneer-User-ID"].Value}"
                         + $"/authentications";
            MakeSignature(client, "POST", endpoint);
            var dto = new CreateAuthenticatedURLDto
            {
                Action = "view",
                Uri = $"{order.SerializableAttributes["Payoneer-Order-URI"].Value}/shipments",
            };
            string resultRaw;
            try
            {
                resultRaw = client.UploadString(endpoint, JsonConvert.SerializeObject(dto));
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
                return null;
            }
            catch (Exception ex)
            {
                ////HandleWebException(ex);
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
            var result = JsonConvert.DeserializeObject<CreateAuthenticatedURLReturn>(resultRaw);
            UpsertPayoneerAttribute("Payoneer-Authenticated-URL-Shipments-Data", order.SerializableAttributes, JsonConvert.SerializeObject(result));
            return result!.Url;
        }
        /* Include this as the form to insert the tracking number to the Payoneer escrow order
        <!-- The Payoneer Escrow lightbox JS requires jQuery. If your site already includes jQuery, omit this line -->
        <script src="https://code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
        <!-- Sandbox environment? Use this script: -->
        <script src="https://sandbox.armorpayments.com/assets/js/modal.min.js" type="text/javascript"></script>
        <!-- Production environment? Use this script: -->
        <!-- <script src="https://{{ applicationUrl }}/assets/js/modal.min.js" type="text/javascript"></script> -->
        <script type="text/javascript">
            // Use the URL returned from the /authentications request
            var url = "https://{{ applicationUrl }}/accounts/290465/orders/1401028714/shipments?action=view&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e7";
            armor.openModal(url);
        </script>
        */
        #endregion

        #region 8. Ask Buyer to release funds (Action)
        /// <summary>Ask buyer to release funds.</summary>
        /// <param name="user"> The user.</param>
        /// <param name="order">The order.</param>
        /// <returns>an authenticated release funds URL for an escrow order.</returns>
        public string? GetAnAuthenticatedReleaseFundsURLForAnEscrowOrder(IUserModel user, ISalesOrderModel order)
        {
            Contract.Requires<InvalidOperationException>(
                user.SerializableAttributes!.ContainsKey("Payoneer-User-URI"),
                "Payoneer Customer data has not been set up properly for this seller user");
            Contract.Requires<InvalidOperationException>(
                order.SerializableAttributes!.ContainsKey("Payoneer-Order-URI"),
                "Payoneer Order data has not been set up properly for this order");
            // See https://escrow.payoneer.com/api/pages/integrationGuide/goods.html for primary process reference
            // See https://escrow.payoneer.com/api/classes/ArmorPayments.Api.Resource.Orders.html for the specific endpoint
            using var client = GetWebClient();
            var endpoint = $"/accounts/{user.SerializableAttributes["Payoneer-Account-ID"].Value}"
                         + $"/users/{user.SerializableAttributes["Payoneer-User-ID"].Value}"
                         + $"/authentications";
            MakeSignature(client, "POST", endpoint);
            var dto = new CreateAuthenticatedURLDto
            {
                Action = "release",
                Uri = order.SerializableAttributes["Payoneer-Order-URI"].Value,
            };
            string resultRaw;
            try
            {
                resultRaw = client.UploadString(endpoint, JsonConvert.SerializeObject(dto));
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
                return null;
            }
            var result = JsonConvert.DeserializeObject<CreateAuthenticatedURLReturn>(resultRaw);
            UpsertPayoneerAttribute("Payoneer-Authenticated-URL-Release-Funds-Data", order.SerializableAttributes, JsonConvert.SerializeObject(result));
            return result!.Url;
        }
        /* Include this as the form to ask the buyer to release funds
        <!-- The Payoneer Escrow lightbox JS requires jQuery. If your site already includes jQuery, omit this line -->
        <script src="https://code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
        <!-- Sandbox environment? Use this script: -->
        <script src="https://sandbox.armorpayments.com/assets/js/modal.min.js" type="text/javascript"></script>
        <!-- Production environment? Use this script: -->
        <!-- <script src="https://{{ applicationUrl }}/assets/js/modal.min.js" type="text/javascript"></script> -->
        <script type="text/javascript">
            // Use the URL returned from the /authentications request
            var url = "https://{{ applicationUrl }}/accounts/290465/orders/1401028714?action=release&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e7";
            armor.openModal(url);
        </script>
        */
        #endregion

        #region Status Timeouts
        /*
        Status Timeouts

        Certain order status values are time limited. If the order remains in the same status
        beyond the time limit, the order will automatically transition into another status.

        Status      Timeout                 New Status      Notes
        Sent        20 business days        Cancelled
        Shipped     15–35 business days     Released        Shipment timeout varies by carrier
        Delivered   3 business days         Released
        */
        #endregion
    }
}
