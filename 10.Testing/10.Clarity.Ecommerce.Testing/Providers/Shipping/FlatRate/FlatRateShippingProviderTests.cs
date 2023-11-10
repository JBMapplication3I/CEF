// <copyright file="FlatRateShippingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat rate shipping provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.FlatRate.Testing
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Xunit;

    [Trait("Category", "Providers.Shipping.FlatRate")]
    public class FlatRateShippingProviderTests
    {
        [Fact(Skip = "Proofing Functionality")]
        public void GenerateJson()
        {
            var jsonThreshold = 8;
            var rates = new ShipmentRates
            {
                Domestic = new List<ShipmentRate>
                {
                    new ShipmentRate
                    {
                        CarrierName = "USPS",
                        FullOptionName = "USPS Standard",
                        OptionCode = "Standard",
                        OptionName = "USPS Standard",
                        Rate = 3.00m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "UPS",
                        FullOptionName = "UPS Standard",
                        OptionCode = "Standard",
                        OptionName = "UPS Standard",
                        Rate = 3.00m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "USPS",
                        FullOptionName = "USPS Signature Confirmation",
                        OptionCode = "Signature",
                        OptionName = "USPS Signature Confirmation",
                        Rate = 3.95m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "UPS",
                        FullOptionName = "UPS Signature Confirmation",
                        OptionCode = "Signature",
                        OptionName = "UPS Signature Confirmation",
                        Rate = 3.95m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "UPS",
                        FullOptionName = "UPS Tracking",
                        OptionCode = "Tracking",
                        OptionName = "UPS Tracking",
                        Rate = 11.95m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "USPS",
                        FullOptionName = "USPS Priority",
                        OptionCode = "Priority",
                        OptionName = "UPS Priority",
                        Rate = 10.00m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "UPS",
                        FullOptionName = "2-Day UPS",
                        OptionCode = "2-Day",
                        OptionName = "2-Day UPS",
                        Rate = 17.95m
                    },
                    new ShipmentRate
                    {
                        CarrierName = "UPS",
                        FullOptionName = "Next Day Air UPS",
                        OptionCode = "NextDay",
                        OptionName = "Next Day Air UPS",
                        Rate = 29.95m
                    }
                },
                Canada = new List<ShipmentRate>
                {
                    new ShipmentRate
                    {
                        CarrierName = "USPS",
                        FullOptionName = "USPS Shipping to Canada",
                        OptionCode = "Canada",
                        OptionName = "Shipping to Canada",
                        Rate = 7.00m
                    }
                },
                International = new List<ShipmentRate>
                {
                    new ShipmentRate
                    {
                        CarrierName = "USPS",
                        FullOptionName = "USPS International Priority",
                        OptionCode = "International",
                        OptionName = "USPS International Priority",
                        Rate = 7.00m
                    }
                }
            };
            var _ = JsonConvert.SerializeObject(jsonThreshold);
            var json = JsonConvert.SerializeObject(rates);
            var __ = JsonConvert.DeserializeObject<ShipmentRates>(json);
        }
    }
}
