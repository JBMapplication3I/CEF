// <copyright file="OAuthTokenGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the OAuthTokenGenerator class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using Models;
    using Newtonsoft.Json;

    /// <summary>An authentication token generator.</summary>
    public class OAuthTokenGenerator
    {
        /// <summary>Type of the grant.</summary>
        private const string GrantType = "password";

        /// <summary>Gets a token.</summary>
        /// <param name="baseUrl"> URL of the base.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token.</returns>
        public static OAuthToken? GetToken(string baseUrl, string userName, string password)
        {
            const string OAuthUrl = PayTracePaymentsProviderConfig.UrlOAuth;
            OAuthToken? oAuthTokenResult = new();
            var tryCount = 0;
            while (tryCount < 3 && oAuthTokenResult!.AccessToken == null)
            {
                try
                {
                    // Create a request using a URL that can receive a post.
                    var request = WebRequest.Create(baseUrl + OAuthUrl);
                    // Set the Method property of the request to POST.
                    request.Method = "POST";
                    // To set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
                    ((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/x-www-form-urlencoded";
                    ServicePointManager.Expect100Continue = true;
                    ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                    ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                    // 1.2+ is the only thing that should be allowed
                    // Create Request data and convert it to a byte array.
                    var requestData = GetFormattedRequest(userName, password);
                    var byteArray = Encoding.UTF8.GetBytes(requestData);
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;
                    // Get the request stream.
                    using var requestStream = request.GetRequestStream();
                    // Write the data to the request stream.
                    requestStream.Write(byteArray, 0, byteArray.Length);
                    // To Get the response.
                    using var response = request.GetResponse();
                    // Assuming Response status is OK otherwise catch{} will be executed
                    // Get the stream containing content returned by the server.
                    using var responseStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    using var reader = new StreamReader(
                        responseStream ?? throw new InvalidOperationException());
                    // Read the content.
                    var responseFromServer = reader.ReadToEnd();
                    // Display the Response content
                    oAuthTokenResult = AuthTokenData(responseFromServer);
                }
                catch (WebException e)
                {
                    tryCount++;
                    // This exception will be raised if the server didn't return 200 - OK within response.
                    // Retrieve more information about the error
                    if (e.Response == null)
                    {
                        continue;
                    }
                    if (tryCount == 3)
                    {
                        using (var responseStream = e.Response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                var temp = new StreamReader(responseStream).ReadToEnd();
                                oAuthTokenResult!.Error = JsonConvert.DeserializeObject<OAuthError>(temp);
                            }
                        }
                        // Retrieve http Error
                        var err = (HttpWebResponse)e.Response;
                        oAuthTokenResult!.Error!.HttpTokenError = (int)err.StatusCode + " " + err.StatusDescription;
                    }
                }
                catch (InvalidOperationException)
                {
                    tryCount++;
                }
                if (tryCount == 3)
                {
                    oAuthTokenResult!.ErrorFlag = true;
                }
            }
            return oAuthTokenResult;
        }

        /// <summary>Gets formatted request.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The formatted request.</returns>
        private static string GetFormattedRequest(string userName, string password)
        {
            return $"grant_type={GrantType}&username={userName}&password={password}";
        }

        /// <summary>Authentication token data.</summary>
        /// <param name="responseData">Information describing the response.</param>
        /// <returns>An OAuthToken.</returns>
        private static OAuthToken? AuthTokenData(string responseData)
        {
            // Parse JSON data into C# obj
            return responseData == null ? null : JsonConvert.DeserializeObject<OAuthToken>(responseData);
        }
    }
}
