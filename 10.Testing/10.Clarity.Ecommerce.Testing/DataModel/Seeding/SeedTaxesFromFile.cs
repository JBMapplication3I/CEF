// <copyright file="SeedTaxesFromFile.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the seed taxes from file class</summary>
#if !NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.SeedDataBase
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using JetBrains.Annotations;
    using LINQtoCSV;
    using Xunit;

    [Trait("Category", "Seed Database.FromFile.Taxes")]
    public class SeedTaxesFromFile
    {
        private const string FolderPath = @"C:\Data\TAXRATES_ZIP5";
        private readonly DateTime now = DateExtensions.GenDateTime;

        [Fact(Skip = "Run this manually to import taxes from CSV")]
        public async Task ImportTaxesCSVAsync()
        {
            var inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
            };
            var cc = new CsvContext();
            var taxRates = new List<TaxRate>();
            foreach (var file in Directory.GetFiles(FolderPath).Where(x => x.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)))
            {
                taxRates.AddRange(cc.Read<TaxRate>(file, inputFileDescription));
            }
            var taxRegions = new List<TaxRegion>(taxRates.Count);
            using (var context = new ClarityEcommerceEntities())
            {
                foreach (var taxRate in taxRates.GroupBy(
                    x => x.State,
                    x => x,
                    (state, taxRate) => new { State = state, TaxRate = taxRate }))
                {
                    var region = await context.Regions.SingleOrDefaultAsync(x => x.Code == taxRate.State).ConfigureAwait(false);
                    if (region == null) { continue; }
                    taxRegions.AddRange(taxRate.TaxRate.Select(x => new TaxRegion
                    {
                        RegionID = region.ID,
                        Name = region.Name,
                        Rate = x.StateRate,
                        CreatedDate = now,
                        Active = true,
                        CustomKey = x.ZipCode,
                    }));
                }
                taxRegions.ForEach(x => context.TaxRegions.Add(x));
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        [PublicAPI]
        private class TaxRate
        {
            [CsvColumn(Name = "State")]
            public string? State { get; set; }

            [CsvColumn(Name = "ZipCode")]
            public string? ZipCode { get; set; }

            [CsvColumn(Name = "TaxRegionName")]
            public string? TaxRegionName { get; set; }

            [CsvColumn(Name = "StateRate")]
            public decimal StateRate { get; set; }

            [CsvColumn(Name = "EstimatedCombinedRate")]
            public decimal EstimatedCombinedRate { get; set; }

            [CsvColumn(Name = "EstimatedCountyRate")]
            public decimal EstimatedCountyRate { get; set; }

            [CsvColumn(Name = "EstimatedCityRate")]
            public decimal EstimatedCityRate { get; set; }

            [CsvColumn(Name = "EstimatedSpecialRate")]
            public decimal EstimatedSpecialRate { get; set; }

            [CsvColumn(Name = "RiskLevel")]
            public int RiskLevel { get; set; }
        }
    }
}
#endif
