// <copyright file="SharedSalesOrders.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shared sales orders class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A shared sales orders.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class SharedSalesOrders
    {
        /// <summary>Gets a value indicating whether the shipping is hidden.</summary>
        /// <value>True if hide shipping, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.HideShipping"),
         DefaultValue(false)]
        internal static bool HideShipping
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(SharedSalesOrders)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets the alternate name shipping.</summary>
        /// <value>The alternate name shipping.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.AltNameShipping"),
         DefaultValue("Shipping")]
        internal static string AltNameShipping
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(SharedSalesOrders)) ? asValue : "Shipping";
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets a value indicating whether the handling is hidden.</summary>
        /// <value>True if hide handling, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.HideHandling"),
         DefaultValue(false)]
        internal static bool HideHandling
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(SharedSalesOrders)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets the alternate name handling.</summary>
        /// <value>The alternate name handling.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.AltNameHandling"),
         DefaultValue("Handling")]
        internal static string AltNameHandling
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(SharedSalesOrders)) ? asValue : "Handling";
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets a value indicating whether the fees is hidden.</summary>
        /// <value>True if hide fees, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.HideFees"),
         DefaultValue(false)]
        internal static bool HideFees
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(SharedSalesOrders)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets the alternate name fees.</summary>
        /// <value>The alternate name fees.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.AltNameFees"),
         DefaultValue("Fees")]
        internal static string AltNameFees
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(SharedSalesOrders)) ? asValue : "Fees";
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets a value indicating whether the discounts is hidden.</summary>
        /// <value>True if hide discounts, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.HideDiscounts"),
         DefaultValue(false)]
        internal static bool HideDiscounts
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(SharedSalesOrders)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets the alternate name discounts.</summary>
        /// <value>The alternate name discounts.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.AltNameDiscounts"),
         DefaultValue("Discounts")]
        internal static string AltNameDiscounts
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(SharedSalesOrders)) ? asValue : "Discounts";
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets a value indicating whether the taxes is hidden.</summary>
        /// <value>True if hide taxes, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.HideTaxes"),
         DefaultValue(false)]
        internal static bool HideTaxes
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(SharedSalesOrders)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets the alternate name taxes.</summary>
        /// <value>The alternate name taxes.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.AltNameTaxes"),
         DefaultValue("Taxes")]
        internal static string AltNameTaxes
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(SharedSalesOrders)) ? asValue : "Taxes";
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets a value indicating whether the total is hidden.</summary>
        /// <value>True if hide total, false if not.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.HideTotal"),
         DefaultValue(false)]
        internal static bool HideTotal
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(SharedSalesOrders)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Gets the alternate name total.</summary>
        /// <value>The alternate name total.</value>
        [AppSettingsKey("Clarity.Notifications.Sales.Totals.AltNameTotal"),
         DefaultValue("Total")]
        internal static string AltNameTotal
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(SharedSalesOrders)) ? asValue : "Total";
            private set => CEFConfigDictionary.TrySet(value, typeof(SharedSalesOrders));
        }

        /// <summary>Loads this SharedSalesOrders Dictionary of Settings.</summary>
        internal static void Load()
        {
            CEFConfigDictionary.Load(typeof(SharedSalesOrders));
        }

        /// <summary>Standard order replacements.</summary>
        /// <param name="model">                The sales collection model.</param>
        /// <param name="salesItems">           The sales items.</param>
        /// <param name="replacementDictionary">The replacement dictionary.</param>
        /// <returns>A Task.</returns>
        internal static Task StandardSalesCollectionReplacementsAsync(
            ISalesCollectionBaseModel model,
            IEnumerable<ISalesItemBaseModel> salesItems,
            Dictionary<string, string?> replacementDictionary)
        {
            var lineItemsHTML = salesItems
                    .Aggregate(
                        "<table style=\"width:100%;\"><thead><tr><th style=\"padding: 6px;\">Name</th><th style=\"padding: 6px; text-align: right;\">Item Number</th><th style=\"padding: 6px; text-align: right;\">Quantity</th><th style=\"padding: 6px; text-align: right;\">UOM</th><th style=\"padding: 6px; text-align: right;\">Price</th></tr></thead><tbody>",
                        (current, item) => current
                            + "<tr>"
                            + $"<td style=\"padding: 6px;\"><a href=\"{{{{RootUrl}}}}/{{{{ProductDetailUrlFragment}}}}/{item.ProductSeoUrl}\">{item.ProductName}</a></td>"
                            + $"<td style=\"padding: 6px;text-align: right;\">{item.ProductKey}</td>"
                            + $"<td style=\"padding: 6px;text-align: right;\">{item.TotalQuantity:#,##0.####}</td>"
                            + $"<td style=\"padding: 6px;text-align: right;\">{item.ProductUnitOfMeasure}</td>"
                            + $"<td style=\"padding: 6px;text-align: right;\">{item.ExtendedPrice:C2}</td>"
                            + "</tr>")
                + "</tbody><tfoot>";
            const string TotalsLineFormat = "<tr><td colspan=\"2\" style=\"font-weight: bold; text-align: right;\">{0}</td><td style=\"padding: 6px;text-align: right;\">{1:C2}</td></tr>";
            if (HideShipping)
            {
                // Hidden
            }
            else if (model.Totals!.Shipping != 0m)
            {
                lineItemsHTML += string.Format(
                    TotalsLineFormat,
                    AltNameShipping,
                    model.Totals.Shipping);
            }
            if (HideHandling)
            {
                // Hidden
            }
            else if (model.Totals!.Handling != 0m)
            {
                lineItemsHTML += string.Format(
                    TotalsLineFormat,
                    AltNameHandling,
                    model.Totals.Handling);
            }
            if (HideFees)
            {
                // Hidden
            }
            else if (model.Totals!.Fees != 0m)
            {
                lineItemsHTML += string.Format(
                    TotalsLineFormat,
                    AltNameFees,
                    model.Totals.Fees);
            }
            if (HideDiscounts)
            {
                // Hidden
            }
            else if (model.Totals!.Discounts != 0m)
            {
                lineItemsHTML += string.Format(
                    TotalsLineFormat,
                    AltNameDiscounts,
                    model.Totals.Discounts);
            }
            if (HideTaxes)
            {
                // Hidden
            }
            else if (model.Totals!.Tax != 0m)
            {
                lineItemsHTML += string.Format(
                    TotalsLineFormat,
                    AltNameTaxes,
                    model.Totals.Tax);
            }
            if (HideTotal)
            {
                // Hidden
            }
            else if (model.Totals!.Total != 0m)
            {
                lineItemsHTML += string.Format(
                    TotalsLineFormat,
                    AltNameTotal,
                    model.Totals.Total);
            }
            lineItemsHTML += "</tfoot></table>";
            replacementDictionary["{{ID}}"] = model.ID.ToString();
            replacementDictionary["{{ProductDetailUrlFragment}}"] = CEFConfigDictionary.ProductDetailRouteRelativePath;
            replacementDictionary["{{PurchaseDetails}}"] = lineItemsHTML;
            replacementDictionary["{{AccountKey}}"] = model.AccountKey ?? model.Account?.CustomKey ?? string.Empty;
            replacementDictionary["{{AccountID}}"] = model.AccountID?.ToString() ?? model.Account?.ID.ToString() ?? string.Empty;
            // replacementDictionary["{{ShippingMethod}}"] = shipping;
            replacementDictionary["{{Total}}"]
                = replacementDictionary["{{OrderTotal}}"]
                    = model.Totals?.Total.ToString("C2") ?? string.Empty;
            replacementDictionary["{{Date}}"]
                = replacementDictionary["{{OrderDate}}"]
                    = (model.OriginalDate ?? model.CreatedDate).ToString("f");
            replacementDictionary["{{UserID}}"] = model.UserID?.ToString();
            replacementDictionary["{{AccountName}}"] = model.AccountName;
            replacementDictionary["{{CustomKey}}"] = model.CustomKey;
            replacementDictionary["{{BillingCompany}}"] = model.AccountName;
            replacementDictionary["{{ShippingCompany}}"] = model.AccountName;
            replacementDictionary["{{AccountContactName}}"] = model.UserContactFirstName + " " + model.UserContactLastName;
            replacementDictionary["{{ShippingMethod}}"] = model.RateQuotes?.Count > 0
                ? model.RateQuotes.Find(x => x.Active && x.Selected)?.ShipCarrierMethodName
                : string.Empty;
            var storePickupGuid = string.Empty;
            if (model.SerializableAttributes!.ContainsKey("InStorePickupGuid"))
            {
                storePickupGuid = model.SerializableAttributes["InStorePickupGuid"].Value;
            }
            else if (model.SerializableAttributes.ContainsKey("ShipToStoreGuid"))
            {
                storePickupGuid = model.SerializableAttributes["ShipToStoreGuid"].Value;
            }
            replacementDictionary["{{Guid}}"] = storePickupGuid;
            if (model.BillingContact != null)
            {
                replacementDictionary["{{Email}}"]
                    = replacementDictionary["{{BillingEmail}}"]
                        = model.BillingContact.Email1 ?? string.Empty;
                replacementDictionary["{{FirstName}}"]
                    = replacementDictionary["{{BillingFirstName}}"]
                        = model.BillingContact.FirstName ?? string.Empty;
                replacementDictionary["{{LastName}}"]
                    = replacementDictionary["{{BillingLastName}}"]
                        = model.BillingContact.LastName ?? string.Empty;
                replacementDictionary["{{Name}}"]
                    = replacementDictionary["{{BillingName}}"]
                        = model.BillingContact.FullName ?? string.Empty;
                replacementDictionary["{{Phone}}"]
                    = replacementDictionary["{{BillingPhone}}"]
                        = model.BillingContact.Phone1 ?? string.Empty;
                if (model.BillingContact.Address != null)
                {
                    // ReSharper disable MissingLinebreak
                    replacementDictionary["{{BillingStreetAddress}}"] = (model.BillingContact.Address.Street1 ?? string.Empty)
                        + " " + (model.BillingContact.Address.Street2 ?? string.Empty)
                        + " " + (model.BillingContact.Address.Street3 ?? string.Empty);
                    replacementDictionary["{{BillingCity}}"] = model.BillingContact.Address.City ?? string.Empty;
                    replacementDictionary["{{BillingState}}"] = model.BillingContact.Address.RegionCode ?? string.Empty;
                    replacementDictionary["{{BillingZipCode}}"] = model.BillingContact.Address.PostalCode ?? string.Empty;
                    replacementDictionary["{{BillingAddress}}"] = (model.BillingContact.Address.Street1 ?? string.Empty)
                        + " " + (model.BillingContact.Address.Street2 ?? string.Empty)
                        + " " + (model.BillingContact.Address.Street3 ?? string.Empty)
                        + " " + (model.BillingContact.Address.City ?? string.Empty)
                        + " " + (model.BillingContact.Address.RegionName ?? string.Empty)
                        + " " + (model.BillingContact.Address.PostalCode ?? string.Empty);
                    // ReSharper restore MissingLinebreak
                }
                else
                {
                    // ReSharper disable MissingIndent
                    replacementDictionary["{{BillingStreetAddress}}"]
                        = replacementDictionary["{{BillingCity}}"]
                            = replacementDictionary["{{BillingState}}"]
                                = replacementDictionary["{{BillingZipCode}}"]
                                    = replacementDictionary["{{BillingAddress}}"]
                                        = string.Empty;
                    // ReSharper restore MissingIndent
                }
            }
            else
            {
                // ReSharper disable MissingIndent
                replacementDictionary["{{Email}}"]
                    = replacementDictionary["{{FirstName}}"]
                        = replacementDictionary["{{LastName}}"]
                            = replacementDictionary["{{Name}}"]
                                = replacementDictionary["{{Phone}}"]
                                    = replacementDictionary["{{BillingName}}"]
                                        = replacementDictionary["{{BillingFirstName}}"]
                                            = replacementDictionary["{{BillingLastName}}"]
                                                = replacementDictionary["{{BillingEmail}}"]
                                                    = replacementDictionary["{{BillingPhone}}"]
                                                        = replacementDictionary["{{BillingStreetAddress}}"]
                                                            = replacementDictionary["{{BillingCity}}"]
                                                                = replacementDictionary["{{BillingState}}"]
                                                                    = replacementDictionary["{{BillingZipCode}}"]
                                                                        = replacementDictionary["{{BillingAddress}}"]
                                                                            = string.Empty;
                // ReSharper restore MissingIndent
            }
            if (!(model.ShippingSameAsBilling ?? false) && model.ShippingContact != null)
            {
                replacementDictionary["{{ShippingEmail}}"] = model.ShippingContact.Email1 ?? string.Empty;
                replacementDictionary["{{ShippingFirstName}}"] = model.ShippingContact.FirstName ?? string.Empty;
                replacementDictionary["{{ShippingLastName}}"] = model.ShippingContact.LastName ?? string.Empty;
                replacementDictionary["{{ShippingName}}"] = model.ShippingContact.FullName ?? string.Empty;
                replacementDictionary["{{ShippingPhone}}"] = model.ShippingContact.Phone1 ?? string.Empty;
                if (model.ShippingContact.Address != null)
                {
                    // ReSharper disable MissingLinebreak
                    replacementDictionary["{{ShippingStreetAddress}}"] = (model.ShippingContact.Address.Street1 ?? string.Empty)
                        + " " + (model.ShippingContact.Address.Street2 ?? string.Empty)
                        + " " + (model.ShippingContact.Address.Street3 ?? string.Empty);
                    replacementDictionary["{{ShippingCity}}"] = model.ShippingContact.Address.City ?? string.Empty;
                    replacementDictionary["{{ShippingState}}"] = model.ShippingContact.Address.RegionCode ?? string.Empty;
                    replacementDictionary["{{ShippingZipCode}}"] = model.ShippingContact.Address.PostalCode ?? string.Empty;
                    replacementDictionary["{{ShippingAddress}}"] = (model.ShippingContact.Address.Street1 ?? string.Empty)
                        + " " + (model.ShippingContact.Address.Street2 ?? string.Empty)
                        + " " + (model.ShippingContact.Address.Street3 ?? string.Empty)
                        + " " + (model.ShippingContact.Address.City ?? string.Empty)
                        + " " + (model.ShippingContact.Address.RegionName ?? string.Empty)
                        + " " + (model.ShippingContact.Address.PostalCode ?? string.Empty);
                    // ReSharper restore MissingLinebreak
                }
                else
                {
                    // ReSharper disable MissingIndent
                    replacementDictionary["{{ShippingStreetAddress}}"]
                        = replacementDictionary["{{ShippingCity}}"]
                            = replacementDictionary["{{ShippingState}}"]
                                = replacementDictionary["{{ShippingZipCode}}"]
                                    = replacementDictionary["{{ShippingAddress}}"]
                                        = string.Empty;
                    // ReSharper restore MissingIndent
                }
            }
            else if (model.ShippingSameAsBilling ?? false)
            {
                replacementDictionary["{{ShippingName}}"] = replacementDictionary["{{BillingName}}"];
                replacementDictionary["{{ShippingFirstName}}"] = replacementDictionary["{{BillingFirstName}}"];
                replacementDictionary["{{ShippingLastName}}"] = replacementDictionary["{{BillingLastName}}"];
                replacementDictionary["{{ShippingEmail}}"] = replacementDictionary["{{BillingEmail}}"];
                replacementDictionary["{{ShippingPhone}}"] = replacementDictionary["{{BillingPhone}}"];
                replacementDictionary["{{ShippingStreetAddress}}"] = replacementDictionary["{{BillingStreetAddress}}"];
                replacementDictionary["{{ShippingCity}}"] = replacementDictionary["{{BillingCity}}"];
                replacementDictionary["{{ShippingState}}"] = replacementDictionary["{{BillingState}}"];
                replacementDictionary["{{ShippingZipCode}}"] = replacementDictionary["{{BillingZipCode}}"];
                replacementDictionary["{{ShippingAddress}}"] = replacementDictionary["{{BillingAddress}}"];
            }
            else
            {
                // ReSharper disable MissingIndent
                replacementDictionary["{{ShippingName}}"]
                    = replacementDictionary["{{ShippingFirstName}}"]
                        = replacementDictionary["{{ShippingLastName}}"]
                            = replacementDictionary["{{ShippingEmail}}"]
                                = replacementDictionary["{{ShippingPhone}}"]
                                    = replacementDictionary["{{ShippingStreetAddress}}"]
                                        = replacementDictionary["{{ShippingCity}}"]
                                            = replacementDictionary["{{ShippingState}}"]
                                                = replacementDictionary["{{ShippingZipCode}}"]
                                                    = replacementDictionary["{{ShippingAddress}}"]
                                                        = string.Empty;
                // ReSharper restore MissingIndent
            }
            if (model.Store?.Contact != null)
            {
                replacementDictionary["{{StoreEmail}}"] = model.Store.Contact.Email1 ?? string.Empty;
                replacementDictionary["{{StoreFirstName}}"] = model.Store.Contact.FirstName ?? string.Empty;
                replacementDictionary["{{StoreLastName}}"] = model.Store.Contact.LastName ?? string.Empty;
                replacementDictionary["{{StoreName}}"] = model.Store.Name ?? string.Empty;
                // ReSharper disable MissingIndent
                replacementDictionary["{{StorePhone}}"]
                    = replacementDictionary["{{StorePhoneNumber}}"]
                        = model.Store.Contact.Phone1 ?? string.Empty;
                // ReSharper restore MissingIndent
                if (model.Store.Contact.Address != null)
                {
                    // ReSharper disable MissingLinebreak
                    replacementDictionary["{{StoreStreetAddress}}"] = (model.Store.Contact.Address.Street1 ?? string.Empty)
                        + " " + (model.Store.Contact.Address.Street2 ?? string.Empty)
                        + " " + (model.Store.Contact.Address.Street3 ?? string.Empty);
                    replacementDictionary["{{StoreCity}}"] = model.Store.Contact.Address.City ?? string.Empty;
                    replacementDictionary["{{StoreState}}"] = model.Store.Contact.Address.RegionCode ?? string.Empty;
                    replacementDictionary["{{StoreZipCode}}"] = model.Store.Contact.Address.PostalCode ?? string.Empty;
                    replacementDictionary["{{StoreAddress}}"] = (model.Store.Contact.Address.Street1 ?? string.Empty)
                        + " " + (model.Store.Contact.Address.Street2 ?? string.Empty)
                        + " " + (model.Store.Contact.Address.Street3 ?? string.Empty)
                        + " " + (model.Store.Contact.Address.City ?? string.Empty)
                        + " " + (model.Store.Contact.Address.RegionName ?? string.Empty)
                        + " " + (model.Store.Contact.Address.PostalCode ?? string.Empty);
                    // ReSharper restore MissingLinebreak
                }
                else
                {
                    // ReSharper disable MissingIndent
                    replacementDictionary["{{StoreStreetAddress}}"]
                        = replacementDictionary["{{StoreCity}}"]
                            = replacementDictionary["{{StoreState}}"]
                                = replacementDictionary["{{StoreZipCode}}"]
                                    = replacementDictionary["{{StoreAddress}}"]
                                        = string.Empty;
                    // ReSharper restore MissingIndent
                }
            }
            else
            {
                // ReSharper disable MissingIndent
                replacementDictionary["{{StoreName}}"]
                    = replacementDictionary["{{StoreFirstName}}"]
                        = replacementDictionary["{{StoreLastName}}"]
                            = replacementDictionary["{{StoreEmail}}"]
                                = replacementDictionary["{{StorePhone}}"]
                                    = replacementDictionary["{{StorePhoneNumber}}"]
                                        = replacementDictionary["{{StoreStreetAddress}}"]
                                            = replacementDictionary["{{StoreCity}}"]
                                                = replacementDictionary["{{StoreState}}"]
                                                    = replacementDictionary["{{StoreZipCode}}"]
                                                        = replacementDictionary["{{StoreAddress}}"]
                                                            = string.Empty;
                // ReSharper restore MissingIndent
            }
            // Replace attribute key as well if found in the template
            // ReSharper disable once InvertIf
            if (model.SerializableAttributes?.Any() == true)
            {
                foreach (var attr in model.SerializableAttributes)
                {
                    replacementDictionary.Add($"{{{{{attr.Key}}}}}", attr.Value?.Value ?? string.Empty);
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>Standard order replacements.</summary>
        /// <param name="salesItems">           The sales collection model.</param>
        /// <param name="userModel">            The user model.</param>
        /// <param name="replacementDictionary">The replacement dictionary.</param>
        /// <returns>A Task.</returns>
        internal static Task StandardSalesCollectionReplacementsAsync(
            IEnumerable<ISalesItemBaseModel> salesItems,
            IUserModel userModel,
            Dictionary<string, string> replacementDictionary)
        {
            var lineItemsHTML = salesItems
                    .Aggregate(
                        "<table style=\"width:100%;\"><thead><tr><th style=\"padding: 6px;\">Name</th><th style=\"padding: 6px;\">Quantity</th><th style=\"padding: 6px;\">Price</th></tr></thead><tbody>",
                        (current, item) => current
                            + "<tr>"
                            + $"<td style=\"padding: 6px;\"><a href=\"{{{{RootUrl}}}}/{{{{ProductDetailUrlFragment}}}}/{item.ProductSeoUrl}\">{item.ProductName}</a></td>"
                            + $"<td style=\"padding: 6px;text-align: right;\">{item.TotalQuantity:#,##0.####}</td>"
                            + $"<td style=\"padding: 6px;text-align: right;\">{item.ExtendedPrice:C2}</td>"
                            + "</tr>")
                + "</tbody></ table > ";
            replacementDictionary["{{ProductDetailUrlFragment}}"] = CEFConfigDictionary.ProductDetailRouteRelativePath;
            replacementDictionary["{{PurchaseDetails}}"] = lineItemsHTML;
            replacementDictionary["{{Date}}"]
                = replacementDictionary["{{OrderDate}}"]
                    = DateExtensions.GenDateTime.ToString("f");
            replacementDictionary["{{Email}}"]
                = userModel?.Email ?? string.Empty;
            replacementDictionary["{{FirstName}}"]
                = userModel?.ContactFirstName ?? string.Empty;
            replacementDictionary["{{LastName}}"]
                = userModel?.ContactLastName ?? string.Empty;
            return Task.CompletedTask;
        }

        /// <summary>Standard order replacements.</summary>
        /// <param name="order">                 The order.</param>
        /// <param name="replacementsDictionary">Dictionary of replacements.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        internal static async Task StandardOrderReplacementsAsync(
            ISalesOrderModel order,
            Dictionary<string, string?> replacementsDictionary)
        {
            await StandardSalesCollectionReplacementsAsync(order, order.SalesItems!, replacementsDictionary).ConfigureAwait(false);
            // Sales Order specific replacements
            replacementsDictionary["{{PurchaseOrderID}}"] = order.PurchaseOrderNumber ?? string.Empty;
            replacementsDictionary["{{ShippingInstructions}}"] = order.SalesGroupAsSub?.Notes.FirstOrDefault()?.Note1 ?? string.Empty;
            // ReSharper disable MissingIndent
            replacementsDictionary["{{TransactionID}}"]
                = replacementsDictionary["{{PaymentTransactionID}}"]
                    = order.PaymentTransactionID ?? string.Empty;
            // ReSharper restore MissingIndent
            replacementsDictionary["{{TaxTransactionID}}"] = order.TaxTransactionID ?? string.Empty;
            if (order.SalesOrderPayments?.Count > 0)
            {
                replacementsDictionary["{{CardType}}"] = order.SalesOrderPayments[0].Slave!.PaymentMethodName ?? string.Empty;
                replacementsDictionary["{{CardLastFour}}"] = order.SalesOrderPayments[0].Slave!.Last4CardDigits;
            }
            if (order.SerializableAttributes?.ContainsKey("TrackingNumber") != true)
            {
                return;
            }
            var trackingNumber = order.SerializableAttributes["TrackingNumber"].Value;
            if (!Contract.CheckValidKey(trackingNumber))
            {
                return;
            }
            var (href, tag) = SharedShipping.GenTrackingLink(trackingNumber);
            replacementsDictionary["{{TrackingLink}}"] = tag;
            replacementsDictionary["{{TrackingUrl}}"] = href;
            replacementsDictionary["{{TrackingNumber}}"] = trackingNumber;
        }

        /// <summary>Gets order customer email to use.</summary>
        /// <param name="order">             The order.</param>
        /// <param name="whichEmailTemplate">The which email template.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The order customer email to use.</returns>
        internal static async Task<CEFActionResponse<string?>> GetOrderCustomerEmailToUseAsync(
            ISalesOrderModel order,
            string whichEmailTemplate,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var userEmail = await context.Users
                .AsNoTracking()
                .FilterByID(order.UserID)
                .Select(x => x.Email)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckValidKey(userEmail))
            {
                return userEmail.WrapInPassingCEFAR();
            }
            if (!Contract.CheckAnyValidID(order.SalesGroupAsMasterID, order.SalesGroupAsSubID))
            {
                return CEFAR.FailingCEFAR<string?>(
                    $"ERROR! Unable to send Customer order ${whichEmailTemplate} notification as there is no email"
                    + " address to use in the order and it doesn't belong to a sales group to check");
            }
            var billingEmail = await context.SalesGroups
                .AsNoTracking()
                .FilterByID((order.SalesGroupAsMasterID ?? order.SalesGroupAsSubID)!.Value)
                .Where(x => x.BillingContact != null)
                .Select(x => x.BillingContact!.Email1)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckValidKey(billingEmail))
            {
                return billingEmail.WrapInPassingCEFAR();
            }
            return CEFAR.FailingCEFAR<string?>(
                $"ERROR! Unable to send Customer order ${whichEmailTemplate} notification as there is no email"
                + " address to use in the order or the sales group it belongs to");
        }

        /// <summary>Standard payment replacements.</summary>
        /// <param name="payment">               The payment.</param>
        /// <param name="replacementsDictionary">Dictionary of replacements.</param>
        internal static void StandardPaymentReplacements(
            IPaymentModel payment,
            Dictionary<string, string?> replacementsDictionary)
        {
            replacementsDictionary["{{CardType}}"] = payment.PaymentMethodName;
            replacementsDictionary["{{CardLastFour}}"] = payment.Last4CardDigits;
            // ReSharper disable once PossibleInvalidOperationException
            replacementsDictionary["{{Amount}}"] = payment.Amount!.Value.ToString("C");
        }
    }
}
