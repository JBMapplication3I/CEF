// <copyright file="MockPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mock payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Mock
{
    using System;
    using Interfaces.Providers.Payments;

    /// <summary>A mock payments provider extensions.</summary>
    public static class MockPaymentsProviderExtensions
    {
        /// <summary>Converts values to a payment wallet response.</summary>
        /// <returns>This MockPaymentsProviderExtensions as an IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse()
        {
            return new PaymentWalletResponse
            {
                Approved = true,
                ResponseCode = "200",
                Token = "MOCK:" + Guid.NewGuid(),
            };
        }
    }
}
