// <copyright file="PayeezyPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payeezy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;

    /// <summary>A payeezy gateway.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class PayeezyPaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            var stringBuilder = new StringBuilder();
            using var stringWriter = new StringWriter(stringBuilder);
            using var xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
            // build XML string
            xmlWriter.WriteStartElement("Transaction");
            xmlWriter.WriteElementString("ExactID", PayeezyPaymentsProviderConfig.ExactID); // Gateway ID
            xmlWriter.WriteElementString("Password", PayeezyPaymentsProviderConfig.Password); // Password
            xmlWriter.WriteElementString("Transaction_Type", TypePreAuthorizationOnly);
            xmlWriter.WriteElementString("DollarAmount", "0");
            xmlWriter.WriteElementString("Expiry_Date", $"{payment.ExpirationMonth}{payment.ExpirationYear}");
            xmlWriter.WriteElementString("CardHoldersName", payment.CardHolderName);
            xmlWriter.WriteElementString("Card_Number", payment.CardNumber);
            xmlWriter.WriteEndElement();
            var xmlString = stringBuilder.ToString();
            return Task.FromResult(SendWalletTransaction(xmlString));
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Sends a wallet transaction.</summary>
        /// <param name="xmlString">The XML string.</param>
        /// <returns>A PayeezyWalletResponse.</returns>
        private static IPaymentWalletResponse SendWalletTransaction(string xmlString)
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
                using (var responseStream = new StreamReader(webResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    responseString = responseStream.ReadToEnd();
                }
                // load xml
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseString);
                var nodeList = xmlDoc.SelectSingleNode("TransactionResult")!;
                return PayeezyPaymentsProviderExtensions.ToPaymentWalletResponse(nodeList);
            }
            catch (WebException ex)
            {
                // read stream for remote error response
                return PayeezyPaymentsProviderExtensions.ToPaymentWalletResponse(false, ex.Message);
            }
        }
    }
}
