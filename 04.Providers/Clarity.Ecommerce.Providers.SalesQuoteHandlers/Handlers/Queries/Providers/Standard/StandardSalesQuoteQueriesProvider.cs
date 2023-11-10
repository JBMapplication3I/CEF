// <copyright file="StandardSalesQuoteQueriesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard queries provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Standard
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A standard sales quote queries provider.</summary>
    /// <seealso cref="SalesQuoteQueriesProviderBase"/>
    public class StandardSalesQuoteQueriesProvider : SalesQuoteQueriesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSalesQuoteQueriesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<ISalesQuoteModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName)
        {
            var model = await Workflows.SalesQuotes.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckNotNull(model)
                && accountIDs.Exists(x => x == model!.AccountID)
                && model!.Active)
            {
                return model;
            }
            throw HttpError.Unauthorized("Unauthorized to view this SalesQuote");
        }
    }
}
