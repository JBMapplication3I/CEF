// <copyright file="PhonePrefixLookupCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the phone prefix lookup workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Utilities;

    public partial class PhonePrefixLookupWorkflow
    {
        /// <inheritdoc/>
        public async Task<(List<IPhonePrefixLookupModel> results, int totalPages, int totalCount)> ReversePhonePrefixToCityRegionCountryAsync(
            string prefix, string? contextProfileName)
        {
            Contract.RequiresValidKey(prefix);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            (List<IPhonePrefixLookupModel> results, int totalPages, int totalCount) results = (new(), 0, 0);
            var query = context.PhonePrefixLookups.AsNoTracking().FilterByActive(true);
            var search = prefix.Length > 5 ? prefix[..5] : prefix;
            var model = new PhonePrefixLookupSearchModel { Prefix = search };
            if (query.Any(x => x.Prefix != null && x.Prefix.StartsWith(search)))
            {
                return await SearchAsync(model, true, contextProfileName).ConfigureAwait(false);
            }
            if (search.Length >= 4)
            {
                search = search[..4];
                model.Prefix = search;
                if (query.Any(x => x.Prefix != null && x.Prefix.StartsWith(search)))
                {
                    return await SearchAsync(model, true, contextProfileName).ConfigureAwait(false);
                }
            }
            if (search.Length >= 3)
            {
                search = search[..3];
                model.Prefix = search;
                if (query.Any(x => x.Prefix != null && x.Prefix.StartsWith(search)))
                {
                    return await SearchAsync(model, true, contextProfileName).ConfigureAwait(false);
                }
            }
            if (search.Length >= 2)
            {
                search = search[..2];
                model.Prefix = search;
                if (query.Any(x => x.Prefix != null && x.Prefix.StartsWith(search)))
                {
                    return await SearchAsync(model, true, contextProfileName).ConfigureAwait(false);
                }
            }
            // ReSharper disable once InvertIf
            if (search.Length >= 1 && !search.StartsWith("+"))
            {
                search = search[..1];
                model.Prefix = search;
                if (query.Any(x => x.Prefix != null && x.Prefix.StartsWith(search)))
                {
                    return await SearchAsync(model, true, contextProfileName).ConfigureAwait(false);
                }
            }
            return results;
        }
    }
}
