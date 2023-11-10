// <copyright file="CalendarEventWithProductsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate calendar event products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class CalendarEventWithProductsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task DeactivateObjectAdditionalPropertiesAsync(
            ICalendarEventProduct entity,
            DateTime timestamp)
        {
            if (entity.Slave == null)
            {
                return Task.CompletedTask;
            }
            entity.Slave.UpdatedDate = timestamp;
            entity.Slave.Active = false;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            ICalendarEventProduct newEntity,
            ICalendarEventProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = Contract.CheckValidID(model.SlaveID)
                ? model.SlaveID
                : await Workflows.Products.ResolveWithAutoGenerateToIDAsync(
                        model.SlaveID,
                        model.SlaveKey,
                        model.SlaveName,
                        model.Slave,
                        context)
                    .ConfigureAwait(false);
        }
    }
}
