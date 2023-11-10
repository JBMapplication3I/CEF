// <copyright file="SagePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A sage payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class SagePaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => SagePaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

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
            Contract.RequiresNotNull(billing);
            var isCreditCard = !Contract.CheckValidKey(payment.AccountNumber)
                && !Contract.CheckValidKey(payment.RoutingNumber)
                && Contract.CheckValidKey(payment.CardNumber);
            var strRequest = isCreditCard
                ? CreateCreditCardJson(payment)
                : CreateECheckJson(payment, billing!);
            var url = isCreditCard
                ? SagePaymentsProviderConfig.CreditCardUrl!
                : SagePaymentsProviderConfig.eCheckUrl!;
            try
            {
                var webRequest = CreateRequest("POST", url, strRequest);
                var byteArray = Encoding.ASCII.GetBytes(strRequest);
                using var dataStream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false);
#if NET5_0_OR_GREATER
                await dataStream.WriteAsync(byteArray.AsMemory(0, byteArray.Length)).ConfigureAwait(false);
#else
                await dataStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
#endif
                using var webResponse = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false);
                using var dataStream2 = webResponse.GetResponseStream() ?? throw new InvalidOperationException();
                using var reader = new StreamReader(dataStream2);
                var responseFromServer = await reader.ReadToEndAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<SagePaymentResponse>(responseFromServer)
                    .ToPaymentResponse();
            }
            catch (WebException wex)
            {
                using var stream = wex.Response!.GetResponseStream() ?? throw new InvalidOperationException();
                using var reader = new StreamReader(stream);
                var value = await reader.ReadToEndAsync().ConfigureAwait(false);
                var errorResponse = JsonConvert.DeserializeObject<SageErrorResponse>(value);
                // ReSharper disable once PossibleNullReferenceException
                await Logger.LogErrorAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}",
                        message: errorResponse!.message,
                        ex: wex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return errorResponse.ToPaymentResponse();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(AuthorizeAndACaptureAsync)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return ex.ToPaymentResponse();
            }
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Creates eCheck JSON.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="billing">The billing.</param>
        /// <returns>The new eCheck JSON.</returns>
        private static string CreateECheckJson(IProviderPayment payment, IContactModel billing)
        {
            var request = new SageECheckRequest
            {
                secCode = "PPD",
                originatorId = "000000000",
                orderNumber = "Order " + DateExtensions.GenDateTime.Millisecond,
                amounts = new()
                {
                    total = 10,
                    tax = 0,
                    shipping = 0,
                },
                account = new()
                {
                    type = "Checking",
                    routingNumber = payment.RoutingNumber,
                    accountNumber = payment.AccountNumber,
                },
                billing = new()
                {
                    Name = new()
                    {
                        first = billing.FirstName,
                        last = billing.LastName,
                    },
                    Address = billing.Address?.Street1,
                    City = billing.Address?.City,
                    State = billing.Address?.RegionCode,
                    PostalCode = billing.Address?.PostalCode,
                    Country = billing.Address?.CountryCode,
                },
                vault = new()
                {
                    operation = "Create",
                },
            };
            return JsonConvert.SerializeObject(request);
        }

        /// <summary>Creates credit card JSON.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>The new credit card JSON.</returns>
        private static string CreateCreditCardJson(IProviderPayment payment)
        {
            Contract.Requires<ArgumentOutOfRangeException>(payment.Amount is > 0);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var request = new SageCreditCardRequest
            {
                ecommerce = new()
                {
                    orderNumber = $"Order {DateExtensions.GenDateTime.Ticks}",
                    amounts = new()
                    {
                        total = payment.Amount!.Value,
                    },
                },
            };
            if (Contract.CheckValidKey(payment.Token))
            {
                request.vault = new()
                {
                    operation = "Read",
                    token = payment.Token,
                };
                return JsonConvert.SerializeObject(request);
            }
            request.ecommerce.cardData = new()
            {
                number = payment.CardNumber,
                expiration = CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value),
            };
            return JsonConvert.SerializeObject(request);
        }

        /// <summary>Creates a request.</summary>
        /// <param name="verb">   The verb.</param>
        /// <param name="url">    URL of the document.</param>
        /// <param name="content">The content.</param>
        /// <returns>The new request.</returns>
        private static HttpWebRequest CreateRequest(string verb, string url, string content)
        {
            var t = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            var timestamp = t.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            var nonce = timestamp;
            // TH - Build the Authorization
            var authToken = verb + url + content + SagePaymentsProviderConfig.MerchantID + nonce + timestamp;
            var hashAuthToken = new HMACSHA512(Encoding.ASCII.GetBytes(SagePaymentsProviderConfig.ClientSecret!))
                .ComputeHash(Encoding.ASCII.GetBytes(authToken));
            var hash64AuthToken = Convert.ToBase64String(hashAuthToken);
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            // TH - Set the headers and details
            var webRequestHeaders = webRequest.Headers;
            webRequestHeaders["clientId"] = SagePaymentsProviderConfig.ClientID;
            webRequestHeaders["merchantId"] = SagePaymentsProviderConfig.MerchantID;
            webRequestHeaders["merchantKey"] = SagePaymentsProviderConfig.MerchantKey;
            webRequestHeaders["nonce"] = nonce;
            webRequestHeaders["timestamp"] = timestamp;
            webRequestHeaders["authorization"] = hash64AuthToken;
            webRequest.Method = verb;
            // TH - Format the request
            var byteArray = Encoding.ASCII.GetBytes(content);
            // TH - Send the data
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = byteArray.Length;
            return webRequest;
        }
    }
}
