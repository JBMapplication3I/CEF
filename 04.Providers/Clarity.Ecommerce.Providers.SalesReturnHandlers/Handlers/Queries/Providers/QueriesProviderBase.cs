// <copyright file="QueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries provider base class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesReturnHandlers.Queries;
    using Models;

    /// <summary>The sales return queries provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesReturnQueriesProviderBase"/>
    public abstract class SalesReturnQueriesProviderBase : ProviderBase, ISalesReturnQueriesProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesReturnQueriesHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ValidateSalesOrderReadyForReturnAsync(
            int salesOrderID,
            string? contextProfileName,
            bool isBackendOverride = false);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ValidateSalesOrderReadyForReturnAsync(
            string salesOrderStatusKey,
            DateTime salesOrderCreatedDate,
            bool isBackendOverride,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ValidateSalesReturnAsync(
            ISalesReturnModel salesReturnModel,
            string? contextProfileName,
            bool isBackendOverride = false);

        /// <inheritdoc/>
        public abstract Task<ISalesReturnModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);
    }
}
