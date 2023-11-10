// <copyright file="PayPalPayflowProPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay palette pay flow pro payments provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPal.PayFlowPro.Testing
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using PayPalPayflowPro;
    using Xunit;

    [Trait("Category", "Providers.Payments.PayPalPayFlowPro")]
    public class PayPalPayflowProPaymentsProviderTests
    {
        private PaymentModel Payment { get; } = new PaymentModel
        {
            CardNumber = "4111111111111111",
            Amount = 42,
            CVV = "234",
            ExpirationMonth = 12,
            ExpirationYear = 19,
            TransactionNumber = Guid.Empty.ToString(),
        };

        private ContactModel BillingModel { get; } = new ContactModel
        {
            FirstName = "John",
            LastName = "McMiller",
            Address = new AddressModel
            {
                Company = "John McMiller",
                Street1 = "Away from humanity",
                PostalCode = "78787",
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
                Street1 = "Away from humanity",
                PostalCode = "78787",
                CountryCode = "USA",
                City = "TomorrowLand",
                RegionCode = "TX"
            }
        };

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payments_AuthorizeAndCapture_Succeeds()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
            Assert.Equal(42, result.Amount);
            Assert.NotNull(result.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payments_Authorize_WithLater_Capture_Succeeds()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved, result.ResponseCode);
            Assert.Equal(42, result.Amount);
            Assert.NotNull(result.TransactionID);
            Assert.NotNull(result.AuthorizationCode);
            // Act
            var result2 = await provider.CaptureAsync(result.TransactionID, Payment, null).ConfigureAwait(false);
            // Assert
            Assert.True(result2.Approved, result2.AuthorizationCode);
            Assert.Equal(42, result2.Amount);
            Assert.NotNull(result2.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Refunds_GeneralRefund_Succeeds()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            var payment = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Act
            var result = await provider.RefundAsync(Payment, payment.TransactionID, 42m, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved, result.AuthorizationCode);
            Assert.Equal(42, result.Amount);
            Assert.NotNull(result.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Wallet_CreateCustomerProfile_Succeeds()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
            Assert.NotNull(result.Token);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Wallet_UpdateCustomerProfile_Succeeds()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.UpdateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
            Assert.NotNull(result.Token);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Wallet_GetCustomerProfile_Throws_NotImplementedException()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act/Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => provider.GetCustomerProfileAsync(Payment, null)).ConfigureAwait(false);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Wallet_DeleteCustomerProfile_Throws_NotImplementedException()
        {
            // Arrange
            var provider = new PayPalPayflowProPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act/Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => provider.DeleteCustomerProfileAsync(Payment, null)).ConfigureAwait(false);
        }
    }
}
