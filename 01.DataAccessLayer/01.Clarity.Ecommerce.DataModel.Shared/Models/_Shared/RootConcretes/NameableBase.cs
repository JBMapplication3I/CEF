// <copyright file="NameableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the nameable base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    /// <summary>A nameable base.</summary>
    /// <seealso cref="Base"/>
    /// <seealso cref="INameableBase"/>
    public abstract class NameableBase : Base, INameableBase
    {
        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), Index, DefaultValue(null)]
        public virtual string? Name { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), DontMapOutWithListing, DefaultValue(null)]
        public virtual string? Description { get; set; }

        #region ICloneable
        /// <summary>Converts this Base to a hash-able string.</summary>
        /// <returns>This Base as a string.</returns>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // NameableBase
            builder.AppendLine("NameableBase");
            builder.Append("N: ").AppendLine(Name ?? string.Empty);
            builder.Append("D: ").AppendLine(Description ?? string.Empty);
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
