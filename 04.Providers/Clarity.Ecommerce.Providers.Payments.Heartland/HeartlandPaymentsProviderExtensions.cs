// <copyright file="HeartlandPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the heartland payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Heartland
{
    using System;
    using GlobalPayments.Api.Entities;
    using GlobalPayments.Api.PaymentMethods;
    using Interfaces.Providers;

    /// <summary>A heartland payments provider extensions.</summary>
    public static class HeartlandPaymentsProviderExtensions
    {
        /// <summary>Gets the payment to card.</summary>
        /// <value>The payment to card.</value>
        public static Func<IProviderPayment, CreditCardData> PaymentToCard => x => new CreditCardData
        {
            Number = x.CardNumber, // "4263970000005262",
            ExpMonth = x.ExpirationMonth, // 12,
            ExpYear = x.ExpirationYear // 2025,
                + (x.ExpirationYear < 2000 ? 2000 : 0) // Handle numbers too small
                + (x.ExpirationYear > 200000 ? -200000 : 0), // Handle numbers too big
            Cvn = x.CVV, // "131",
            CardHolderName = x.CardHolderName, // "James Mason"
            CardPresent = false,
            ReaderPresent = false,
        };

        /// <summary>Gets the token to card.</summary>
        /// <value>The token to card.</value>
        public static Func<IProviderPayment, CreditCardData> TokenToCard => x => new CreditCardData
        {
            Token = x.Token,
        };

        /// <summary>Gets the street and zip to address.</summary>
        /// <value>The street and zip to address.</value>
        public static Func<string, string, Address> StreetAndZipToAddress => (x, y) => new Address
        {
            StreetAddress1 = x,
            PostalCode = y,
        };
    }
}
