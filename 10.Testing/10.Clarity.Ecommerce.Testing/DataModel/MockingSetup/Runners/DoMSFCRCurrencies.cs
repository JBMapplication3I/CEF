// <copyright file="DoMockingSetupForContextRunnerCurrenciesAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner currencies class</summary>
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
        private async Task DoMockingSetupForContextRunnerCurrenciesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Currencies
            if (DoAll || DoCurrencies || DoCurrencyTable)
            {
                var index = 0;
                RawCurrencies = new()
                {
                    await CreateADummyCurrencyAsync(id: ++index, key: "USD", name: "United States Dollar", desc: "desc", unicodeSymbolValue: 36).ConfigureAwait(false),
                    await CreateADummyCurrencyAsync(id: ++index, key: "EUR", name: "Euro", desc: "desc", unicodeSymbolValue: 8364).ConfigureAwait(false),
                    await CreateADummyCurrencyAsync(id: ++index, key: "GBP", name: "Great Britain Pound", desc: "desc", unicodeSymbolValue: 163).ConfigureAwait(false),
                };
                await InitializeMockSetCurrenciesAsync(mockContext, RawCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Currency Conversions
            if (DoAll || DoCurrencies || DoCurrencyConversionTable)
            {
                var index = 0;
                RawCurrencyConversions = new()
                {
                    await CreateADummyCurrencyConversionAsync(id: ++index, key: "USDtoEUR", rate: 1.25m).ConfigureAwait(false),
                    await CreateADummyCurrencyConversionAsync(id: ++index, key: "EURtoGBP", rate: 1.35m).ConfigureAwait(false),
                    await CreateADummyCurrencyConversionAsync(id: ++index, key: "GBPtoUSD", rate: 0.45m).ConfigureAwait(false),
                };
                await InitializeMockSetCurrencyConversionsAsync(mockContext, RawCurrencyConversions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Currency Images
            if (DoAll || DoCurrencies || DoCurrencyImageTable)
            {
                var index = 0;
                RawCurrencyImages = new()
                {
                    await CreateADummyCurrencyImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetCurrencyImagesAsync(mockContext, RawCurrencyImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Currency Image Types
            if (DoAll || DoCurrencies || DoCurrencyImageTypeTable)
            {
                var index = 0;
                RawCurrencyImageTypes = new()
                {
                    await CreateADummyCurrencyImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCurrencyImageTypesAsync(mockContext, RawCurrencyImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Historical Currency Rates
            if (DoAll || DoCurrencies || DoHistoricalCurrencyRateTable)
            {
                var index = 0;
                RawHistoricalCurrencyRates = new()
                {
                    await CreateADummyHistoricalCurrencyRateAsync(id: ++index, key: "USD_GBP_2017-01-01", endingCurrencyID: 3, onDate: new DateTime(2018, 1, 1), rate: 0.812237928090314m).ConfigureAwait(false),
                    await CreateADummyHistoricalCurrencyRateAsync(id: ++index, key: "USD_GBP_2017-01-02", endingCurrencyID: 3, onDate: new DateTime(2018, 1, 2), rate: 0.81356903965599625m).ConfigureAwait(false),
                };
                await InitializeMockSetHistoricalCurrencyRatesAsync(mockContext, RawHistoricalCurrencyRates).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
