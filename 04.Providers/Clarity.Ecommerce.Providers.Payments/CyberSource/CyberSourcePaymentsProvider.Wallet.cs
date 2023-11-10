// <copyright file="CyberSourcePaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cyber source payments provider class</summary>
// ReSharper disable CommentTypo
#if NET5_0_OR_GREATER
#else
namespace Clarity.Ecommerce.Providers.Payments.CyberSource
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Models;
    using global::CyberSource.Api;
    using global::CyberSource.Client;
    using global::CyberSource.Model;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A cyber source payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    internal partial class CyberSourcePaymentsProvider : IWalletProviderBase
    {
        private static readonly Regex RegexCreditCardTypeVisa = new(@"^4[0-9]{6,}$");
        private static readonly Regex RegexCreditCardTypeMasterCard = new(@"^5[1-5][0-9]{5,}|222[1-9][0-9]{3,}|22[3-9][0-9]{4,}|2[3-6][0-9]{5,}|27[01][0-9]{4,}|2720[0-9]{3,}$");
        private static readonly Regex RegexCreditCardTypeAmericanExpress = new(@"^3[47][0-9]{5,}$");
        private static readonly Regex RegexCreditCardTypeDiscover = new(@"^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$");
        private static readonly Regex RegexCreditCardTypeAmex = new(@"^3[47][0-9]{13}$");
        private static readonly Regex RegexCreditCardTypeBCGlobal = new(@"^(6541|6556)[0-9]{12}$");
        private static readonly Regex RegexCreditCardTypeCarteBlanch = new(@"^389[0-9]{11}$");
        private static readonly Regex RegexCreditCardTypeDinersClub = new(@"^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
        private static readonly Regex RegexCreditCardTypeInstaPaymentCard = new(@"^63[7-9][0-9]{13}$");
        private static readonly Regex RegexCreditCardTypeJCBCard = new(@"^(?:2131|1800|35\d{3})\d{11}$");
        private static readonly Regex RegexCreditCardTypeKoreanLocal = new(@"^9[0-9]{15}$");
        private static readonly Regex RegexCreditCardTypeLaserCard = new(@"^(6304|6706|6709|6771)[0-9]{12,15}$");
        private static readonly Regex RegexCreditCardTypeMaestro = new(@"^(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15}$");
        private static readonly Regex RegexCreditCardTypeSolo = new(@"^(6334|6767)[0-9]{12}|(6334|6767)[0-9]{14}|(6334|6767)[0-9]{15}$");
        private static readonly Regex RegexCreditCardTypeSwitchCard = new(@"^(4903|4905|4911|4936|6333|6759)[0-9]{12}|(4903|4905|4911|4936|6333|6759)[0-9]{14}|(4903|4905|4911|4936|6333|6759)[0-9]{15}|564182[0-9]{10}|564182[0-9]{12}|564182[0-9]{13}|633110[0-9]{10}|633110[0-9]{12}|633110[0-9]{13}$");
        private static readonly Regex RegexCreditCardTypeUnionPay = new(@"^(62[0-9]{14,17})$");

        /// <inheritdoc />
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            _ = Contract.CheckValidKey(payment.Token);
            var result = await DeletePaymentInstrumentIDAsync(payment.Token!, contextProfileName).ConfigureAwait(false);
            return new PaymentWalletResponse
            {
                Approved = result.ActionSucceeded,
                Token = payment.Token,
                ResponseCode = result.ActionSucceeded ? "Deleted" : "Did not delete",
            };
        }

        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            try
            {
                // Steps to do the thing:
                // 0.5. Find or create the user's profile ID
                // 1. Create an instrument ID
                // 2. Use the instrument ID to create a payment instrument
                // 3. Maybe tie the payment instrument to a customer ID? Not sure
                EnsureCreditCardType(payment);
                var instrumentID = await CreateAndGetInstrumentIDForNumberAsync(payment, contextProfileName).ConfigureAwait(false);
                var paymentInstrumentResult = await CreatePaymentInstrumentForInstrumentIDAsync(payment, billing, instrumentID).ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = true,
                    Token = paymentInstrumentResult.Id,
                    ResponseCode = paymentInstrumentResult.State,
                    CardType = paymentInstrumentResult.Card!.Type,
                    ExpDate = $"{paymentInstrumentResult.Card.ExpirationMonth}-{paymentInstrumentResult.Card.ExpirationYear}",
                };
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CyberSourcePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}",
                        message: $"Failed to create customer profile: {ex.Message}",
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = false,
                };
            }
        }

        /// <summary>Ensures that credit card type.</summary>
        /// <param name="payment">The payment.</param>
        private static void EnsureCreditCardType(IProviderPayment payment)
        {
            if (Contract.CheckValidKey(payment.CardType) || !Contract.CheckValidKey(payment.CardNumber))
            {
                return;
            }
            var typeEnum = CreditCardNumberToCardType(payment.CardNumber!);
            if (typeEnum is CreditCardType.NotFormatted
                or CreditCardType.Unknown
                or CreditCardType.BCGlobal
                or CreditCardType.InstaPaymentCard
                or CreditCardType.SwitchCard)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(payment.CardType),
                    "A valid card type or card number that can be checked as a valid card type is required");
            }
            payment.CardType = typeEnum; // V2: ((int)typeEnum).ToString("000");
        }

        /// <summary>Credit card number to card type.</summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>A string.</returns>
        private static string CreditCardNumberToCardType(string cardNumber)
        {
            return RegexCreditCardTypeVisa.IsMatch(cardNumber) ? CreditCardType.Visa
                : RegexCreditCardTypeMasterCard.IsMatch(cardNumber) ? CreditCardType.MasterCard
                : RegexCreditCardTypeAmericanExpress.IsMatch(cardNumber) ? CreditCardType.AmericanExpress
                : RegexCreditCardTypeDiscover.IsMatch(cardNumber) ? CreditCardType.Discover
                : RegexCreditCardTypeAmex.IsMatch(cardNumber) ? CreditCardType.Amex
                : RegexCreditCardTypeBCGlobal.IsMatch(cardNumber) ? CreditCardType.BCGlobal
                : RegexCreditCardTypeCarteBlanch.IsMatch(cardNumber) ? CreditCardType.CarteBlanch
                : RegexCreditCardTypeDinersClub.IsMatch(cardNumber) ? CreditCardType.DinersClub
                : RegexCreditCardTypeInstaPaymentCard.IsMatch(cardNumber) ? CreditCardType.InstaPaymentCard
                : RegexCreditCardTypeJCBCard.IsMatch(cardNumber) ? CreditCardType.JCBCard
                : RegexCreditCardTypeKoreanLocal.IsMatch(cardNumber) ? CreditCardType.KoreanLocal
                : RegexCreditCardTypeLaserCard.IsMatch(cardNumber) ? CreditCardType.LaserCard
                : RegexCreditCardTypeMaestro.IsMatch(cardNumber) ? CreditCardType.Maestro
                : RegexCreditCardTypeSolo.IsMatch(cardNumber) ? CreditCardType.Solo
                : RegexCreditCardTypeSwitchCard.IsMatch(cardNumber) ? CreditCardType.SwitchCard
                : RegexCreditCardTypeUnionPay.IsMatch(cardNumber) ? CreditCardType.UnionPay
                : cardNumber
                        .Where(e => e is >= '0' and <= '9')
                        .Reverse()
                        .Select((e, i) => (e - 48) * (i % 2 == 0 ? 1 : 2))
                        .Sum(e => e / 10 + e % 10) == 0
                    ? CreditCardType.NotFormatted
                    : CreditCardType.Unknown;
        }

        /// <summary>Gets payment instrument API.</summary>
        /// <returns>The payment instrument API.</returns>
        private PaymentInstrumentApi GetPaymentInstrumentApi()
        {
            var configDictionary = CyberSourcePaymentsProviderConfig.GetConfigDictionary();
            var config = new Configuration(merchConfigDictObj: configDictionary);
            return new PaymentInstrumentApi(config);
        }

        private async Task<CEFActionResponse> DeleteInstrumentIDAsync(string instrumentID, string? contextProfileName)
        {
            var client = GetInstrumentIdentifierApi();
            try
            {
                await client.DeleteInstrumentIdentifierAsync(instrumentID).ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            catch (Exception ex)
            {
                await Logger.LogInformationAsync(
                   name: $"{nameof(CyberSourcePaymentsProvider)}.{nameof(DeleteInstrumentIDAsync)}.{nameof(DeleteInstrumentIDAsync)}",
                   message: ex.Message,
                   contextProfileName: contextProfileName)
                .ConfigureAwait(false);
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        private async Task<CEFActionResponse> DeletePaymentInstrumentIDAsync(string instrumentID, string? contextProfileName)
        {
            var client = GetPaymentInstrumentApi();
            try
            {
                await client.DeletePaymentInstrumentAsync(instrumentID).ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            catch (Exception ex)
            {
                await Logger.LogInformationAsync(
                   name: $"{nameof(CyberSourcePaymentsProvider)}.{nameof(DeletePaymentInstrumentIDAsync)}.{nameof(DeletePaymentInstrumentIDAsync)}",
                   message: ex.Message,
                   contextProfileName: contextProfileName)
                .ConfigureAwait(false);
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        private async Task<Tmsv2customersEmbeddedDefaultPaymentInstrument> CreatePaymentInstrumentForInstrumentIDAsync(
            IProviderPayment payment,
            IContactModel billing,
            string instrumentID)
        {
            // This is the documentation page for this request
            // https://developer.cybersource.com/docs/cybs/en-us/tms/developer/all/rest/tms-developer/payment-instrument-v1/payment-instrument-v1-create.html
            // Per docs, we've already converted the number to an instrument id
            // Now we add the billing info and if it was a card, the rest of the card info (not necessary for ACH)
            PostPaymentInstrumentRequest request = new()
            {
                Card = new()
                {
                    ExpirationMonth = payment.ExpirationMonth!.Value.ToString("00"),
                    ExpirationYear = ((payment.ExpirationYear < 2000 ? 2000 : 0) + payment.ExpirationYear!).Value.ToString("0000"),
                    Type = Contract.RequiresValidKey(payment.CardType, $"{nameof(payment)}.${nameof(payment.CardType)} is required").ToLower(),
                },
                BillTo = new()
                {
                    FirstName = billing.FirstName,
                    LastName = billing.LastName,
                    Address1 = billing.Address?.Street1,
                    Address2 = billing.Address?.Street2,
                    Company = billing.Address?.Company,
                    Locality = billing.Address?.City,
                    AdministrativeArea = billing.Address?.RegionCode,
                    PostalCode = billing.Address?.PostalCode,
                    Country = billing.Address?.CountryCode,
                    Email = billing.Email1,
                    // PhoneNumber = billing.Phone1,
                },
                InstrumentIdentifier = new()
                {
                    Id = instrumentID,
                },
            };
            var client = this.GetPaymentInstrumentApi();
            var paymentInstrumentResult = await client.PostPaymentInstrumentAsync(request).ConfigureAwait(false);
            return paymentInstrumentResult;
        }

        /* Enum of values for CyberSource
        Value that indicates the card type.
        * Valid v2 : v1 - description
        Values:
        * 001 : visa
        * 002 : mastercard - Eurocard—European regional brand of Mastercard
        * 003 : american express
        * 004 : discover
        * 005 : diners club
        * 006 : carte blanche
        * 007 : jcb
        * 008 : optima
        * 011 : twinpay credit
        * 012 : twinpay debit
        * 013 : walmart
        * 014 : enRoute
        * 015 : lowes consumer
        * 016 : home depot consumer
        * 017 : mbna
        * 018 : dicks sportswear
        * 019 : casual corner
        * 020 : sears
        * 021 : jal
        * 023 : disney
        * 024 : maestro uk domestic
        * 025 : sams club consumer
        * 026 : sams club business
        * 028 : bill me later
        * 029 : bebe
        * 030 : restoration hardware
        * 031 : delta online — use this value only for Ingenico ePayments. For other processors, use 001 for all Visa card types.
        * 032 : solo
        * 033 : visa electron
        * 034 : dankort
        * 035 : laser
        * 036 : carte bleue — formerly Cartes Bancaires
        * 037 : carta si
        * 038 : pinless debit
        * 039 : encoded account
        * 040 : uatp
        * 041 : household
        * 042 : maestro international
        * 043 : ge money uk
        * 044 : korean cards
        * 045 : style
        * 046 : jcrew
        * 047 : payease china processing ewallet
        * 048 : payease china processing bank transfer
        * 049 : meijer private label
        * 050 : hipercard — supported only by the Comercio Latino processor.
        * 051 : aura — supported only by the Comercio Latino processor.
        * 052 : redecard
        * 054 : elo — supported only by the Comercio Latino processor.
        * 055 : capital one private label
        * 056 : synchrony private label
        * 057 : costco private label
        * 060 : mada
        * 062 : china union pay
        * 063 : falabella private label
        */

        /// <summary>Gets instrument identifier API.</summary>
        /// <returns>The instrument identifier API.</returns>
        private InstrumentIdentifierApi GetInstrumentIdentifierApi()
        {
            var configDictionary = CyberSourcePaymentsProviderConfig.GetConfigDictionary();
            var config = new Configuration(merchConfigDictObj: configDictionary);
            return new InstrumentIdentifierApi(config);
        }

        private async Task<string> CreateAndGetInstrumentIDForNumberAsync(
            IProviderPayment payment,
            // ReSharper disable once UnusedParameter.Local
            string? contextProfileName)
        {
            // This is the documentation page for this request
            // https://developer.cybersource.com/docs/cybs/en-us/tms/developer/all/rest/tms-developer/instrument-identifier-token/instrument-identifier-create.html
            // Per docs, for a credit card, you only send the card number, for ACH/eChecks you send the account number and routing number
            // No additional information
            PostInstrumentIdentifierRequest request = new();
            var isACH = Contract.CheckAllValidKeys(payment.RoutingNumber, payment.AccountNumber);
            if (isACH)
            {
                request.BankAccount = new() { Number = payment.AccountNumber, RoutingNumber = payment.RoutingNumber };
            }
            else
            {
                request.Card = new() { Number = payment.CardNumber, };
            }
            var client = GetInstrumentIdentifierApi();
            var rawResponse = await client.PostInstrumentIdentifierAsync(request).ConfigureAwait(false);
            var response = Contract.RequiresNotNull(rawResponse);
            /*
            await Logger.LogInformationAsync(
                    name: $"{nameof(CyberSourcePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.{nameof(CreateAndGetInstrumentIDForNumberAsync)}",
                    message: "Instrument ID for number starting with: '"
                        + $"{(isACH ? payment.AccountNumber![..4] : payment.CardNumber![..4])}' is '{response.Id}'",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            */
            return response.Id;
        }

        /// <summary>Values that represent credit card types.</summary>
        // ReSharper disable MultipleSpaces
#pragma warning disable format
        private static class CreditCardType
        {
            public const string NotFormatted     = "Not Formatted";
            public const string Unknown          = "Unknown";

            public const string Visa             = "visa"; // 001;
            public const string MasterCard       = "mastercard"; // 002;
            public const string AmericanExpress  = "american express"; // 003;
            public const string Amex             = "amex"; // 003;
            public const string Discover         = "discover"; // 004;
            public const string DinersClub       = "diners club"; // 005;
            public const string CarteBlanch      = "carte blanche"; // 006;
            public const string JCBCard          = "jcb"; // 007;
            public const string Maestro          = "maestro"; // 024;
            public const string Solo             = "solo"; // 032;
            public const string LaserCard        = "laser"; // 035;
            public const string KoreanLocal      = "korean cards"; // 044;
            public const string UnionPay         = "china union pay"; // 062;

            public const string BCGlobal         = "bc global"; // 500;
            public const string InstaPaymentCard = "insta payment card"; // 501;
            public const string SwitchCard       = "switch card"; // 502;
        }
    }
}
#endif
