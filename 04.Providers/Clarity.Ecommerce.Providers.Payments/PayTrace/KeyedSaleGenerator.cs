// <copyright file="KeyedSaleGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the KeyedSaleGenerator class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Method for building Transaction with Json Request,call the actual transaction execution method and
    /// call for Deserialize Json and Return the object.</summary>
    public class KeyedSaleGenerator
    {
        /// <summary>Keyed sale transaction.</summary>
        /// <param name="baseUrl">         URL of the base.</param>
        /// <param name="token">           The token.</param>
        /// <param name="keyedSaleRequest">The keyed sale request.</param>
        /// <returns>A KeyedSaleResponse.</returns>
        public static async Task<KeyedSaleResponse> KeyedSaleTransAsync(
            string baseUrl,
            string token,
            KeyedSaleRequest keyedSaleRequest)
        {
            // Header details are available at Authentication header page.
            var methodUrl = PayTracePaymentsProviderConfig.UrlKeyedSale;
            // Converting request into JSON string
            var requestJson = JsonSerializer.GetSerializedString(keyedSaleRequest);
            // Call for actual request and response
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    baseUrl + methodUrl,
                    token,
                    requestJson)
                .ConfigureAwait(false);
            // Create and assign the deserialized object to appropriate response type
            var keyedSaleResponse = JsonSerializer.DeserializeResponse<KeyedSaleResponse>(tempResponse);
            // Assign the http error
            JsonSerializer.AssignError(tempResponse, keyedSaleResponse);
            // Return the Deserialized object
            return keyedSaleResponse;
        }
    }
}
