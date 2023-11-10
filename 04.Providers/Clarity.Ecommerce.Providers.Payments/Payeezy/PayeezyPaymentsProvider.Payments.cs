// <copyright file="PayeezyPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payeezy
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Utilities;

    /// <summary>A payeezy gateway.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class PayeezyPaymentsProvider : PaymentsProviderBase
    {
        /// <summary>The type purchase.</summary>
        private const string TypePurchase = "00";

        /// <summary>The type pre authorization.</summary>
        private const string TypePreAuthorization = "01";

        /// <summary>The type pre authorization only.</summary>
        private const string TypePreAuthorizationOnly = "05";

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PayeezyPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets a value indicating whether the test mode.</summary>
        /// <value>true if test mode, false if not.</value>
        private static bool TestMode => ProviderMode == Enums.PaymentProviderMode.Testing;

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
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(payment.Amount);
            Contract.RequiresNotNull(billing);
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                using var xmlWriter = new XmlTextWriter(stringWriter);
                // build XML string
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartElement("Transaction");
                xmlWriter.WriteElementString("ExactID", PayeezyPaymentsProviderConfig.ExactID); // Gateway ID
                xmlWriter.WriteElementString("Password", PayeezyPaymentsProviderConfig.Password); // Password
                xmlWriter.WriteElementString("Transaction_Type", TypePreAuthorization);
                xmlWriter.WriteElementString("DollarAmount", payment.Amount!.Value.ToString("G"));
                xmlWriter.WriteElementString("Expiry_Date", $"{payment.ExpirationMonth}{payment.ExpirationYear}");
                xmlWriter.WriteElementString("CardHoldersName", $"{billing!.FirstName} {billing.LastName}");
                xmlWriter.WriteElementString("Card_Number", payment.CardNumber);
                xmlWriter.WriteEndElement();
            }
            var xmlString = stringBuilder.ToString();
            return Task.FromResult(SendTransaction(xmlString));
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            Contract.RequiresNotNull(payment.Amount);
            Contract.RequiresNotNull(billing);
            StringBuilder stringBuilder = new();
            using var stringWriter = new StringWriter(stringBuilder);
            using var xmlWriter = new XmlTextWriter(stringWriter);
            // build XML string
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartElement("Transaction");
            xmlWriter.WriteElementString("ExactID", PayeezyPaymentsProviderConfig.ExactID); // Gateway ID
            xmlWriter.WriteElementString("Password", PayeezyPaymentsProviderConfig.Password); // Password
            xmlWriter.WriteElementString("Transaction_Type", TypePurchase);
            xmlWriter.WriteElementString("DollarAmount", payment.Amount!.Value.ToString("G"));
            xmlWriter.WriteElementString("Expiry_Date", $"{payment.ExpirationMonth}{payment.ExpirationYear}");
            xmlWriter.WriteElementString("CardHoldersName", $"{billing!.FirstName} {billing.LastName}");
            xmlWriter.WriteElementString("Card_Number", payment.CardNumber);
            if (PayeezyPaymentsProviderConfig.Level3)
            {
                xmlWriter.WriteStartElement("Level3");
                {
                    ////var cart = payment.Cart;
                    xmlWriter.WriteElementString("TaxAmount", "0" /*cart?.Totals?.Tax.ToString("G")*/);
                    xmlWriter.WriteElementString("TaxRate", "0");
                    xmlWriter.WriteElementString("AltTaxAmount", "0");
                    xmlWriter.WriteElementString("AltTaxId", "0");
                    xmlWriter.WriteElementString("DutyAmount", "0");
                    xmlWriter.WriteElementString("FreightAmount", "0");
                    xmlWriter.WriteElementString("DiscountAmount", "0" /*cart?.Totals?.Discounts.ToString("G")*/);
                    xmlWriter.WriteElementString("ShipFromZip", "22182");
                    // Shipping info
                    {
                        xmlWriter.WriteStartElement("ShipToAddress");
                        {
                            xmlWriter.WriteElementString("ShipFromZip", shipping?.Address?.Street1);
                            xmlWriter.WriteElementString("City", shipping?.Address?.City);
                            xmlWriter.WriteElementString("State", shipping?.Address?.RegionCode);
                            xmlWriter.WriteElementString("Country", shipping?.Address?.CountryName);
                            xmlWriter.WriteElementString("CustomerNumber", string.Empty);
                            xmlWriter.WriteElementString("Email", shipping?.Email1);
                            xmlWriter.WriteElementString("Phone", shipping?.Phone1);
                            xmlWriter.WriteElementString("Name", shipping?.FullName);
                            xmlWriter.WriteElementString("Zip", shipping?.Address?.PostalCode);
                        }
                        xmlWriter.WriteEndElement();
                    }
                    // one new element per line item
                    if (cart?.SalesItems != null)
                    {
                        foreach (var item in cart.SalesItems)
                        {
                            xmlWriter.WriteStartElement("LineItem");
                            {
                                xmlWriter.WriteElementString("CommodityCode", "824");
                                xmlWriter.WriteElementString("Description", item.ProductDescription);
                                xmlWriter.WriteElementString("DiscountAmount", "0.00");
                                xmlWriter.WriteElementString("DiscountIndicator", "0"); // bool
                                xmlWriter.WriteElementString("GrossNetIndicator", "1"); // bool
                                xmlWriter.WriteElementString("LineItemTotal", item.UnitCorePrice.ToString("G"));
                                xmlWriter.WriteElementString("ProductCode", item.ProductKey);
                                xmlWriter.WriteElementString("Quantity", item.Quantity.ToString(CultureInfo.InvariantCulture));
                                xmlWriter.WriteElementString("TaxAmount", "0");
                                xmlWriter.WriteElementString("TaxRate", "0");
                                xmlWriter.WriteElementString("TaxType", string.Empty);
                                xmlWriter.WriteElementString("UnitCost", item.UnitCorePrice.ToString("G"));
                                xmlWriter.WriteElementString("UnitOfMeasure", item.UnitOfMeasure);
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            var xmlString = stringBuilder.ToString();
            return Task.FromResult(SendTransaction(xmlString));
        }

        /// <summary>Creates web request.</summary>
        /// <param name="xmlString">The XML string.</param>
        /// <returns>The new web request.</returns>
        private static HttpWebRequest CreateWebRequest(string xmlString)
        {
            var encoder = new ASCIIEncoding();
            var xmlByte = encoder.GetBytes(xmlString);
            var sha1Crypto = new SHA1CryptoServiceProvider();
            var hash = BitConverter.ToString(sha1Crypto.ComputeHash(xmlByte)).Replace("-", string.Empty);
            var hashedContent = hash.ToLower();
            // assign values to hashing and header variables
            var keyID = PayeezyPaymentsProviderConfig.KeyID; // key ID
            var key = PayeezyPaymentsProviderConfig.Hmac!; // Hmac key
            const string Method = "POST\n";
            const string Type = "application/xml"; // REST XML
            var time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            const string URI = "/transaction/v12";
            var hashData = Method + Type + "\n" + hashedContent + "\n" + time + "\n" + URI;
            // hmac sha1 hash with key + hash_data
            var hmacSha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key)); // key
            var hmacData = hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(hashData)); // data
            var base64Hash = Convert.ToBase64String(hmacData);
            var baseUrl = TestMode ? PayeezyPaymentsProviderConfig.TestUrl : PayeezyPaymentsProviderConfig.ProdUrl;
            var url = baseUrl + URI; // DEMO Endpoint
            // begin HttpWebRequest
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = Type;
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("x-gge4-date", time);
            webRequest.Headers.Add("x-gge4-content-sha1", hashedContent);
            webRequest.Headers.Add("Authorization", "GGE4_API " + keyID + ":" + base64Hash);
            webRequest.ContentLength = xmlString.Length;
            return webRequest;
        }

        /// <summary>Sends a transaction.</summary>
        /// <param name="xmlString">The XML string.</param>
        /// <returns>A PayeezyGatewayResponse.</returns>
        private static IPaymentResponse SendTransaction(string xmlString)
        {
            var webRequest = CreateWebRequest(xmlString);
            // write and send request data
            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(xmlString);
            }
            // get response and read into string
            try
            {
                using var webResponse = (HttpWebResponse)webRequest.GetResponse();
                string responseString;
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var responseStream = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseString = responseStream.ReadToEnd();
                }
                // load xml
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseString);
                var nodeList = xmlDoc.SelectSingleNode("TransactionResult")!;
                return PayeezyPaymentsProviderExtensions.ToPaymentResponse(nodeList);
            }
            catch (WebException ex)
            {
                // Read stream for remote error response
                return PayeezyPaymentsProviderExtensions.ToPaymentResponse(false, ex.Message);
            }
        }
    }
}
