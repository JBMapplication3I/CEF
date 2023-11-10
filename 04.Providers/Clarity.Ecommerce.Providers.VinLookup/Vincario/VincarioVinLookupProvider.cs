// <copyright file="VincarioVinLookupProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>
// https://vindecoder.eu/api/
// https://vindecoder.eu/my/api/3.1/docs/actions#toc-decode-request
// Implements the Vincario provider class
// </summary>
namespace Clarity.Ecommerce.Providers.VinLookup.Vincario
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    /// <inheritdoc/>
    public class VincarioVinLookupProvider : VinLookupProviderBase
    {
        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => VincarioVinLookupProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        public override async Task<CEFActionResponse<bool>> ValidateVinAsync(string vinNumber, string? contextProfileName)
        {
            try
            {
                var controlSum = Sha1(
                        $"{vinNumber}|"
                        + $"{VincarioVinLookupProviderConfig.ID}|"
                        + $"{VincarioVinLookupProviderConfig.APIKey}|"
                        + $"{VincarioVinLookupProviderConfig.SecretKey}")
                    .Substring(0, 10);
                var url = $"{VincarioVinLookupProviderConfig.BaseAddress}/" +
                    $"{VincarioVinLookupProviderConfig.APIKey}/{controlSum}/decode/{vinNumber}.json";
                using var client = new HttpClient();
                var response = await client.GetAsync(url)
                    .ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                // valid response
                //var responseString = "{\"price\":0,\"price_currency\":\"USD\",\"balance\":{\"API Decode\":19},\"decode\":[{\"label\":\"VIN\",\"value\":\"1FTFW1R6XBFB08616\"},{\"label\":\"Make\",\"value\":\"Ford\"},{\"label\":\"Manufacturer\",\"value\":\"Ford Motor Co\"},{\"label\":\"Plant Country\",\"value\":\"UNITED STATES (USA)\"},{\"label\":\"Manufacturer Address\",\"value\":\"American Road, Dearborn  MI 48121\"},{\"label\":\"Check Digit\",\"value\":\"X\"},{\"label\":\"Sequential Number\",\"value\":\"B08616\"},{\"label\":\"Body\",\"value\":\"Pickup\"},{\"label\":\"Engine Cylinders\",\"value\":8},{\"label\":\"Engine Displacement (ccm)\",\"value\":6210},{\"label\":\"Drive\",\"value\":\"4x4 - Four-wheel drive\"},{\"label\":\"Engine Power (kW)\",\"value\":307},{\"label\":\"Fuel Type - Primary\",\"value\":\"Gasoline\"},{\"label\":\"Model\",\"value\":\"F-150\"},{\"label\":\"Model Year\",\"value\":2011},{\"label\":\"Plant City\",\"value\":\"DEARBORN\"},{\"label\":\"Trim\",\"value\":\"Raptor SVT\"},{\"label\":\"Plant State\",\"value\":\"MICHIGAN\"},{\"label\":\"Length (mm)\",\"value\":5940},{\"label\":\"Width (mm)\",\"value\":2150},{\"label\":\"Height (mm)\",\"value\":2050},{\"label\":\"Weight Empty (kg)\",\"value\":2984},{\"label\":\"Max roof load (kg)\",\"value\":85},{\"label\":\"Product Type\",\"value\":\"Truck\"},{\"label\":\"Number of Seats\",\"value\":\"5\"},{\"label\":\"Number of Axles\",\"value\":2},{\"label\":\"Wheelbase (mm)\",\"value\":3710},{\"label\":\"Wheel Size\",\"value\":\"315\\/70 R17 121\\/118S\"},{\"label\":\"Transmission\",\"value\":\"Automatic\"},{\"label\":\"Emission Standard\",\"value\":\"Euro 5\"},{\"label\":\"Max Weight (kg)\",\"value\":3311},{\"label\":\"Permitted trailer load without brakes (kg)\",\"value\":750},{\"label\":\"Wheelbase Array (mm)\",\"value\":[3710]},{\"label\":\"Wheel Size Array\",\"value\":[\"315\\/70 R17 121\\/118S\"]}]}";
                // Invalid response
                //var responseString = "{\"error\":true,\"message\":\"invalid\",\"reasons\":[\"length\"]}";
                var vincarioResponse = JsonConvert.DeserializeObject<VincarioResponse>(responseString);
                if (vincarioResponse == null)
                {
                    return CEFAR.FailingCEFAR<bool>();
                }
                if (vincarioResponse.Error
                    && vincarioResponse.Reasons != null)
                {
                    return CEFAR.FailingCEFAR<bool>("The VIN is invalid because of these reasons: " +
                        string.Join(",", vincarioResponse.Reasons));
                }
                if (vincarioResponse.Error
                    && vincarioResponse.Message != null)
                {
                    return CEFAR.FailingCEFAR<bool>(vincarioResponse.Message);
                }
                return CEFAR.PassingCEFAR(true);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("Validate Vin", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR<bool>(ex.Message);
            }
        }

        private string Sha1(string encode)
        {
            return string.Join(string.Empty,
                SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(encode)).Select(x => x.ToString("x2")));
        }
    }
}
