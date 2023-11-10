// <copyright file="ISampleRequestQueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISampleRequestQueriesProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SampleRequestHandlers.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for sample request queries provider.</summary>
    public interface ISampleRequestQueriesProviderBase : IProviderBase
    {
        /// <summary>>Gets the sample request only if the supplied AccountID exists on the record.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountIDs">        The account IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Sample Request requested that is confirmed by the supplied account id.</returns>
        Task<ISampleRequestModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);
    }
}
