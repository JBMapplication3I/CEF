// <copyright file="CardConnectPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CardConnect payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.CardConnect
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Newtonsoft.Json;
    using ServiceStack;
    using Utilities;

    /// <summary>A CardConnect payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class CardConnectPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => CardConnectPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
        public override async Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(billing);
            try
            {
                CardConnectAuthorizationRequest request = new()
                {
                    MerchantId = CardConnectPaymentsProviderConfig.MerchantId,
                    Account = payment.CardNumber,
                    Cvv2 = payment.CVV,
                    ExpiryMMYY = payment.ExpirationMonth?.ToString("00")
                        + (payment.ExpirationYear.ToString()!.Length > 2
                            ? payment.ExpirationYear.ToString()!
                                .Substring(payment.ExpirationYear.ToString()!.Length - 2, 2)
                            : payment.ExpirationYear.ToString()),
                    AmountInCents = payment.Amount.HasValue ? (payment.Amount * 100).ToString() : null,
                    AccountHolderName = payment.CardHolderName,
                    Address = billing!.Address!.Street1,
                    Address2 = billing.Address.Street2,
                    City = billing.Address.City,
                    Region = billing.Address.RegionCode,
                    Country = billing.Address.CountryCode == "USA" ? "US" : billing.Address.CountryCode,
                    PostalCode = string.IsNullOrEmpty(payment.Zip)
                        ? billing.Address.PostalCode
                        : payment.Zip,
                    Phone = billing.Phone1,
                    Email = billing.Email1,
                    PaymentOrigin = PaymentOrigin.Ecommerce,
                };
                var result = await SendRequestAsync("POST", JsonConvert.SerializeObject(request), "auth").ConfigureAwait(false);
                var response = result is null ? null : JsonConvert.DeserializeObject<CardConnectAuthorizationResponse>(result);
                return CardConnectPaymentsProviderExtensions.ToPaymentResponse(response);
            }
            catch (Exception ex)
            {
                var response = new CardConnectAuthorizationResponse
                {
                    ResponseStatus = ApprovalStatus.Declined,
                    ResponseText = ex.Message,
                };
                return CardConnectPaymentsProviderExtensions.ToPaymentResponse(response);
            }
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> CaptureAsync(
            string authorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            var request = new CardConnectCaptureRequest
            {
                MerchantId = CardConnectPaymentsProviderConfig.MerchantId,
                RetrievalReference = authorizationToken,
            };
            var result = await SendRequestAsync("POST", JsonConvert.SerializeObject(request), "capture").ConfigureAwait(false);
            var response = result is null ? null : JsonConvert.DeserializeObject<CardConnectCaptureResponse>(result);
            return CardConnectPaymentsProviderExtensions.ToPaymentResponse(response);
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
            var authorizeResponse = await AuthorizeAsync(
                    payment: payment,
                    billing: billing,
                    shipping: shipping,
                    paymentAlreadyConverted: paymentAlreadyConverted,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!authorizeResponse.Approved)
            {
                return authorizeResponse;
            }
            return await CaptureAsync(
                    authorizationToken: Contract.RequiresValidKey(authorizeResponse.TransactionID),
                    payment: payment,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Sends a post request.</summary>
        /// <param name="httpMethod"> The HttpMethod.</param>
        /// <param name="data">       The data.</param>
        /// <param name="urlFragment">The URL fragment.</param>
        /// <returns>A string.</returns>
        private static async Task<string?> SendRequestAsync(string httpMethod, string data, string urlFragment)
        {
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
            // 1.2+ is the only thing that should be allowed
            try
            {
                var baseUrl = CardConnectPaymentsProviderConfig.TestMode
                    ? CardConnectPaymentsProviderConfig.TestUrl
                    : CardConnectPaymentsProviderConfig.ProdUrl;
                var url = baseUrl!;
                if (!url.EndsWith("/"))
                {
                    url += "/";
                }
                url += urlFragment;
                var post = (HttpWebRequest)WebRequest.Create(url);
                post.Method = httpMethod;
                post.KeepAlive = true;
                post.AddBasicAuth(CardConnectPaymentsProviderConfig.Username, CardConnectPaymentsProviderConfig.Password);
                post.ContentType = "application/json";
                var byteArray = Encoding.UTF8.GetBytes(data);
#if NET5_0_OR_GREATER
                await (await post.GetRequestStreamAsync()).WriteAsync(byteArray.AsMemory(0, byteArray.Length)).ConfigureAwait(false);
                using var response = await post.GetResponseAsync().ConfigureAwait(false);
                await using var stream = Contract.RequiresNotNull(response).GetResponseStream();
#else
                await post.GetRequestStream().WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
                using var response = await post.GetResponseAsync().ConfigureAwait(false);
                using var stream = Contract.RequiresNotNull(response).GetResponseStream();
#endif
                using var reader = new StreamReader(Contract.RequiresNotNull(stream));
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            catch (WebException webEx)
            {
                using var reader = new StreamReader(Contract.RequiresNotNull(webEx.Response!.GetResponseStream()));
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
