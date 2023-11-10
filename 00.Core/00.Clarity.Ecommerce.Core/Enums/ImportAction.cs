// <copyright file="ImportAction.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import action class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent import actions.</summary>
    public enum ImportAction
    {
        /// <summary>An enum constant representing the do nothing option.</summary>
        DoNothing,

        /// <summary>An enum constant representing the create or update option.</summary>
        CreateOrUpdate,

        /// <summary>An enum constant representing the deactivate option.</summary>
        Deactivate,

        /// <summary>An enum constant representing the delete option.</summary>
        Delete,
    }
}
