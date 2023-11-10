// <copyright file="UnitedTranzactionPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the united tranzaction payments provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Net;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.UnitedTranzaction;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.UnitedTranz")]
    public class UnitedTranzactionPaymentsProviderTests : BasePaymentTest
    {
        protected UnitedTranzactionPaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private static async Task<UnitedTranzactionPaymentsProvider> GenProviderAsync()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12;
            var provider = new UnitedTranzactionPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            return provider;
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCapture()
        {
            var provider = await GenProviderAsync();
            var ret = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            Assert.NotNull(ret);
            Assert.True(ret.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AddCustomerProfile()
        {
            var provider = await GenProviderAsync();
            Payment.CardType = "VISA";
            var ret = await provider.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            Assert.NotNull(ret);
            Assert.True(ret.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_PayWithToken()
        {
            var provider = await GenProviderAsync();
            Payment.CardType = "VISA";
            var ret = await provider.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            Assert.NotNull(ret);
            Assert.True(ret.Approved);
            Payment.Token = ret.Token;
            var ret2 = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            Assert.NotNull(ret2);
            Assert.True(ret2.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Authorize_Then_Capture()
        {
            var provider = await GenProviderAsync();
            var ret = await provider.AuthorizeAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            Assert.NotNull(ret);
            Assert.True(ret.Approved);
            var ret2 = await provider.CaptureAsync(ret.TransactionID, Payment, null).ConfigureAwait(false);
            Assert.NotNull(ret2);
            Assert.True(ret2.Approved);
        }
    }
}
