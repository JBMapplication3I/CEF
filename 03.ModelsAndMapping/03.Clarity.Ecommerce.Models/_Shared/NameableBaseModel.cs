// <copyright file="NameableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the nameable base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the nameable base.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="INameableBaseModel"/>
    public abstract class NameableBaseModel : BaseModel, INameableBaseModel
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name of the object, required, max length 128 characters")]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Description), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Description of the object, optional, no max length and ideal for storing long HTML content")]
        public string? Description { get; set; }

        #region ICloneable
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Base
            builder.AppendLine("NameableBase");
            builder.Append("N: ").AppendLine(Name ?? string.Empty);
            builder.Append("D: ").AppendLine(Description ?? string.Empty);
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
