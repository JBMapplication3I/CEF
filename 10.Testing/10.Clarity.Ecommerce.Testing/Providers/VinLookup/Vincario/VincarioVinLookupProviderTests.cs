// <copyright file="VincarioVinLookupProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vincario vin lookup provider tests class</summary>
namespace Clarity.Ecommerce.Providers.VinLookup.Vincario
{
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "Providers.VinLookup.Vincario")]
    public class VincarioVinLookupProviderTests
    {
        [Fact(Skip = "Validation only")]
        public async Task Verify_VinLookup_ValidateVin_ReturnsTrueWhenValidVinIsProvided()
        {
            var provider = RegistryLoaderWrapper.GetVinLookupProvider(null);
            Assert.NotNull(provider);
            Assert.True(provider!.HasValidConfiguration);
            var vinNumber = "1FTFW1R6XBFB08616";
            var response = await provider.ValidateVinAsync(vinNumber, null).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.True(response.ActionSucceeded);
            Assert.True(response.Result);
        }

        [Fact(Skip = "Validation only")]
        public async Task Verify_VinLookup_ValidateVin_ReturnsFalseWhenAnInvalidVinIsProvided()
        {
            var provider = RegistryLoaderWrapper.GetVinLookupProvider(null);
            Assert.NotNull(provider);
            Assert.True(provider!.HasValidConfiguration);
            var vinNumber = "1FTFW1R6";
            var response = await provider.ValidateVinAsync(vinNumber, null).ConfigureAwait(false);
            Assert.NotNull(response);
            Assert.False(response.ActionSucceeded);
            Assert.False(response.Result);
        }
    }
}
