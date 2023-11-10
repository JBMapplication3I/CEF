// <copyright file="SalesQuoteQueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries provider base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesQuoteHandlers.Queries;

    /// <summary>The sales quote queries provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesQuoteQueriesProviderBase"/>
    public abstract class SalesQuoteQueriesProviderBase : ProviderBase, ISalesQuoteQueriesProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesQuoteQueriesHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<ISalesQuoteModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);
    }
}
