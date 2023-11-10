// <copyright file="PayeezyAPIPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy a pi payments provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.PayeezyAPI;
    using Models;
    using Xunit;

    [Trait("Category", "Providers.Payments.PayeezyAPI")]
    public class PayeezyAPIPaymentsProviderTests
    {
        private PaymentModel Payment { get; } = new PaymentModel
        {
            CardHolderName = "John Smith",
            CardType = "VISA",
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
        public async Task Verify_AuthorizeAndCapture_Works()
        {
            var gateway = new PayeezyAPIPaymentsProvider();
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(gateway.HasValidConfiguration);
            Payment.CVV = "123";
            Payment.Amount = 78m;
            // Act
            var ret = await gateway.AuthorizeAndACaptureAsync(Payment, BillingModel, ShippingModel, null).ConfigureAwait(false);
            var ret2 = await gateway.RefundAsync(Payment, ret.TransactionID, ret.Amount, null).ConfigureAwait(false);
            // Assert
            Assert.True(ret.Approved);
            Assert.Equal(78, ret.Amount);
            Assert.NotNull(ret.TransactionID);
            Assert.True(ret2.Approved);
            Assert.Equal(78, ret2.Amount);
            Assert.NotNull(ret2.TransactionID);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_TokenCreation_Works()
        {
            var gateway = new PayeezyAPIPaymentsProvider();
            await gateway.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(gateway.HasValidConfiguration);
            // Act
            var ret = await gateway.CreateCustomerProfileAsync(Payment, BillingModel, null).ConfigureAwait(false);
            //var ret2 = gateway.Refund(ret);
            // Assert
            Assert.True(ret.Approved, ret.Token);
            Assert.False(string.IsNullOrEmpty(ret.Token));
        }
    }
}
