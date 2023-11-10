// <autogenerated>
// <copyright file="MembershipAdZoneAccessModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Membership Ad Zone Access.</summary>
    public partial class MembershipAdZoneAccessModel
        : AmARelationshipTableBaseModel
            , IMembershipAdZoneAccessModel
    {
        #region IAmARelationshipTable<IMembershipModel,IAdZoneAccessModel>
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Master record.")]
        public string? MasterName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterDisplayName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The DisplayName of the Master record.")]
        public string? MasterDisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterTranslationKey), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The TranslationKey of the Master record.")]
        public string? MasterTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "The SortOrder of the Master record.")]
        public int? MasterSortOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Slave record.")]
        public string? SlaveName { get; set; }

        /// <inheritdoc/>
        public MembershipModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IAdZoneAccessModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "AdZoneAccessModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public AdZoneAccessModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IAdZoneAccessModel? IAmARelationshipTableBaseModel<IAdZoneAccessModel>.Slave { get => Slave; set => Slave = (AdZoneAccessModel?)value; }
        #endregion
    }
}
