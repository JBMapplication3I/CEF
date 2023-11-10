// <copyright file="IHaveNotesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveNotesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for have notes base.</summary>
    public interface IHaveNotesBase : IBase
    {
        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        ICollection<Note>? Notes { get; set; }
    }
}
