// <copyright file="ISalesQuoteQueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesQuoteQueriesProviderBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Providers.SalesQuoteHandlers.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for sales quote queries provider.</summary>
    public interface ISalesQuoteQueriesProviderBase : IProviderBase
    {
        /// <summary>>Gets the sales quote only if the supplied AccountID exists on the record.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountIDs">        The account IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Sales Quote requested that is confirmed by the supplied account id.</returns>
        Task<ISalesQuoteModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);
    }
}
