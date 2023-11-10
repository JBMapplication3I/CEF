// <copyright file="SeedTaxesFromFile.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the seed taxes from file class</summary>
namespace Clarity.Ecommerce.Providers.SeedDataBase
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using MoreLinq;
    using Newtonsoft.Json;
    using Xunit;

    [Trait("Category", "Seed Database.FromFile.CountriesAndRegions")]
    public class SeedCountriesRegionsFromFile
    {
        private const string FolderPath = @"C:\Data\Documents";
        // http://download.geonames.org/export/dump/
        private const string CountriesFileName = "countryInfo.txt";
        private const string RegionsFileName = "admin1CodesASCII.txt";
        private static DateTime Now => DateTime.Now;

        private GeneralAttribute countryIso = null!;
        private GeneralAttribute countryIsoNumeric = null!;
        private GeneralAttribute countryContinent = null!;
        private GeneralAttribute countryCurrencyCode = null!;
        private GeneralAttribute countryCurrencyName = null!;
        private GeneralAttribute countryPhone = null!;
        private GeneralAttribute countryPostalCodeFormat = null!;
        private GeneralAttribute countryPostalCodeRegex = null!;
        private GeneralAttribute countryLanguages = null!;

        [Fact(Skip = "Run this manually to import countries and regions")]
        public async Task ImportCountriesRegionsFileAsync()
        {
            var countryStringArrays = new List<string[]>();
            using (var streamReader = new StreamReader($"{FolderPath}\\{CountriesFileName}"))
            {
                while (streamReader.Peek() > 0)
                {
                    var line = await streamReader.ReadLineAsync().ConfigureAwait(false);
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    {
                        continue;
                    }
                    countryStringArrays.Add(line.Split('\t'));
                }
            }
            var regionStringArrays = new List<string[]>();
            using (var streamReader = new StreamReader($"{FolderPath}\\{RegionsFileName}"))
            {
                while (streamReader.Peek() > 0)
                {
                    var line = await streamReader.ReadLineAsync().ConfigureAwait(false);
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var splitLine = line.Split('\t');
                    var countryRegionSplit = splitLine[0].Split('.');
                    var newSplitLine = new string[splitLine.Length + 1];
                    newSplitLine[0] = countryRegionSplit[0];
                    newSplitLine[1] = countryRegionSplit[1];
                    for (var i = 1; i < splitLine.Length; i++)
                    {
                        newSplitLine[i + 1] = splitLine[i];
                    }
                    regionStringArrays.Add(newSplitLine);
                }
            }
            var groupedRegionStringArrays = regionStringArrays
                .Where(x => !string.IsNullOrEmpty(x[0]))
                .GroupBy(
                    x => x[0],
                    x => x,
                    (countryCode, regionInfo) => new
                    {
                        CountryCode = countryCode,
                        RegionInfo = regionInfo,
                    })
                .ToList();
            using (var context = new ClarityEcommerceEntities())
            {
                var countryAttributeType = await context.AttributeTypes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY").ConfigureAwait(false)
                    ?? context.AttributeTypes.Add(new AttributeType
                    {
                        Name = "Country",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY",
                        DisplayName = "Country",
                        JsonAttributes = "{}",
                    });
                countryIso = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-ISO").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "ISO",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-ISO",
                        IsFilter = true,
                    });
                countryIsoNumeric = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-ISO-Numeric").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "ISO-Numeric",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-ISO-Numeric",
                        IsFilter = true,
                    });
                countryContinent = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-Continent").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "Continent",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-Continent",
                        IsFilter = true,
                    });
                countryCurrencyCode = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-CurrencyCode").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "CurrencyCode",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-CurrencyCode",
                        IsFilter = true,
                    });
                countryCurrencyName = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-CurrencyName").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "CurrencyName",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-CurrencyName",
                        IsFilter = true,
                    });
                countryPhone = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-Phone").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "Phone",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-Phone",
                        IsFilter = true,
                    });
                countryPostalCodeFormat = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-Postal Code Format").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "Postal Code Format",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-Postal Code Format",
                        IsFilter = true,
                    });
                countryPostalCodeRegex = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-Postal Code Regex").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "Postal Code Regex",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-Postal Code Regex",
                        IsFilter = false,
                    });
                countryLanguages = await context.GeneralAttributes.SingleOrDefaultAsync(x => x.CustomKey == "COUNTRY-Languages").ConfigureAwait(false)
                    ?? context.GeneralAttributes.Add(new GeneralAttribute
                    {
                        Type = countryAttributeType,
                        Name = "Languages",
                        Active = true,
                        CreatedDate = Now,
                        CustomKey = "COUNTRY-Languages",
                        IsFilter = true,
                    });
                await context.SaveChangesAsync().ConfigureAwait(false);
                foreach (var countryStringArray in countryStringArrays)
                {
                    var countryCode = countryStringArray[1];
                    var code = countryCode;
                    var country = await context.Countries.SingleOrDefaultAsync(x => x.Code == code).ConfigureAwait(false);
                    if (country != null && country.Code != "USA")
                    {
                        continue;
                    }
                    var dictionary = GetCountrySerializableAttributes(countryStringArray);
                    var serializableAttributesDictionary = new SerializableAttributesDictionary(dictionary);
                    var jsonAttributes = JsonConvert.SerializeObject(serializableAttributesDictionary);
                    if (country?.Code == "USA")
                    {
                        country.JsonAttributes = jsonAttributes;
                        continue;
                    }
                    country = context.Countries.Add(new Country
                    {
                        Active = true,
                        Name = countryStringArray[4],
                        CustomKey = countryStringArray[4],
                        Code = countryStringArray[1],
                        CreatedDate = Now,
                        JsonAttributes = jsonAttributes,
                    });
                    countryCode = countryStringArray[0];
                    var region = groupedRegionStringArrays.SingleOrDefault(x => x.CountryCode == countryCode);
                    if (region == null)
                    {
                        continue;
                    }
                    region.RegionInfo
                        .Select(x => new Region
                        {
                            Active = true,
                            Name = x[2],
                            CustomKey = x[2],
                            Code = x[1],
                            CreatedDate = Now,
                            Country = country,
                        })
                        .ForEach(x => context.Regions.Add(x));
                }
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private Dictionary<string, SerializableAttributeObject> GetCountrySerializableAttributes(IReadOnlyList<string> countryStringArray)
        {
            return new Dictionary<string, SerializableAttributeObject>
            {
                [countryIso.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryIso.ID,
                    Key = countryIso.CustomKey!,
                    Value = countryStringArray[0],
                },
                [countryIsoNumeric.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryIsoNumeric.ID,
                    Key = countryIsoNumeric.CustomKey!,
                    Value = countryStringArray[2],
                },
                [countryContinent.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryContinent.ID,
                    Key = countryContinent.CustomKey!,
                    Value = countryStringArray[8],
                },
                [countryCurrencyCode.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryCurrencyCode.ID,
                    Key = countryCurrencyCode.CustomKey!,
                    Value = countryStringArray[10],
                },
                [countryCurrencyName.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryCurrencyName.ID,
                    Key = countryCurrencyName.CustomKey!,
                    Value = countryStringArray[11],
                },
                [countryPhone.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryPhone.ID,
                    Key = countryPhone.CustomKey!,
                    Value = countryStringArray[12],
                },
                [countryPostalCodeFormat.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryPostalCodeFormat.ID,
                    Key = countryPostalCodeFormat.CustomKey!,
                    Value = countryStringArray[13],
                },
                [countryPostalCodeRegex.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryPostalCodeRegex.ID,
                    Key = countryPostalCodeRegex.CustomKey!,
                    Value = countryStringArray[14],
                },
                [countryLanguages.CustomKey!] = new SerializableAttributeObject
                {
                    ID = countryLanguages.ID,
                    Key = countryLanguages.CustomKey!,
                    Value = countryStringArray[15]
                },
            };
        }
    }
}
