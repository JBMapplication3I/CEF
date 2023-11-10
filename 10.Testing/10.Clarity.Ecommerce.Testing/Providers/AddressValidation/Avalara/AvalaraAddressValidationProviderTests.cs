// <copyright file="AvalaraAddressValidationProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Avalara address validation provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Ecommerce.Providers.AddressValidation;
    using Ecommerce.Providers.AddressValidation.Avalara;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using StructureMap;
    using Xunit;

    [Trait("Category", "Providers.AddressValidation.Avalara")]
    public class AvalaraAddressValidationProviderTests
    {
        private IAddressModel SourceAddress { get; } = new AddressModel
        {
            Street1 = "11160 Jollyville Rd",
            City = "Austin",
            RegionCode = "TX",
            PostalCode = "78759",
            CountryCode = "USA"
        };

        [Fact(Skip = "Don't Run Automatically")]
        public async Task Verify_ValidateAddress_ReturnsValidResponse()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new AvalaraAddressValidationProvider();
                var request = new ValidateAddress { Address = (AddressModel)SourceAddress };
                // Act
                var validation = await provider.ValidateAddressAsync(request, contextProfileName).ConfigureAwait(false);
                Assert.NotNull(validation);
                var validatedAddress = validation.ValidatedAddress;
                // Assert
                Assert.Equal(nameof(AvalaraAddressValidationProvider), provider.Name);
                Assert.True(validation.IsValid, validation.Message);
                Assert.NotNull(validation.MergedAddress);
                Assert.NotNull(validatedAddress);
                Assert.Equal("11160 Jollyville Rd", validatedAddress.Street1);
                Assert.Equal("Austin", validatedAddress.City);
                Assert.Equal("TX", validatedAddress.RegionCode);
                Assert.Equal("78759-5943", validatedAddress.PostalCode);
                Assert.Equal("USA", validatedAddress.CountryCode);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't Run Automatically")]
        public async Task Verify_ValidateAddress_ReturnsValidResponseOnBadAddress()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var badAddress = new AddressModel
                {
                    Street1 = "NO STREET",
                    PostalCode = "1234",
                    CountryCode = "US"
                };
                var provider = new AvalaraAddressValidationProvider();
                var request = new ValidateAddress { Address = badAddress };
                // Act
                var validation = await provider.ValidateAddressAsync(request, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Equal(nameof(AvalaraAddressValidationProvider), provider.Name);
                Assert.NotNull(validation);
                Assert.False(validation.IsValid);
                Assert.False(string.IsNullOrWhiteSpace(validation.Message));
                Assert.Null(validation.MergedAddress);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        private static async Task<string> SetupContainerAsync(
            MockingSetup mockingSetup,
            IContainer childContainer,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string memberName = "")
        {
            var contextProfileName = $"{sourceFilePath}|{memberName}";
            await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.For<ICartValidatorConfig>().Use(config);
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
            AvalaraAddressValidationProviderConfig.ReadFromDb = false;
            ProviderTestHelper.SetupProviderConfig("Address", "Avalara");
            AvalaraAddressValidationService.Initialize(
                "1100110323",
                "B2DABBE49A8BDA7E",
                "https://development.avalara.net/");
            AvalaraAddressValidationProviderConfig.AddressServiceEnabled = true;
            AvalaraAddressValidationProviderConfig.AddressServiceCountries = "USA CAN";
            return contextProfileName;
        }
    }
}
