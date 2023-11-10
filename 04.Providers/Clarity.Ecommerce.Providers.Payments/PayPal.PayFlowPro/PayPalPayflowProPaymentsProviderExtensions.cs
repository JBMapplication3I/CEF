// <copyright file="PayPalPayflowProPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal payflow pro payments provider extensions class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;
    using PayPalPayflowPro;
    using Utilities;

    /// <summary>A PayPal payflow pro payments provider extensions.</summary>
    public class PayPalPayflowProPaymentsProviderExtensions : IPayPalPayflowProPaymentsProviderExtensions
    {
        /// <summary>Gets URL of PayPal.</summary>
        /// <value>The PayPal URL.</value>
        protected virtual string PayPalUrl { get; } = PayPalPayflowProPaymentsProviderConfig.TestMode
            ? "https://pilot-payflowpro.paypal.com"
            : "https://payflowpro.paypal.com";

        /// <inheritdoc/>
        public virtual bool IsACH(IProviderPayment payment)
        {
            return Contract.CheckAllValidKeys(
                payment.CardHolderName,
                payment.CardType,
                payment.AccountNumber,
                payment.RoutingNumber,
                payment.BankName);
        }

        /// <inheritdoc/>
        public virtual PayflowCreditCardOrACHParameters InfoToCardParameters(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            string? expDate)
        {
            PayflowCreditCardOrACHParameters creditCardOrACH = IsACH(payment)
                ? new()
                {
                    Tender = "A",
                    AccountType = payment.CardType == "Checking" ? "C" : "S",
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

        /// <inheritdoc/>
        public virtual PayflowCreditCardOrACHParameters InfoToRefundParameters(
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

        /// <inheritdoc/>
        public virtual PayflowCreditCardOrACHParameters InfoToWalletParameters(
            IProviderPayment payment,
            IContactModel billing,
            string? expDate)
        {
            var isACH = IsACH(payment);
            PayflowCreditCardOrACHParameters creditCardOrACH = isACH
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

        /// <inheritdoc/>
        public virtual string CreditCardOrACHToRequestBody(PayflowCreditCardOrACHParameters creditCardOrACH)
        {
            var body = DefaultBodyPrefix(creditCardOrACH);
            AddToBodyIfValid(ref body, "TRXTYPE", creditCardOrACH.TransactionType);
            AddToBodyIfValid(ref body, "TENDER", creditCardOrACH.Tender);
            AddToBodyIfValid(ref body, "ACCT", creditCardOrACH.Account);
            AddToBodyIfValid(ref body, "ABA", creditCardOrACH.ABA);
            AddToBodyIfValid(ref body, "ACCTTYPE", creditCardOrACH.AccountType);
            AddToBodyIfValid(ref body, "FIRSTNAME", creditCardOrACH.AccountHolderName);
            AddToBodyIfValid(ref body, "EXPDATE", creditCardOrACH.ExpirationDate);
            AddToBodyIfValid(ref body, "AMT", creditCardOrACH.Amount);
            AddToBodyIfValid(ref body, "COMMENT1", creditCardOrACH.Comment1);
            AddToBodyIfValid(ref body, "COMMENT2", creditCardOrACH.Comment2);
            AddToBodyIfValid(ref body, "CVV2", creditCardOrACH.CVV2);
            AddToBodyIfValid(ref body, "RECURRING", creditCardOrACH.Recurring);
            AddToBodyIfValid(ref body, "SWIPE", creditCardOrACH.Swipe);
            AddToBodyIfValid(ref body, "ORDERID", creditCardOrACH.OrderID);
            AddToBodyIfValid(ref body, "ORIGID", creditCardOrACH.OriginalID);
            AddToBodyIfValid(ref body, "BILLTOFIRSTNAME", creditCardOrACH.BillToFirstName);
            AddToBodyIfValid(ref body, "BILLTOLASTNAME", creditCardOrACH.BillToLastName);
            AddToBodyIfValid(ref body, "BILLTOSTREET", creditCardOrACH.BillToStreet);
            AddToBodyIfValid(ref body, "BILLTOSTREET2", creditCardOrACH.BillToStreet2);
            AddToBodyIfValid(ref body, "BILLTOCITY", creditCardOrACH.BillToCity);
            AddToBodyIfValid(ref body, "BILLTOSTATE", creditCardOrACH.BillToState);
            AddToBodyIfValid(ref body, "BILLTOZIP", creditCardOrACH.BillToZip);
            AddToBodyIfValid(ref body, "BILLTOCOUNTRY", creditCardOrACH.BillToCountry);
            AddToBodyIfValid(ref body, "SHIPTOFIRSTNAME", creditCardOrACH.ShipToFirstName);
            AddToBodyIfValid(ref body, "SHIPTOLASTNAME", creditCardOrACH.ShipToLastName);
            AddToBodyIfValid(ref body, "SHIPTOSTREET", creditCardOrACH.ShipToStreet);
            AddToBodyIfValid(ref body, "SHIPTOCITY", creditCardOrACH.ShipToCity);
            AddToBodyIfValid(ref body, "SHIPTOSTATE", creditCardOrACH.ShipToState);
            AddToBodyIfValid(ref body, "SHIPTOZIP", creditCardOrACH.ShipToZip);
            AddToBodyIfValid(ref body, "SHIPTOCOUNTRY", creditCardOrACH.ShipToCountry);
            return body;
        }

        /// <inheritdoc/>
        public virtual string CreditCardToDelayedCaptureRequestBody(PayflowCreditCardOrACHParameters creditCardOrACH)
        {
            var body = DefaultBodyPrefix(creditCardOrACH);
            AddToBodyIfValid(ref body, "TRXTYPE", creditCardOrACH.TransactionType);
            AddToBodyIfValid(ref body, "TENDER", creditCardOrACH.Tender);
            AddToBodyIfValid(ref body, "ORIGID", creditCardOrACH.OriginalID);
            AddToBodyIfValid(ref body, "AMT", creditCardOrACH.Amount);
            return body;
        }

        /// <inheritdoc/>
        public virtual string CreditCardOrACHToRefundRequestBody(PayflowCreditCardOrACHParameters creditCardOrACH)
        {
            var body = DefaultBodyPrefix(creditCardOrACH);
            AddToBodyIfValid(ref body, "TRXTYPE", creditCardOrACH.TransactionType);
            AddToBodyIfValid(ref body, "TENDER", creditCardOrACH.Tender);
            AddToBodyIfValid(ref body, "ACCT", creditCardOrACH.Account);
            AddToBodyIfValid(ref body, "ABA", creditCardOrACH.ABA);
            AddToBodyIfValid(ref body, "ACCTTYPE", creditCardOrACH.AccountType);
            AddToBodyIfValid(ref body, "FIRSTNAME", creditCardOrACH.AccountHolderName);
            AddToBodyIfValid(ref body, "EXPDATE", creditCardOrACH.ExpirationDate);
            AddToBodyIfValid(ref body, "AMT", creditCardOrACH.Amount);
            AddToBodyIfValid(ref body, "ORIGID", creditCardOrACH.OriginalID);
            return body;
        }

        /// <inheritdoc/>
        public virtual IPaymentResponse PaymentBodyToRequestAndGetResult(
            string requestBody,
            string? contextProfileName,
            ILogger logger)
        {
            var result = GetRawResponseStringFromRequest(requestBody, contextProfileName, logger);
            if (result == null)
            {
                logger.LogInformation(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(PaymentBodyToRequestAndGetResult)}",
                    $"Request:\r\n{requestBody}\r\nResult:\r\nnull\r\n",
                    contextProfileName);
                return new PaymentResponse();
            }
            var response = ToPaymentResponse(result, contextProfileName, logger);
            if (response.Approved)
            {
                logger.LogInformation(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(PaymentBodyToRequestAndGetResult)}",
                    $"Request:\r\n{requestBody}\r\nResult:\r\n{result}\r\nEnd Result:\r\n{JsonConvert.SerializeObject(response)}",
                    contextProfileName);
            }
            else
            {
                logger.LogError(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(PaymentBodyToRequestAndGetResult)}.Error",
                    $"Request:\r\n{requestBody}\r\nResult:\r\n{result}\r\nEnd Result:\r\n{JsonConvert.SerializeObject(response)}",
                    contextProfileName);
            }
            return response;
        }

        /// <inheritdoc/>
        public virtual IPaymentWalletResponse PaymentBodyToWalletRequestAndGetResult(
            string requestBody,
            string? contextProfileName,
            ILogger logger)
        {
            var result = GetRawResponseStringFromRequest(requestBody, contextProfileName, logger);
            if (result == null)
            {
                logger.LogInformation(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(PaymentBodyToWalletRequestAndGetResult)}",
                    $"Request:\r\n{requestBody}\r\nResult:\r\nnull\r\n",
                    contextProfileName);
                return new PaymentWalletResponse();
            }
            var response = ToPaymentWalletResponse(result, contextProfileName, logger);
            if (response.Approved)
            {
                logger.LogInformation(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(PaymentBodyToWalletRequestAndGetResult)}",
                    $"Request:\r\n{requestBody}\r\nResult:\r\n{result}\r\nEnd Result:\r\n{JsonConvert.SerializeObject(response)}",
                    contextProfileName);
            }
            else
            {
                logger.LogError(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(PaymentBodyToWalletRequestAndGetResult)}.Error",
                    $"Request:\r\n{requestBody}\r\nResult:\r\n{result}\r\nEnd Result:\r\n{JsonConvert.SerializeObject(response)}",
                    contextProfileName);
            }
            return response;
        }

        /// <summary>Default body prefix.</summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>A string.</returns>
        protected virtual string DefaultBodyPrefix(PayflowRequestBase creditCard)
        {
            return $"USER={creditCard.User}&VENDOR={creditCard.Vendor}&PARTNER={creditCard.Partner}"
                + $"&PWD={creditCard.Password}&VERBOSITY={creditCard.Verbosity}";
        }

        /// <summary>Converts this PayPalPayflowProPaymentsProviderExtensions to a payment response.</summary>
        /// <param name="result">            The result.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The given data converted to an IPaymentResponse.</returns>
        protected virtual IPaymentResponse ToPaymentResponse(string result, string? contextProfileName, ILogger logger)
        {
            var resultValues = ParseResult(result);
            var approved = resultValues["RESULT"] == ((int)PayPalPayFlowResponseCodes.Approved).ToString();
            if (approved)
            {
                var response = new PaymentResponse
                {
                    Approved = true,
                    ResponseCode = resultValues["RESULT"],
                    AuthorizationCode = resultValues.ContainsKey("AUTHCODE") ? resultValues["AUTHCODE"] : null,
                    TransactionID = resultValues["PNREF"],
                };
                if (resultValues.ContainsKey("AMT") && decimal.TryParse(resultValues["AMT"], out var amount))
                {
                    response.Amount = amount;
                }
                return response;
            }
            var resultCodeWithDescription = resultValues["RESULT"];
            if (Enum.TryParse<PayPalPayFlowResponseCodes>(resultValues["RESULT"], out var code))
            {
                resultCodeWithDescription += " - " + code.Description();
            }
            var responseMessage = resultValues.ContainsKey("RESPMSG")
                ? resultValues["RESPMSG"]
                : "No response message from PayPal";
            var message = $"{resultCodeWithDescription}\r\n{responseMessage}";
            logger.LogError(
                $"{nameof(PayPalPayflowProPaymentsProvider)}.StringResult.{nameof(ToPaymentResponse)}.Error",
                message,
                contextProfileName);
            return new PaymentResponse
            {
                Approved = false,
                ResponseCode = resultValues["RESULT"],
                TransactionID = resultValues.TryGetValue("PNREF", out var transactionID) ? transactionID : null,
                AuthorizationCode = message,
                Amount = 0,
            };
        }

        /// <summary>Converts this PayPalPayflowProPaymentsProviderExtensions to a payment wallet response.</summary>
        /// <param name="result">            The result.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The given data converted to an IPaymentWalletResponse.</returns>
        protected virtual IPaymentWalletResponse ToPaymentWalletResponse(
            string result,
            string? contextProfileName,
            ILogger logger)
        {
            var resultValues = ParseResult(result);
            var approved = resultValues["RESULT"] == ((int)PayPalPayFlowResponseCodes.Approved).ToString();
            if (approved)
            {
                return new PaymentWalletResponse
                {
                    Approved = true,
                    ResponseCode = resultValues["RESULT"],
                    Token = resultValues["PNREF"],
                };
            }
            var resultCodeWithDescription = resultValues["RESULT"];
            if (Enum.TryParse<PayPalPayFlowResponseCodes>(resultValues["RESULT"], out var code))
            {
                resultCodeWithDescription += " - " + code.Description();
            }
            var responseMessage = resultValues.ContainsKey("RESPMSG")
                ? resultValues["RESPMSG"]
                : "No response message from PayPal";
            var message = $"{resultCodeWithDescription}\r\n{responseMessage}";
            logger.LogError(
                $"{nameof(PayPalPayflowProPaymentsProvider)}.StringResult.{nameof(ToPaymentWalletResponse)}.Error",
                message,
                contextProfileName);
            return new PaymentWalletResponse
            {
                Approved = false,
                ResponseCode = resultValues["RESULT"],
                Token = resultValues["PNREF"],
                CardType = message,
            };
        }

        /// <summary>Adds to the body if valid.</summary>
        /// <param name="body">    The body.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">   The value.</param>
        protected virtual void AddToBodyIfValid(ref string body, string? property, string? value)
        {
            if (!Contract.CheckValidKey(property))
            {
                return;
            }
            if (!Contract.CheckValidKey(value))
            {
                return;
            }
            // TODO: Enforce string length limits? Enforce special character usages?
            body += $"&{property}={value}";
        }

        /// <summary>Gets raw response string from request.</summary>
        /// <param name="requestBody">       The request body.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The raw response string from request.</returns>
        protected virtual string? GetRawResponseStringFromRequest(
            string requestBody,
            string? contextProfileName,
            ILogger logger)
        {
            var request = (HttpWebRequest)WebRequest.Create(PayPalUrl);
            request.ContentType = "text/plain";
            request.Method = "POST";
            var buffer = Encoding.UTF8.GetBytes(requestBody);
            request.ContentLength = buffer.Length;
            using (var writer = request.GetRequestStream())
            {
                writer.Write(buffer, 0, buffer.Length);
            }
            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            if (stream == null)
            {
                logger.LogError(
                    $"{nameof(PayPalPayflowProPaymentsProvider)}.{nameof(GetRawResponseStringFromRequest)}.Error",
                    $"Request:\r\n{requestBody}\r\nResult:\r\nNo Response from PayPal\r\n",
                    contextProfileName);
                return null;
            }
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var result = reader.ReadToEnd();
            return result;
        }

        /// <summary>Parse result.</summary>
        /// <param name="result">The result.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        protected virtual Dictionary<string, string> ParseResult(string result)
        {
            var dict = new Dictionary<string, string>();
            var inPropertyName = true;
            var inPropertyValue = false;
            var currentPropertyName = string.Empty;
            var currentPropertyValue = string.Empty;
            var currentPropertyLengthString = string.Empty;
            var currentPropertyLength = 0;
            for (var i = 0; i < result.Length;)
            {
                if (result[i] == '&')
                {
                    if (inPropertyValue)
                    {
                        dict[currentPropertyName] = currentPropertyValue;
                        inPropertyValue = false;
                        // Reset
                        currentPropertyLengthString = string.Empty;
                        currentPropertyLength = 0;
                        currentPropertyName = string.Empty;
                        currentPropertyValue = string.Empty;
                    }
                    inPropertyName = true;
                    i++;
                    continue;
                }
                if (inPropertyName && result[i] == '[')
                {
                    i++; // Skip '['
                    for (var j = 0; i + j < result.Length; j++)
                    {
                        if (result[i + j] == ']')
                        {
                            break;
                        }
                        currentPropertyLengthString += result[i + j];
                    }
                    currentPropertyLength = int.Parse(currentPropertyLengthString);
                    i += currentPropertyLengthString.Length;
                    currentPropertyLengthString = string.Empty;
                    i++; // Skip ']'
                    continue;
                }
                if (inPropertyName && result[i] == '=')
                {
                    inPropertyName = false;
                    i++;
                    continue;
                }
                if (inPropertyName)
                {
                    currentPropertyName += result[i];
                    i++;
                    continue;
                }
                inPropertyValue = true;
                if (currentPropertyLength > 0)
                {
                    currentPropertyValue = result.Substring(i, currentPropertyLength);
                    i += currentPropertyLength;
                    continue;
                }
                currentPropertyValue += result[i];
                i++;
            }
            if (inPropertyValue)
            {
                dict[currentPropertyName] = currentPropertyValue;
            }
            return dict;
        }
    }
}
