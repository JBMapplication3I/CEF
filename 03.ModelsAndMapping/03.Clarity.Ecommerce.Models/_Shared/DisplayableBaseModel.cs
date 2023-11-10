// <copyright file="DisplayableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the displayable base.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IDisplayableBaseModel"/>
    public abstract class DisplayableBaseModel : NameableBaseModel, IDisplayableBaseModel
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(DisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Display Name of the object")]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Sort Order of the object")]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TranslationKey), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Display Name of the object")]
        public string? TranslationKey { get; set; }
    }
}
