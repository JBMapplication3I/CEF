// <copyright file="SalesCollectionWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow for sales collection bases class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Utilities;

    /// <summary>A workflow base for sales collections.</summary>
    /// <typeparam name="TIModel">            Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">      Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">           Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">            Type of the entity.</typeparam>
    /// <typeparam name="TStatus">            Type of the status entity.</typeparam>
    /// <typeparam name="TType">              Type of the type entity.</typeparam>
    /// <typeparam name="TITypeModel">        Type of the type model's interface.</typeparam>
    /// <typeparam name="TISalesItem">        Type of the sales item entity's interface.</typeparam>
    /// <typeparam name="TSalesItem">         Type of the sales item entity.</typeparam>
    /// <typeparam name="TDiscount">          Type of the discount entity.</typeparam>
    /// <typeparam name="TState">             Type of the state entity.</typeparam>
    /// <typeparam name="TStoredFile">        Type of the stored file entity.</typeparam>
    /// <typeparam name="TIStoredFileModel">  Type of the stored file model's interface.</typeparam>
    /// <typeparam name="TContact">           Type of the contact entity.</typeparam>
    /// <typeparam name="TIContactModel">     Type of the contact model's interface.</typeparam>
    /// <typeparam name="TSalesEvent">        Type of the sales event entity.</typeparam>
    /// <typeparam name="TISalesEventModel">  Type of the sales event model's entity.</typeparam>
    /// <typeparam name="TSalesEventType">    Type of the sales event type entity.</typeparam>
    /// <typeparam name="TSalesItemDiscount"> Type of the sales item discount entity.</typeparam>
    /// <typeparam name="TSalesItemTarget">   Type of the sales item target entity.</typeparam>
    /// <typeparam name="TIDiscountModel">    Type of the discount model's interface.</typeparam>
    /// <typeparam name="TIItemDiscountModel">Type of the item discount model's interface.</typeparam>
    public abstract class SalesCollectionWorkflowBase<TIModel,
            TISearchModel,
            TIEntity,
            TEntity,
            TStatus,
            TType,
            TITypeModel,
            TISalesItem,
            TSalesItem,
            TDiscount,
            TState,
            TStoredFile,
            TIStoredFileModel,
            TContact,
            TIContactModel,
            TSalesEvent,
            TISalesEventModel,
            TSalesEventType,
            TSalesItemDiscount,
            TSalesItemTarget,
            TIDiscountModel,
            TIItemDiscountModel>
        : WorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>,
            ISalesCollectionWorkflowBase<TIModel,
                TISearchModel,
                TIEntity,
                TEntity,
                TStatus,
                TType,
                TISalesItem,
                TSalesItem,
                TDiscount,
                TState,
                TStoredFile,
                TContact,
                TSalesItemDiscount,
                TSalesItemTarget,
                TSalesEvent,
                TSalesEventType>
        where TIModel : class, ISalesCollectionBaseModel<TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel>
        where TISearchModel : class, ISalesCollectionBaseSearchModel
        where TIEntity : ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
        where TEntity : class, TIEntity, new()
        where TStatus : class, IStatusableBase, new()
        where TType : class, ITypableBase, new()
        where TITypeModel : ITypableBaseModel
        where TISalesItem : ISalesItemBase<TSalesItem, TSalesItemDiscount, TSalesItemTarget>
        where TSalesItem : class, TISalesItem, new()
        where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
        where TState : class, IStateableBase, new()
        where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
        where TIStoredFileModel : IAmAStoredFileRelationshipTableModel
        where TContact : IAmAContactRelationshipTable<TEntity, TContact>
        where TIContactModel : IAmAContactRelationshipTableModel
        where TSalesItemDiscount : IAppliedDiscountBase<TSalesItem, TSalesItemDiscount>
        where TSalesItemTarget : ISalesItemTargetBase
        where TIDiscountModel : IAppliedDiscountBaseModel
        where TIItemDiscountModel : IAppliedDiscountBaseModel
        where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
        where TSalesEventType : ITypableBase
        where TISalesEventModel : ISalesEventBaseModel
    {
        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> CheckExistsAndOwnershipByPortalAdminAsync(
            int id,
            int userID,
            List<int>? accountIDs,
            int portalID,
            Enums.APIKind currentAPIKind,
            string? contextProfileName)
        {
            if (Contract.CheckInvalidID(await CheckExistsAsync(id, contextProfileName).ConfigureAwait(false)))
            {
                return CEFAR.FailingCEFAR("ERROR! Record not found.");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Set<TEntity>()
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(id);
            switch (currentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    query = query.FilterByNullableBrandID(portalID);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    query = query.FilterByNullableFranchiseID(portalID);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    query = query.FilterByNullableStoreID(portalID);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin when typeof(TEntity) == typeof(DataModel.PurchaseOrder):
                case Enums.APIKind.VendorAdmin when typeof(TEntity) == typeof(DataModel.PurchaseOrder):
                {
                    // Manufacturer and Vendor Admins should only be able to see Purchase Orders
                    query = (IQueryable<TEntity>)((IQueryable<DataModel.PurchaseOrder>)query)
                        .Where(x => x.VendorID == portalID);
                    break;
                }
                case Enums.APIKind.Storefront when Contract.CheckNotEmpty(accountIDs) && typeof(TEntity) != typeof(DataModel.PurchaseOrder):
                {
                    // Storefront should never see Purchase Orders
                    query = query.Where(x => x.AccountID > 0 && accountIDs!.Contains(x.AccountID.Value));
                    break;
                }
                default:
                {
                    return CEFAR.FailingCEFAR("ERROR! No rights to the specified record");
                }
            }
            var existing = await query.Select(x => x.ID).SingleOrDefaultAsync().ConfigureAwait(false);
            return Contract.CheckValidID(existing)
                .BoolToCEFAR("ERROR! No rights to the specified record");
        }

        /// <inheritdoc/>
        protected override Task AssignAdditionalPropertiesAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.Active = model.Active;
            entity.CustomKey = model.CustomKey;
            // SalesCollectionBase Properties
            if (model.Totals != null)
            {
                entity.SubtotalItems = model.Totals.SubTotal;
                entity.SubtotalDiscounts = model.Totals.Discounts;
                entity.SubtotalFees = model.Totals.Fees;
                entity.SubtotalHandling = model.Totals.Handling;
                entity.SubtotalShipping = model.Totals.Shipping;
                entity.SubtotalTaxes = model.Totals.Tax;
                entity.Total = model.Totals.Total;
            }
            entity.DueDate = model.DueDate;
            entity.ShippingSameAsBilling = model.ShippingSameAsBilling;
            // Related Objects
            // ReSharper disable once InvertIf
            if (model.ShippingSameAsBilling == true)
            {
                model.ShippingContactID = model.BillingContactID;
                model.ShippingContactKey = model.BillingContactKey;
                model.ShippingContact = model.BillingContact;
            }
            return RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName);
        }

        /// <inheritdoc/>
        [SuppressMessage(
            "StyleCop.CSharp.ReadabilityRules",
            "SA1110:OpeningParenthesisMustBeOnDeclarationLine",
            Justification = "Reviewed. Suppression is OK here. Cannot satisfy without causing another rule issue.")]
        protected override async Task<IQueryable<TEntity>> FilterQueryByModelExtensionAsync(
            IQueryable<TEntity> query,
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterSalesCollectionsBySearchModel<TEntity,
                    TStatus,
                    TType,
                    TSalesItem,
                    TDiscount,
                    TState,
                    TStoredFile,
                    TContact,
                    TSalesItemDiscount,
                    TSalesItemTarget,
                    TSalesEvent,
                    TSalesEventType>(search);
        }
    }
}
