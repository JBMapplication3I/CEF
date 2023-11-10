// <copyright file="YRCShippingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRC shipping provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Testing
{
    using Ecommerce.Models;
    using Shipping.Testing;
    using Xunit;

    [Trait("Category", "Providers.Shipping.YRC")]
    public class YRCShippingProviderTests : ShippingProviderTestsBase
    {
        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates_BadOrigin()
        {
            var request = YRCResponseGenerator.CreateYRCRequest(
                Items,
                new ContactModel { Address = (AddressModel)BadOrigin },
                new ContactModel { Address = (AddressModel)Destination });
            var response = YRCResponseGenerator.GetYRCResponse(request);
            Assert.False(response.IsSuccess);
        }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates_BadDestination()
        {
            var request = YRCResponseGenerator.CreateYRCRequest(
                Items,
                new ContactModel { Address = (AddressModel)Origin },
                new ContactModel { Address = (AddressModel)BadDestination });
            var response = YRCResponseGenerator.GetYRCResponse(request);
            Assert.False(response.IsSuccess);
        }

        [Fact(Skip = "Don't run automatically")]
        public void Verify_GetRates()
        {
            var request = YRCResponseGenerator.CreateYRCRequest(
                Items,
                new ContactModel { Address = (AddressModel)Origin },
                new ContactModel { Address = (AddressModel)Destination });
            var response = YRCResponseGenerator.GetYRCResponse(request);
            Assert.True(response.IsSuccess);
        }
    }
}
