// <copyright file="OracleResponseGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle response generator class</summary>

namespace Clarity.Ecommerce.Providers.Shipping.Oracle
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Newtonsoft.Json;

    /// <summary>An oracle response generator.</summary>
    public class OracleResponseGenerator
    {
        /// <summary>Creates oracle request.</summary>
        /// <param name="origin">       The origin.</param>
        /// <param name="shipping">     The shipping.</param>
        /// <param name="suborderTotal">The suborder total.</param>
        /// <returns>The new oracle request.</returns>
        public static OracleRequest CreateOracleRequest(IContactModel origin, IContactModel shipping, decimal suborderTotal)
        {
            var request = new OracleRequest
            {
                Origin = new()
                {
                    Street1 = origin?.Address?.Street1,
                    Street2 = origin?.Address?.Street2,
                    City = origin?.Address?.City,
                    PostalCode = origin?.Address?.PostalCode,
                    CountryName = origin?.Address?.CountryName,
                    RegionName = origin?.Address?.RegionName,
                    Active = origin is { Active: true },
                    CreatedDate = origin?.CreatedDate,
                    SerializableAttributes = new(),
                },
                Shipping = new()
                {
                    Street1 = shipping?.Address?.Street1,
                    Street2 = shipping?.Address?.Street2,
                    City = shipping?.Address?.City,
                    PostalCode = shipping?.Address?.PostalCode,
                    CountryName = shipping?.Address?.CountryName,
                    RegionName = shipping?.Address?.RegionName,
                    Active = shipping is { Active: true },
                    CreatedDate = shipping?.CreatedDate,
                    SerializableAttributes = new(),
                },
                Subtotal = suborderTotal,
            };
            request.Origin.SerializableAttributes["Province"] = new() { Key = "Province", Value = string.Empty };
            request.Origin.SerializableAttributes["CustAcctSiteId"] = new() { Key = "CustAcctSiteId", Value = string.Empty };
            request.Origin.SerializableAttributes["LocationId"] = new() { Key = "LocationId", Value = string.Empty };
            request.Origin.SerializableAttributes["PartySiteId"] = new() { Key = "PartySiteId", Value = string.Empty };
            request.Shipping.SerializableAttributes["Province"] = new() { Key = "Province", Value = string.Empty };
            request.Shipping.SerializableAttributes["CustAcctSiteId"] = new() { Key = "CustAcctSiteId", Value = string.Empty };
            request.Shipping.SerializableAttributes["LocationId"] = new() { Key = "LocationId", Value = string.Empty };
            request.Shipping.SerializableAttributes["PartySiteId"] = new() { Key = "PartySiteId", Value = string.Empty };
            return request;
        }

        /// <summary>Gets oracle response.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The oracle response.</returns>
        public static async Task<OracleResponse> GetOracleResponseAsync(OracleRequest request)
        {
            var result = new OracleResponse { IsSuccess = false };
            try
            {
                var requestData = JsonConvert.SerializeObject(request);
                // Create a request using a URL that can receive a post.
                var webRequest = WebRequest.Create(OracleShippingProviderConfig.Url!);
                // Set the Method property of the request to POST.
                webRequest.Method = "POST";
                // To set HTTP version of the current request, use the Version10 and Version11 fields of the HttpVersion class.
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                ////ServicePointManager.Expect100Continue = true;
                // 1.2+ is the only thing that should be allowed
                // Set the ContentType property of the WebRequest.
                webRequest.ContentType = "application/json";
                ((HttpWebRequest)webRequest).Accept = "application/json";
                var byteArray = Encoding.UTF8.GetBytes(requestData);
                // Set the ContentLength property of the WebRequest.
                webRequest.ContentLength = byteArray.Length;
                // Get the request stream.
                using var requestStream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false);
                // Write the data to the request stream.
#if NET5_0_OR_GREATER
                await requestStream.WriteAsync(byteArray.AsMemory(0, byteArray.Length)).ConfigureAwait(false);
#else
                await requestStream.WriteAsync(byteArray, 0, byteArray.Length).ConfigureAwait(false);
#endif
                // Close the Stream object.
                requestStream.Close();
                // To Get the response.
                using var response = await webRequest.GetResponseAsync().ConfigureAwait(false);
                // Assuming Response status is OK otherwise catch{} will be executed
                // Get the stream containing content returned by the server.
                using var responseStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                using var reader = new StreamReader(responseStream ?? throw new InvalidOperationException());
                // Read the content.
                var jsonResponse = await reader.ReadToEndAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<OracleResponse>(jsonResponse);
                return result!;
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    return result!;
                }
                using var errorResponse = (HttpWebResponse)ex.Response;
                using var reader = new StreamReader(
                    errorResponse.GetResponseStream() ?? throw new InvalidOperationException());
                var error = await reader.ReadToEndAsync().ConfigureAwait(false);
                result!.Message = error;
            }
            catch (Exception e)
            {
                result!.Message = e.ToString();
            }
            return result;
        }
    }
}
