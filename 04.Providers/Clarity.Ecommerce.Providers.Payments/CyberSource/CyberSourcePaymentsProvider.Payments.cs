// <copyright file="CyberSourcePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cyber source payments provider class</summary>
#if NET5_0_OR_GREATER
#else
namespace Clarity.Ecommerce.Providers.Payments.CyberSource
{
    using System;
    using System.Threading.Tasks;
    using global::CyberSource.Api;
    using global::CyberSource.Client;
    using global::CyberSource.Model;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A cyber source payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    internal partial class CyberSourcePaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration => true;

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            CreatePaymentRequest request = null!;
            PtsV2PaymentsPost201Response? result = null;
            try
            {
                var paymentApi = GetPaymentAPIClient();
                // Build out the main part of the request body (billing details and totals)
                request = new()
                {
                    OrderInformation = new()
                    {
                        AmountDetails = new()
                        {
                            TotalAmount = payment.Amount?.ToString(),
                            Currency = payment.CurrencyKey,
                        },
                        BillTo = new()
                        {
                            FirstName = billing!.FirstName,
                            LastName = billing.LastName,
                            Address1 = billing.Address?.Street1,
                            Address2 = billing.Address?.Street2,
                            Address3 = billing.Address?.Street3,
                            // Locality = City
                            Locality = billing.Address?.City,
                            // AdministrativeArea = State
                            AdministrativeArea = billing.Address?.RegionCode,
                            PostalCode = billing.Address?.PostalCode,
                            Country = billing.Address?.CountryCode,
                            Email = billing.Email1,
                            PhoneNumber = billing.Phone1,
                        },
                    },
                    ProcessingInformation = new()
                    {
                        Capture = true,
                    },
                    PaymentInformation = new(),
                };
                // Append the payment information
                if (useWalletToken || Contract.CheckValidKey(payment.Token))
                {
                    // Wallet
                    request.PaymentInformation.PaymentInstrument = new()
                    {
                        Id = payment.Token,
                    };
                }
                else
                {
                    if (Contract.CheckAllValidKeys(payment.AccountNumber, payment.RoutingNumber))
                    {
                        // eCheck
                        request.PaymentInformation.Bank = new()
                        {
                            Account = new()
                            {
                                Number = payment.AccountNumber,
                            },
                            RoutingNumber = payment.RoutingNumber,
                        };
                    }
                    else
                    {
                        // Credit Card
                        request.PaymentInformation.Card = new()
                        {
                            Number = payment.CardNumber,
                            ExpirationMonth = Contract.RequiresNotNull(payment.ExpirationMonth).ToString(),
                            ExpirationYear = Contract.RequiresNotNull(payment.ExpirationYear).ToString(),
                            SecurityCode = payment.CVV,
                        };
                    }
                }
                // Send the request
                result = await paymentApi.CreatePaymentAsync(request).ConfigureAwait(false);
                var approved = result.Status is "AUTHORIZED" or "PARTIAL_AUTHORIZED";
                if (!approved)
                {
                    throw new InvalidOperationException("Payment failure at the payment processor");
                }
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = decimal.TryParse(result.OrderInformation?.AmountDetails?.AuthorizedAmount, out var amt)
                        ? amt
                        : -1m,
                    ReferenceCode = result.ReconciliationId,
                    AuthorizationCode = result.ProcessorInformation?.ApprovalCode,
                    ResponseCode = result.ProcessorInformation?.ResponseCode,
                    TransactionID = result.Id,
                };
            }
            catch (Exception ex)
            {
                var guid = await Logger.LogErrorAsync(
                        name: $"{nameof(CyberSourcePaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}.{ex.GetType().Name}",
                        message: $"Failed to auth and capture payment: {ex.Message}",
                        ex: ex,
                        data: JsonConvert.SerializeObject(new
                        {
                            Request = CloneAndBlankSensitiveDetails(request),
                            Response = result,
                        }),
                        forceEmail: false,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentResponse
                {
                    Approved = false,
                    ReferenceCode = ex.Message,
                    ResponseCode = guid.ToString("N")[..12],
                };
            }
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            CreatePaymentRequest request = null!;
            PtsV2PaymentsPost201Response? result = null;
            try
            {
                var paymentApi = GetPaymentAPIClient();
                // Build out the main part of the request body (billing details and totals)
                request = new()
                {
                    OrderInformation = new()
                    {
                        AmountDetails = new()
                        {
                            TotalAmount = payment.Amount?.ToString(),
                            Currency = payment.CurrencyKey,
                        },
                        BillTo = new()
                        {
                            FirstName = billing!.FirstName,
                            LastName = billing.LastName,
                            Address1 = billing.Address?.Street1,
                            Address2 = billing.Address?.Street2,
                            Address3 = billing.Address?.Street3,
                            // Locality = City
                            Locality = billing.Address?.City,
                            // AdministrativeArea = State
                            AdministrativeArea = billing.Address?.RegionCode,
                            PostalCode = billing.Address?.PostalCode,
                            Country = billing.Address?.CountryCode,
                            Email = billing.Email1,
                            PhoneNumber = billing.Phone1,
                        },
                    },
                    ProcessingInformation = new()
                    {
                        Capture = false,
                    },
                    PaymentInformation = new(),
                };
                // Append the payment information
                if (Contract.CheckValidKey(payment.Token))
                {
                    // Wallet
                    request.PaymentInformation.PaymentInstrument = new()
                    {
                        Id = payment.Token,
                    };
                }
                else
                {
                    if (Contract.CheckAllValidKeys(payment.AccountNumber, payment.RoutingNumber))
                    {
                        // eCheck
                        request.PaymentInformation.Bank = new()
                        {
                            Account = new()
                            {
                                Number = payment.AccountNumber,
                            },
                            RoutingNumber = payment.RoutingNumber,
                        };
                    }
                    else
                    {
                        // Credit Card
                        request.PaymentInformation.Card = new()
                        {
                            Number = payment.CardNumber,
                            ExpirationMonth = Contract.RequiresNotNull(payment.ExpirationMonth).ToString(),
                            ExpirationYear = Contract.RequiresNotNull(payment.ExpirationYear).ToString(),
                            SecurityCode = payment.CVV,
                        };
                    }
                }
                // Send the request
                result = await paymentApi.CreatePaymentAsync(request).ConfigureAwait(false);
                var approved = result.Status is "AUTHORIZED" or "PARTIAL_AUTHORIZED";
                if (!approved)
                {
                    throw new InvalidOperationException("Payment failure at the payment processor");
                }
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = decimal.TryParse(result.OrderInformation?.AmountDetails?.AuthorizedAmount, out var amt)
                        ? amt
                        : -1m,
                    ReferenceCode = result.ReconciliationId,
                    AuthorizationCode = result.ProcessorInformation?.ApprovalCode,
                    ResponseCode = result.ProcessorInformation?.ResponseCode,
                    TransactionID = result.Id,
                };
            }
            catch (Exception ex)
            {
                var guid = await Logger.LogErrorAsync(
                        name: $"{nameof(CyberSourcePaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}.{ex.GetType().Name}",
                        message: $"Failed to auth and capture payment: {ex.Message}",
                        ex: ex,
                        data: JsonConvert.SerializeObject(new
                        {
                            Request = CloneAndBlankSensitiveDetails(request),
                            Response = result,
                        }),
                        forceEmail: false,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentResponse
                {
                    Approved = false,
                    ReferenceCode = ex.Message,
                    ResponseCode = guid.ToString("N")[..12],
                };
            }
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Clone and blank sensitive details.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CreatePaymentRequest.</returns>
        private static CreatePaymentRequest CloneAndBlankSensitiveDetails(CreatePaymentRequest request)
        {
            var req = (JsonConvert.DeserializeObject<CreatePaymentRequest>(JsonConvert.SerializeObject(request))
                ?? throw new ArgumentException("Unable to create payment failure log"))!;
            if (Contract.CheckValidKey(req.PaymentInformation?.PaymentInstrument?.Id))
            {
                req.PaymentInformation!.PaymentInstrument!.Id = "****";
            }
            if (Contract.CheckValidKey(req.PaymentInformation?.Bank?.Account?.Number))
            {
                req.PaymentInformation!.Bank!.Account!.Number = "****";
            }
            if (Contract.CheckValidKey(req.PaymentInformation?.Bank?.RoutingNumber))
            {
                req.PaymentInformation!.Bank!.RoutingNumber = "****";
            }
            if (Contract.CheckValidKey(req.PaymentInformation?.Card?.Number))
            {
                req.PaymentInformation!.Card!.Number = "****";
            }
            if (Contract.CheckValidKey(req.PaymentInformation?.Card?.SecurityCode))
            {
                req.PaymentInformation!.Card!.SecurityCode = "****";
            }
            return req;
        }

        /// <summary>Gets payment API client.</summary>
        /// <returns>The payment API client.</returns>
        private PaymentsApi GetPaymentAPIClient()
        {
            return new(new Configuration(merchConfigDictObj: CyberSourcePaymentsProviderConfig.GetConfigDictionary()));
        }
    }
}
#endif
