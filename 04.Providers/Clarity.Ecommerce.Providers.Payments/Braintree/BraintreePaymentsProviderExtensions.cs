// <copyright file="BraintreePaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.Payments.BraintreePayments
{
    using Braintree;
    using Interfaces.Providers.Payments;

    /// <summary>Provides helper methods for the Braintree payment provider.</summary>
    public static class BraintreePaymentsProviderExtensions
    {
        /// <summary>Converts a Braintree Transaction Response into a CEF PaymentResponse.</summary>
        /// <param name="result">Braintree result object based on a transaction attempt.</param>
        /// <returns>Returns an IPaymentResponse with the relevant details.</returns>
        public static IPaymentResponse ToPaymentResponse(Result<Transaction> result)
        {
            if (result?.IsSuccess() == true)
            {
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = result.Target.Amount!.Value,
                    AuthorizationCode = result.Target.ProcessorAuthorizationCode,
                    ResponseCode = result.Target.ProcessorResponseCode,
                    TransactionID = result.Target.Id,
                };
            }
            return new PaymentResponse
            {
                Approved = false,
                AuthorizationCode = result?.Transaction?.ProcessorAuthorizationCode,
                ResponseCode = result?.Transaction?.ProcessorResponseCode,
            };
        }

        /// <summary>Converts a Braintree Customer Result into a CEF Payment Wallet Response.</summary>
        /// <param name="result">Braintree result object for credit card creation atttempt.</param>
        /// <returns>Returns an IPaymentWalletResponse with the relevant details.</returns>
        public static IPaymentWalletResponse CustomerToPaymentWalletResponse(Result<Customer> result)
        {
            return new PaymentWalletResponse
            {
                Approved = result.IsSuccess(),
                ResponseCode = result.Message,
                Token = result.Target?.Id,
            };
        }

        /// <summary>Converts a Braintree Customer with Credit Card Result into a CEF Payment Wallet Response.</summary>
        /// <param name="result">Braintree result object for credit card creation atttempt.</param>
        /// <param name="token">Token for Credit Card.</param>
        /// <returns>Returns an IPaymentWalletResponse with the relevant details.</returns>
        public static IPaymentWalletResponse CardToPaymentWalletResponse(
            Result<CreditCard> result,
            string token)
        {
            if (token == null)
            {
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = "Unable to save card, token is null",
                };
            }
            return new PaymentWalletResponse
            {
                Approved = result.IsSuccess(),
                ResponseCode = result.Message,
                Token = result.Target.Token,
            };
        }

        /// <summary>Converts a Braintree Customer with Credit Card Result into a CEF Payment Wallet Response.</summary>
        /// <param name="result">Braintree result object for credit card creation atttempt.</param>
        /// <returns>Returns an IPaymentWalletResponse with the relevant details.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(
            Result<Transaction>? result)
        {
            return new PaymentWalletResponse
            {
                Approved = result?.IsSuccess() == true,
                Token = result?.Target.CustomerDetails.Id,
                ResponseCode = result?.Message,
            };
        }

        /// <summary>Converts a Braintree Customer with Credit Card Result into a CEF Payment Wallet Response.</summary>
        /// <param name="result">Braintree result object for credit card creation atttempt.</param>
        /// <returns>Returns an IPaymentWalletResponse with the relevant details.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(
            Customer result)
        {
            return new PaymentWalletResponse
            {
                Approved = true,
                Token = result.Id,
                ResponseCode = string.Empty,
            };
        }

        /// <summary>Converts Braintree response for payment method deletion into a payment wallet response.</summary>
        /// <param name="result">The braintree API response.</param>
        /// <returns>Returns an IPaymentWalletResponse with the relevant details.</returns>
        public static IPaymentWalletResponse DeletePaymentMethodToPaymentWalletResponse(Result<PaymentMethod> result)
        {
            return new PaymentWalletResponse
            {
                Approved = result.IsSuccess(),
                Token = result.Target.Token,
                ResponseCode = result.Message,
            };
        }
    }
}
