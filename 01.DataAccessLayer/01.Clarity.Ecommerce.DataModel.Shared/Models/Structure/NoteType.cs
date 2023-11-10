// <copyright file="NoteType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the note type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface INoteType : ITypableBase
    {
        /// <summary>Gets or sets a value indicating whether this INoteType is public.</summary>
        /// <value>True if this INoteType is public, false if not.</value>
        bool IsPublic { get; set; }

        /// <summary>Gets or sets a value indicating whether this INoteType is customer.</summary>
        /// <value>True if this INoteType is customer, false if not.</value>
        bool IsCustomer { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using Interfaces.DataModel;

    [SqlSchema("System", "NoteType")]
    public class NoteType : TypableBase, INoteType
    {
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsPublic { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsCustomer { get; set; }
    }
}
