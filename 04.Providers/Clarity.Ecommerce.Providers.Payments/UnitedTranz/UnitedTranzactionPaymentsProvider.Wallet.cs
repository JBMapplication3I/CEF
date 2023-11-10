// <copyright file="UnitedTranzactionPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the united tranzaction payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.UnitedTranzaction
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;

    /// <content>An united tranzaction payments provider.</content>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class UnitedTranzactionPaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            try
            {
#if NET5_0_OR_GREATER
                var template = await File.ReadAllTextAsync(UnitedTranzactionPaymentsProviderConfig.AddCustomerProfileTemplatePath);
#else
                var template = File.ReadAllText(UnitedTranzactionPaymentsProviderConfig.AddCustomerProfileTemplatePath);
#endif
                template = template.Replace("{UserName}", UnitedTranzactionPaymentsProviderConfig.UserName);
                template = template.Replace("{Password}", UnitedTranzactionPaymentsProviderConfig.Password);
                template = template.Replace("{MerchantNo}", UnitedTranzactionPaymentsProviderConfig.MerchantNo);
                template = template.Replace("{CustomerID}", Guid.NewGuid().ToString()); // TBD -- Needs to be different
                template = template.Replace("{FirstName}", billing.FirstName);
                template = template.Replace("{LastName}", billing.LastName);
                template = template.Replace("{CompanyName}", billing.AddressKey);
                template = template.Replace("{CreditCardNumber}", payment.CardNumber);
                template = template.Replace("{ExpMonth}", (payment.ExpirationMonth ?? 0).ToString("00"));
                template = template.Replace("{ExpYear}", (payment.ExpirationYear ?? 0).ToString("00"));
                template = template.Replace("{CVV}", payment.CVV);
                template = template.Replace("{CardHolderName}", payment.CardHolderName);
                template = template.Replace("{CCType}", payment.CardType);
                var webRequest = CreateWebRequest(template);
                // write and send request data
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
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
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentWalletResponse(responseNode);
                }
                catch (WebException ex)
                {
                    // Read stream for remote error response
                    return UnitedTranzactionPaymentsProviderExtensions.ToPaymentWalletResponse(false, ex.Message);
                }
            }
            catch
            {
                // Do Nothing
            }
            return UnitedTranzactionPaymentsProviderExtensions.ToPaymentWalletResponse(false, "The result was null");
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment, IContactModel billing, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment, string? contextProfileName)
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
    }
}
