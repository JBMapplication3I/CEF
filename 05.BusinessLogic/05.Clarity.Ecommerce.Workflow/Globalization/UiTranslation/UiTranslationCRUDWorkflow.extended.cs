// <copyright file="UiTranslationCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ui translation workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    /// <summary>A UI Translation workflow.</summary>
    public partial class UiTranslationWorkflow
    {
        /// <inheritdoc/>
        public async Task<Dictionary<string, Dictionary<string, string>>> SearchAndReturnDictionaryAsync(
            IUiTranslationSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.UiTranslations.AsNoTracking().Include(x => x.UiKey).AsQueryable();
            return (await FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterByModifiedSince(search.ModifiedSince)
                .SelectListUiTranslationAndMapToUiTranslationModel(search.Paging, search.Sorts, search.Groupings, contextProfileName)
                .results
                .GroupBy(x => x.Locale!)
                .ToDictionary(x => x.Key!, v => v.ToDictionary(v1 => v1.UiKey!.CustomKey!, v2 => v2.Value!));
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<UiTranslation>> FilterQueryByModelCustomAsync(
            IQueryable<UiTranslation> query,
            IUiTranslationSearchModel search,
            IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidID(search.LanguageID))
            {
                search.Locale = await context.Languages
                    .AsNoTracking()
                    .FilterByID(search.LanguageID!.Value)
                    .Select(x => x.Locale)
                    .SingleAsync();
            }
            return query
                .FilterUiTranslationsByLocale(search.Locale)
                .FilterUiTranslationsByKeyStartsWith(search.KeyStartsWith)
                .FilterUiTranslationsByKeyEndsWith(search.KeyEndsWith)
                .FilterUiTranslationsByKeyContains(search.KeyContains)
                .FilterUiTranslationsByValue(search.Value);
        }
    }
}
