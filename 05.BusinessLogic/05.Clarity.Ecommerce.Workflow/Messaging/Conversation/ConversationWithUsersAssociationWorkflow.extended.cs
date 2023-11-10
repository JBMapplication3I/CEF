// <copyright file="ConversationWithUsersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate conversation users workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ConversationWithUsersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IConversationUser newEntity,
            IConversationUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(newEntity);
            Contract.RequiresNotNull(model);
            int? userID = null;
            // Try to look up the user
            if (model.SlaveID > 0 && model.SlaveID != int.MaxValue)
            {
                userID = await context.Users
                    .AsNoTracking()
                    .FilterByID(model.SlaveID)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            if (userID == null && !string.IsNullOrWhiteSpace(model.SlaveKey))
            {
                userID = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(model.SlaveKey, true)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            Contract.RequiresValidID(userID, "ERROR! Cannot assign null to a non-nullable UserID on ConversationUser entity");
            newEntity.SlaveID = userID!.Value;
        }
    }
}
