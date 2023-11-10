// <copyright file="UnitedTranzactionPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the united tranzaction payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.UnitedTranzaction
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Utilities;

    /// <content>An united tranzaction payments provider.</content>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class UnitedTranzactionPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => UnitedTranzactionPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
        public override async Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            try
            {
#if NET5_0_OR_GREATER
                var template = await File.ReadAllTextAsync(UnitedTranzactionPaymentsProviderConfig.CCCaptureTemplatePath);
#else
                var template = File.ReadAllText(UnitedTranzactionPaymentsProviderConfig.CCCaptureTemplatePath);
#endif
                template = template.Replace("{UserName}", UnitedTranzactionPaymentsProviderConfig.UserName);
                template = template.Replace("{Password}", UnitedTranzactionPaymentsProviderConfig.Password);
                template = template.Replace("{MerchantNo}", UnitedTranzactionPaymentsProviderConfig.MerchantNo);
                template = template.Replace("{Amount}", payment.Amount.ToString());
                template = template.Replace("{PreAuthID}", paymentAuthorizationToken);
                var webRequest = CreateWebRequest(template);
                // write and send request data
#if NET5_0_OR_GREATER
                await using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
#else
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
#endif
                {
                    await streamWriter.WriteAsync(template).ConfigureAwait(false);
                }
                // get response and read into string
                try
                {
                    using var webResponse = (HttpWebResponse)webRequest.GetResponse();
                    string responseString;
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var responseStream = new StreamReader(webResponse.GetResponseStream()))
                    {
                        responseString = await responseStream.ReadToEndAsync().ConfigureAwait(false);
                    }
                    // load xml
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseString);
                    var responseNode = xmlDoc.SelectSingleNode("UTA")?.SelectNodes("RESPONSE")?.Item(0);
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentResponse(responseNode);
                }
                catch (WebException ex)
                {
                    // read stream for remote error response
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentResponse(false, ex.Message);
                }
            }
            catch
            {
                // Do Nothing
            }
            return new PaymentResponse();
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
#if NET5_0_OR_GREATER
                var template = await File.ReadAllTextAsync(UnitedTranzactionPaymentsProviderConfig.CCAuthTemplatePath);
#else
                var template = File.ReadAllText(UnitedTranzactionPaymentsProviderConfig.CCAuthTemplatePath);
#endif
                template = BasicReplace(template, payment, billing!);
                var webRequest = CreateWebRequest(template);
                // Write and send request data
#if NET5_0_OR_GREATER
                await using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
#else
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
#endif
                {
                    await streamWriter.WriteAsync(template).ConfigureAwait(false);
                }
                // Get response and read into string
                try
                {
                    using var webResponse = (HttpWebResponse)webRequest.GetResponse();
                    string responseString;
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var responseStream = new StreamReader(webResponse.GetResponseStream()))
                    {
                        responseString = await responseStream.ReadToEndAsync().ConfigureAwait(false);
                    }
                    // load xml
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseString);
                    var responseNode = xmlDoc.SelectSingleNode("UTA")?.SelectNodes("RESPONSE")?.Item(0);
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentResponse(responseNode);
                }
                catch (WebException ex)
                {
                    // Read stream for remote error response
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentResponse(false, ex.Message);
                }
            }
            catch
            {
                // Do Nothing
            }
            return new PaymentResponse();
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
            try
            {
#if NET5_0_OR_GREATER
                var template = await File.ReadAllTextAsync(
#else
                var template = File.ReadAllText(
#endif
                    string.IsNullOrEmpty(payment.Token)
                        ? UnitedTranzactionPaymentsProviderConfig.CCTemplatePath
                        : UnitedTranzactionPaymentsProviderConfig.CCTokenTemplatePath);
                template = BasicReplace(template, payment, billing!);
                var webRequest = CreateWebRequest(template);
                // write and send request data
#if NET5_0_OR_GREATER
                await using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
#else
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
#endif
                {
                    await streamWriter.WriteAsync(template).ConfigureAwait(false);
                }
                // get response and read into string
                try
                {
                    using var webResponse = (HttpWebResponse)webRequest.GetResponse();
                    string responseString;
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var responseStream = new StreamReader(webResponse.GetResponseStream()))
                    {
                        responseString = await responseStream.ReadToEndAsync().ConfigureAwait(false);
                    }
                    // load xml
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseString);
                    var responseNode = xmlDoc.SelectSingleNode("UTA")?.SelectNodes("RESPONSE")?.Item(0);
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentResponse(responseNode);
                }
                catch (WebException ex)
                {
                    // read stream for remote error response
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentResponse(false, ex.Message);
                }
            }
            catch
            {
                // Do Nothing
            }
            return new PaymentResponse();
        }

        /// <summary>Basic replace.</summary>
        /// <param name="template">The template.</param>
        /// <param name="payment"> The payment.</param>
        /// <param name="billing"> The billing.</param>
        /// <returns>A string.</returns>
        private static string BasicReplace(string template, IProviderPayment payment, IContactModel billing)
        {
            var expYear = (payment.ExpirationYear ?? 0).ToString().Length == 4
                ? (payment.ExpirationYear ?? 0).ToString()[2..]
                : (payment.ExpirationYear ?? 0).ToString("00");
            template = template.Replace("{UserName}", UnitedTranzactionPaymentsProviderConfig.UserName);
            template = template.Replace("{Password}", UnitedTranzactionPaymentsProviderConfig.Password);
            template = template.Replace("{MerchantNo}", UnitedTranzactionPaymentsProviderConfig.MerchantNo);
            template = template.Replace("{CustomerID}", Guid.NewGuid().ToString()); // TBD
            template = template.Replace("{TransactionDate}", DateExtensions.GenDateTime.ToString("dd/MM/yyyy"));
            template = template.Replace("{CreditCardNumber}", payment.CardNumber);
            template = template.Replace("{ExpMonth}", (payment.ExpirationMonth ?? 0).ToString("00"));
            template = template.Replace("{ExpYear}", expYear);
            template = template.Replace("{CVV}", payment.CVV);
            template = template.Replace("{Amount}", payment.Amount.ToString());
            template = template.Replace("{CardHolderName}", payment.CardHolderName);
            template = template.Replace("{CCType}", payment.CardType);
            template = template.Replace("{InvoiceNo}", "11"); // TBD
            template = template.Replace("{BillingStreet1}", billing?.Address?.Street1);
            template = template.Replace("{BillingCity}", billing?.Address?.City);
            template = template.Replace("{BillingStateCode}", billing?.Address?.RegionCode);
            template = template.Replace("{BillingZipCode}", billing?.Address?.PostalCode);
            template = template.Replace("{Pin}", "123132131231"); // TBD
            template = template.Replace("{EncryptedTrack}", "sdfsfsdfsfs"); // TBD
            template = template.Replace("{Token}", payment.Token);
            return template;
        }

        /// <summary>Creates web request.</summary>
        /// <param name="xmlString">The XML string.</param>
        /// <returns>The new web request.</returns>
        private static HttpWebRequest CreateWebRequest(string xmlString)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(UnitedTranzactionPaymentsProviderConfig.URL!);
            ServicePointManager.Expect100Continue = false;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/xml";
            webRequest.Accept = "*/*";
            webRequest.ContentLength = xmlString.Length;
            return webRequest;
        }
    }
}
