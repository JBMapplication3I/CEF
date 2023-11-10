// <copyright file="YRCResponseGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestGenerator class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Models;
    using Newtonsoft.Json;

    /// <summary>A YRC response generator.</summary>
    public static class YRCResponseGenerator
    {
        /// <summary>Method for building Transaction with Json Request,call the actual transaction execution method and
        /// call for Deserialize Json and Return the object.</summary>
        /// <param name="items">      The items.</param>
        /// <param name="origin">     The origin.</param>
        /// <param name="destination">Destination for the.</param>
        /// <returns>Returns the YRCResponse response.</returns>
        public static YRCRequest CreateYRCRequest(List<IProviderShipment> items, IContactModel origin, IContactModel destination)
        {
            var request = new YRCRequest
            {
                Login = new()
                {
                    Username = YRCShippingProviderConfig.UserName,
                    Password = YRCShippingProviderConfig.Password,
                    BusId = YRCShippingProviderConfig.BusId,
                    BusRole = YRCShippingProviderConfig.BusRole,
                    PaymentTerms = YRCShippingProviderConfig.PaymentTerms,
                },
                Details = new()
                {
                    ServiceClass = "STD",
                    TypeQuery = "QUOTE",
                    PickupDate = DateTime.Today.ToString("yyyyMMdd"),
                },
                OriginLocation = new()
                {
                    City = origin.Address!.City,
                    State = origin.Address.RegionCode,
                    PostalCode = origin.Address.PostalCode,
                    Country = origin.Address.CountryCode == "US" ? "USA" : origin.Address.CountryCode,
                },
                DestinationLocation = new()
                {
                    City = destination.Address!.City,
                    State = destination.Address.RegionCode,
                    PostalCode = destination.Address.PostalCode,
                    Country = destination.Address.CountryCode == "US" ? "USA" : destination.Address.CountryCode,
                },
            };
            var commodities = new List<YRCRequestCommodity>();
            foreach (var item in items)
            {
                var commodity = new YRCRequestCommodity
                {
                    NmfcClass = CalculateNmfcClass(item.Height ?? 1, item.Depth ?? 1, item.Width ?? 1, item.Weight),
                    HandlingUnits = 1,
                    PackageCode = "PLT",
                    PackageHeight = Convert.ToInt16(Math.Ceiling(item.Height ?? 1)),
                    PackageLength = Convert.ToInt16(Math.Ceiling(item.Depth ?? 1)),
                    PackageWidth = Convert.ToInt16(Math.Ceiling(item.Width ?? 1)),
                    PackageWeight = Convert.ToInt16(Math.Ceiling(item.Weight)),
                };
                commodities.Add(commodity);
            }
            request.ListOfCommodities = new() { Commodities = commodities.ToArray() };
            // Options for shipments:
            // * (SS) Single Shipment
            // * (IP) Inside Pickup
            // * (ID) Inside Delivery
            // * (HOMP) Residential Pickup
            // * (HOMD) Residential Delivery
            // * (NTFY) Notify before delivery
            // * (LFTO) Liftgate Service at Pickup
            // * (LFTD) Liftgate Service at Delivery
            // * (HAZM) Hazardous Materials
            // TODO: Check with client if they would like to specify any shipping options (accessorials)
            request.ServiceOpts = new() { AccOptions = Array.Empty<string>() };
            return request;
        }

        /// <summary>Gets yrc response.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The yrc response.</returns>
        public static YRCResponse GetYRCResponse(YRCRequest request)
        {
            var result = new YRCResponse { IsSuccess = false };
            try
            {
                var requestData = JsonConvert.SerializeObject(request);
                // Create a request using a URL that can receive a post.
                var webRequest = WebRequest.Create(YRCShippingProviderConfig.Url!);
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
                using (var requestStream = webRequest.GetRequestStream())
                {
                    // Write the data to the request stream.
                    requestStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    requestStream.Close();
                    // To Get the response.
                    using var response = webRequest.GetResponse();
                    // Assuming Response status is OK otherwise catch{} will be executed
                    // Get the stream containing content returned by the server.
                    using var responseStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    using var reader = new StreamReader(responseStream ?? throw new InvalidOperationException());
                    // Read the content.
                    var jsonResponse = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<YRCResponse>(jsonResponse);
                }
                return result!;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using var errorResponse = (HttpWebResponse)ex.Response;
                    using var reader = new StreamReader(errorResponse.GetResponseStream() ?? throw new InvalidOperationException());
                    var error = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<YRCResponse>(error);
                }
            }
            return result!;
        }

        /// <summary>Calculates the nmfc class.</summary>
        /// <param name="height">The height.</param>
        /// <param name="length">The length.</param>
        /// <param name="width"> The width.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>The calculated nmfc class.</returns>
        public static string CalculateNmfcClass(decimal height, decimal length, decimal width, decimal weight)
        {
            var density = weight / (height * length * width);
            if (density > 30)
            {
                return "50";
            }
            if (density > 22.5m)
            {
                return "60";
            }
            if (density > 15)
            {
                return "65";
            }
            if (density > 12)
            {
                return "70";
            }
            if (density > 10)
            {
                return "85";
            }
            if (density > 8)
            {
                return "92.5";
            }
            if (density > 6)
            {
                return "100";
            }
            if (density > 4)
            {
                return "125";
            }
            if (density > 3)
            {
                return "175";
            }
            if (density > 2)
            {
                return "250";
            }
            if (density > 1)
            {
                return "300";
            }
            return "400";
        }
    }
}
