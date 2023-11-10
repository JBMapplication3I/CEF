// <copyright file="TNTShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tnt shipping provider class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.TNT
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Shipping;
    using Models;

    /// <summary>A TNT shipping provider.</summary>
    /// <seealso cref="ShippingProviderBase"/>
    public class TNTShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => TNTShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IRateQuoteModel>?>> GetRateQuotesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            string? contextProfileName)
        {
            if (salesItemIDs == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Line Items are required to get TNT shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get TNT shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get TNT shipping rate quotes");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Origin is required to get TNT shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Destination is required to get TNT shipping rate quotes");
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "TNT");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            // Generate a TNT Rate Request
            var stringBuilder = new StringBuilder();
            WriteXMLRequest(stringBuilder, items, destination, origin.Address!.CountryCode!);
            var url = TNTShippingProviderConfig.URL;
            HttpResponseMessage response;
            var httpContent = new StringContent(stringBuilder.ToString(), Encoding.UTF8, "application/xml");
            // Run the Request
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "text/xml");
                try
                {
                    // ReSharper disable once AsyncConverter.AsyncWait
                    response = await client.PostAsync(new Uri(url), httpContent);
                }
                catch (Exception ex)
                {
                    await Logger.LogErrorAsync("Get TNT Shipping Rates", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                    return new List<IRateQuoteModel>().WrapInFailingCEFAR($"TNT GetRates: {ex.Message}");
                }
            }
            // Check for Errors
            if (!response.IsSuccessStatusCode)
            {
                const string Message = "The response code was invalid";
                await Logger.LogErrorAsync("Get TNT Shipping Rates", Message, contextProfileName).ConfigureAwait(false);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"TNT GetRates: {Message}");
            }
            // Parse the response
            // ReSharper disable once AsyncConverter.AsyncWait
            var responseStr = await response.Content.ReadAsStringAsync();
            var rates = ParseResponse(responseStr);
            // Check for Minimum Rate Amount requirements and apply if set
            var defaultMinimumPrice = TNTShippingProviderConfig.DefaultMinimumPrice;
            if (TNTShippingProviderConfig.UseDefaultMinimumPricing && defaultMinimumPrice != 0)
            {
                foreach (var rate in rates!.Where(x => x.Rate < defaultMinimumPrice))
                {
                    rate.Rate = defaultMinimumPrice;
                }
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID: cartID,
                    hash: hash,
                    currentShippingProvider: "TNT",
                    rates: rates,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return results.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override Task<List<IShipmentRate>> GetBaseOrNetChargesAsync(
            int cartID,
            List<int> salesItemIDs,
            IContactModel origin,
            IContactModel destination,
            bool expedited,
            bool useBaseCharge,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Writes an XML request.</summary>
        /// <param name="stringBuilder">    The string builder.</param>
        /// <param name="items">            The items.</param>
        /// <param name="destination">      Destination for the.</param>
        /// <param name="originCountryCode">The origin country code.</param>
        private static void WriteXMLRequest(
            StringBuilder stringBuilder,
            IEnumerable<IProviderShipment>? items,
            IContactModel destination,
            string originCountryCode)
        {
            if (items is null)
            {
                return;
            }
            using var stringWriter = new StringWriter(stringBuilder);
            using var xmlWriter = new XmlTextWriter(stringWriter);
            // build XML string
            xmlWriter.Formatting = Formatting.Indented;
            // Document
            {
                xmlWriter.WriteStartElement("Document");
                xmlWriter.WriteElementString("Application", "MYSHP");
                xmlWriter.WriteElementString("Version", "3.0");
                // Login
                {
                    xmlWriter.WriteStartElement("Login");
                    xmlWriter.WriteElementString("Customer", TNTShippingProviderConfig.CustomerID);
                    xmlWriter.WriteElementString("User", TNTShippingProviderConfig.UserID);
                    xmlWriter.WriteElementString("Password", TNTShippingProviderConfig.Password);
                    xmlWriter.WriteElementString("LangID", "EN");
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteElementString("ApplicationFunction", "priceCheck");
                // Details
                {
                    xmlWriter.WriteStartElement("Details");
                    xmlWriter.WriteElementString("AccountNo", "08038403");
                    // Package
                    {
                        xmlWriter.WriteStartElement("Package");
                        var itemSeqNo = 0;
                        foreach (var item in items)
                        {
                            // ITEMS
                            xmlWriter.WriteStartElement("Items");
                            xmlWriter.WriteElementString("ItemSeqNo", itemSeqNo.ToString());
                            xmlWriter.WriteElementString("Type", "0");
                            xmlWriter.WriteElementString("INumber", "1");
                            xmlWriter.WriteElementString("IWeight", item.Weight.ToString("n3"));
                            xmlWriter.WriteElementString("IDescription", string.Empty);
                            xmlWriter.WriteElementString("Length", item.Depth.ToString());
                            // Review and see if we can get the length
                            xmlWriter.WriteElementString("Height", item.Height.ToString());
                            xmlWriter.WriteElementString("Width", item.Width.ToString());
                            xmlWriter.WriteEndElement();
                            ++itemSeqNo;
                        }
                        xmlWriter.WriteEndElement();
                    }
                    // Common
                    {
                        xmlWriter.WriteStartElement("Common");
                        xmlWriter.WriteElementString("ContactName", destination.FullName ?? $"{destination.FirstName} {destination.LastName}".Trim());
                        xmlWriter.WriteElementString("Service", string.Empty);
                        xmlWriter.WriteElementString("Insurance", string.Empty);
                        xmlWriter.WriteElementString("InsuranceCurrency", "EUR");
                        xmlWriter.WriteElementString("SenderReference", string.Empty);
                        xmlWriter.WriteElementString("Payment", "0");
                        xmlWriter.WriteElementString("Instructions", string.Empty);
                        xmlWriter.WriteElementString("SpecialGoods", "N");
                        xmlWriter.WriteEndElement();
                    }
                    if (destination.Address!.CountryCode == "IT")
                    {
                        // Domestic
                        {
                            xmlWriter.WriteStartElement("Domestic");
                            // COD
                            {
                                xmlWriter.WriteStartElement("COD");
                                xmlWriter.WriteElementString("Amount", string.Empty);
                                xmlWriter.WriteElementString("Currency", "EUR");
                                xmlWriter.WriteElementString("SenderComm", string.Empty);
                                xmlWriter.WriteElementString("SenderRefund", "N");
                                xmlWriter.WriteEndElement();
                            }
                            // OperationalOptions
                            {
                                xmlWriter.WriteStartElement("OperationalOptions");
                                xmlWriter.WriteElementString("Option", "0");
                                xmlWriter.WriteEndElement();
                            }
                            // EOM
                            {
                                xmlWriter.WriteStartElement("EOM");
                                xmlWriter.WriteElementString("Division", "0");
                                xmlWriter.WriteElementString("Enclosure", "0");
                                xmlWriter.WriteElementString("Unification", "0");
                                xmlWriter.WriteElementString("OfferNo", "0");
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    else
                    {
                        xmlWriter.WriteStartElement("International");
                        xmlWriter.WriteElementString("GoodsValue", string.Empty);
                        xmlWriter.WriteElementString("GoodsValueCurrency", "EUR");
                        xmlWriter.WriteElementString("Priority", "N");
                        xmlWriter.WriteElementString("FDA", "N");
                        xmlWriter.WriteElementString("DryIce", "N");
                        xmlWriter.WriteElementString("StatNo", string.Empty);
                        xmlWriter.WriteElementString("CatMerced", string.Empty);
                        xmlWriter.WriteElementString("OriginCountry", originCountryCode);
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteElementString("CheckPriceEnabled", "Y");
                    xmlWriter.WriteEndElement();
                }
                // Shipment
                {
                    xmlWriter.WriteStartElement("Shipment");
                    xmlWriter.WriteElementString("ShipmentKey", string.Empty);
                    xmlWriter.WriteElementString("isChanged", string.Empty);
                    xmlWriter.WriteElementString("Date", DateExtensions.GenDateTime.ToString("dd.MM.yyyy"));
                    // Template
                    {
                        xmlWriter.WriteStartElement("Template");
                        xmlWriter.WriteElementString("TemplateName", string.Empty);
                        xmlWriter.WriteElementString("TemplateUse", "0");
                        xmlWriter.WriteElementString("AmendTemplate", "N");
                        xmlWriter.WriteEndElement();
                    }
                    // Receiver
                    {
                        xmlWriter.WriteStartElement("Receiver");
                        // Address
                        {
                            xmlWriter.WriteStartElement("Address");
                            xmlWriter.WriteElementString("ShortName", string.Empty);
                            xmlWriter.WriteElementString("CompanyName", "TEST COMPANY");
                            xmlWriter.WriteElementString("ReceiverAccountNo", string.Empty);
                            xmlWriter.WriteElementString("AddressLine1", destination.Address.Street1);
                            xmlWriter.WriteElementString("AddressLine2", destination.Address.Street2);
                            xmlWriter.WriteElementString("AddressLine3", destination.Address.Street3);
                            xmlWriter.WriteElementString("TownId", string.Empty);
                            xmlWriter.WriteElementString("Town", destination.Address.City);
                            xmlWriter.WriteElementString("ProvinceState", destination.Address.RegionCode);
                            xmlWriter.WriteElementString("Postcode", destination.Address.PostalCode);
                            xmlWriter.WriteElementString("CountryID", destination.Address.CountryCode);
                            xmlWriter.WriteElementString("ContactName", destination.FullName ?? $"{destination.FirstName} {destination.LastName}".Trim());
                            xmlWriter.WriteElementString("PhoneID1", destination.Phone1);
                            xmlWriter.WriteElementString("PhoneID2", destination.Phone2);
                            xmlWriter.WriteElementString("FaxID1", destination.Fax1);
                            xmlWriter.WriteElementString("FaxID2", string.Empty);
                            xmlWriter.WriteElementString("Email", destination.Email1);
                            xmlWriter.WriteElementString("PreAlert", "N");
                            xmlWriter.WriteElementString("AmendAddress", "N");
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteElementString("Incomplete", "N");
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteElementString("CMessage", string.Empty);
                xmlWriter.WriteElementString("ExtraCee", "N");
                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>Parse response.</summary>
        /// <param name="responseStr">The response string.</param>
        /// <returns>A List{IShipmentRate}.</returns>
        private static List<IShipmentRate>? ParseResponse(string responseStr)
        {
            var xmlDoc = new XmlDocument();
            responseStr = responseStr.Replace("<!DOCTYPE ResultSet SYSTEM \"shpOU.dtd\">", string.Empty);
            xmlDoc.LoadXml(responseStr);
            var nodeList = xmlDoc.SelectSingleNode("ResultSet/Complete/ServicesList");
            var serviceNodes = nodeList?.SelectNodes("Service");
            if (serviceNodes == null)
            {
                return null;
            }
            var list = new List<IShipmentRate>();
            foreach (XmlNode node in serviceNodes)
            {
                var shipmentRate = new ShipmentRate();
                var codeNode = node.SelectSingleNode("Code");
                var descriptionNode = node.SelectSingleNode("Description");
                var priceNode = node.SelectSingleNode("Price");
                shipmentRate.OptionCode = codeNode?.InnerText;
                shipmentRate.CarrierName = "TNT";
                shipmentRate.FullOptionName = descriptionNode?.InnerText;
                shipmentRate.OptionName = descriptionNode?.InnerText;
                shipmentRate.Rate = priceNode != null ? decimal.Parse(priceNode.InnerText.Replace(",", ".")) : 0m;
                list.Add(shipmentRate);
            }
            return list;
        }
    }
}
