// <copyright file="ProductWithProductRestrictionsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithProductRestrictionsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IProductRestrictionModel model,
            IProductRestriction entity,
            IClarityEcommerceEntities context)
        {
            return entity.CanPurchaseDomestically == model.CanPurchaseDomestically
                && entity.CanPurchaseInternationally == model.CanPurchaseInternationally
                && entity.CanPurchaseIntraRegion == model.CanPurchaseIntraRegion
                && entity.CanShipDomestically == model.CanShipDomestically
                && entity.CanShipInternationally == model.CanShipInternationally
                && entity.CanShipIntraRegion == model.CanShipIntraRegion
                && entity.OverrideWithAccountTypeID == model.OverrideWithAccountTypeID
                && entity.OverrideWithRoles == model.OverrideWithRoles
                && entity.ProductID == model.ProductID
                && entity.RestrictionsApplyToCity == model.RestrictionsApplyToCity
                && entity.RestrictionsApplyToCountryID == model.RestrictionsApplyToCountryID
                && entity.RestrictionsApplyToPostalCode == model.RestrictionsApplyToPostalCode
                && entity.RestrictionsApplyToRegionID == model.RestrictionsApplyToRegionID;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IProductRestriction newEntity,
            IProductRestrictionModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // ProductAssociation Properties
            newEntity.Active = model.Active;
            newEntity.CanPurchaseDomestically = model.CanPurchaseDomestically;
            newEntity.CanPurchaseInternationally = model.CanPurchaseInternationally;
            newEntity.CanPurchaseIntraRegion = model.CanPurchaseIntraRegion;
            newEntity.CanShipDomestically = model.CanShipDomestically;
            newEntity.CanShipInternationally = model.CanShipInternationally;
            newEntity.CanShipIntraRegion = model.CanShipIntraRegion;
            // Related Objects
            if (Contract.CheckValidIDOrAnyValidKey(
                model.RestrictionsApplyToCountry?.ID ?? model.RestrictionsApplyToCountryID,
                model.RestrictionsApplyToCountryKey,
                model.RestrictionsApplyToCountryName,
                model.RestrictionsApplyToCountry?.CustomKey,
                model.RestrictionsApplyToCountry?.Name))
            {
                newEntity.RestrictionsApplyToCountryID = await Workflows.Countries.ResolveWithAutoGenerateToIDAsync(
                        model.RestrictionsApplyToCountryID,
                        model.RestrictionsApplyToCountryKey,
                        model.RestrictionsApplyToCountryName,
                        model.RestrictionsApplyToCountry,
                        context)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckValidIDOrAnyValidKey(
                model.RestrictionsApplyToRegion?.ID ?? model.RestrictionsApplyToRegionID,
                model.RestrictionsApplyToRegionKey,
                model.RestrictionsApplyToRegionName,
                model.RestrictionsApplyToRegion?.CustomKey,
                model.RestrictionsApplyToRegion?.Name))
            {
                newEntity.RestrictionsApplyToRegionID = await Workflows.Regions.ResolveWithAutoGenerateToIDAsync(
                        model.RestrictionsApplyToRegionID,
                        model.RestrictionsApplyToRegionKey,
                        model.RestrictionsApplyToRegionName,
                        model.RestrictionsApplyToRegion,
                        context)
                    .ConfigureAwait(false);
            }
        }
    }
}
