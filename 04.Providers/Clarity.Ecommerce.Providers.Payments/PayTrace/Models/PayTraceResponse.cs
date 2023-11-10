// <copyright file="PayTraceResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTraceResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>class to hold temporary Json Response and Error message for any response.</summary>
    public class PayTraceResponse
    {
        /// <summary>Gets or sets the JSON response.</summary>
        /// <value>The JSON response.</value>
        public string? JsonResponse { get; set; }

        /// <summary>Gets or sets a message describing the error.</summary>
        /// <value>A message describing the error.</value>
        public string? ErrorMessage { get; set; }

        /// <summary>Process the transaction.</summary>
        /// <param name="methodUrl">  URL of the method.</param>
        /// <param name="token">      The token.</param>
        /// <param name="requestData">Information describing the request.</param>
        /// <returns>A PayTraceResponse.</returns>
        public static async Task<PayTraceResponse> ProcessTransactionAsync(
            string methodUrl,
            string token,
            string requestData)
        {
            var tempResponse = new PayTraceResponse();
            try
            {
                // Create a request using a URL that can receive a post.
                var request = WebRequest.Create(methodUrl);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // To set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
                ((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";
                // Set the Authorization token
                ((HttpWebRequest)request).Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                var byteArray = Encoding.UTF8.GetBytes(requestData);
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                ServicePointManager.Expect100Continue = true;
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                // 1.2+ is the only thing that should be allowed
                // Get the request stream.
                using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
                {
                    // Write the data to the request stream.
#if NET5_0_OR_GREATER
                    await requestStream.WriteAsync(byteArray).ConfigureAwait(false);
#else
                    await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
#endif
                    // Close the Stream object.
                    requestStream.Close();
                    // To Get the response.
                    using var response = await request.GetResponseAsync().ConfigureAwait(false);
                    // Assuming Response status is OK otherwise catch{} will be executed
                    // Get the stream containing content returned by the server.
                    using var responseStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    using var reader = new StreamReader(responseStream ?? throw new InvalidOperationException());
                    // Read the content.
                    var responseFromServer = await reader.ReadToEndAsync().ConfigureAwait(false);
                    // Assign/store Transaction Json Response to TempResponse Object
                    tempResponse.JsonResponse = responseFromServer;
                }
                return tempResponse;
            }
            catch (WebException e)
            {
                // This exception will be raised if the server didn't return 200 - OK within response.
                // Retrieve more information about the error and API response
                if (e.Response == null)
                {
                    return tempResponse;
                }
                // To retrieve the actual JSON response when any error occurs.
                using (var responseStream = e.Response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using var reader = new StreamReader(responseStream);
                        tempResponse.JsonResponse = await reader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
                // Retrieve http Error
                var err = (HttpWebResponse)e.Response;
                if (err != null)
                {
                    tempResponse.ErrorMessage = (int)err.StatusCode + " " + err.StatusDescription;
                }
            }
            return tempResponse;
        }
    }
}
