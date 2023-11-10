// <copyright file="BridgePayPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bridge pay payments provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.BridgePay;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.BridgePay")]
    public class BridgePayPaymentsProviderTests : BasePaymentTest
    {
        protected BridgePayPaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public async Task TestBridgePay()
        {
            var provider = new BridgePayPaymentsProvider();
            var ret = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            Assert.True(ret.Approved);
        }
    }
}
