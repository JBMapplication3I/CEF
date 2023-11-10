// <copyright file="KeyedAuthorizationGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the KeyedAuthorizationGenerator class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>A keyed authorization generator.</summary>
    public class KeyedAuthorizationGenerator
    {
        /// <summary>Method for building Transaction with Json Request,call the actual transaction execution method and
        /// call for Deserialize Json and Return the object.</summary>
        /// <param name="baseUrl">         URL of the base.</param>
        /// <param name="token">           The token.</param>
        /// <param name="keyedSaleRequest">The keyed sale request.</param>
        /// <returns>Returns the KeyedSaleResponse Type.</returns>
        public static async Task<KeyedSaleResponse> KeyedAuthorizationTransAsync(
            string baseUrl,
            string token,
            KeyedSaleRequest keyedSaleRequest)
        {
            // Header details are available at Authentication header page.
            const string MethodUrl = PayTracePaymentsProviderConfig.UrlKeyedAuthorization;
            // Converting request into JSON string
            var requestJson = JsonSerializer.GetSerializedString(keyedSaleRequest);
            // Call for actual request and response
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    baseUrl + MethodUrl,
                    token,
                    requestJson)
                .ConfigureAwait(false);
            // Create and assign the deserialized object to appropriate response type
            var keyedSaleResponse = JsonSerializer.DeserializeResponse<KeyedSaleResponse>(tempResponse);
            // Assign the http error if any
            JsonSerializer.AssignError(tempResponse, keyedSaleResponse);
            // Return the Deserialized object
            return keyedSaleResponse;
        }
    }
}
