// <copyright file="BridgePayPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bridge pay payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BridgePay
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;

    /// <summary>A bridge pay payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public class BridgePayPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BridgePayPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
            var stringBuilder = new StringBuilder();
            var priceStr = payment.Amount?.ToString("#.##") ?? "0";
            var price = (int)(decimal.Parse(priceStr) * 100);
            var paymentYear = payment.ExpirationYear.ToString();
            paymentYear = paymentYear!.Length == 4 ? paymentYear[2..] : paymentYear;
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                using var xmlWriter = new XmlTextWriter(stringWriter);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartElement("requestHeader");
                {
                    xmlWriter.WriteElementString("ClientIdentifier", "SOAP");
                    xmlWriter.WriteElementString("RequestType", "004");
                    xmlWriter.WriteElementString("RequestDateTime", DateExtensions.GenDateTime.ToString("yyyyMMddhhmmss"));
                    xmlWriter.WriteElementString("User", BridgePayPaymentsProviderConfig.Login);
                    xmlWriter.WriteElementString("Password", BridgePayPaymentsProviderConfig.Password);
                    xmlWriter.WriteStartElement("requestMessage");
                    {
                        xmlWriter.WriteElementString("MerchantCode", BridgePayPaymentsProviderConfig.MerchantCode);
                        xmlWriter.WriteElementString("MerchantAccountCode", BridgePayPaymentsProviderConfig.MerchantAccountCode);
                        xmlWriter.WriteElementString("PaymentAccountNumber", payment.CardNumber);
                        xmlWriter.WriteElementString("ExpirationDate", $"{payment.ExpirationMonth}{paymentYear}");
                        xmlWriter.WriteElementString("SecurityCode", payment.CVV);
                        xmlWriter.WriteElementString("Amount", price.ToString());
                        xmlWriter.WriteElementString("TransactionType", "sale");
                        xmlWriter.WriteElementString("TransIndustryType", "EC");
                        xmlWriter.WriteElementString("TransCatCode", "B");
                        xmlWriter.WriteElementString("AcctType", "R");
                        xmlWriter.WriteElementString("HolderType", "P");
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            var request = stringBuilder.ToString();
            var bytes = Encoding.UTF8.GetBytes(request);
            var request64 = Convert.ToBase64String(bytes);
            try
            {
                string ret64;
                if (ProviderMode == Enums.PaymentProviderMode.Production)
                {
                    using var client = new BridgePayProdService.RequestHandlerClient();
                    ret64 = await client.ProcessRequestAsync(request64).ConfigureAwait(false);
                }
                else
                {
                    using var client = new BridgePayTestService.RequestHandlerClient();
                    ret64 = await client.ProcessRequestAsync(request64).ConfigureAwait(false);
                }
                var base64EncodedBytes = Convert.FromBase64String(ret64);
                var ret = Encoding.UTF8.GetString(base64EncodedBytes);
                return BridgePayPaymentsProviderExtensions.ToPaymentResponse(ret);
            }
            catch (WebException ex)
            {
                var resp = await new StreamReader(ex.Response?.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEndAsync().ConfigureAwait(false);
                return BridgePayPaymentsProviderExtensions.ToPaymentResponse(resp, false);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return BridgePayPaymentsProviderExtensions.ToPaymentResponse(message, false);
            }
        }
    }
}
