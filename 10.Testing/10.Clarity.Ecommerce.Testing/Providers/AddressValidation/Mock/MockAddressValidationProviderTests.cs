// <copyright file="MockAddressValidationProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mock address validation provider tests class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Mock.Testing
{
    using System.Configuration;
    using System.Threading.Tasks;
    using Models;
    using Providers.Testing;
    using Xunit;

    [Trait("Category", "Providers.AddressValidation.Mock")]
    public class MockAddressValidationProviderTests
    {
        private readonly AddressModel sourceAddress = new AddressModel
        {
            Street1 = "11160 Jollyville Rd",
            City = "Austin",
            RegionCode = "TX",
            PostalCode = "78759",
            CountryCode = "USA"
        };

        // This Test does not pass when run with all other tests. Probably due to the fact they run in parallel
        [Fact(Skip = "Test does not pass when run with all other tests. Run test individually to confirm.")]
        public async Task Verify_ValidateAddress_ReturnsValidResponse()
        {
            ProviderTestHelper.SetupProviderPluginPathsConfig("Address", "Mock");
            ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"] = "MockAddressProvider";
            var addressProvider = new MockAddressValidationProvider();
            var request = new ValidateAddress { Address = sourceAddress };
            var addressValidation = await addressProvider.ValidateAddressAsync(request, null).ConfigureAwait(false);
            Assert.Equal("MockAddressProvider", addressProvider.Name);
            Assert.NotNull(addressValidation);
            Assert.True(addressValidation.IsValid);
            Assert.NotNull(addressValidation.MergedAddress);
            Assert.Equal(sourceAddress.Street1, addressValidation.MergedAddress.Street1);
            Assert.Equal(sourceAddress.City, addressValidation.MergedAddress.City);
            Assert.Equal(sourceAddress.RegionCode, addressValidation.MergedAddress.RegionCode);
            Assert.Equal(sourceAddress.PostalCode, addressValidation.MergedAddress.PostalCode);
            Assert.Equal(sourceAddress.CountryCode, addressValidation.MergedAddress.CountryCode);
        }
    }
}
