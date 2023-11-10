// <copyright file="BaseShippingTest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base shipping test class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Ecommerce.Testing;
    using Interfaces.Models;
    using Interfaces.Providers;
    using JSConfigs;
    using Models;
    using Xunit;

    public class ShippingProviderTestsBase : XUnitLogHelper
    {
        protected List<IProviderShipment> Items;
        protected IContactModel Destination;
        protected IContactModel Origin;
        protected IContactModel BadDestination;
        protected IContactModel BadOrigin;

        protected static IContactModel ShipmentOrigin => new ContactModel
        {
            Address = new AddressModel
            {
                Company = CEFConfigDictionary.CompanyName,
                Street1 = CEFConfigDictionary.ShippingOriginAddressStreet1,
                Street2 = CEFConfigDictionary.ShippingOriginAddressStreet2,
                Street3 = CEFConfigDictionary.ShippingOriginAddressStreet3,
                City = CEFConfigDictionary.ShippingOriginAddressCity,
                PostalCode = CEFConfigDictionary.ShippingOriginAddressPostalCode,
                RegionCode = CEFConfigDictionary.ShippingOriginAddressRegionCode,
                CountryCode = CEFConfigDictionary.ShippingOriginAddressCountryCode
            },
        };

        protected ShippingProviderTestsBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls;   // Disable 1.0
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
            Items = new List<IProviderShipment>
            {
                new ProviderShipment
                {
                    Weight = 50m,
                    ItemCode = "Test1",
                },
                new ProviderShipment
                {
                    Weight = 60m,
                    ItemCode = "Test2",
                },
                new ProviderShipment { Weight = 500m, Width = 48m, Height = 48m, Depth = 48m },
                new ProviderShipment { Weight = 12m },
                new ProviderShipment { Weight = 12m },
                new ProviderShipment { Weight = 12m, Width = 5m, Depth = 0.3m },
                new ProviderShipment { Weight = 12m, Width = 5m, Depth = 0.3m },
                new ProviderShipment { Weight = 12m, Width = 5m, Depth = 0.3m },
                new ProviderShipment { Weight = 12m, Width = 5m, Depth = 0.3m },
                new ProviderShipment { Weight = 12m, Width = 5m, Depth = 0.3m },
            };
            Origin = new ContactModel
            {
                Active = true,
                Address = new AddressModel
                {
                    Active = true,
                    Company = "Clarity Ventures",
                    Street1 = "6805 N Capital of Texas Hwy",
                    Street2 = "Suite 312",
                    City = "Austin",
                    RegionCode = "TX",
                    PostalCode = "78731",
                    CountryCode = "USA"
                },
            };
            Destination = new ContactModel
            {
                Active = true,
                Address = new AddressModel
                {
                    Active = true,
                    Company = "Joe Blow",
                    Street1 = "11160 Jollyville Rd",
                    City = "Austin",
                    RegionCode = "TX",
                    PostalCode = "78759",
                    CountryCode = "USA"
                },
            };
            BadOrigin = new ContactModel
            {
                Active = true,
                Address = new AddressModel
                {
                    Active = true,
                    Company = "GFS",
                    Street1 = "9442 N Capital of Texas Hwy",
                    City = "Austin",
                    RegionCode = "TX",
                    PostalCode = "78759",
                    CountryCode = "USA"
                },
            };
            BadDestination = new ContactModel
            {
                Active = true,
                Address = new AddressModel
                {
                    Active = true,
                    Company = "Test User",
                    Street1 = "328 E 75th St",
                    Street2 = "Apt 37",
                    City = "Austin",
                    RegionCode = "Texas",
                    PostalCode = "78759",
                    CountryCode = "USA"
                },
            };
        }

        protected static void Verify_CEFAR_Failed_WithSingleMessage(CEFActionResponse result, string expectMessage)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Single(result.Messages);
            Assert.Equal(expectMessage, result.Messages[0]);
        }

        protected static void Verify_CEFAR_Passed_WithNoMessages(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(
                result.ActionSucceeded,
                result.Messages?.DefaultIfEmpty("No Messages").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(result.Messages!);
        }
    }
}
