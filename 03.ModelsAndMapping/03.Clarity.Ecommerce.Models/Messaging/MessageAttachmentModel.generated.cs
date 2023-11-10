// <autogenerated>
// <copyright file="MessageAttachmentModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Model Interfaces generated to provide base setups</summary>
// <remarks>This file was auto-generated by Models.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#pragma warning disable 618 // Ignore Obsolete warnings
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data transfer model for Message Attachment.</summary>
    public partial class MessageAttachmentModel
        : AmARelationshipTableNameableBaseModel
            , IMessageAttachmentModel
    {
        #region IHaveSeoBaseModel
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoKeywords), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO Keywords to use in the Meta tags of the page for this object")]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO URL to use to link to the page for this object")]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoDescription), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO Description to use in the Meta tags of the page for this object")]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoMetaData), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO General Meta Data to use in the Meta tags of the page for this object")]
        public string? SeoMetaData { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SeoPageTitle), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "SEO Page Title to use in the Meta tags of the page for this object")]
        public string? SeoPageTitle { get; set; }
        #endregion
        #region IAmARelationshipTable<IMessageModel,IStoredFileModel>

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Slave record.")]
        public string? SlaveName { get; set; }

        /// <inheritdoc/>
        public MessageModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IStoredFileModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "StoredFileModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public StoredFileModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IStoredFileModel? IAmARelationshipTableBaseModel<IStoredFileModel>.Slave { get => Slave; set => Slave = (StoredFileModel?)value; }
        #endregion
        #region IAmAStoredFileRelationshipTable
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(FileAccessTypeID), DataType = "int", ParameterType = "body", IsRequired = false)]
        public int FileAccessTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SortOrder), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? SortOrder { get; set; }
        #endregion
    }
}
