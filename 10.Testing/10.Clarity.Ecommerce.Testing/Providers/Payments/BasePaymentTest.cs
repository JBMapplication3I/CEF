// <copyright file="BasePaymentTest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base payment test class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using Interfaces.Models;
    using Models;
    using Xunit.Abstractions;

    public class BasePaymentTest : XUnitLogHelper
    {
        protected readonly IPaymentModel Payment;
        protected readonly IContactModel BillingModel;
        protected readonly IContactModel ShippingModel;

        protected BasePaymentTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Payment = new PaymentModel
            {
                CardNumber = "4111111111111111",
                Amount = 42,
                CVV = "234",
                ExpirationMonth = 12,
                ExpirationYear = DateTime.Today.Year - 2000
            };
            BillingModel = new ContactModel
            {
                FirstName = "John",
                LastName = "McMiller",
                Address = new AddressModel
                {
                    Company = "John McMiller",
                    Street1 = "4 Northeastern Blvd",
                    PostalCode = "03105",
                    CountryCode = "USA",
                    City = "Salem",
                    RegionCode = "NH"
                },
                Email1 = "test@test.com"
            };
            ShippingModel = new ContactModel
            {
                Address = new AddressModel
                {
                    Street1 = "4 Northeastern Blvd",
                    PostalCode = "03105",
                    CountryCode = "USA",
                    City = "Salem",
                    RegionCode = "NH"
                }
            };
        }
    }
}
