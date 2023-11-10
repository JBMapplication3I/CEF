// <copyright file="UPSShippingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ups shipping provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Providers;
    using Models;
    using UPS;
    using Xunit;

    [Trait("Category", "Providers.Shipping.UPS")]
    public class UPSShippingProviderTests : ShippingProviderTestsBase
    {
        // private string AccessLicenseNumber = "2CF4B451E1EAF296";
        // private string Username = "ClarityTim";
        // private string Password = "Thedoor7";
        // private string ShipperNumber = "85953Y";
        private static bool CombinePackagesWhenGettingShippingRate { get; set; } = true;

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates_CombinePackages()
        {
            var packageTypes = Items.ToList().ToUPSPackageTypeArray(CombinePackagesWhenGettingShippingRate, false);
            Assert.NotNull(packageTypes);
            Assert.Single(packageTypes);
        }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates_DoNotCombinePackages()
        {
            CombinePackagesWhenGettingShippingRate = false;
            var packageTypes = Items.ToList().ToUPSPackageTypeArray(CombinePackagesWhenGettingShippingRate, false);
            Assert.NotNull(packageTypes);
            Assert.Equal(2, packageTypes.Length);
        }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates_DoesNotUseDimensionalWeights()
        {
            // Override Items
            Items = new List<IProviderShipment>
            {
                new ProviderShipment
                {
                    Height = 10m,
                    Depth = 10m,
                    Width = 10m,
                    Weight = 10m
                }
            };
            var packageType = Items.ToList().ToUPSPackageTypeArray(CombinePackagesWhenGettingShippingRate, false).FirstOrDefault();
            Assert.NotNull(packageType);
            Assert.Null(packageType.Dimensions);
        }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates_UsesDimensionalWeights()
        {
            // Override Items
            Items = new List<IProviderShipment>
            {
                new ProviderShipment
                {
                    Height = 10m,
                    Depth = 10m,
                    Width = 10m,
                    Weight = 10m
                }
            };
            var packageType = Items.ToList().ToUPSPackageTypeArray(CombinePackagesWhenGettingShippingRate, true).FirstOrDefault();
            Assert.NotNull(packageType);
            Assert.NotNull(packageType.Dimensions);
        }
    }
}
