// <copyright file="AvalaraAddressValidationService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Avalara address validation service class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>An Avalara address validation service.</summary>
    internal static class AvalaraAddressValidationService
    {
        /// <summary>The account number.</summary>
        private static string? accountNum;

        /// <summary>The license.</summary>
        private static string? license;

        /// <summary>URL of the svc.</summary>
        private static string? svcURL;

        /// <summary>Gets or sets a value indicating whether the initialized.</summary>
        /// <value>True if initialized, false if not.</value>
        public static bool Initialized { get; internal set; }

        /// <summary>Initializes this <see cref="AvalaraAddressValidationService"/>.</summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="licenseKey">   The license key.</param>
        /// <param name="serviceURL">   URL of the service.</param>
        public static void Initialize(string accountNumber, string licenseKey, string serviceURL)
        {
            accountNum = accountNumber;
            license = licenseKey;
            svcURL = serviceURL.TrimEnd('/') + "/1.0/";
            Initialized = true;
        }

        /// <summary>Validates the address described by req.</summary>
        /// <param name="req">The request.</param>
        /// <returns>A ValidateAddressResult.</returns>
        public static ValidateAddressResult ValidateAddress(ValidateAddressRequest req)
        {
            var uriBuilder = new UriBuilder($"{svcURL}address/validate");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (Contract.CheckValidKey(req.Line1))
            {
                query["Line1"] = req.Line1;
            }
            if (Contract.CheckValidKey(req.Line2))
            {
                query["Line2"] = req.Line2;
            }
            if (Contract.CheckValidKey(req.Line3))
            {
                query["Line3"] = req.Line3;
            }
            if (Contract.CheckValidKey(req.City))
            {
                query["City"] = req.City;
            }
            if (Contract.CheckValidKey(req.Region))
            {
                query["Region"] = req.Region;
            }
            if (Contract.CheckValidKey(req.Country))
            {
                query["Country"] = req.Country;
            }
            if (Contract.CheckValidKey(req.PostalCode))
            {
                query["PostalCode"] = req.PostalCode;
            }
            uriBuilder.Query = query.ToString();
            // Call the service
            var request = (HttpWebRequest)WebRequest.Create(new Uri(uriBuilder.ToString()));
            request.Headers.Add(
                HttpRequestHeader.Authorization,
                $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{accountNum}:{license}"))}");
            request.ContentType = "application/json";
            request.Method = "GET";
            var success = false;
            var json = string.Empty;
            var message = string.Empty;
            try
            {
                using var response = (HttpWebResponse)request.GetResponse();
                using var stream = response.GetResponseStream() ?? throw new NullReferenceException();
                using var reader = new StreamReader(stream);
                json = reader.ReadToEnd();
                success = true;
            }
            catch (WebException webEx)
            {
                using var stream = webEx.Response!.GetResponseStream() ?? throw new NullReferenceException();
                using var reader = new StreamReader(stream);
                json = reader.ReadToEnd();
                success = false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            var result = JsonConvert.DeserializeObject<ValidateAddressResult>(json);
            if (result is null)
            {
                ValidateAddressResult retVal = new();
                retVal.Messages.Add(new("Error: ", "Couldn't deserialize result"));
                if (!success && Contract.CheckValidKey(message))
                {
                    retVal.Messages.Add(new("Error: ", message));
                }
                return retVal;
            }
            if (!success && Contract.CheckValidKey(message))
            {
                result.Messages.Add(new("Error: ", message));
            }
            return result;
        }
    }
}
