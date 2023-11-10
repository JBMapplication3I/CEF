// <copyright file="SagePaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A sage payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class SagePaymentsProvider : IWalletProviderBase
    {
        /// <summary>Gets an array of empty bytes.</summary>
        /// <value>An Array of empty bytes.</value>
        private static byte[] EmptyByteArray { get; } = Array.Empty<byte>();

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var request = new SageEcommerce
            {
                cardData = new()
                {
                    number = payment.CardNumber,
                    expiration = $"{payment.ExpirationMonth!.Value:00}{payment.ExpirationYear!.Value + (payment.ExpirationYear > 200000 ? -200000 : payment.ExpirationYear > 2000 ? -2000 : 0):00}",
                },
            };
            var serialized = JsonConvert.SerializeObject(request);
            try
            {
                var webRequest = CreateRequest("POST", SagePaymentsProviderConfig.TokenUrl!, serialized);
                using var dataStream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false);
                var byteArray = Encoding.ASCII.GetBytes(serialized);
#if NET5_0_OR_GREATER
                await dataStream.WriteAsync(byteArray.AsMemory(0, byteArray.Length)).ConfigureAwait(false);
#else
                await dataStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
#endif
                using var webResponse = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false);
                await Logger.LogInformationAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}",
                        message: webResponse.StatusDescription,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                using var dataStream2 = webResponse.GetResponseStream() ?? throw new InvalidOperationException();
                using var reader = new StreamReader(dataStream2);
                var responseFromServer = await reader.ReadToEndAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<SageWalletResponse>(responseFromServer)
                    .ToPaymentWalletResponse(true);
            }
            catch (WebException wex)
            {
                using var stream = wex.Response!.GetResponseStream() ?? throw new InvalidOperationException();
                using var reader = new StreamReader(stream);
                var response = await reader.ReadToEndAsync().ConfigureAwait(false);
                var errorResponse = JsonConvert.DeserializeObject<SageErrorResponse>(response);
                await Logger.LogErrorAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.Error",
                        message: errorResponse!.message,
                        ex: wex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return errorResponse.ToPaymentWalletResponse();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return ex.ToPaymentWalletResponse();
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            try
            {
                var webRequest = CreateRequest("DELETE", string.Format(SagePaymentsProviderConfig.DeleteUrl!, payment.Token), string.Empty);
                using var dataStream1 = await webRequest.GetRequestStreamAsync().ConfigureAwait(false) ?? throw new InvalidOperationException();
#if NET5_0_OR_GREATER
                await dataStream1.WriteAsync(EmptyByteArray.AsMemory(0, EmptyByteArray.Length)).ConfigureAwait(false);
#else
                await dataStream1.WriteAsync(EmptyByteArray, 0, EmptyByteArray.Length).ConfigureAwait(false);
#endif
                using var webResponse = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false);
                await Logger.LogInformationAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(DeleteCustomerProfileAsync)}",
                        message: webResponse.StatusDescription,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                using var dataStream2 = webResponse.GetResponseStream() ?? throw new InvalidOperationException();
                using var reader = new StreamReader(dataStream2);
                var responseFromServer = await reader.ReadToEndAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<SageWalletResponse>(responseFromServer)
                    .ToPaymentWalletResponse(true);
            }
            catch (WebException wex)
            {
                using var dataStream = wex.Response!.GetResponseStream() ?? throw new InvalidOperationException();
                using var reader = new StreamReader(dataStream);
                var responseFromServer = await reader.ReadToEndAsync().ConfigureAwait(false);
                var errorResponse = JsonConvert.DeserializeObject<SageErrorResponse>(responseFromServer);
                await Logger.LogErrorAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.Error",
                        message: errorResponse!.message,
                        ex: wex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return errorResponse.ToPaymentWalletResponse();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(SagePaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return ex.ToPaymentWalletResponse();
            }
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
    }
}
