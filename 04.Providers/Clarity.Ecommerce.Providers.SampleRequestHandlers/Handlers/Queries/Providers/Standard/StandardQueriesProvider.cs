// <copyright file="StandardQueriesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard queries provider class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Standard
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A standard sample request queries provider.</summary>
    /// <seealso cref="SampleRequestQueriesProviderBase"/>
    public class StandardSampleRequestQueriesProvider : SampleRequestQueriesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSampleRequestQueriesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<ISampleRequestModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName)
        {
            var model = await Workflows.SampleRequests.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckNotNull(model)
                && accountIDs.Exists(x => x == model!.AccountID)
                && model!.Active)
            {
                return model;
            }
            throw HttpError.Unauthorized("Unauthorized to view this SampleRequest");
        }
    }
}
