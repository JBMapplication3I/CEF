// <autogenerated>
// <copyright file="DiscountAccountTypeModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Discount Account Type.</summary>
    public partial class DiscountAccountTypeModel
        : AmARelationshipTableBaseModel
            , IDiscountAccountTypeModel
    {
        #region IAmARelationshipTable<IDiscountModel,ITypeModel>
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Master record.")]
        public string? MasterName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Slave record.")]
        public string? SlaveName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveDisplayName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The DisplayName of the Slave record.")]
        public string? SlaveDisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveTranslationKey), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The TranslationKey of the Slave record.")]
        public string? SlaveTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "The SortOrder of the Slave record.")]
        public int? SlaveSortOrder { get; set; }

        /// <inheritdoc/>
        public DiscountModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{ITypeModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "TypeModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public TypeModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        ITypeModel? IAmARelationshipTableBaseModel<ITypeModel>.Slave { get => Slave; set => Slave = (TypeModel?)value; }
        #endregion
        #region IAmADiscountFilterRelationshipTable<ITypeModel>
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmADiscountFilterRelationshipTableModel<ITypeModel>.DiscountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmADiscountFilterRelationshipTableModel<ITypeModel>.DiscountKey { get => MasterKey; set => MasterKey = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmADiscountFilterRelationshipTableModel<ITypeModel>.DiscountName { get => MasterName; set => MasterName = value; }
        #endregion
    }
}
