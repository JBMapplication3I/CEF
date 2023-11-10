// <copyright file="AccountWithAccountPricePointsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate account price points workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class AccountWithAccountPricePointsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IAccountPricePoint newEntity,
            IAccountPricePointModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            if (model.SlaveID != 0
                || newEntity.SlaveID != 0
                || string.IsNullOrEmpty(model.SlaveKey))
            {
                return;
            }
            // Create price point and assign it
            var pricePoint = context.PricePoints
                .AsNoTracking()
                .FirstOrDefault(pp => pp.CustomKey == model.SlaveKey);
            if (pricePoint == null)
            {
                pricePoint = new()
                {
                    Active = true,
                    CustomKey = model.SlaveKey,
                    CreatedDate = DateExtensions.GenDateTime,
                    Name = !string.IsNullOrEmpty(model.SlaveName) ? model.SlaveName : model.SlaveKey,
                };
                pricePoint = context.PricePoints.Add(pricePoint);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            newEntity.SlaveID = pricePoint.ID;
        }
    }
}
