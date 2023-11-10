// <copyright file="MessageWithMessageRecipientsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate message recipients workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class MessageWithMessageRecipientsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IMessageRecipient newEntity,
            IMessageRecipientModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(model);
            newEntity.SlaveID = await Workflows.Users.ResolveToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.Slave,
                    context.ContextProfileName)
                .ConfigureAwait(false);
            newEntity.GroupID = await Workflows.Groups.ResolveWithAutoGenerateToIDAsync(
                    model.GroupID,
                    model.GroupKey,
                    model.GroupName,
                    model.Group,
                    context.ContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
