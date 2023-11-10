// <copyright file="AuthorizePaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authorize net test class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Net;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.Authorize;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.Authorize")]
    public class AuthorizePaymentsProviderTests : BasePaymentTest
    {
        protected AuthorizePaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Test Transaction data is invalid")]
        public async Task Verify_AuthorizeAndCapture_Works()
        {
            var gateway = new AuthorizePaymentsProvider();
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(this.Payment, this.BillingModel, this.ShippingModel, null).ConfigureAwait(false);
            await gateway.RefundAsync(Payment, ret.TransactionID, ret.Amount, null).ConfigureAwait(false);
            // Assert
            Assert.True(ret.Approved);
            Assert.Equal(42, ret.Amount);
            Assert.NotNull(ret.TransactionID);
        }

        [Fact(Skip = "Do not run")]
        public async Task Verify_Authorize_And_Capture_Later_Works()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var gateway = new AuthorizePaymentsProvider();
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            // Act
            var ret = await gateway.AuthorizeAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            Payment.InvoiceNumber = "424242";
            ret = await gateway.CaptureAsync(ret.TransactionID, Payment, null).ConfigureAwait(false);
            // Assert
            Assert.True(ret.Approved);
            Assert.Equal(42, ret.Amount);
            Assert.NotNull(ret.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AuthorizeAndCaptureECheck_Works()
        {
            var gateway = new AuthorizePaymentsProvider();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            Payment.AccountNumber = "1234567890";
            Payment.RoutingNumber = "121042882";
            Payment.BankName = "Wells Fargo Bank NA";
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(ret.Approved);
            Assert.Equal(42, ret.Amount);
            Assert.NotNull(ret.TransactionID);
        }

        ////[Theory]
        ////[InlineData("1810722746", "1805417984")]
        ////public async Task Verify_PaymentWithToken_Returns_True(string token, string paymentID)
        ////{
        ////    var gateway = new AuthorizePaymentsProvider();
        ////    gateway.InitConfiguration();
        ////    Payment.Token = $"{token}/{paymentID}";
        ////    // Act
        ////    var ret = gateway.AuthorizeAndACapture(Payment, BillingModel, ShippingModel);
        ////    // Assert
        ////    Assert.True(ret.Approved);
        ////    Assert.Equal(42, ret.Amount);
        ////    Assert.NotNull(ret.TransactionID);
        ////}

        ////[Fact]
        ////public async Task Verify_CreateCustomerProfile_Returns_Token()
        ////{
        ////    ServicePointManager.Expect100Continue = true;
        ////    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ////    var gateway = new AuthorizePaymentsProvider();
        ////    gateway.InitConfiguration();
        ////    // Act
        ////    var Payment1 = new PaymentModel
        ////    {
        ////        CardNumber = "370000000000002",
        ////        Amount = 42,
        ////        CVV = "234",
        ////        ExpirationMonth = 12,
        ////        ExpirationYear = 20
        ////    };
        ////    var ret = gateway.CreateCustomerProfile(Payment1, BillingModel);
        ////    // Assert
        ////    Assert.NotNull(ret);
        ////    Assert.Equal(true, ret.Approved);
        ////    Assert.NotEmpty(ret.Token);
        ////}

        ////[Theory]
        ////[InlineData("1810708396")]
        ////public async Task Verify_DeleteCustomerProfile_Returns_True(string token)
        ////{
        ////    ServicePointManager.Expect100Continue = true;
        ////    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ////    var gateway = new AuthorizePaymentsProvider();
        ////    gateway.InitConfiguration();
        ////    Payment.Token = "1502633225/1502169219";
        ////    // Act
        ////    var ret = gateway.DeleteCustomerProfile(Payment);
        ////    // Assert
        ////    Assert.NotNull(ret);
        ////}

        ////[Fact]
        ////public async Task Verify_DeleteExistingCustomerProfiles()
        ////{
        ////    ServicePointManager.Expect100Continue = true;
        ////    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ////    var gateway = new AuthorizePaymentsProvider();
        ////    gateway.InitConfiguration();
        ////    // Act
        ////    gateway.ListCustomerProfiles();
        ////    gateway.DeleteCustomerProfiles("1522460608");
        ////    gateway.DeleteCustomerProfiles("1522445948");
        ////    gateway.DeleteCustomerProfiles("1522448343");
        ////    // Assert
        ////    Assert.NotNull(ret);
        ////}
    }
}
