// <copyright file="ProductWithProductDownloadsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate product downloads workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithProductDownloadsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IProductDownloadModel model)
        {
            // Must have something to recognize the Product by
            return (Contract.CheckAnyValidID(model.ProductID, model.Product?.ID)
                || Contract.CheckAnyValidKey(
                        model.ProductKey,
                        model.ProductName,
                        model.Product?.CustomKey,
                        model.Product?.Name))
                // Must have a valid URL, either absolute or relative
                && (model.IsAbsoluteUrl && Contract.CheckValidKey(model.AbsoluteUrl)
                    || Contract.CheckValidKey(model.RelativeUrl))
                // Must have a type
                && (Contract.CheckAnyValidID(model.TypeID, model.Type?.ID)
                    || Contract.CheckAnyValidKey(
                        model.TypeKey,
                        model.TypeName,
                        model.TypeDisplayName,
                        model.Type?.CustomKey,
                        model.Type?.Name,
                        model.Type?.DisplayName));
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IProductDownloadModel model,
            IProductDownload entity,
            IClarityEcommerceEntities context)
        {
            if (entity.IsAbsoluteUrl != model.IsAbsoluteUrl)
            {
                return false; // else continue
            }
            if (entity.AbsoluteUrl != model.AbsoluteUrl)
            {
                return false; // else continue
            }
            if (entity.RelativeUrl != model.RelativeUrl)
            {
                return false; // else continue
            }
            if (entity.Name != model.Name)
            {
                return false; // else continue
            }
            if (entity.Description != model.Description)
            {
                return false; // else continue
            }
            var typeID = await Workflows.ProductDownloadTypes.ResolveWithAutoGenerateToIDAsync(
                    model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.TypeDisplayName,
                    model.Type,
                    context)
                .ConfigureAwait(false);
            if (entity.TypeID != typeID)
            {
                return false; // else continue
            }
            var productID = await Workflows.Products.ResolveToIDAsync(
                    model.ProductID,
                    model.ProductKey,
                    model.ProductName,
                    // TODO@JTG: Add SEO Url for matching check here
                    model.Product,
                    context)
                .ConfigureAwait(false);
            if (entity.ProductID != productID)
            {
                return false; // else continue
            }
            return entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IProductDownload newEntity,
            IProductDownloadModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass valid, existing product info and a type and a absolute or relative url");
            // Product Download Properties
            newEntity.IsAbsoluteUrl = model.IsAbsoluteUrl;
            if (newEntity.IsAbsoluteUrl)
            {
                newEntity.AbsoluteUrl = model.AbsoluteUrl;
                newEntity.RelativeUrl = null;
            }
            else
            {
                newEntity.AbsoluteUrl = null;
                newEntity.RelativeUrl = model.RelativeUrl;
            }
            // Related Objects
            var typeID = await Workflows.ProductDownloadTypes.ResolveWithAutoGenerateToIDAsync(
                    model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.TypeDisplayName,
                    model.Type,
                    context)
                .ConfigureAwait(false);
            newEntity.TypeID = typeID;
        }
    }
}
