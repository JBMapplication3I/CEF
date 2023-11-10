// <copyright file="AccountWithAccountCurrenciesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate account currencies workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class AccountWithAccountCurrenciesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IAccountCurrencyModel model,
            IAccountCurrency entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(entity.CustomName?.Trim() == model.CustomName?.Trim());
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IAccountCurrency newEntity,
            IAccountCurrencyModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.CustomName = model.CustomName?.Trim();
            newEntity.SlaveID = await Workflows.Currencies.ResolveToIDAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    byName: model.SlaveName,
                    model: model.Slave,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
