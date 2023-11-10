// <copyright file="ICartTypeWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartTypeWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.DataModel;
    using Ecommerce.Models;
    using Models;

    public partial interface ICartTypeWorkflow
    {
        /// <summary>Gets types for user.</summary>
        /// <param name="userID">                 Identifier for the user.</param>
        /// <param name="includeNotCreatedByUser">true to include, false to exclude the not created by user.</param>
        /// <param name="filterCartsByOrderRequest">true to filter cart by order requests.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>The types for user.</returns>
        Task<List<ICartTypeModel>> GetTypesForUserAsync(
            int userID,
            bool includeNotCreatedByUser,
            bool filterCartsByOrderRequest,
            string? contextProfileName);

        /// <summary>Gets for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="typeName">          Name of the type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>Get for user.</returns>
        Task<CEFActionResponse<ICartTypeModel>> GetForUserAsync(
            int userID,
            string typeName,
            string? contextProfileName);

        /// <summary>Upsert for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="doSave">            True to do save.</param>
        /// <returns>A Task{ICartTypeModel}.</returns>
        Task<ICartTypeModel?> UpsertForUserAsync(
            int userID,
            ICartTypeModel model,
            string? contextProfileName,
            bool doSave = true);

        /// <summary>Creates for user.</summary>
        /// <param name="userID"> Identifier for the user.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="doSave"> True to do save.</param>
        /// <returns>The new for user.</returns>
        Task<ICartTypeModel?> CreateForUserAsync(
            int userID,
            ICartTypeModel model,
            IClarityEcommerceEntities context,
            bool doSave = true);

        /// <summary>Deletes for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="cartTypeName">      Name of the cart type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="doSave">            True to do save.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> DeleteForUserAsync(
            int userID,
            string cartTypeName,
            string? contextProfileName,
            bool doSave = true);

        /// <summary>Deletes for user.</summary>
        /// <param name="brandID">           ID of the current user's brand.</param>
        /// <param name="userIDs">           Array of the selected users.</param>
        /// <param name="cartTypeID">        ID of the cart type.</param>
        /// <param name="currentAccountID">  ID of the current account.</param>
        /// <param name="currentUserID">     ID of the current user.</param>
        /// <param name="productQuantities"> A dictionary holding the product quantities.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ShareShoppingCartsWithSelectedUsersAsync(
            int brandID,
            int[]? userIDs,
            int? cartTypeID,
            int? currentAccountID,
            int? currentUserID,
            Dictionary<int, int>? productQuantities,
            string? contextProfileName);
    }
}
