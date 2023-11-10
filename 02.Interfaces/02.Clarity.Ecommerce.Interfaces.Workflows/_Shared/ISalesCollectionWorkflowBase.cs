// <copyright file="ISalesCollectionWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesCollectionWorkflowBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for workflow for sales collection bases.</summary>
    /// <typeparam name="TIModel">        Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">  Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">       Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">        Type of the entity.</typeparam>
    /// <typeparam name="TStatus">        Type of the status.</typeparam>
    /// <typeparam name="TType">          Type of the type.</typeparam>
    /// <typeparam name="TISalesItem">    Type of the sales item interface.</typeparam>
    /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
    /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
    /// <typeparam name="TState">         Type of the state.</typeparam>
    /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
    /// <typeparam name="TContact">       Type of the contact.</typeparam>
    /// <typeparam name="TItemDiscount">  Type of the item discount.</typeparam>
    /// <typeparam name="TItemTarget">    Type of the item target.</typeparam>
    /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
    /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
    /// <seealso cref="IWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface ISalesCollectionWorkflowBase<TIModel,
            TISearchModel,
            TIEntity,
            TEntity,
            TStatus,
            TType,
            in TISalesItem,
            in TSalesItem,
            TDiscount,
            TState,
            TStoredFile,
            TContact,
            TItemDiscount,
            TItemTarget,
            TSalesEvent,
            TSalesEventType>
        : IWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, ISalesCollectionBaseModel
        where TISearchModel : class, ISalesCollectionBaseSearchModel
        where TIEntity : ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
        where TEntity : class, TIEntity, new()
        where TISalesItem : ISalesItemBase<TSalesItem, TItemDiscount, TItemTarget>
        where TSalesItem : class, TISalesItem, IHaveAppliedDiscountsBase<TSalesItem, TItemDiscount>, new()
        where TStatus : class, IStatusableBase, new()
        where TState : class, IStateableBase, new()
        where TType : class, ITypableBase, new()
        where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
        where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
        where TContact : IAmAContactRelationshipTable<TEntity, TContact>
        where TItemDiscount : IAppliedDiscountBase<TSalesItem, TItemDiscount>
        where TItemTarget : ISalesItemTargetBase
        where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
        where TSalesEventType : ITypableBase
    {
        /// <summary>Check exists and ownership by portal admin.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="accountIDs">        The account IDs.</param>
        /// <param name="portalID">          Identifier for the portal.</param>
        /// <param name="currentAPIKind">    The current a pi kind.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> CheckExistsAndOwnershipByPortalAdminAsync(
            int id,
            int userID,
            List<int>? accountIDs,
            int portalID,
            Enums.APIKind currentAPIKind,
            string? contextProfileName);
    }
}
