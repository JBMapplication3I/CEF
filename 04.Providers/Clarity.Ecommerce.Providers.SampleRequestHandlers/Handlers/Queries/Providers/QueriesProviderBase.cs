// <copyright file="QueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries provider base class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SampleRequestHandlers.Queries;

    /// <summary>The sample reqeust queries provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISampleRequestQueriesProviderBase"/>
    public abstract class SampleRequestQueriesProviderBase : ProviderBase, ISampleRequestQueriesProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SampleRequestQueriesHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<ISampleRequestModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);
    }
}
