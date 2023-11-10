// <copyright file="PayTracePaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace payments provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Testing
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Xunit;

    [Trait("Category", "Providers.Payments.PayTrace")]
    public class PayTracePaymentsProviderTests
    {
        private PaymentModel Payment { get; } = new PaymentModel
        {
            CardNumber = "4012000098765439",
            Amount = 42,
            CVV = "999",
            ExpirationMonth = 12,
            ExpirationYear = 20,
            TransactionNumber = long.MaxValue.ToString(),
        };

        private ContactModel BillingModel { get; } = new ContactModel
        {
            FirstName = "John",
            LastName = "McMiller",
            Address = new AddressModel
            {
                Company = "John McMiller",
                Street1 = "8320 Test Street",
                PostalCode = "85284",
                CountryCode = "USA",
                City = "TomorrowLand",
                RegionCode = "TX"
            },
            Email1 = "test@test.com"
        };

        private ContactModel ShippingModel { get; } = new ContactModel
        {
            Address = new AddressModel
            {
                Street1 = "8320 Test Street",
                PostalCode = "85284",
                CountryCode = "USA",
                City = "TomorrowLand",
                RegionCode = "TX"
            }
        };

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payments_AuthorizeAndCapture_Succeeds()
        {
            // Arrange
            var provider = new PayTracePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, false, contextProfileName: null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
            Assert.Equal(42, result.Amount);
            Assert.NotNull(result.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payments_Authorize_WithLater_Capture_Succeeds()
        {
            // Arrange
            var provider = new PayTracePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAsync(Payment, BillingModel, ShippingModel, false, contextProfileName: null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved, result.ResponseCode);
            Assert.Equal(42, result.Amount);
            Assert.NotNull(result.TransactionID);
            Assert.NotNull(result.AuthorizationCode);
            // Act
            var result2 = await provider.CaptureAsync(result.TransactionID!, Payment, null).ConfigureAwait(false);
            // Assert
            Assert.True(result2.Approved, result2.AuthorizationCode);
            Assert.Equal(42, result2.Amount);
            Assert.NotNull(result2.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payments_ExportTransactions_Succeeds()
        {
            // Arrange
            const string trasactionID = "1000";
            var provider = new PayTracePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var transactions = await provider.ExportTransactionsAsync(DateTime.Now, DateTime.Now, trasactionID).ConfigureAwait(false);
            // Assert
            Assert.True(transactions.Success);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payments_AuthorizeAndCaptureWithWalletToken_Succeeds()
        {
            // Arrange
            var provider = new PayTracePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, false, null, useWalletToken: true).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
            Assert.NotNull(result.TransactionID);
        }
    }
}
