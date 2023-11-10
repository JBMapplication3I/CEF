// <copyright file="AccountAssociationCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account association workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Linq;

    public partial class AccountAssociationWorkflow
    {
        /// <inheritdoc/>
        public List<int> GetOneLevelDownAccountAssociationIDs(int masterID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.AccountAssociations
                .Where(x => x.MasterID == masterID)
                .Select(x => x.SlaveID)
                .ToList();
        }
    }
}
