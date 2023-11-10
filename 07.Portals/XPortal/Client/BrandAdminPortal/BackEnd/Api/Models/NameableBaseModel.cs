// <copyright file="NameableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.ComponentModel;
    using ServiceStack;

    /// <summary>A data Model for the nameable base.</summary>
    /// <seealso cref="BaseModel"/>
    public partial class NameableBaseModel : BaseModel, INameableBaseModel
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [DefaultValue(null),
            ApiMember(Name = "Name", DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name of the object, required, max length 128 characters")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [DefaultValue(null),
            ApiMember(Name = "Description", DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Description of the object, optional, no max length and ideal for storing long HTML content")]
        public string? Description { get; set; }
    }
}
