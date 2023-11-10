// <copyright file="PayeezyPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy payments provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.Payeezy;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.Payeezy")]
    public class PayeezyPaymentsProviderTests : BasePaymentTest
    {
        protected PayeezyPaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Working")]
        public async Task Verify_AuthorizeAndCapture_Works()
        {
            var gateway = new PayeezyPaymentsProvider();
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            Payment.CVV = "123";
            Payment.Amount = 78m;
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            ////var ret2 = gateway.Refund(ret);
            // Assert
            Assert.True(ret.Approved);
            Assert.Equal(42, ret.Amount);
            Assert.NotNull(ret.TransactionID);
            ////Assert.True(ret2.Approved);
            ////Assert.Equal(42, ret2.Amount);
            ////Assert.NotNull(ret2.TransactionID);
        }

        [Fact(Skip = "Working")]
        public async Task Verify_TokenCreation_Works()
        {
            var gateway = new PayeezyPaymentsProvider();
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            // Act
            var ret = await gateway.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            //var ret2 = gateway.Refund(ret);
            // Assert
            Assert.True(ret.Approved);
            Assert.True(!string.IsNullOrEmpty(ret.Token));
        }
    }
}
