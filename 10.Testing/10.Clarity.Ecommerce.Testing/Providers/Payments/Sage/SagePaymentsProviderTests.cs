// <copyright file="SagePaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage payments provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Sage.Testing
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Models;
    using Utilities;
    using Xunit;

    [Trait("Category", "Providers.Payments.Sage")]
    public class SagePaymentsProviderTests
    {
        private PaymentModel Payment { get; } = new PaymentModel
        {
            CardNumber = "4111111111111111",
            Amount = 42,
            CVV = "234",
            ExpirationMonth = 12,
            ExpirationYear = DateTime.Today.Year - 2000
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
        public async Task Verify_Payment_WithCC_WithValidData_Works()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payment_WithCC_WithInvalidData_Fails()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            Payment.CardNumber = "23423423422222";
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(result.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payment_WithEcheck_WithValidData_Works()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            Payment.CardNumber = string.Empty;
            Payment.AccountNumber = "12345678901234";
            Payment.RoutingNumber = "056008849";
            BillingModel.FirstName = "John";
            BillingModel.LastName = "Doe";
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payment_WithEcheck_WithInvalidData_Fails()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            Payment.CardNumber = string.Empty;
            Payment.AccountNumber = "00000000001234";
            Payment.RoutingNumber = "0099999";
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(result.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_Payment_WithCCToken_WithValidData_Works()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            Payment.Token = "7efd3fc58081475ca3d3edae955ba3dd";
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AddCreditCardToWallet_WithInvalidData_Fails()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            Payment.CardNumber = "23423423422222";
            // Act
            var result = await provider.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            // Assert
            Assert.False(result.Approved);
            Assert.Equal("400000", result.ResponseCode);
            Assert.Equal("Server Error", result.Token);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_AddCreditCardToWallet_WithValidData_Works_ThenRemoveFromWallet_Works()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result1 = await provider.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            // Assert
            Assert.True(result1.Approved);
            Contract.RequiresValidKey(result1.Token);
            // Re-Arrange
            Payment.Token = result1.Token;
            // Act
            var result2 = await provider.DeleteCustomerProfileAsync(Payment, null).ConfigureAwait(false);
            // Assert
            Assert.True(result2.Approved);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_RemoveCreditCardFromWallet_WithInvalidData_Fails()
        {
            // Arrange
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var provider = new SagePaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            Payment.Token = "Invalid";
            // Act
            var result = await provider.DeleteCustomerProfileAsync(Payment, null).ConfigureAwait(false);
            // Assert
            Assert.False(result.Approved);
            Assert.Equal("100006", result.ResponseCode);
            Assert.Equal("Server Error", result.Token);
        }
    }
}
