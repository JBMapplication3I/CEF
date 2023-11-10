// <copyright file="DisplayableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.ComponentModel;
    using ServiceStack;

    /// <content>A data Model for the displayable base.</content>
    /// <seealso cref="NameableBaseModel"/>
    public abstract partial class DisplayableBaseModel : NameableBaseModel
    {
        [DefaultValue(null),
            ApiMember(Name = nameof(DisplayName), DataType = "string?", ParameterType = "body", IsRequired = false)]
        public string? DisplayName { get; set; }

        [DefaultValue(null),
            ApiMember(Name = nameof(TranslationKey), DataType = "string?", ParameterType = "body", IsRequired = false)]
        public string? TranslationKey { get; set; }

        [DefaultValue(null),
            ApiMember(Name = nameof(SortOrder), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? SortOrder { get; set; }
    }
}
