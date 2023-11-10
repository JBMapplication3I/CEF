// <copyright file="EvoPaymentProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo payment provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Ecommerce;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>An EVO payments provider extensions.</summary>
    public class EvoPaymentProviderExtensions
    {
        /// <summary>Gets URL of PayFabric.</summary>
        /// <value>The PayFabric URL.</value>
        protected virtual string EvoUrl { get; } = EvoPaymentProviderConfig.TestMode
            ? "https://sandbox.payfabric.com/payment/api"
            : "https://evo.com"; // TODO: Production Url

        public virtual bool IsACH(IProviderPayment payment)
        {
            return Contract.CheckAllValidKeys(
                payment.CardHolderName,
                payment.CardType,
                payment.AccountNumber,
                payment.RoutingNumber,
                payment.BankName);
        }

        public virtual EvoPaymentProviderParameters InfoToCardParameters(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            string? expDate)
        {
            EvoPaymentProviderParameters creditCardOrACH = IsACH(payment)
                ? new()
                {
                    Tender = "A",
                    AccountType = payment.CardType!.Equals("Checking") ? "C" : "S",
                    AccountHolderName = payment.CardHolderName,
                    ABA = payment.RoutingNumber,
                    Account = payment.AccountNumber,
                    Amount = $"{payment.Amount:0.00}",
                    Comment1 = payment.PurchaseOrderNumber,
                }
                : new()
                {
                    Tender = "C",
                    Account = payment.CardNumber,
                    ExpirationDate = expDate,
                    Amount = $"{payment.Amount:0.00}",
                    Comment1 = payment.PurchaseOrderNumber,
                    CVV2 = payment.CVV,
                };
            if (billing != null)
            {
                creditCardOrACH.BillToFirstName = billing.FirstName;
                creditCardOrACH.BillToLastName = billing.LastName;
                if (billing.Address != null)
                {
                    creditCardOrACH.BillToStreet = billing.Address.Street1;
                    creditCardOrACH.BillToStreet2 = billing.Address.Street2;
                    creditCardOrACH.BillToCity = billing.Address.City;
                    creditCardOrACH.BillToState = billing.Address.RegionCode;
                    creditCardOrACH.BillToZip = billing.Address.PostalCode;
                    creditCardOrACH.BillToCountry = billing.Address.CountryCode;
                }
            }
            if (shipping == null)
            {
                return creditCardOrACH;
            }
            creditCardOrACH.ShipToFirstName = shipping.FirstName;
            creditCardOrACH.ShipToLastName = shipping.LastName;
            if (shipping.Address == null)
            {
                return creditCardOrACH;
            }
            creditCardOrACH.ShipToStreet = shipping.Address.Street1;
            creditCardOrACH.ShipToCity = shipping.Address.City;
            creditCardOrACH.ShipToState = shipping.Address.RegionCode;
            creditCardOrACH.ShipToZip = shipping.Address.PostalCode;
            creditCardOrACH.ShipToCountry = shipping.Address.CountryCode;
            return creditCardOrACH;
        }

        /// <summary>Information to refund parameters.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="amount"> The amount.</param>
        /// <param name="expDate">The exponent date.</param>
        /// <returns>The EvoPaymentProviderParameters.</returns>
        public virtual EvoPaymentProviderParameters InfoToRefundParameters(
            IProviderPayment payment,
            decimal? amount,
            string? expDate)
        {
            return IsACH(payment)
                ? new()
                {
                    Tender = "A",
                    AccountType = payment.CardType == "Checking" ? "C" : "S",
                    AccountHolderName = payment.CardHolderName,
                    ABA = payment.RoutingNumber,
                    Account = payment.AccountNumber,
                    Amount = $"{amount ?? payment.Amount:0.00}",
                }
                : new()
                {
                    Tender = "C",
                    Account = payment.CardNumber,
                    ExpirationDate = expDate,
                    Amount = $"{amount ?? payment.Amount:0.00}",
                };
        }

        public virtual EvoPaymentProviderParameters InfoToWalletParameters(
            IProviderPayment payment,
            IContactModel? billing,
            string? expDate)
        {
            var isACH = IsACH(payment);
            EvoPaymentProviderParameters creditCardOrACH = isACH
                ? new()
                {
                    Tender = "A",
                    AccountType = payment.CardType == "Checking" ? "C" : "S",
                    AccountHolderName = payment.CardHolderName,
                    ABA = payment.RoutingNumber,
                    Account = payment.CardNumber,
                }
                : new()
                {
                    Tender = "C",
                    Account = payment.CardNumber,
                    ExpirationDate = expDate,
                };
            if (billing == null)
            {
                return creditCardOrACH;
            }
            creditCardOrACH.BillToFirstName = billing.FirstName;
            creditCardOrACH.BillToLastName = billing.LastName;
            if (billing.Address == null)
            {
                return creditCardOrACH;
            }
            creditCardOrACH.BillToStreet = billing.Address.Street1;
            creditCardOrACH.BillToStreet2 = billing.Address.Street2;
            creditCardOrACH.BillToCity = billing.Address.City;
            creditCardOrACH.BillToState = billing.Address.RegionCode;
            creditCardOrACH.BillToZip = billing.Address.PostalCode;
            creditCardOrACH.BillToCountry = billing.Address.CountryCode;
            return creditCardOrACH;
        }

        public virtual IPaymentWalletResponse ToPaymentWalletResponse(
            bool approved,
            string customerProfileId,
            string responseCode)
        {
            return new PaymentWalletResponse
            {
                Approved = approved,
                Token = customerProfileId,
                ResponseCode = responseCode,
            };
        }

        public virtual async Task<IPaymentResponse> GetAuthorizationTokenAsync(
            string authKey,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/token/create";
            var httpWebRequest = HttpWebGetRequest(url, authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<PayFabricAuthenticationToken>(result);
            PaymentResponse response = new()
            {
                Approved = false,
                ResponseCode = null,
                AuthorizationCode = null,
                TransactionID = null,
            };
            if (obj != null)
            {
                response.Approved = true;
                response.AuthorizationCode = obj.Token;
            }
            return response;
        }

        public virtual async Task<string> GetAccountTransactionAsync(
            string authKey,
            string transactionID,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/transaction/{transactionID}";
            var httpWebRequest = HttpWebGetRequest(url, authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<PayFabricGetTransactionResponse[]>(result);
            return JsonConvert.SerializeObject(obj);
        }

        public virtual async Task<string> GetAccountAddressesAsync(
            string authKey,
            string account,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/addresses/{account}";
            var httpWebRequest = HttpWebGetRequest(url, authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<PayFabricAddress[]>(result);
            return JsonConvert.SerializeObject(obj);
        }

        public virtual async Task<IPaymentResponse?> RefundCustomerAsync(
            string authKey,
            PayFabricTransactionRequest model,
            string? contextProfileName,
            ILogger logger)
        {
            model.Type = "Credit";
            var url = $"{EvoUrl}/transaction/process?cvc=111";
            var jsonModel = JsonConvert.SerializeObject(model);
            var data = Encoding.UTF8.GetBytes(jsonModel);
            var httpWebRequest = HttpWebPostRequest(url, authKey, data);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<PayFabricTransactionResponse>(result);
            if (obj is null)
            {
                return null;
            }
            return new PaymentResponse
            {
                Approved = obj.Status == "Approved",
                ResponseCode = obj.Message,
            };
        }

        public virtual async Task<IPaymentWalletResponse?> GetAccountWalletAsync(
            string authKey,
            string account,
            bool getCreditCard,
            string? contextProfileName,
            ILogger logger)
        {
            var tender = "CreditCard";
            if (!getCreditCard)
            {
                tender = "ECheck";
            }
            var url = $"{EvoUrl}/wallet/get/{account}?tender={tender}";
            var httpWebRequest = HttpWebGetRequest(url, authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var list = JsonConvert.DeserializeObject<PayFabricWalletRequest[]>(result);
            if (list is null)
            {
                return null;
            }
            var wallet = new PaymentWalletResponse();
            foreach (var obj in list)
            {
                if (!obj.IsDefaultCard)
                {
                    continue;
                }
                wallet.Approved = true;
                wallet.CardType = tender;
                wallet.Token = obj.GatewayToken;
            }
            return wallet;
        }

        public virtual async Task<PayFabricGatewayAccountProfile?> GetAccountProfileAsync(
            string authKey,
            string profileType,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/setupid";
            var httpWebRequest = HttpWebGetRequest(url, authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var list = JsonConvert.DeserializeObject<PayFabricGatewayAccountProfile[]>(result);
            if (list is null)
            {
                return null;
            }
            foreach (var obj in list)
            {
                switch (obj.CardClass)
                {
                    case "Credit":
                    {
                        return obj;
                    }
                    case "ECheck":
                    {
                        return obj;
                    }
                }
            }
            return null;
        }

        public virtual async Task<string?> GetAccountProfileGatewayAsync(
            string authKey,
            string profileType,
            string? contextProfileName,
            ILogger logger)
        {
            var httpWebRequest = HttpWebGetRequest($"{EvoUrl}/setupid", authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var accountProfiles = JsonConvert.DeserializeObject<PayFabricGatewayAccountProfile[]>(result);
            return accountProfiles!.FirstOrDefault()?.Name;
        }

        public virtual async Task<IPaymentWalletResponse> CreateWalletAsync(
            PayFabricWalletRequest model,
            bool createCreditCard,
            string authKey,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/wallet/create";
            model.Tender = createCreditCard ? "CreditCard" : "ECheck";
            var jsonModel = JsonConvert.SerializeObject(
                model,
                SerializableAttributesDictionaryExtensions.JsonSettings);
            var data = Encoding.UTF8.GetBytes(jsonModel);
            var httpWebRequest = HttpWebPostRequest(url, authKey, data);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            // Parse response
            var obj = JsonConvert.DeserializeObject<PayFabricWalletResponseResult>(result);
            return new PaymentWalletResponse { Approved = obj?.Result != null, Token = obj?.Result };
        }

        public virtual async Task<IPaymentResponse> CreatePaymentTransactionAsync(
            PayFabricTransactionRequest model,
            string authKey,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/transaction/create";
            var jsonModel = JsonConvert.SerializeObject(model);
            var data = Encoding.UTF8.GetBytes(jsonModel);
            var httpWebRequest = HttpWebPostRequest(url, authKey, data);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<PayFabricResponseKey>(result);
            return new PaymentResponse
            {
                TransactionID = obj?.Key,
            };
        }

        public virtual async Task<IPaymentResponse> CreateAndProcessPaymentTransactionAsync(
            PayFabricTransactionRequest model,
            string authKey,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/transaction/process?cvc=111";
            var jsonModel = JsonConvert.SerializeObject(model);
            var data = Encoding.UTF8.GetBytes(jsonModel);
            var httpWebRequest = HttpWebPostRequest(url, authKey, data);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<PayFabricTransactionResponse>(result);
            return new PaymentResponse
            {
                Approved = obj?.Status == "Approved",
                ResponseCode = obj?.ResultCode,
                TransactionID = obj?.TrxKey,
                AuthorizationCode = obj?.AuthCode,
            };
        }

        public virtual async Task<IPaymentWalletResponse> RemoveWalletAsync(
            string token,
            string authKey,
            string? contextProfileName,
            ILogger logger)
        {
            var url = $"{EvoUrl}/wallet/delete/{token}";
            var httpWebRequest = HttpWebGetRequest(url, authKey);
            var result = await GetHttpWebResponseAsStringAsync(httpWebRequest).ConfigureAwait(false);
            // Parse response
            var obj = JsonConvert.DeserializeObject<PayFabricWalletResponseResult>(result);
            return new PaymentWalletResponse { Approved = obj?.Result != null && obj.Result == "True" };
        }

        public virtual async Task<List<IPaymentWalletResponse>> GetAccountWalletsAsync(
            string authKey,
            string account,
            bool getCreditCard,
            string? contextProfileName,
            ILogger logger)
        {
            var tender = "CreditCard";
            if (!getCreditCard)
            {
                tender = "ECheck";
            }
            var result = await GetHttpWebResponseAsStringAsync(
                    HttpWebGetRequest($"{EvoUrl}/wallet/get/{account}?tender={tender}", authKey))
                .ConfigureAwait(false);
            return JsonConvert.DeserializeObject<PayFabricWalletRequest[]>(result)!
                .Select(x => new PaymentWalletResponse
                {
                    Approved = true,
                    CardType = x.CardName,
                    Token = x.ID,
                    Account = x.Account,
                    ExpDate = x.ExpDate,
                    Customer = x.Customer,
                })
                .Cast<IPaymentWalletResponse>()
                .ToList();
        }

        /// <summary>HTTP web get request.</summary>
        /// <param name="url">    URL of the document.</param>
        /// <param name="authKey">The authentication key.</param>
        /// <returns>A HttpWebRequest.</returns>
        protected virtual HttpWebRequest HttpWebGetRequest(
            string url,
            string authKey)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers["authorization"] = authKey;
            httpWebRequest.Method = "GET";
            return httpWebRequest;
        }

        /// <summary>HTTP web post request.</summary>
        /// <param name="url">    URL of the document.</param>
        /// <param name="authKey">The authentication key.</param>
        /// <param name="data">   The data.</param>
        /// <returns>A HttpWebRequest.</returns>
        protected virtual HttpWebRequest HttpWebPostRequest(
            string url,
            string authKey,
            byte[] data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Headers["authorization"] = authKey;
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = data.Length;
            using (var stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            return httpWebRequest;
        }

        /// <summary>Gets HTTP web response as string.</summary>
        /// <param name="httpWebRequest">The HTTP web request.</param>
        /// <returns>The HTTP web response as string.</returns>
        protected virtual async Task<string> GetHttpWebResponseAsStringAsync(HttpWebRequest httpWebRequest)
        {
            using var httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync().ConfigureAwait(false);
            using var responseStream = httpWebResponse.GetResponseStream() ?? throw new InvalidOperationException();
            using var streamReader = new StreamReader(responseStream);
            return await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
