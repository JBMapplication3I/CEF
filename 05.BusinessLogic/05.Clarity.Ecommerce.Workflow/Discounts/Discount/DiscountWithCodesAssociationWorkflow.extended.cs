// <copyright file="DiscountWithCodesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate discount codes workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class DiscountWithCodesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IDiscountCodeModel model)
        {
            return Contract.CheckValidKey(model.Code);
        }

        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IDiscountCodeModel model,
            IDiscountCode entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(entity.Code == model.Code);
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            IDiscountCode newEntity,
            IDiscountCodeModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass the Code to match against");
            newEntity.Code = model.Code;
            return Task.CompletedTask;
        }
    }
}
