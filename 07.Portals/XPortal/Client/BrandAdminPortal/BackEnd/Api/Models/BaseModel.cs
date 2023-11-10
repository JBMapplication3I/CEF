// <copyright file="BaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.ComponentModel;
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>A data Model for the base.</summary>
    [PublicAPI]
    public partial class BaseModel : IBaseModel, IHaveJsonAttributesBaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [DefaultValue(0),
            ApiMember(Name = "ID", DataType = "int", ParameterType = "body", IsRequired = false,
                Description = "Identifier of the object (required when submitting Updates, must be blank or 0 for Creates")]
        public int ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        [DefaultValue(null),
            ApiMember(Name = "CustomKey", DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Custom Key of the object, optional when not using an ERP integration, max 128 characters")]
        public string? CustomKey { get; set; }

        /// <summary>Gets a value indicating whether the active.</summary>
        /// <value>True if active, false if not.</value>
        [DefaultValue(false),
            ApiMember(Name = "Active", DataType = "bool", ParameterType = "body", IsRequired = false,
                Description = "Whether the object is active. Inactive objects are essentially 'Soft-Deleted'. Ensure you set this to true to keep the object alive!")]
        public bool Active { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        [ApiMember(Name = "CreatedDate", DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The original date this object was created. Not required for Creates, will be automatically set")]
        public DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the updated date.</summary>
        /// <value>The updated date.</value>
        [DefaultValue(null),
            ApiMember(Name = "UpdatedDate", DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The last time the object was modified. Not Required for Updates, will be automatically set")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>Gets or sets the hash.</summary>
        /// <value>The hash.</value>
        [DefaultValue(null),
            ApiMember(Name = "Hash", DataType = "long?", ParameterType = "body", IsRequired = false,
                Description = "The Hash of the object data points coming from Clarity Connect")]
        public long? Hash { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            ApiMember(Name = "SerializableAttributes", DataType = "SerializableAttributes", ParameterType = "body", IsRequired = true,
                Description = "A dictionary of extending Attributes for the this object")]
        public SerializableAttributesDictionary SerializableAttributes { get; set; } = new();
    }
}
