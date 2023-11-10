// <copyright file="CaptureTransactionGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CaptureTransactionGenerator class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>A capture transaction generator.</summary>
    public class CaptureTransactionGenerator
    {
        /// <summary>Method for building Transaction with Json Request, call the actual transaction execution method and
        /// call for Deserialize Json and Return the object.</summary>
        /// <param name="baseUrl">                  URL of the base.</param>
        /// <param name="token">                    The token.</param>
        /// <param name="captureTransactionRequest">The capture transaction request.</param>
        /// <returns>Returns the PayTraceExternalTransResponse Type.</returns>
        public static async Task<PayTraceExternalTransResponse> CaptureTransactionTransAsync(
            string baseUrl,
            string token,
            CaptureTransactionRequest captureTransactionRequest)
        {
            // Header details are available at Authentication header page.
            const string MethodUrl = PayTracePaymentsProviderConfig.UrlCapture;
            // Converting request into JSON string
            var requestJson = JsonSerializer.GetSerializedString(captureTransactionRequest);
            // Call for actual request and response
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    baseUrl + MethodUrl,
                    token,
                    requestJson)
                .ConfigureAwait(false);
            // Create and assign the deserialized object to appropriate response type
            var payTraceExternalTransResponse = JsonSerializer.DeserializeResponse<PayTraceExternalTransResponse>(tempResponse);
            // Assign the http error if any
            JsonSerializer.AssignError(tempResponse, payTraceExternalTransResponse);
            // Return the Deserialized object
            return payTraceExternalTransResponse;
        }
    }
}
