// <copyright file="PriceRuleWithPriceRuleUserRolesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate price rule store workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class PriceRuleWithPriceRuleUserRolesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IPriceRuleUserRoleModel model)
        {
            // Must have something to recognize the UserRole by
            return Contract.CheckValidKey(model.RoleName);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IPriceRuleUserRoleModel model,
            IPriceRuleUserRole entity,
            IClarityEcommerceEntities context)
        {
            return entity.RoleName == model.RoleName
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            IPriceRuleUserRole newEntity,
            IPriceRuleUserRoleModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass the Role Name to match against");
            newEntity.RoleName = model.RoleName;
            return Task.CompletedTask;
        }
    }
}
