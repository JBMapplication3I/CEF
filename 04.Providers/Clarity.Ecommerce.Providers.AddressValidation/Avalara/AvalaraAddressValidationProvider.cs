// <copyright file="AvalaraAddressValidationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Avalara address validation provider class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>An Avalara address validation provider.</summary>
    /// <seealso cref="AddressValidationProviderBase"/>
    public class AvalaraAddressValidationProvider : AddressValidationProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => AvalaraAddressValidationProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<IAddressValidationResultModel> ValidateAddressAsync(
            IAddressValidationRequestModel request,
            string? contextProfileName)
        {
            if (!AvalaraAddressValidationProviderConfig.Initialized)
            {
                await AvalaraAddressValidationProviderConfig.InitializeAsync(contextProfileName).ConfigureAwait(false);
            }
            var resultModel = new AddressValidationResultModel(request);
            if (!AvalaraAddressValidationProviderConfig.AddressServiceEnabled)
            {
                return resultModel.WithSuccess(null, "Service Disabled");
            }
            if (!Contract.CheckValidIDOrKey(request.Address?.CountryID, request.Address?.CountryCode))
            {
                return resultModel.WithFailure("Address isn't populated");
            }
            if (!Contract.CheckValidKey(request.Address!.CountryCode))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var country = context.Countries
                    .AsNoTracking()
                    .FilterByID(request.Address.CountryID)
                    .SelectFirstFullCountryAndMapToCountryModel(contextProfileName)
                    ?? throw new ArgumentException("ERROR! Must supply information that matches an existing record.");
                request.Address.Country = country;
                request.Address.CountryKey = country.CustomKey;
                request.Address.CountryName = country.Name;
                request.Address.CountryCode = country.Code;
            }
            if (!AvalaraAddressValidationProviderConfig.AddressServiceCountries!.Contains(request.Address.CountryCode!))
            {
                return resultModel.WithSuccess(null, "Country not set up for validation");
            }
            var validateAddressRequest = new ValidateAddressRequest(resultModel.SourceAddress!);
            if (!AvalaraAddressValidationService.Initialized)
            {
                AvalaraAddressValidationService.Initialize(
                    AvalaraAddressValidationProviderConfig.AccountNumber,
                    AvalaraAddressValidationProviderConfig.LicenseKey,
                    AvalaraAddressValidationProviderConfig.ServiceUrl);
            }
            var result = AvalaraAddressValidationService.ValidateAddress(validateAddressRequest);
            IAddressModel? resolvedAddress = null;
            if (Contract.CheckValidID(request.AccountContactID) && result.Success)
            {
                var ac = await Workflows.AccountContacts.GetAsync(
                        request.AccountContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (ac != null)
                {
                    ac.Slave!.Address!.Street1 = result.Address!.Line1;
                    ac.Slave.Address.Street2 = result.Address.Line2;
                    ac.Slave.Address.Street3 = result.Address.Line3;
                    ac.Slave.Address.City = result.Address.City;
                    ac.Slave.Address.PostalCode = result.Address.PostalCode;
                    ac.Slave.Address.RegionCode = result.Address.Region;
                    ac.Slave.Address.RegionID = null;
                    ac.Slave.Address.Region = null;
                    ac.Slave.Address.RegionCustom = ac.Slave.Address.RegionKey = ac.Slave.Address.RegionName = null;
                    ac.Slave.Address.CountryCode = result.Address.Country == "US" ? "USA" : result.Address.Country;
                    ac.Slave.Address.CountryID = null;
                    ac.Slave.Address.Country = null;
                    ac.Slave.Address.CountryCustom = ac.Slave.Address.CountryKey = ac.Slave.Address.CountryName = null;
                    ac.Slave.Address.SerializableAttributes![$"Validated-By-{nameof(AvalaraAddressValidationProvider)}"]
                        = new()
                        {
                            Key = $"Validated-By-{nameof(AvalaraAddressValidationProvider)}",
                            Value = Newtonsoft.Json.JsonConvert.SerializeObject(
                                new { Result = result, Timestamp = DateExtensions.GenDateTime }),
                        };
                    var updateResponse = await Workflows.Addresses.UpdateAsync(
                            ac.Slave.Address,
                            contextProfileName)
                        .ConfigureAwait(false);
                    resolvedAddress = ac.Slave.Address = await Workflows.Addresses.GetAsync(
                            updateResponse.Result,
                            contextProfileName)
                        .ConfigureAwait(false);
                }
            }
            if (result.Success && resolvedAddress == null)
            {
                resolvedAddress = ConvertValidateAddressResultToAddressValidationAddress(result, contextProfileName);
            }
            return result.Success
                ? resultModel.WithSuccess(resolvedAddress)
                : resultModel.WithFailure(result.Errors);
        }

        /// <summary>Validates the address result to address validation address described by result.</summary>
        /// <param name="result">            The result.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IAddressModel.</returns>
        private static IAddressModel? ConvertValidateAddressResultToAddressValidationAddress(
            ValidateAddressResult? result,
            string? contextProfileName)
        {
            if (result?.Address == null)
            {
                return null;
            }
            var retVal = RegistryLoaderWrapper.GetInstance<IAddressModel>(contextProfileName);
            retVal.Street1 = result.Address.Line1;
            retVal.Street2 = result.Address.Line2;
            retVal.Street3 = result.Address.Line3;
            retVal.City = result.Address.City;
            retVal.PostalCode = result.Address.PostalCode;
            retVal.RegionCode = result.Address.Region;
            retVal.CountryCode = result.Address.Country == "US" ? "USA" : result.Address.Country;
            return retVal;
        }
    }
}
