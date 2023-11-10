// <copyright file="MenuCategoryModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the menu category model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the menu category.</summary>
    /// <seealso cref="IMenuCategoryModel"/>
    [DataContract]
    public class MenuCategoryModel : IMenuCategoryModel
    {
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(ID), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? ID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(HasChildren), DataType = "bool", ParameterType = "body", IsRequired = false)]
        public bool HasChildren { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(DisplayName), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(CustomKey), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? CustomKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(PrimaryImageFileName), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? PrimaryImageFileName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SortOrder), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? SortOrder { get; set; }

        /// <inheritdoc cref="IMenuCategoryModel.Children"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Children), DataType = "List<MenuCategoryModel>", ParameterType = "body", IsRequired = false)]
        public List<MenuCategoryModel>? Children { get; set; }

        /// <inheritdoc/>
        List<IMenuCategoryModel>? IMenuCategoryModel.Children
        {
            get => Children?.ToList<IMenuCategoryModel>();
            set => Children = value?.Cast<MenuCategoryModel>().ToList();
        }

        /// <summary>Should serialize.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A bool?.</returns>
        public bool? ShouldSerialize(string name) => this.IgnoreEmptyData(name);
    }
}
