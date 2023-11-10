// <copyright file="FedExShippingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fed ex shipping provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FedEx;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Workflow;
    using Mapper;
    using Models;
    using Xunit;

    [Trait("Category", "Providers.Shipping.FedEx")]
    public class FedExShippingProviderTests : ShippingProviderTestsBase
    {
        // private string AccountNumber = "510087984";
        // private string MeterNumber = "118681677";
        // private string Username = "a4NyFokkcUdkDJep";
        // private string Password = "pjqbKIUGlTwwExGqiLgmetdyw";
        private readonly bool CombinePackagesWhenGettingShippingRate = true;
        private bool useDimensionalWeight;

        public FedExShippingProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
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
                    Weight = 10m,
                },
            };
            var packageLineItem = Items
                .ToList()
                .ToFedExLineItemArray(CombinePackagesWhenGettingShippingRate, useDimensionalWeight, contextProfileName: null)!
                .FirstOrDefault();
            Assert.NotNull(packageLineItem);
            Assert.Null(packageLineItem!.Dimensions);
        }

        [Fact]
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
                },
            };
            useDimensionalWeight = true;
            var packageLineItem = Items
                .ToList()
                .ToFedExLineItemArray(CombinePackagesWhenGettingShippingRate, useDimensionalWeight, contextProfileName: null)!
                .FirstOrDefault();
            Assert.NotNull(packageLineItem);
            Assert.NotNull(packageLineItem!.Dimensions);
            Assert.Equal("10", packageLineItem.Dimensions.Height);
            Assert.Equal("10", packageLineItem.Dimensions.Length);
            Assert.Equal("10", packageLineItem.Dimensions.Width);
        }

        [Fact(Skip = "Only works with live data")]
        public async Task Verify_ShipTimesAsync()
        {
            // Arrange
            FedExShippingProviderConfig.ForceProviderOnForTesting = true;
            FedExShippingProviderConfig.ForceNoCacheForTesting = true;
            var provider = new FedExShippingProvider();
            Assert.True(provider.HasValidConfiguration);
            var workflows = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();
            BaseModelMapper.Initialize();
            // Act
            var response = await workflows.Carts.GetRateQuotesAsync(
                    lookupKey: new CartByIDLookupKey(29),
                    origin: ShipmentOrigin,
                    expedited: false,
                    contextProfileName: null)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(response);
            Assert.NotNull(response.Result);
            Assert.IsType<List<IRateQuoteModel>>(response.Result);
            Assert.Equal(7, response.Result!.Count);
            foreach (var result in response.Result)
            {
                TestOutputHelper.WriteLine(result.ToString());
            }
        }
    }
}
