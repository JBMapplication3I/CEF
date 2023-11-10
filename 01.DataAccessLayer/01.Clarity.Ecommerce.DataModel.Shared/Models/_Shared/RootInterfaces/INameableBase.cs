// <copyright file="INameableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the INameableBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for nameable base.</summary>
    /// <seealso cref="IBase"/>
    public interface INameableBase : IBase
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        string? Description { get; set; }
    }
}
