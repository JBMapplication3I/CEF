// <copyright file="DoMockingSetupForContextRunnerTax.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner tax class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerTaxAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Historical Tax Rates
            if (DoAll || DoTax || DoHistoricalTaxRateTable)
            {
                RawHistoricalTaxRates = new()
                {
                    await CreateADummyHistoricalTaxRateAsync(id: 1, key: "HISTORICAL-TAX-RATE-1", cartHash: 123456879, onDate: CreatedDate, provider: "Avalara", countryLevelRate: 0.01m, districtLevelRate: 0.02m, regionLevelRate: 0.04m, rate: 0.10m, totalAmount: 1.99m, totalTax: 1.99m, totalTaxable: 1.99m, totalTaxCalculated: 0.199m).ConfigureAwait(false),
                };
                await InitializeMockSetHistoricalTaxRatesAsync(mockContext, RawHistoricalTaxRates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Tax Countries
            if (DoAll || DoTax || DoTaxCountryTable)
            {
                RawTaxCountries = new()
                {
                    await CreateADummyTaxCountryAsync(id: 1, key: "USA", name: "United States of America", desc: "desc", rate: 0.02m).ConfigureAwait(false),
                    await CreateADummyTaxCountryAsync(id: 2, key: "CA", name: "Canada", desc: "desc", countryID: 2, rate: 0.01m).ConfigureAwait(false),
                };
                await InitializeMockSetTaxCountriesAsync(mockContext, RawTaxCountries).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Tax Districts
            if (DoAll || DoTax || DoTaxDistrictTable)
            {
                RawTaxDistricts = new()
                {
                    await CreateADummyTaxDistrictAsync(id: 1, key: "WILCO", name: "Williamson County", desc: "desc", rate: 0.01m).ConfigureAwait(false),
                };
                await InitializeMockSetTaxDistrictsAsync(mockContext, RawTaxDistricts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Tax Regions
            if (DoAll || DoTax || DoTaxRegionTable)
            {
                RawTaxRegions = new()
                {
                    await CreateADummyTaxRegionAsync(id: 1, key: "AL", name: "Alabama", desc: "desc", rate: 0.05m).ConfigureAwait(false),
                    await CreateADummyTaxRegionAsync(id: 2, key: "AK", name: "Alaska", desc: "desc", rate: 0.06m, regionID: 2).ConfigureAwait(false),
                    await CreateADummyTaxRegionAsync(id: 3, key: "AZ", name: "Arizona", desc: "desc", rate: 0.07m, regionID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetTaxRegionsAsync(mockContext, RawTaxRegions).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
