// <copyright file="IStoreWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IStoreWorkflow
    {
        /// <summary>Gets a full.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The full.</returns>
        Task<IStoreModel?> GetFullAsync(int id, string? contextProfileName);

        /// <summary>Gets a full.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The full.</returns>
        Task<IStoreModel?> GetFullAsync(string key, string? contextProfileName);

        /// <summary>Gets sales count.</summary>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="type">              The type.</param>
        /// <param name="status">            The status.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales count.</returns>
        Task<CEFActionResponse<decimal>> GetSalesCountAsync(int storeID, string type, string status, string? contextProfileName);

        /// <summary>Gets sales count.</summary>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="type">              The type.</param>
        /// <param name="status">            The status.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales count.</returns>
        Task<CEFActionResponse<decimal>> GetSalesCountAsync(int storeID, string type, string[] status, string? contextProfileName);

        /// <summary>Gets a revenue.</summary>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="requestDays">       The request in days.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The revenue.</returns>
        Task<CEFActionResponse<decimal>> GetRevenueAsync(int storeID, int requestDays, string? contextProfileName);

        /// <summary>Gets store inventory locations matrix last modified.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The store inventory locations matrix last modified.</returns>
        Task<DateTime?> GetStoreInventoryLocationsMatrixLastModifiedAsync(string? contextProfileName);

        /// <summary>Gets store inventory locations matrix.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The store inventory locations matrix.</returns>
        Task<List<IStoreInventoryLocationsMatrixModel>> GetStoreInventoryLocationsMatrixAsync(string? contextProfileName);

        /// <summary>Gets store administrator user.</summary>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The store administrator user.</returns>
        Task<CEFActionResponse<IUserModel>> GetStoreAdministratorUserAsync(int storeID, string? contextProfileName);

        /// <summary>Updates the attributes.</summary>
        /// <param name="storeID">               Identifier for the store.</param>
        /// <param name="serializableAttributes">The serializable attributes.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse{SerializableAttributesDictionary}.</returns>
        Task<CEFActionResponse<SerializableAttributesDictionary>> UpdateAttributesAsync(
            int storeID,
            SerializableAttributesDictionary serializableAttributes,
            string? contextProfileName);

        /// <summary>Clones a store.</summary>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The revenue.</returns>
        Task<CEFActionResponse<IStoreModel>> CloneStoreAsync(int storeID, string? contextProfileName);

        /// <summary>Gets store identifier for admin portal.</summary>
        /// <param name="hostUrl">           URL of the host.</param>
        /// <param name="sessionUserAuthId"> Identifier for the session user authentication.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The store identifier for admin portal.</returns>
        Task<int?> GetStoreIDForAdminPortalAsync(string hostUrl, string sessionUserAuthId, string? contextProfileName);
    }
}
