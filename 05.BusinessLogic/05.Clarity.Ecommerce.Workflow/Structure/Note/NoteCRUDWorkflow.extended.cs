// <copyright file="NoteCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the note workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class NoteWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            INote entity,
            INoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateNoteFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Note: It should be any one of the following, not more than one TODO@JTG: Fix this so it can only actually take one related master
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }
    }
}
