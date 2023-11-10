// <copyright file="StripePaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stripe payments provider tests class</summary>
// Stripe Documentation for testing can be found at https://stripe.com/docs/testing#cards
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.Stripe;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.Stripe")]
    public class StripePaymentsProviderTests : BasePaymentTest
    {
        protected StripePaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCapture_Works()
        {
            // Arrange
            var gateway = new StripePaymentsProvider();
            Payment.CardNumber = "4242424242424242"; // Stripe doesn't use normal Test visa number
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(ret.Approved);
            Assert.Equal(42, ret.Amount);
            Assert.NotNull(ret.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCapture_Fails_Generic()
        {
            // Arrange
            var gateway = new StripePaymentsProvider();
            var generic = "4000000000000002"; // Generic Decline
            Payment.CardNumber = generic;
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(ret.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCapture_Fails_BadNumber()
        {
            // Arrange
            var gateway = new StripePaymentsProvider();
            var incorrect = "4242424242424241"; // Bad number Decline
            Payment.CardNumber = incorrect;
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(ret.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCapture_Fails_Cvc()
        {
            // Arrange
            var gateway = new StripePaymentsProvider();
            // Failure card numbers from stripe docs
            var badCvc = "4000000000000127"; // incorrect cvc Decline
            Payment.CardNumber = badCvc;
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(ret.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCapture_Fails_Expired()
        {
            // Arrange
            var gateway = new StripePaymentsProvider();
            // Failure card numbers from stripe docs
            var expired = "4000000000000069"; // expired Decline
            Payment.CardNumber = expired;
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(ret.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_CreateToken_Works()
        {
            // Arrange
            var gateway = new StripePaymentsProvider();
            // Act
            var ret = await gateway.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            // Assert
            Assert.NotNull(ret);
            Assert.True(ret.Approved);
            Assert.False(string.IsNullOrEmpty(ret.Token));
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_CreateToken_AndPayment_Work()
        {
            var gateway = new StripePaymentsProvider();
            var ret = await gateway.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            Assert.NotNull(ret);
            Assert.True(ret.Approved);
            Assert.True(!string.IsNullOrEmpty(ret.Token));
            Payment.Token = ret.Token;
            var retPayment = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            Assert.True(retPayment.Approved);
            Assert.Equal(42, retPayment.Amount);
            Assert.NotNull(retPayment.TransactionID);
        }
    }
}
