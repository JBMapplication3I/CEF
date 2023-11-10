// <copyright file="SalesOrdersWarehouseShippingInformationToBackOfficeEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales orders warehouse shipping information to back office email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The sales orders warehouse shipping information to back office email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesOrdersWarehouseShippingInformationToBackOfficeEmail : EmailSettingsBase
    {
        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
         DefaultValue(true)]
        public override bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Warehouse Shipping Information for Sales Order at {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : "Warehouse Shipping Information for Sales Order at {{CompanyName}}";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesOrders.BackOffice.WarehouseShipping.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : "SalesOrders.BackOffice.WarehouseShipping.html";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
         DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : "noreply@claritymis.com";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".To"),
         DefaultValue("clarity-local@claritymis.com")]
        public override string? To
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : "clarity-local@claritymis.com";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.SalesOrders.BackOffice.WarehouseShipping";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("cart") == true && parameters["cart"] != null,
                "This email requires a parameter in the dictionary of { [\"cart\"] = <ICartModel> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("orderID") && parameters["orderID"] != null,
                "This email requires a parameter in the dictionary of { [\"orderID\"] = <int> }");
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("regionCode") && parameters["regionCode"] != null,
                "This email requires a parameter in the dictionary of { [\"regionCode\"] = <string> }");
            var cart = parameters["cart"] as ICartModel;
            var orderID = parameters["orderID"] as int?;
            var regionCode = parameters["regionCode"] as string;
            if (!CEFConfigDictionary.GetClosestWarehouseWithStock)
            {
                return CEFAR.PassingCEFAR(0); // Ignored
            }
            try
            {
                var pils = new List<IProductInventoryLocationSectionModel>();
                foreach (var item in cart!.SalesItems!)
                {
                    var closestSection = Workflows.ProductInventoryLocationSections.GetClosestWarehouseByRegionCode(
                        regionCode!,
                        item.ProductID ?? 0,
                        contextProfileName);
                    if (closestSection != null)
                    {
                        pils.Add(closestSection);
                    }
                }
                return await QueueAsync(pils!, cart!, orderID!.Value, contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex2)
            {
                await Logger.LogErrorAsync(
                        "Send Checkout Warehouse and Shipping Email",
                        ex2.Message,
                        ex2,
                        contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<int>("An unknown error has occurred with queueing th email to send");
            }
        }

        private async Task<CEFActionResponse<int>> QueueAsync(
            List<IProductInventoryLocationSectionModel> pils,
            ICartModel cart,
            int orderID,
            string? contextProfileName)
        {
            var items = new List<IProviderShipment>();
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider != null)
            {
                var itemsResult = await packagingProvider.GetItemPackagesAsync(cart.ID, contextProfileName).ConfigureAwait(false);
                if (itemsResult.ActionSucceeded)
                {
                    items = itemsResult.Result;
                }
            }
            var replacementDictionary = await FedexWarehouseReplacementsAsync(
                    cart: cart,
                    pils: pils,
                    items: items!,
                    orderID: orderID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    email: string.Empty,
                    replacementDictionary: replacementDictionary,
                    emailSettings: this,
                    attachmentPath: null,
                    attachmentType: Enums.FileEntityType.StoredFileSalesOrder,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
        }

        /// <summary>Standard payment replacements.</summary>
        /// <param name="cart">              The payment.</param>
        /// <param name="pils">              The pils.</param>
        /// <param name="items">             The items.</param>
        /// <param name="orderID">           Identifier for the order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        private async Task<Dictionary<string, string?>> FedexWarehouseReplacementsAsync(
            ICartModel cart,
            List<IProductInventoryLocationSectionModel> pils,
            List<IProviderShipment> items,
            int orderID,
            string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                foreach (var item in pils)
                {
                    item.MasterKey = context.Products.FilterByID(item.MasterID).Select(x => x.CustomKey).FirstOrDefault();
                }
            }
            var pilsItemsHTML = pils
                    .Aggregate(
                        "<table style=\"width:100%;\"><thead><tr>" +
                        "<th style=\"padding: 6px;\">Item Code</th>" +
                        "<th style=\"padding: 6px;\">Warehouse</th>" +
                        "<th style=\"padding: 6px;\">Bin Location</th>" +
                        "<th style=\"padding: 6px;\">Quantity</th>" +
                        "</tr></thead><tbody>",
                        (current, item) => current
                            + "<tr>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{item.MasterKey}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{item.Slave?.InventoryLocationName}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{item.SlaveName}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{item.Quantity:#,##0.####}</td>"
                            + "</tr>")
                + "</tbody></ table > ";
            var lineItemsHTML = items
                    .Aggregate(
                        "<table style=\"width:100%;\"><thead><tr>" +
                        "<th style=\"padding: 6px;\">Item Code</th>" +
                        "<th style=\"padding: 6px;\">Item Name</th>" +
                        "<th style=\"padding: 6px;\">Package Quantity</th>" +
                        "<th style=\"padding: 6px;\">Depth</th>" +
                        "<th style=\"padding: 6px;\">Height</th>" +
                        "<th style=\"padding: 6px;\">Weight</th>" +
                        "<th style=\"padding: 6px;\">Dimensional Weight</th>" +
                        "</tr></thead><tbody>",
                        (current, shipment) => current
                            + "<tr>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.ItemCode}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.ItemName}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.PackageQuantity:#,##0.####}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.Depth} {shipment.DepthUnitOfMeasure}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.Height} {shipment.HeightUnitOfMeasure}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.Weight} {shipment.WeightUnitOfMeasure}</td>"
                            + $"<td style=\"padding: 6px;text-align: left;\">{shipment.DimensionalWeight} {shipment.DimensionalWeightUnitOfMeasure}</td>"
                            + "</tr>")
                + "</tbody></ table > ";
            var sameAsBilling = cart.ShippingSameAsBilling ?? false;
            var destination = await Workflows.Contacts.GetAsync(
                    sameAsBilling ? cart.BillingContactID!.Value : cart.ShippingContactID!.Value,
                    contextProfileName)
                .ConfigureAwait(false);
            var salesItemIDs = cart.SalesItems!.Select(x => x.ID).ToList();
            var shippingProviders = RegistryLoaderWrapper.GetShippingProviders(contextProfileName);
            var fedEx = shippingProviders.Single(x => x.Name == "FedExShippingProvider");
            var baseChargeRates = await fedEx.GetBaseOrNetChargesAsync(
                    cart.ID,
                    salesItemIDs,
                    cart.BillingContact!,
                    destination!,
                    expedited: false,
                    useBaseCharge: true,
                    contextProfileName)
                .ConfigureAwait(false);
            var netChargeRates = await fedEx.GetBaseOrNetChargesAsync(
                    cart.ID,
                    salesItemIDs,
                    cart.BillingContact!,
                    destination!,
                    expedited: false,
                    useBaseCharge: false,
                    contextProfileName)
                .ConfigureAwait(false);
            var rateHtml = string.Join(
                string.Empty,
                "<table style=\"width:100%;\"><thead><tr>",
                "<th style=\"padding: 6px;\">Base Charge Shipping Rate</th>",
                "<th style=\"padding: 6px;\">Net Charge Shipping Rate</th>",
                "</tr></thead><tbody>");
            var rates = baseChargeRates.Zip(
                netChargeRates,
                (baseCharge, netCharge) => string.Join(
                    string.Empty,
                    "<tr>",
                    $"<td style=\"padding: 6px;text-align: left;\">{baseCharge.OptionName}</td>",
                    $"<td style=\"padding: 6px;text-align: left;\">{netCharge.OptionName}</td>",
                    "</tr>"));
            var joinedRates = string.Join(string.Empty, rates);
            rateHtml = string.Join(string.Empty, rateHtml, joinedRates);
            rateHtml = string.Join(string.Empty, rateHtml, "</tbody></table>");
            var subTotal = cart.SalesItems!.Sum(x => x.ExtendedPrice);
            var taxesProvider = await RegistryLoaderWrapper.GetTaxProviderAsync(contextProfileName).ConfigureAwait(false);
            var selectedRate = cart.RateQuotes!.FirstOrDefault(x => x.Selected);
            var shipping = selectedRate?.Rate ?? 0;
            var tax = 0.00m;
            if (CEFConfigDictionary.TaxesEnabled && taxesProvider != null)
            {
                tax = (await taxesProvider.CalculateCartAsync(cart, cart.UserID, contextProfileName).ConfigureAwait(false)).TotalTaxes;
            }
            var total = subTotal + tax + shipping;
            return new()
            {
                { "{{RootUrl}}", CEFConfigDictionary.SiteRouteHostUrlSSL ?? CEFConfigDictionary.SiteRouteHostUrl },
                { "{{ID}}", cart.ID.ToString() },
                { "{{BillingFirstName}}", cart.BillingContact?.FirstName },
                { "{{BillingLastName}}", cart.BillingContact?.LastName },
                { "{{BillingCompany}}", cart.BillingContact?.Address?.Company },
                { "{{BillingStreetAddress}}", $"{cart.BillingContact?.Address?.Street1} {cart.BillingContact?.Address?.Street2} {cart.BillingContact?.Address?.Street3}" },
                { "{{BillingCity}}", cart.BillingContact?.Address?.City },
                { "{{BillingState}}", cart.BillingContact?.Address?.RegionCode },
                { "{{BillingZipCode}}", cart.BillingContact?.Address?.PostalCode },
                { "{{ShippingFirstName}}", cart.ShippingSameAsBilling == true ? "Shipping Same As Billing" : cart.ShippingContact?.FirstName },
                { "{{ShippingLastName}}", cart.ShippingContact?.LastName },
                { "{{ShippingCompany}}", cart.ShippingContact?.Address?.Company },
                { "{{ShippingStreetAddress}}", $"{cart.ShippingContact?.Address?.Street1} {cart.ShippingContact?.Address?.Street2} {cart.ShippingContact?.Address?.Street3}" },
                { "{{ShippingCity}}", cart.ShippingContact?.Address?.City },
                { "{{ShippingState}}", cart.ShippingContact?.Address?.RegionCode },
                { "{{ShippingZipCode}}", cart.ShippingContact?.Address?.PostalCode },
                { "{{SubTotal}}", subTotal.ToString("C") },
                { "{{Shipping}}", $"${shipping:0.00} {selectedRate?.ShipCarrierMethodName}" },
                { "{{Tax}}", tax.ToString("C") },
                { "{{Total}}", total.ToString("C") },
                { "{{warehouseDetails}}", pilsItemsHTML },
                { "{{shippingDetails}}", lineItemsHTML },
                { "{{rates}}", rateHtml },
                { "{{orderID}}", orderID.ToString() },
            };
        }
    }
}
