// <copyright file="UiTranslationService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UI translation service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Globalization/UiTranslationsDictionary", "GET",
            Summary = "Use to get a list of ui translations")]
    public class GetUiTranslationsDictionary
        : UiTranslationSearchModel,
            IReturn<Dictionary<string, Dictionary<string, string>>>
    {
    }

    public partial class UiTranslationService
    {
        protected override List<string> AdditionalUrnIDs => new()
        {
            UrnId.Create<GetUiTranslationsDictionary>(string.Empty),
        };

        public async Task<object?> Get(GetUiTranslationsDictionary request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.UiTranslations.GetLastModifiedForResultSetAsync(request, contextProfileName: null),
                    () => Workflows.UiTranslations.SearchAndReturnDictionaryAsync(request, null))
                .ConfigureAwait(false);
        }
    }
}
