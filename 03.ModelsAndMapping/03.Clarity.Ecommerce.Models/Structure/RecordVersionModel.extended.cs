// <copyright file="RecordVersionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the record version model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the record version.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IRecordVersionModel"/>
    public partial class RecordVersionModel
    {
        #region RecordVersion Properties
        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(RecordID), DataType = "int?", ParameterType = "body", IsRequired = false,
             Description = "The identifier of the record that this is a version of, optional/nullable.")]
        public int? RecordID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(OriginalPublishDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false,
             Description = "The original publish date, optional/nullable. Normally set by the system when publishing for the first time.")]
        public DateTime? OriginalPublishDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(MostRecentPublishDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false,
             Description = "The most recent publish date, optional/nullable. Normally set by the system when publishing each time.")]
        public DateTime? MostRecentPublishDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false),
         ApiMember(Name = nameof(IsDraft), DataType = "bool", ParameterType = "body", IsRequired = true,
             Description = "Whether this version is a draft (pre-first-publish), required to set a value.")]
        public bool IsDraft { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(SerializedRecord), DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "The serialized content of this record, required to set a value.")]
        public string? SerializedRecord { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(PublishedByUserID), DataType = "int?", ParameterType = "body", IsRequired = false,
             Description = "The identifier of the user that last published this version of the record. Set by the server.")]
        public int? PublishedByUserID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
         ApiMember(Name = nameof(PublishedByUserKey), DataType = "string", ParameterType = "body", IsRequired = false,
             Description = "The custom key of the user that last published this version of the record. Set by the server.")]
        public string? PublishedByUserKey { get; set; }

        /// <inheritdoc cref="IRecordVersionModel.PublishedByUser" />
        [DefaultValue(null),
         ApiMember(Name = nameof(PublishedByUser), DataType = "int?", ParameterType = "body", IsRequired = false,
             Description = "The user that last published this version of the record. Set by the server0")]
        public UserModel? PublishedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IRecordVersionModel.PublishedByUser { get => PublishedByUser; set => PublishedByUser = (UserModel?)value; }
        #endregion
    }
}
