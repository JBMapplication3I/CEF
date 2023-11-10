// <copyright file="CheckoutMapper.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout mapper class</summary>
namespace Clarity.Ecommerce.Mapper
{
    using Interfaces.Models;
    using Models;
    using Utilities;

    public static partial class BaseModelMapper
    {
        /// <summary>An ICheckoutModel extension method that creates payment model from checkout model.</summary>
        /// <param name="checkout">The checkout to act on.</param>
        /// <returns>The new payment model from checkout model.</returns>
        public static IPaymentModel CreatePaymentModelFromCheckoutModel(this ICheckoutModel checkout)
        {
            Contract.RequiresNotNull(checkout);
            var model = new PaymentModel
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                RoutingNumber = checkout.PayByECheck?.RoutingNumber,
                AccountNumber = checkout.PayByECheck?.AccountNumber,
                BankName = checkout.PayByECheck?.BankName,
                CardHolderName = checkout.PayByCreditCard?.CardHolderName,
                CardNumber = checkout.PayByCreditCard?.CardNumber,
                CardType = checkout.PayByCreditCard?.CardType,
                CVV = checkout.PayByCreditCard?.CVV ?? checkout.PayByWalletEntry?.WalletCVV,
                ExpirationMonth = checkout.PayByCreditCard?.ExpirationMonth,
                ExpirationYear = checkout.PayByCreditCard?.ExpirationYear,
                PayoneerAccountID = checkout.PayByPayoneer?.PayoneerAccountID,
                PayoneerCustomerID = checkout.PayByPayoneer?.PayoneerCustomerID,
                Token = checkout.PayByWalletEntry?.WalletToken
                    ?? checkout.PayByCreditCard?.CardToken
                    ?? checkout.PayByPayPal?.PayPalToken,
            };
            if (Contract.CheckValidKey(checkout.PayByCreditCard?.CardNumber) && checkout.PayByCreditCard?.CardNumber!.Length > 3)
            {
                model.Last4CardDigits = checkout.PayByCreditCard!.CardNumber![(checkout.PayByCreditCard!.CardNumber!.Length - 4)..];
            }
            return model;
        }
    }
}
