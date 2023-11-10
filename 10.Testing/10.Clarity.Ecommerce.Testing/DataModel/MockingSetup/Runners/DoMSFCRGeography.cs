// <copyright file="DoMockingSetupForContextRunnerGeography.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner geography class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerGeographyAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Addresses
            if (DoAll || DoGeography || DoAddressTable)
            {
                RawAddresses = new()
                {
                    await CreateADummyAddressAsync(id: 1, key: "BILL TO",   company: "James Gray", city: "Austin",  latitude: 30.390397m, longitude: -97.748598m, postalCode: "78759", regionID: 43, countryID: 1, street1: "9442 N Capital of TX Hwy", street2: "Plaza 1, Ste 925").ConfigureAwait(false),
                    await CreateADummyAddressAsync(id: 2, key: "SHIP TO",   company: "James Gray", city: "Austin",  latitude: 30.390397m, longitude: -97.748598m, postalCode: "78759", regionID: 43, countryID: 1, street1: "9442 N Capital of TX Hwy", street2: "Plaza 1, Ste 925").ConfigureAwait(false),
                    await CreateADummyAddressAsync(id: 3, key: "SHIP HOME", company: "James Gray", city: "Leander", latitude: 30.609011m, longitude: -97.895478m, postalCode: "78641", regionID: 43, countryID: 1, street1: "100 Terry Ln").ConfigureAwait(false),
                };
                await InitializeMockSetAddressesAsync(mockContext, RawAddresses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Countries
            if (DoAll || DoGeography || DoCountryTable)
            {
                RawCountries = new()
                {
                    await CreateADummyCountryAsync(1, key: "USA", name: "United States of America", desc: "desc", code: "USA").ConfigureAwait(false),
                    await CreateADummyCountryAsync(2, key: "CA", name: "Canada", desc: "desc", code: "CA").ConfigureAwait(false),
                };
                await InitializeMockSetCountriesAsync(mockContext, RawCountries).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Country Currencies
            if (DoAll || DoGeography || DoCountryCurrencyTable)
            {
                RawCountryCurrencies = new()
                {
                    await CreateADummyCountryCurrencyAsync(id: 1, key: "COUNTRY-CURRENCY-1").ConfigureAwait(false),
                };
                await InitializeMockSetCountryCurrenciesAsync(mockContext, RawCountryCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Country Images
            if (DoAll || DoGeography || DoCountryImageTable)
            {
                RawCountryImages = new()
                {
                    await CreateADummyCountryImageAsync(id: 1, key: "COUNTRY-IMAGE-1", name: "Country Image 1", desc: "desc", isPrimary: true, originalFileName: "country-image.jpg", originalBytes: Array.Empty<byte>(), thumbnailFileName: "country-image-thumb.jpg", thumbnailBytes: Array.Empty<byte>()).ConfigureAwait(false),
                };
                await InitializeMockSetCountryImagesAsync(mockContext, RawCountryImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Country Image Types
            if (DoAll || DoGeography || DoCountryImageTypeTable)
            {
                RawCountryImageTypes = new()
                {
                    await CreateADummyCountryImageTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCountryImageTypesAsync(mockContext, RawCountryImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Country Languages
            if (DoAll || DoGeography || DoCountryLanguageTable)
            {
                RawCountryLanguages = new()
                {
                    await CreateADummyCountryLanguageAsync(id: 1, key: "COUNTRY-LANGUAGE-KEY-1").ConfigureAwait(false),
                };
                await InitializeMockSetCountryLanguagesAsync(mockContext, RawCountryLanguages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Districts
            if (DoAll || DoGeography || DoDistrictTable)
            {
                var index = 0;
                RawDistricts = new()
                {
                    await CreateADummyDistrictAsync(id: ++index, key: "WILCO", name: "Williamson County", desc: "desc", code: "WILCO", regionID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetDistrictsAsync(mockContext, RawDistricts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: District Currencies
            if (DoAll || DoGeography || DoDistrictCurrencyTable)
            {
                RawDistrictCurrencies = new()
                {
                    await CreateADummyDistrictCurrencyAsync(id: 1, key: "DISTRICT-CURRENCY-1").ConfigureAwait(false),
                };
                await InitializeMockSetDistrictCurrenciesAsync(mockContext, RawDistrictCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: District Images
            if (DoAll || DoGeography || DoDistrictImageTable)
            {
                RawDistrictImages = new()
                {
                    await CreateADummyDistrictImageAsync(id: 1, key: "DISTRICT-IMAGE-1", name: "District Image 1", desc: "desc", isPrimary: true, originalFileName: "region-image.jpg", originalBytes: Array.Empty<byte>(), thumbnailFileName: "region-image-thumb.jpg", thumbnailBytes: Array.Empty<byte>()).ConfigureAwait(false),
                };
                await InitializeMockSetDistrictImagesAsync(mockContext, RawDistrictImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: District Image Types
            if (DoAll || DoGeography || DoDistrictImageTypeTable)
            {
                var index = 0;
                RawDistrictImageTypes = new()
                {
                    await CreateADummyDistrictImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetDistrictImageTypesAsync(mockContext, RawDistrictImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: District Languages
            if (DoAll || DoGeography || DoDistrictLanguageTable)
            {
                RawDistrictLanguages = new()
                {
                    await CreateADummyDistrictLanguageAsync(id: 1, key: "DISTRICT-LANGUAGE-KEY-1").ConfigureAwait(false),
                };
                await InitializeMockSetDistrictLanguagesAsync(mockContext, RawDistrictLanguages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Historical Address Validations
            if (DoAll || DoGeography || DoHistoricalAddressValidationTable)
            {
                RawHistoricalAddressValidations = new()
                {
                    await CreateADummyHistoricalAddressValidationAsync(id: 1, key: "HISTORICAL-ADDRESS-VALIDATION-KEY-1", isValid: true, onDate: CreatedDate, provider: "Avalara").ConfigureAwait(false),
                };
                await InitializeMockSetHistoricalAddressValidationsAsync(mockContext, RawHistoricalAddressValidations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Phone Prefix Lookups
            if (DoAll || DoGeography || DoPhonePrefixLookupTable)
            {
                var index = 0;
                RawPhonePrefixLookups = new()
                {
                    await CreateADummyPhonePrefixLookupAsync(id: ++index, key: "null", cityName: "Saint John's", prefix: "+1268", timeZone: "SA Western Standard time").ConfigureAwait(false),
                };
                await InitializeMockSetPhonePrefixLookupsAsync(mockContext, RawPhonePrefixLookups).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Regions
            if (DoAll || DoGeography || DoRegionTable)
            {
                var index = 0;
                RawRegions = new()
                {
                    await CreateADummyRegionAsync(id: ++index, key: "AL", name: "Alabama", desc: "desc", code: "AL").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "AK", name: "Alaska", desc: "desc", code: "AK").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "AZ", name: "Arizona", desc: "desc", code: "AZ").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "AR", name: "Arkansas", desc: "desc", code: "AR").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "CA", name: "California", desc: "desc", code: "CA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "CO", name: "Colorado", desc: "desc", code: "CO").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "CT", name: "Connecticut", desc: "desc", code: "CT").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "DE", name: "Delaware", desc: "desc", code: "DE").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "FL", name: "Florida", desc: "desc", code: "FL").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "GA", name: "Georgia", desc: "desc", code: "GA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "HI", name: "Hawaii", desc: "desc", code: "HI").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "ID", name: "Idaho", desc: "desc", code: "ID").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "IL", name: "Illinois", desc: "desc", code: "IL").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "IN", name: "Indiana", desc: "desc", code: "IN").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "IA", name: "Iowa", desc: "desc", code: "IA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "KS", name: "Kansas", desc: "desc", code: "KS").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "KY", name: "Kentucky", desc: "desc", code: "KY").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "LA", name: "Louisiana", desc: "desc", code: "LA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "ME", name: "Maine", desc: "desc", code: "ME").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MD", name: "Maryland", desc: "desc", code: "MD").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MA", name: "Massachusetts", desc: "desc", code: "MA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MI", name: "Michigan", desc: "desc", code: "MI").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MN", name: "Minnesota", desc: "desc", code: "MN").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MS", name: "Mississippi", desc: "desc", code: "MS").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MO", name: "Missouri", desc: "desc", code: "MO").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "MT", name: "Montana", desc: "desc", code: "MT").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NE", name: "Nebraska", desc: "desc", code: "NE").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NV", name: "Nevada", desc: "desc", code: "NV").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NH", name: "New Hampshire", desc: "desc", code: "NH").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NJ", name: "New Jersey", desc: "desc", code: "NJ").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NM", name: "New Mexico", desc: "desc", code: "NM").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NY", name: "New York", desc: "desc", code: "NY").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "NC", name: "North Carolina", desc: "desc", code: "NC").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "ND", name: "North Dakota", desc: "desc", code: "ND").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "OH", name: "Ohio", desc: "desc", code: "OH").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "OK", name: "Oklahoma", desc: "desc", code: "OK").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "OR", name: "Oregon", desc: "desc", code: "OR").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "PA", name: "Pennsylvania", desc: "desc", code: "PA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "RI", name: "Rhode Island", desc: "desc", code: "RI").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "SC", name: "South Carolina", desc: "desc", code: "SC").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "SD", name: "South Dakota", desc: "desc", code: "SD").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "TN", name: "Tennessee", desc: "desc", code: "TN").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "TX", name: "Texas", desc: "desc", code: "TX").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "UT", name: "Utah", desc: "desc", code: "UT").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "VT", name: "Vermont", desc: "desc", code: "VT").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "VA", name: "Virginia", desc: "desc", code: "VA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "WA", name: "Washington", desc: "desc", code: "WA").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "WV", name: "West Virginia", desc: "desc", code: "WV").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "WI", name: "Wisconsin", desc: "desc", code: "WI").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "WY", name: "Wyoming", desc: "desc", code: "WY").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "DC", name: "Washington D.C.", desc: "desc", code: "DC").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "PR", name: "Puerto Rico", desc: "desc", code: "PR").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "VI", name: "Virgin Islands", desc: "desc", code: "VI").ConfigureAwait(false),
                    await CreateADummyRegionAsync(id: ++index, key: "QU", name: "Quebec", desc: "desc", code: "QU", countryID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetRegionsAsync(mockContext, RawRegions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Region Currencies
            if (DoAll || DoGeography || DoRegionCurrencyTable)
            {
                RawRegionCurrencies = new()
                {
                    await CreateADummyRegionCurrencyAsync(id: 1, key: "REGION-CURRENCY-1").ConfigureAwait(false),
                };
                await InitializeMockSetRegionCurrenciesAsync(mockContext, RawRegionCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Region Images
            if (DoAll || DoGeography || DoRegionImageTable)
            {
                RawRegionImages = new()
                {
                    await CreateADummyRegionImageAsync(id: 1, key: "REGION-IMAGE-1", name: "Region Image 1", desc: "desc", isPrimary: true, originalFileName: "region-image.jpg", originalBytes: Array.Empty<byte>(), thumbnailFileName: "region-image-thumb.jpg", thumbnailBytes: Array.Empty<byte>()).ConfigureAwait(false),
                };
                await InitializeMockSetRegionImagesAsync(mockContext, RawRegionImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Region Image Types
            if (DoAll || DoGeography || DoRegionImageTypeTable)
            {
                var index = 0;
                RawRegionImageTypes = new()
                {
                    await CreateADummyRegionImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetRegionImageTypesAsync(mockContext, RawRegionImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Region Languages
            if (DoAll || DoGeography || DoRegionLanguageTable)
            {
                RawRegionLanguages = new()
                {
                    await CreateADummyRegionLanguageAsync(id: 1, key: "REGION-LANGUAGE-KEY-1").ConfigureAwait(false),
                };
                await InitializeMockSetRegionLanguagesAsync(mockContext, RawRegionLanguages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Zip Codes
            if (DoAll || DoGeography || DoZipCodeTable)
            {
                var index = 0;
                RawZipCodes = new()
                {
                    await CreateADummyZipCodeAsync(id: ++index, key: "78759", areaCode: "512", cityName: "Austin", cityType: "Metropolitan", countyName: "United States of America", mSACode: null, stateAbbreviation: "TX", stateName: "Texas", timeZone: "Central Standard Time", zipCodeValue: "78759").ConfigureAwait(false),
                    await CreateADummyZipCodeAsync(id: ++index, key: "78758", areaCode: "512", cityName: "Austin", cityType: "Metropolitan", countyName: "United States of America", mSACode: null, stateAbbreviation: "TX", stateName: "Texas", timeZone: "Central Standard Time", zipCodeValue: "78758").ConfigureAwait(false),
                };
                await InitializeMockSetZipCodesAsync(mockContext, RawZipCodes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
