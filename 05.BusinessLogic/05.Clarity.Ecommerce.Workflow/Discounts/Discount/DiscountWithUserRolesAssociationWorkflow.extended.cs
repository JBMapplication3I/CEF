// <copyright file="DiscountWithUserRolesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate discount user roles workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class DiscountWithUserRolesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IDiscountUserRoleModel model)
        {
            return Contract.CheckValidKey(model.RoleName);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IDiscountUserRoleModel model,
            IDiscountUserRole entity,
            IClarityEcommerceEntities context)
        {
            return entity.RoleName == model.RoleName;
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            IDiscountUserRole newEntity,
            IDiscountUserRoleModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model),
                "Must pass the RoleName to match against");
            newEntity.RoleName = model.RoleName;
            return Task.CompletedTask;
        }
    }
}
