// <copyright file="StoreDumpReader.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store dump reader class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Domain.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    /// <summary>A store dump reader.</summary>
    public class StoreDumpReader
    {
        private const decimal CoreWeightMultiplier = 100m;

        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the products in this collection.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process the products in this collection.</returns>
        public IEnumerable<IndexableStoreModel> GetStores(string contextProfileName)
        {
            Logger.LogInformation($"{nameof(StoreDumpReader)}.${nameof(GetStores)}", "Entered", contextProfileName);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var attributes = context.GeneralAttributes
                .FilterByActive(true)
                .Where(x => x.IsFilter)
                .Where(x => !x.IsMarkup && !x.HideFromStorefront)
                .Select(x => new AttrModel
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = true,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    SortOrder = x.SortOrder,
                    IsFilter = true,
                    TypeID = x.TypeID,
                })
                .ToDictionary(x => x.CustomKey, x => x);
            // Get List of type keys we want to filter by
            var keys = SearchConfiguration.FilterByTypeKeysStore;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!/*.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)*/.Select(x => x.Trim()).ToArray();
            }
            // TODO: Figure out how to apply this with the multiple variants
            foreach (var storeVersion in context.Stores
                ////.Include(x => x.ProductCategories)
                ////.Include(x => x.ProductCategories.Select(y => y.Slave))
                .FilterByActive(true)
                .FilterByTypeKeys<Store, StoreType>(keyList)
                .OrderBy(x => x.ID)
                .Take(200_000)
                .Select(x => new IndexableStoreModel
                {
                    // Base Properties
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = true,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    // NameableBase Properties
                    Name = x.Name.Replace("  ", " ").Replace("  ", " "),
                    Description = x.Description,
                    // IHaveSeoBase Properties
                    SeoUrl = x.SeoUrl,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    ////SeoPageTitle = x.SeoPageTitle,
                    // IHaveJsonAttributesBase Properties
                    JsonAttributes = x.JsonAttributes,
                    // Store Properties
                    Slogan = x.Slogan,
                    MissionStatement = x.MissionStatement,
                    ExternalUrl = x.ExternalUrl,
                    About = x.About,
                    Overview = x.Overview,
                    OperatingHoursTimeZoneId = x.OperatingHoursTimeZoneId,
                    OperatingHoursMondayStart = x.OperatingHoursMondayStart,
                    OperatingHoursMondayEnd = x.OperatingHoursMondayEnd,
                    OperatingHoursTuesdayStart = x.OperatingHoursTuesdayStart,
                    OperatingHoursTuesdayEnd = x.OperatingHoursTuesdayEnd,
                    OperatingHoursWednesdayStart = x.OperatingHoursWednesdayStart,
                    OperatingHoursWednesdayEnd = x.OperatingHoursWednesdayEnd,
                    OperatingHoursThursdayStart = x.OperatingHoursThursdayStart,
                    OperatingHoursThursdayEnd = x.OperatingHoursThursdayEnd,
                    OperatingHoursFridayStart = x.OperatingHoursFridayStart,
                    OperatingHoursFridayEnd = x.OperatingHoursFridayEnd,
                    OperatingHoursSaturdayStart = x.OperatingHoursSaturdayStart,
                    OperatingHoursSaturdayEnd = x.OperatingHoursSaturdayEnd,
                    OperatingHoursSundayStart = x.OperatingHoursSundayStart,
                    OperatingHoursSundayEnd = x.OperatingHoursSundayEnd,
                    OperatingHoursClosedStatement = x.OperatingHoursClosedStatement,
                    // Convenience Properties
                    PrimaryImageFileName = x.Images
                        .Where(y => y.Active)
                        .OrderByDescending(y => y.IsPrimary)
                        .ThenByDescending(y => y.OriginalWidth)
                        .ThenByDescending(y => y.OriginalHeight)
                        .Select(y => y.ThumbnailFileName ?? y.OriginalFileName)
                        .FirstOrDefault(),
                }))
            {
                // QuickLogger.Logger(contextProfileName, "Entered StoreDumpReader.GetStores(...)", $"adding store ID {storeVersion.ID} to the 'to index' list");
                storeVersion.Attributes = !string.IsNullOrEmpty(storeVersion.JsonAttributes)
                    ? storeVersion.JsonAttributes.DeserializeAttributesDictionary()
                        .Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Key != "undefined" && x.Value != null)
                        .Where(x => attributes.ContainsKey(x.Key))
                        .Select(x => new IndexableSerializableAttributeObject
                        {
                            ID = x.Value.ID > 0 ? x.Value.ID : attributes[x.Key].ID,
                            Key = x.Value.Key ?? x.Key,
                            SortOrder = x.Value.SortOrder ?? attributes[x.Key].SortOrder,
                            Value = x.Value.Value?.ToString(),
                            UofM = x.Value.UofM,
                        })
                    : null;
                storeVersion.JsonAttributes = null;
                storeVersion.SuggestedByName = new CompletionField
                {
                    Input = new List<string>(storeVersion.Name.Split(' ')) { storeVersion.Name },
                    Weight = (int)(2 * CoreWeightMultiplier),
                };
                storeVersion.SuggestedByCustomKey = new CompletionField
                {
                    Input = new List<string>((storeVersion.CustomKey ?? storeVersion.Name).Split(' ')) { storeVersion.CustomKey },
                    Weight = (int)(2 * CoreWeightMultiplier),
                };
                yield return storeVersion;
            }
            // QuickLogger.Logger(contextProfileName, "Exited StoreDumpReader.GetStores(...)");
        }
    }
}
