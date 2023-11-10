// <copyright file="HeartlandPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the heartland payments provider tests class</summary>
// ReSharper disable BadCommaSpaces, BadParensSpaces, MultipleSpaces
/*
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.Heartland;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.Heartland")]
    public class HeartlandPaymentsProviderTests : BasePaymentTest
    {
        public HeartlandPaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_CertificationExamples()
        {
            // https://developer.heartlandpaymentsystems.com/Documentation/test-cards/
            // Arrange
            var provider = new HeartlandPaymentsProvider();
            await provider.InitConfigurationAsync(null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            var tokens = new string[4];
            var setups = new List<(string type, string number, string expMonth, string expYear, string cvv, string street1, string zip, decimal amount)>
            {
                // Generate Token
                ("Visa",       "4012002000060016", "12", "2025", "123" , "6860 Dallas Pkwy", "75024"    , 13.01m),
                ("MasterCard", "5473500000000014", "12", "2025", "123" , "6860 Dallas Pkwy", "75024"    , 13.02m),
                ("Discover",   "6011000990156527", "12", "2025", "123" , "6860"            , "750241234", 13.03m),
                ("Amex",       "372700699251018",  "12", "2025", "1234", "6860"            , "75024"    , 13.04m),
                // Use Token to Charge
                ("Visa",       "4012002000060016", "12", "2025", "123" , "6860 Dallas Pkwy", "75024"    , 17.01m),
                ("MasterCard", "5473500000000014", "12", "2025", "123" , "6860 Dallas Pkwy", "75024"    , 17.02m),
                ("Discover",   "6011000990156527", "12", "2025", "123" , "6860"            , "750241234", 17.03m),
                ("Amex",       "372700699251018",  "12", "2025", "1234", "6860"            , "75024"    , 17.04m),
                // Charge only, never token
                ("JCB",        "3566007770007321", "12", "2025", "123" , "6860"            , "750241234", 17.05m)
            };
            // Act
            for (var i = 0; i < setups.Count; i++)
            {
                var (type, number, expMonth, expYear, cvv, street1, zip, amount) = setups[i];
                TestOutputHelper.WriteLine($"{i}-{type}: {number}");
                var billingModel = (IContactModel)BillingModel.DeepCopy();
                billingModel.Address.Street1 = street1;
                billingModel.Address.Street2 = null;
                billingModel.Address.Street3 = null;
                billingModel.Address.PostalCode = zip;
                var payment = new PaymentModel
                {
                    Amount = amount,
                    CardNumber = number,
                    CVV = cvv,
                    ExpirationMonth = int.Parse(expMonth),
                    ExpirationYear = int.Parse(expYear)
                };
                if (i < 4)
                {
                    var result = await provider.CreateCustomerProfileAsync(payment, billingModel, null).ConfigureAwait(false);
                    // Assert
                    Assert.True(result.Approved);
                    Assert.NotNull(result.Token);
                    TestOutputHelper.WriteLine($"Token: '{result.Token}'");
                    tokens[i] = result.Token;
                }
                else if (i < 8)
                {
                    // Apply the token we got earlier
                    payment.Token = tokens[i - 4];
                    var result = await provider.AuthorizeAndACaptureAsync(payment, billingModel, ShippingModel, null).ConfigureAwait(false);
                    // Assert
                    Assert.True(result.Approved);
                    Assert.NotNull(result.ResponseCode);
                    Assert.NotNull(result.AuthorizationCode);
                    Assert.NotNull(result.TransactionID);
                    TestOutputHelper.WriteLine($"Response Code: {result.ResponseCode}");
                    TestOutputHelper.WriteLine($"Authorization Code: {result.AuthorizationCode}");
                    TestOutputHelper.WriteLine($"Transaction ID: {result.TransactionID}");
                }
                else
                {
                    var result = await provider.AuthorizeAndACaptureAsync(payment, billingModel, ShippingModel, null).ConfigureAwait(false);
                    // Assert
                    Assert.True(result.Approved);
                    Assert.NotNull(result.ResponseCode);
                    Assert.NotNull(result.AuthorizationCode);
                    Assert.NotNull(result.TransactionID);
                    TestOutputHelper.WriteLine($"Response Code: {result.ResponseCode}");
                    TestOutputHelper.WriteLine($"Authorization Code: {result.AuthorizationCode}");
                    TestOutputHelper.WriteLine($"Transaction ID: {result.TransactionID}");
                }
            }
        }
    }
}
*/