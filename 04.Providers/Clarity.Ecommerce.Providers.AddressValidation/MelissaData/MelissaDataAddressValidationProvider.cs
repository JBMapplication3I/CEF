// <copyright file="MelissaDataAddressValidationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the melissa data address validation provider class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.MelissaData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>Address Validation Provider for Melissa Data.</summary>
    public class MelissaDataAddressValidationProvider : AddressValidationProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => MelissaDataAddressValidationProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<IAddressValidationResultModel> ValidateAddressAsync(
            IAddressValidationRequestModel request,
            string? contextProfileName)
        {
            ////BuildCodeMessages();
            var resultModel = new AddressValidationResultModel(request);
            if (!Contract.CheckValidKey(request.Address?.CountryName))
            {
                return resultModel.WithFailure("Country Name isn't populated");
            }
            var result = await ValidateAddressAsync(request.Address!, contextProfileName).ConfigureAwait(false);
            if (result is null)
            {
                return new AddressValidationResultModel(request).WithFailure();
            }
            var messages = new List<string>();
            var addressInvalid = CheckResponseCodesForCodesSupplied(
                    result,
                    MelissaDataAddressValidationProviderConfig.ErrorCodes,
                    ref messages)
                || CheckResponseCodesForCodesSupplied(
                    result,
                    MelissaDataAddressValidationProviderConfig.PartiallyVerified,
                    ref messages);
            if (addressInvalid)
            {
                return resultModel.WithFailure(string.Join("\r\n", messages));
            }
            var addressHighlyValid = CheckResponseCodesForCodesSupplied(
                result,
                MelissaDataAddressValidationProviderConfig.HighLevelVerified,
                ref messages);
            var addressLowValid = false;
            if (!addressHighlyValid)
            {
                if (MelissaDataAddressValidationProviderConfig.Av2Countries.Contains(result.Records![0].CountryISO3166_1_Alpha2))
                {
                    addressLowValid = CheckResponseCodesForCodesSupplied(result, new[] { "AV22" }, ref messages);
                }
                else if (MelissaDataAddressValidationProviderConfig.Av3Countries.Contains(result.Records[0].CountryISO3166_1_Alpha2))
                {
                    addressLowValid = CheckResponseCodesForCodesSupplied(result, new[] { "AV23" }, ref messages);
                }
                if (!addressLowValid)
                {
                    return resultModel.WithFailure(string.Join("\r\n", messages));
                }
            }
            CheckResponseCodesForCodesSupplied(
                result,
                MelissaDataAddressValidationProviderConfig.ChangeCodes,
                ref messages);
            var validatedAddress = ResponseModelToAddressModel(result.Records![0]);
            return resultModel.WithSuccess(validatedAddress, string.Join("\r\n", messages));
        }

        /// <summary>Inner ValidateAddress Method that will return the object from the request.</summary>
        /// <param name="address">           The address.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The response from Melissa Data.</returns>
        // ReSharper disable once UnusedParameter.Local
        private static async Task<ValidateAddressResult?> ValidateAddressAsync(IAddressModel address, string? contextProfileName)
        {
            var melissaAddress = MapAddressModelToGlobalAddressRequestModel(address);
            var uri = new Uri(GetRequestUrlFromParameters(melissaAddress));
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Method = "GET";
            string json;
            try
            {
                using var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
                json = await ReadResponseStreamAsync(response.GetResponseStream() ?? throw new InvalidOperationException()).ConfigureAwait(false);
            }
            catch (WebException webEx)
            {
                json = await ReadResponseStreamAsync(webEx.Response!.GetResponseStream() ?? throw new InvalidOperationException()).ConfigureAwait(false);
                await Logger.LogErrorAsync(
                        name: $"{nameof(MelissaDataAddressValidationProvider)}.{nameof(ValidateAddressAsync)}.WebError",
                        message: webEx.Message + "\r\n" + json,
                        ex: webEx,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(MelissaDataAddressValidationProvider)}.{nameof(ValidateAddressAsync)}.Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return null;
            }
            return JsonConvert.DeserializeObject<ValidateAddressResult>(json);
        }

        /// <summary>Map address model to global address request model.</summary>
        /// <param name="address">The address.</param>
        /// <returns>A GlobalAddressRequestModel.</returns>
        private static GlobalAddressRequestModel MapAddressModelToGlobalAddressRequestModel(IAddressModel address)
        {
            return new()
            {
                LicenseKey = MelissaDataAddressValidationProviderConfig.LicenseKey,
                TransmissionReference = $"{address.CustomKey}",
                Options = new()
                {
                    { "OutputScript", "NATIVE" },
                },
                AddressLine1 = address.Street1,
                AddressLine2 = address.Street2,
                AddressLine3 = address.Street3,
                Locality = address.City,
                AdministrativeArea = address.RegionCode,
                PostalCode = address.PostalCode,
                Country = address.CountryCode,
            };
        }

        /// <summary>Gets request URL from parameters.</summary>
        /// <param name="model">The model.</param>
        /// <returns>The request URL from parameters.</returns>
        private static string GetRequestUrlFromParameters(GlobalAddressRequestModel model)
        {
            var uriBuilder = new UriBuilder("http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (Contract.CheckValidKey(model.TransmissionReference))
            {
                query["t"] = model.TransmissionReference;
            }
            if (Contract.CheckValidKey(model.LicenseKey))
            {
                query["id"] = model.LicenseKey;
            }
            if (model.Options!.Count != 0)
            {
                query["opt"] = string.Join(",", model.Options);
            }
            if (Contract.CheckValidKey(model.AddressLine1))
            {
                query["a1"] = model.AddressLine1;
            }
            if (Contract.CheckValidKey(model.AddressLine2))
            {
                query["a2"] = model.AddressLine2;
            }
            if (Contract.CheckValidKey(model.AddressLine3))
            {
                query["a3"] = model.AddressLine3;
            }
            if (Contract.CheckValidKey(model.AddressLine4))
            {
                query["a4"] = model.AddressLine4;
            }
            if (Contract.CheckValidKey(model.AddressLine5))
            {
                query["a5"] = model.AddressLine5;
            }
            if (Contract.CheckValidKey(model.AddressLine6))
            {
                query["a6"] = model.AddressLine6;
            }
            if (Contract.CheckValidKey(model.AddressLine7))
            {
                query["a7"] = model.AddressLine7;
            }
            if (Contract.CheckValidKey(model.AddressLine8))
            {
                query["a8"] = model.AddressLine8;
            }
            if (Contract.CheckValidKey(model.DoubleDependentLocality))
            {
                query["ddeploc"] = model.DoubleDependentLocality;
            }
            if (Contract.CheckValidKey(model.DependentLocality))
            {
                query["deploc"] = model.DependentLocality;
            }
            if (Contract.CheckValidKey(model.Locality))
            {
                query["loc"] = model.Locality;
            }
            if (Contract.CheckValidKey(model.SubAdministrativeArea))
            {
                query["subadmarea"] = model.SubAdministrativeArea;
            }
            if (Contract.CheckValidKey(model.AdministrativeArea))
            {
                query["admarea"] = model.AdministrativeArea;
            }
            if (Contract.CheckValidKey(model.SubNationalArea))
            {
                query["subnatarea"] = model.SubNationalArea;
            }
            if (Contract.CheckValidKey(model.PostalCode))
            {
                query["postal"] = model.PostalCode;
            }
            if (Contract.CheckValidKey(model.Country))
            {
                query["ctry"] = model.Country;
            }
            {
                query["format"] = "JSON";
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        /// <summary>Reads response stream.</summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The response stream.</returns>
        private static async Task<string> ReadResponseStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        /// <summary>Check response codes for codes supplied.</summary>
        /// <param name="result">  The result.</param>
        /// <param name="codes">   The codes.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static bool CheckResponseCodesForCodesSupplied(
            ValidateAddressResult result,
            string[] codes,
            ref List<string> messages)
        {
            var exists = false;
            var record = result.Records![0];
            foreach (var code in record.Results!.Split(','))
            {
                if (!codes.Contains(code))
                {
                    continue;
                }
                exists = true;
                var message = MelissaDataAddressValidationProviderConfig.CodeMessages.FirstOrDefault(x => x.Key == code);
                if (message.Key != null)
                {
                    messages.Add(message.Value);
                }
            }
            return exists;
        }

        /// <summary>Response model to address model.</summary>
        /// <param name="record">The record.</param>
        /// <returns>An IAddressModel.</returns>
        private static IAddressModel ResponseModelToAddressModel(Record record)
        {
            var model = new Ecommerce.Models.AddressModel
            {
                Company = record.Organization,
                Street1 = record.AddressLine1,
                City = record.Locality,
                PostalCode = record.PostalCode,
                Latitude = !string.IsNullOrWhiteSpace(record.Latitude) ? Convert.ToDecimal(record.Latitude) : null,
                Longitude = !string.IsNullOrWhiteSpace(record.Longitude) ? Convert.ToDecimal(record.Longitude) : null,
            };
            if (record.CountryISO3166_1_Alpha2 == "US")
            {
                model.RegionCode = record.AdministrativeArea;
            }
            else
            {
                model.RegionName = record.AdministrativeArea;
            }
            return model;
        }
    }
}
