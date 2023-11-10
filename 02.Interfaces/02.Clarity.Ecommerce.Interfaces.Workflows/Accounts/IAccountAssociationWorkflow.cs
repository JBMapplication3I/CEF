// <copyright file="IAccountAssociationWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountAssociationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;

    public partial interface IAccountAssociationWorkflow
    {
        /// <summary>Gets one level down account association IDs.</summary>
        /// <param name="masterID">          Identifier for the master.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The one level down account association IDs.</returns>
        List<int> GetOneLevelDownAccountAssociationIDs(int masterID, string? contextProfileName);
    }
}
