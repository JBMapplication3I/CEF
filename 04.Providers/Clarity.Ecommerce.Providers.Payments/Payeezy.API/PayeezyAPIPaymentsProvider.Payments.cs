// <copyright file="PayeezyAPIPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy API payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <content>A Payeezy API payments provider.</content>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class PayeezyAPIPaymentsProvider : PaymentsProviderBase
    {
        /// <summary>A pattern specifying the amex.</summary>
        private const string AMEXPattern = "^3[47][0-9]{13}$";

        /// <summary>A pattern specifying the master card.</summary>
        private const string MasterCardPattern = "^5[1-5][0-9]{14}$";

        /// <summary>A pattern specifying the visa card.</summary>
        private const string VisaCardPattern = "^4[0-9]{12}(?:[0-9]{3})?$";

        /// <summary>A pattern specifying the diners club card.</summary>
        private const string DinersClubCardPattern = "^3(?:0[0-5]|[68][0-9])[0-9]{11}$";

        /// <summary>A pattern specifying the en route card.</summary>
        private const string EnRouteCardPattern = "^(2014|2149)";

        /// <summary>A pattern specifying the discover card.</summary>
        private const string DiscoverCardPattern = "^6(?:011|5[0-9]{2})[0-9]{12}$";

        /// <summary>A pattern specifying the jcb card.</summary>
        private const string JCBCardPattern = @"^(?:2131|1800|35\d{3})\d{11}$";

        /// <summary>The API key.</summary>
        private string apiSecret = null!;

        /// <summary>The transaction token.</summary>
        private string token = null!;

        /// <summary>The Authorization Key.</summary>
        private string? authorization;

        /// <summary>The nonce.</summary>
        private string nonce = null!;

        /// <summary>The transaction timestamp.</summary>
        private string timestamp = null!;

        /// <summary>URL of the document.</summary>
        private string? url;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PayeezyAPIPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            token = PayeezyAPIPaymentsProviderConfig.Token!;
            apiSecret = PayeezyAPIPaymentsProviderConfig.Secret!;
            url = $"https://api{(ProviderMode == Enums.PaymentProviderMode.Production ? string.Empty : "-cert")}.payeezy.com/v1/transactions";
            timestamp = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture);
            nonce = (10000000000000000000 * new Random(DateExtensions.GenDateTime.Millisecond).NextDouble()).ToString("0000000000000000000");
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(billing);
            var strOrderNumber = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}";
            var request = CreatePaymentRequestJson(payment, billing!, "authorize", "credit_card", strOrderNumber);
            authorization = CreateHMAC(apiSecret, token, request, nonce, timestamp);
            var result = SendPostRequest(request);
            if (result is null)
            {
                return new PaymentResponse();
            }
            var response = JsonConvert.DeserializeObject<PayeezyAuthorizeRequestResponse>(result);
            if (response is null)
            {
                return new PaymentResponse();
            }
            if (response.transaction_status != "approved")
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(AuthorizeAsync)}.Error",
                        message: $"{{request:{request},response:{result}}}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            else
            {
                await Logger.LogInformationAsync(
                        name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(AuthorizeAsync)}.Success",
                        message: $"{{request:{request},response:{result}}}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return new PaymentResponse
            {
                Approved = response.transaction_status == "approved",
                AuthorizationCode = response.transaction_tag,
                ResponseCode = response.bank_resp_code,
                TransactionID = CreateTransactionID(response, strOrderNumber),
                Amount = Convert.ToDecimal(response.amount) / 100,
            };
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            var transValues = paymentAuthorizationToken.Split('|');
            var request = CreateCaptureRequestJson(payment, transValues[1]);
            authorization = CreateHMAC(apiSecret, token, request, nonce, timestamp);
            var result = SendPostRequest(request, "/" + transValues.FirstOrDefault());
            if (result is null)
            {
                return new PaymentResponse();
            }
            var response = JsonConvert.DeserializeObject<PayeezyCaptureRequestResponse>(result);
            if (response is null)
            {
                return new PaymentResponse();
            }
            if (response.transaction_status != "approved")
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(CaptureAsync)}.Error",
                        message: $"{{request:{request},response:{result}}}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            else
            {
                await Logger.LogInformationAsync(
                        name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(CaptureAsync)}.Success",
                        message: $"{{request:{request},response:{result}}}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return new PaymentResponse
            {
                Approved = response.transaction_status == "approved",
                AuthorizationCode = response.transaction_tag,
                ResponseCode = response.bank_resp_code,
                TransactionID = response.transaction_id,
                Amount = Convert.ToDecimal(response.amount) / 100,
            };
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
            Contract.RequiresNotNull(billing);
            var strOrderNumber = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}";
            if (string.IsNullOrEmpty(payment.Token))
            {
                var request = CreatePaymentRequestJson(payment, billing!, "purchase", "credit_card", strOrderNumber);
                authorization = CreateHMAC(apiSecret, token, request, nonce, timestamp);
                var result = SendPostRequest(request);
                if (result is null)
                {
                    return new PaymentResponse();
                }
                var response = JsonConvert.DeserializeObject<PayeezyTokenPaymentRequestResponse>(result);
                if (response is null)
                {
                    return new PaymentResponse();
                }
                // ReSharper disable once PossibleNullReferenceException
                var amount = response.amount;
                var retVal = new PaymentResponse
                {
                    Approved = response.transaction_status == "approved",
                    AuthorizationCode = response.transaction_tag,
                    ResponseCode = response.gateway_resp_code,
                    Amount = Convert.ToDecimal(amount) / 100,
                };
                if (retVal.Approved)
                {
                    await Logger.LogInformationAsync(
                            name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}.Success",
                            message: $"{{request:{request},response:{result}}}",
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    retVal.TransactionID = CreateTransactionID(response, strOrderNumber);
                }
                else
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}.Error",
                            message: $"{{request:{request},response:{result}}}",
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                return retVal;
            }
            else
            {
                var request = CreatePaymentRequestJson(payment, billing!, "purchase", "token", strOrderNumber);
                if (request is null)
                {
                    return new PaymentResponse();
                }
                authorization = CreateHMAC(apiSecret, token, request, nonce, timestamp);
                var result = SendPostRequest(request);
                if (result is null)
                {
                    return new PaymentResponse();
                }
                var response = JsonConvert.DeserializeObject<PayeezyPaymentRequestResponse>(result);
                if (response is null)
                {
                    return new PaymentResponse();
                }
                // ReSharper disable once PossibleNullReferenceException
                if (response.transaction_status != "approved")
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}.Error",
                            message: $"{{request:{request},response:{result}}}",
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                else
                {
                    await Logger.LogInformationAsync(
                            name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}.Success",
                            message: $"{{request:{request},response:{result}}}",
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                return new PaymentResponse
                {
                    Approved = response.transaction_status == "approved",
                    AuthorizationCode = response.gateway_resp_code,
                    ResponseCode = response.bank_resp_code,
                    TransactionID = response.transaction_id,
                    Amount = Convert.ToDecimal(response.amount) / 100,
                };
            }
        }

        /// <summary>Creates a hmac.</summary>
        /// <param name="apiSecret">The API secret.</param>
        /// <param name="token">    The token.</param>
        /// <param name="payload">  The payload.</param>
        /// <param name="nonce">    The nonce.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <returns>The new hmac.</returns>
        private static string CreateHMAC(
            string apiSecret,
            string token,
            string payload,
            string nonce,
            string timeStamp)
        {
            var hmacData = PayeezyAPIPaymentsProviderConfig.ApiKey + nonce + timeStamp + token + payload;
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            var hex = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(hmacData)));
            hex = hex.Replace("-", string.Empty).ToLower();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(hex));
        }

        /// <summary>Creates payment request JSON.</summary>
        /// <param name="payment">       The payment.</param>
        /// <param name="billing">       The billing.</param>
        /// <param name="transType">     Type of the transaction.</param>
        /// <param name="method">        The method.</param>
        /// <param name="strOrderNumber">The order number.</param>
        /// <returns>The new payment request JSON.</returns>
        private static string CreatePaymentRequestJson(
            IProviderPayment payment,
            IContactModel billing,
            string transType,
            string method,
            string strOrderNumber)
        {
            var strExpirationYear = payment.ExpirationYear.ToString();
            var expirationMonth = payment.ExpirationMonth.ToString();
            // payeezy does not accept amounts with a decimal in them
            var strAmount = Convert.ToInt32(payment.Amount * 100).ToString();
            if (strExpirationYear!.Length > 4)
            {
                strExpirationYear = strExpirationYear[4..];
            }
            else if (strExpirationYear.Length > 2)
            {
                strExpirationYear = strExpirationYear[2..];
            }
            if (!Contract.CheckValidKey(payment.CardHolderName))
            {
                payment.CardHolderName = $"{billing.FirstName} {billing.LastName}";
            }
            if (!Contract.CheckValidKey(payment.CardType))
            {
                payment.CardType = DetermineCardType(payment.CardNumber!);
            }
            if (expirationMonth!.Length < 2)
            {
                expirationMonth = "0" + expirationMonth;
            }
            // Base info
            var request = new PayeezyPaymentRequest
            {
                merchant_ref = strOrderNumber,
                transaction_type = transType,
                method = method,
                amount = strAmount,
                currency_code = "USD",
            };
            if (method == "credit_card")
            {
                // Credit Card Info
                request.credit_card = new()
                {
                    type = payment.CardType,
                    cardholder_name = payment.CardHolderName,
                    card_number = payment.CardNumber,
                    exp_date = $"{expirationMonth}{strExpirationYear}",
                    cvv = payment.CVV,
                };
                if (transType == "purchase")
                {
                    request.partial_redemption = false;
                }
            }
            else
            {
                // Token
                request.token_type = new() { type = payment.CardType };
                request.token_data = new()
                {
                    type = payment.CardType,
                    value = payment.Token,
                    cardholder_name = payment.CardHolderName,
                    exp_date = $"{payment.ExpirationMonth}{strExpirationYear}",
                };
            }
            return JsonConvert.SerializeObject(request);
        }

        /// <summary>Creates capture request JSON.</summary>
        /// <param name="payment"> The payment.</param>
        /// <param name="transTag">The transaction tag.</param>
        /// <returns>The new capture request JSON.</returns>
        private static string CreateCaptureRequestJson(IProviderPayment payment, string transTag)
        {
            var request = new PayeezyCaptureRequest
            {
                merchant_ref = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}",
                transaction_tag = transTag,
                transaction_type = "capture",
                method = "credit_card",
                // payeezy does not accept amounts with a decimal in them
                amount = Convert.ToInt32(payment.Amount * 100).ToString(),
                currency_code = "USD",
            };
            return JsonConvert.SerializeObject(request);
        }

        /// <summary>Creates transaction identifier.</summary>
        /// <param name="response">        The response.</param>
        /// <param name="transMerchantRef">The transaction merchant reference.</param>
        /// <returns>The new transaction identifier.</returns>
        private static string CreateTransactionID(dynamic? response, string transMerchantRef)
        {
            if (response is null)
            {
                return string.Empty;
            }
            return $"{response.transaction_id}|{response.transaction_tag}|{response.card.type}"
                + $"|{response.card.cardholder_name}|{response.token.token_data.value}"
                + $"|{response.card.exp_date}|{transMerchantRef}";
        }

        /// <summary>Determine card type.</summary>
        /// <param name="cardNum">The card number.</param>
        /// <returns>A string.</returns>
        private static string DetermineCardType(string cardNum)
        {
            Dictionary<string, string> cardPatterns = new()
            {
                ["AMEX"] = AMEXPattern,
                ["MasterCard"] = MasterCardPattern,
                ["Visa"] = VisaCardPattern,
                ["DinersClub"] = DinersClubCardPattern,
                ["enRoute"] = EnRouteCardPattern,
                ["Discover"] = DiscoverCardPattern,
                ["JCB"] = JCBCardPattern,
            };
            var cardType = "Unknown";
            try
            {
                foreach (var cardTypeName in cardPatterns.Keys)
                {
                    if (!new Regex(cardPatterns[cardTypeName]).IsMatch(cardNum))
                    {
                        continue;
                    }
                    cardType = cardTypeName;
                    break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return cardType;
        }

        /// <summary>Sends a post request.</summary>
        /// <param name="data">       The data.</param>
        /// <param name="urlFragment">The URL fragment.</param>
        /// <returns>A string.</returns>
        private string? SendPostRequest(string data, string urlFragment = "")
        {
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
            // 1.2+ is the only thing that should be allowed
            try
            {
                var post = (HttpWebRequest)WebRequest.Create(url + urlFragment);
                post.Method = "POST";
                post.KeepAlive = true;
                post.Accept = "*/*";
                post.Headers.Add("Accept-Encoding", "gzip");
                post.Headers.Add("Accept-Language", "en-US");
                post.Headers.Add("apikey", PayeezyAPIPaymentsProviderConfig.ApiKey);
                post.Headers.Add("nonce", nonce);
                post.Headers.Add("timestamp", timestamp);
                post.Headers.Add("Authorization", authorization);
                post.ContentType = "application/json";
                post.Headers.Add("token", token);
                var byteArray = Encoding.UTF8.GetBytes(data);
                post.GetRequestStream().Write(byteArray, 0, byteArray.Length);
                using var response = post.GetResponse();
                Contract.RequiresNotNull(response);
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(Contract.RequiresNotNull(stream));
                return reader.ReadToEnd();
            }
            catch (WebException webEx)
            {
                using var reader = new StreamReader(Contract.RequiresNotNull(webEx.Response!.GetResponseStream()));
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
