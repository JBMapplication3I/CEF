// <copyright file="USPSShippingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the usps shipping provider class</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Shipping.USPS
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using DotNetShipping;
    using DotNetShipping.ShippingProviders;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using JSConfigs;
    using Models;

    /// <summary>The usps shipping provider.</summary>
    public class USPSShippingProvider : ShippingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => USPSShippingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Line Items are required to get USPS shipping rate quotes");
            }
            // Get the item packages out of the database
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get USPS shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Packaging provider is required to get USPS shipping rate quotes");
            }
            var items = itemsResult.Result;
            if (items is null || !items.Any())
            {
                return CEFAR.PassingCEFAR(new List<IRateQuoteModel>(), "NOTE! No items in this cart need to ship");
            }
            // Validate the addresses
            if (origin == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Origin is required to get USPS shipping rate quotes");
            }
            if (destination == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>?>("ERROR! Destination is required to get USPS shipping rate quotes");
            }
            if (string.IsNullOrWhiteSpace(destination.Address?.Country?.Code ?? destination.Address?.CountryCode)
                || string.IsNullOrWhiteSpace(destination.Address?.PostalCode))
            {
                const string Message = "WARNING: Either the destination country code or postal code were blank.";
                await Logger.LogWarningAsync("Get USPS Shipping Rates", Message, contextProfileName).ConfigureAwait(false);
                return new(new(), false, Message);
            }
            // Hash the request parts and check to see if we already have results that would match this hash
            var hash = BuildSimpleRequestHash(origin, destination, items, "USPS");
            var existingRateQuotes = GetExistingRateQuotesForHash(hash, cartID, contextProfileName);
            if (existingRateQuotes.Any())
            {
                return existingRateQuotes
                    .WrapInPassingCEFAR("NOTE! These rate quotes were previously provided and stored.");
            }
            // Generate a USPS Rate Request
            var rateManager = new RateManager();
            // Run the Request
            var packages = new List<Package>();
            packages.AddRange(items
                .Select(i => new Package(
                    ConvertToInches(i.Depth ?? 0, i.DepthUnitOfMeasure),
                    ConvertToInches(i.Width ?? 0, i.WidthUnitOfMeasure),
                    ConvertToInches(i.Height ?? 0, i.HeightUnitOfMeasure),
                    // ReSharper disable once StyleCop.SA1118
                    ConvertToPounds(
                        USPSShippingProviderConfig.UseDimensionalWeight ? i.DimensionalWeight : i.Weight,
                        USPSShippingProviderConfig.UseDimensionalWeight ? i.DimensionalWeightUnitOfMeasure : i.WeightUnitOfMeasure),
                    0)));
            var originAddress = new Address(
                /*CEFConfigDictionary.ShippingOriginAddressStreet1,
                CEFConfigDictionary.ShippingOriginAddressStreet2,
                CEFConfigDictionary.ShippingOriginAddressStreet3,*/
                CEFConfigDictionary.ShippingOriginAddressCity,
                CEFConfigDictionary.ShippingOriginAddressRegionCode,
                CEFConfigDictionary.ShippingOriginAddressPostalCode,
                CEFConfigDictionary.ShippingOriginAddressCountryCode);
            var destinationAddress = new Address(
                /*destination.Street1, destination.Street2, destination.Street3,*/
                destination.Address?.City,
                destination.Address?.Region?.Name ?? destination.Address?.RegionName,
                destination.Address?.PostalCode,
                destination.Address?.Country?.Code ?? destination.Address?.CountryCode);
            originAddress.CountryCode = origin.Address?.CountryCode == "USA"
                ? origin.Address.CountryCode = "US"
                : origin.Address?.CountryCode;
            destinationAddress.CountryCode = destination.Address?.CountryCode == "USA"
                ? destination.Address.CountryCode = "US"
                : destination.Address?.CountryCode;
            if (HasValidConfiguration)
            {
                var domesticProvider = new MyUSPSDomesticProvider(USPSShippingProviderConfig.UserName!) { Logger2 = Logger };
                rateManager.AddProvider(domesticProvider);
                var internationalProvider = new USPSInternationalProvider(USPSShippingProviderConfig.UserName);
                rateManager.AddProvider(internationalProvider);
            }
            Shipment response;
            try
            {
#if NET5_0_OR_GREATER
                response = await rateManager.GetRates(originAddress, destinationAddress, packages);
#else
                response = rateManager.GetRates(originAddress, destinationAddress, packages);
#endif
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("USPSShippingProvider: GetRates", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                return new List<IRateQuoteModel>().WrapInFailingCEFAR($"USPS Error: {ex.Message}")!;
            }
            // Check for Errors
            // TODO: Check USPS Response for Errors
            // Parse the response
            var rates = new List<IShipmentRate>();
            foreach (var rate in response.Rates)
            {
                rates.Add(new ShipmentRate
                {
                    Rate = rate.TotalCharges,
                    CarrierName = "U.S.P.S.",
                    OptionCode = rate.ProviderCode
                        .Replace("USPS", "U.S.P.S.")
                        .Replace("U.S.P.S. U.S.P.S.", "U.S.P.S."),
                    OptionName = rate.Name
                        .Replace("USPS", "U.S.P.S.")
                        .Replace("U.S.P.S. U.S.P.S.", "U.S.P.S."),
                    FullOptionName = $"U.S.P.S. {rate.Name} {rate.TotalCharges:c}"
                        .Replace("U.S.P.S. U.S.P.S.", "U.S.P.S."),
                });
            }
            // Check for Minimum Rate Amount requirements and apply if set
            var defaultMinimumPrice = USPSShippingProviderConfig.DefaultMinimumPrice;
            if (USPSShippingProviderConfig.UseDefaultMinimumPricing && defaultMinimumPrice != 0)
            {
                foreach (var rate in rates.Where(x => x.Rate < defaultMinimumPrice))
                {
                    rate.Rate = defaultMinimumPrice;
                }
            }
            // Save the rates to the table to "cache" them with the hash we calculated above
            var results = await SaveRateQuotesToTableAndReturnResultsAsync(
                    cartID,
                    hash,
                    "USPS",
                    rates,
                    contextProfileName)
                .ConfigureAwait(false);
            return results.WrapInPassingCEFAR()!;
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

        /// <summary>my usps domestic provider.</summary>
        /// <seealso cref="AbstractShippingProvider"/>
        private class MyUSPSDomesticProvider : AbstractShippingProvider
        {
            private const string ProductionURL = "http://production.shippingapis.com/ShippingAPI.dll";
            private readonly string service;
            private readonly string userId;

            /// <summary>Initializes a new instance of the <see cref="MyUSPSDomesticProvider"/> class.</summary>
            /// <param name="userId">Identifier for the user.</param>
            public MyUSPSDomesticProvider(string userId)
            {
                Name = "U.S.P.S.";
                this.userId = userId;
                service = "ALL";
            }

            /// <summary>Sets the logger.</summary>
            /// <value>The logger.</value>
            public ILogger? Logger2 { private get; set; }

            /// <inheritdoc/>
#if NET5_0_OR_GREATER
            public override Task GetRates()
            {
                return GetRates(false, null);
            }
#else
            public override void GetRates()
            {
                GetRates(false, null);
            }
#endif

            /// <summary>Query if 'package' is package large.</summary>
            /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
            /// <param name="package">The package.</param>
            /// <returns>true if package large, false if not.</returns>
            private static bool IsPackageLarge(Package package)
            {
                // Contract.RequiresNotNull(value);
                if (package == null)
                {
                    throw new ArgumentNullException(nameof(package));
                }
                return package.IsOversize || package.Width > 12 || package.Length > 12 || package.Height > 12;
            }

            /// <summary>Query if 'package' is package machinable.</summary>
            /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
            /// <param name="package">The package.</param>
            /// <returns>true if package machinable, false if not.</returns>
            private static bool IsPackageMachinable(Package package)
            {
                // Contract.RequiresNotNull(value);
                if (package == null)
                {
                    throw new ArgumentNullException(nameof(package));
                }
                // Machinable parcels cannot be larger than 27 x 17 x 17 and cannot weight more than 25 lbs.
                if (package.Weight > 25)
                {
                    return false;
                }
                return package.Width <= 27 && package.Height <= 17 && package.Length <= 17
                    || package.Width <= 17 && package.Height <= 27 && package.Length <= 17
                    || package.Width <= 17 && package.Height <= 17 && package.Length <= 27;
            }

            /// <summary>Gets the rates.</summary>
            /// <param name="baseRatesOnly">     true to base rates only.</param>
            /// <param name="contextProfileName">Name of the context profile.</param>
#if NET5_0_OR_GREATER
            private async Task GetRates(bool baseRatesOnly, string? contextProfileName)
#else
            private void GetRates(bool baseRatesOnly, string? contextProfileName)
#endif
            {
                // USPS only available for domestic addresses. International is a different API.
                if (!IsDomesticUSPSAvailable())
                {
                    return;
                }
                var sb = new StringBuilder();
                var settings = new XmlWriterSettings
                {
                    Indent = false,
                    OmitXmlDeclaration = true,
                    NewLineHandling = NewLineHandling.None,
                };
                using (var writer = XmlWriter.Create(sb, settings))
                {
                    writer.WriteStartElement("RateV4Request");
                    writer.WriteAttributeString("USERID", userId);
                    if (!baseRatesOnly)
                    {
                        writer.WriteElementString("Revision", "2");
                    }
                    var i = 0;
                    foreach (var package in Shipment.Packages)
                    {
                        string size;
                        var container = package.Container;
                        if (IsPackageLarge(package))
                        {
                            size = "LARGE";
                            // Container must be RECTANGULAR or NONRECTANGULAR when SIZE is LARGE
                            if (container == null || container.ToUpperInvariant() != "NONRECTANGULAR")
                            {
                                container = "RECTANGULAR";
                            }
                        }
                        else
                        {
                            size = "REGULAR";
                            container ??= string.Empty;
                        }
                        writer.WriteStartElement("Package");
                        writer.WriteAttributeString("ID", i.ToString());
                        writer.WriteElementString("Service", service);
                        writer.WriteElementString("ZipOrigination", Shipment.OriginAddress.PostalCode);
                        writer.WriteElementString("ZipDestination", Shipment.DestinationAddress.PostalCode);
                        writer.WriteElementString("Pounds", package.PoundsAndOunces.Pounds.ToString());
                        writer.WriteElementString("Ounces", package.PoundsAndOunces.Ounces.ToString());
                        writer.WriteElementString("Container", container);
                        writer.WriteElementString("Size", size);
                        writer.WriteElementString("Width", package.RoundedWidth.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Length", package.RoundedLength.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Height", package.RoundedHeight.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Girth", package.CalculatedGirth.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Machinable", IsPackageMachinable(package).ToString());
                        writer.WriteEndElement();
                        i++;
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
                try
                {
                    var url = string.Concat(ProductionURL, "?API=RateV4&XML=", sb.ToString());
                    var webClient = new WebClient();
#if NET5_0_OR_GREATER
                    var response = await webClient.DownloadStringTaskAsync(url);
#else
                    var response = webClient.DownloadString(url);
#endif
                    ParseResult(response);
                }
                catch (Exception ex)
                {
                    Logger2!.LogError("Get USPS Shipping Rates", ex.Message, ex, contextProfileName);
                }
            }

            /// <summary>Queries if a domestic usps is available.</summary>
            /// <returns>true if a domestic usps is available, false if not.</returns>
            private bool IsDomesticUSPSAvailable()
            {
                return Shipment.OriginAddress.IsUnitedStatesAddress() && Shipment.DestinationAddress.IsUnitedStatesAddress();
            }

            /// <summary>Parse result.</summary>
            /// <param name="response">The response.</param>
            private void ParseResult(string response)
            {
                var document = XElement.Parse(response, LoadOptions.None);
                var rates = document
                    .Descendants("Postage")
                    .GroupBy(item => (string)item.Element("MailService")!)
                    .Select(g => new
                    {
                        Name = g.Key,
                        TotalCharges = g.Sum(x => decimal.Parse((string)x.Element("Rate")!)),
                        DeliveryDate = g.Select(x => (string)x.Element("CommitmentDate")!).FirstOrDefault(),
                    });
                foreach (var r in rates)
                {
                    var name = Regex.Replace(r.Name, "&lt.*&gt;", string.Empty);
                    AddRate(
                        name,
                        string.Concat("USPS ", name),
                        r.TotalCharges,
                        r.DeliveryDate != null ? DateTime.Parse(r.DeliveryDate) : DateExtensions.GenDateTime.AddDays(30));
                }
                // check for errors
                if (!document.Descendants("Error").Any())
                {
                    return;
                }
                var errors = document
                    .Descendants("Error")
                    .Select(item => new USPSError
                    {
                        Description = item.Element("Description")?.ToString(),
                        Source = item.Element("Source")?.ToString(),
                        HelpContext = item.Element("HelpContext")?.ToString(),
                        HelpFile = item.Element("HelpFile")?.ToString(),
                        Number = item.Element("Number")?.ToString(),
                    });
                foreach (var err in errors)
                {
                    AddError(err);
                }
            }
        }
    }
}
