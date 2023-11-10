// <autogenerated>
// <copyright file="ZoneModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Zone.</summary>
    public partial class ZoneModel
        : NameableBaseModel
            , IZoneModel
    {
        #region IHaveATypeBaseModel<ITypeModel>
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeID), DataType = "int", ParameterType = "body", IsRequired = true,
                Description = "Identifier for the Type of this Account, required if no TypeModel present")]
        public int TypeID { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel{ITypeModel}.Type"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Type), IsRequired = true, DataType = "TypeModel", ParameterType = "body",
                Description = "Model for Type of this Account, required if no TypeID present")]
        public TypeModel? Type { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        ITypeModel? IHaveATypeBaseModel<ITypeModel>.Type { get => Type; set => Type = (TypeModel?)value; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Key for the Type of this Account, read-only")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name for the Type of this Account, read-only")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Display Name for the Type of this object, read-only")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "Translation Key for the Type of this object, read-only")]
        public string? TypeTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(TypeSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Sort Order for the Type of this object, read-only")]
        public int? TypeSortOrder { get; set; }
        #endregion
        #region IHaveAStatusBaseModel<IStatusModel>
        /// <inheritdoc/>
        [DefaultValue(0),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusID), DataType = "int", ParameterType = "body", IsRequired = true,
                Description = "Identifier for the Status of this object, required if no StatusModel present")]
        public int StatusID { get; set; }

        /// <inheritdoc cref="IHaveAStatusBaseModel{IStatusModel}.Status"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Status), DataType = "StatusModel", ParameterType = "body", IsRequired = true,
                Description = "Model for Status of this object, required if no StatusID present")]
        public StatusModel? Status { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IStatusModel? IHaveAStatusBaseModel<IStatusModel>.Status { get => Status; set => Status = (StatusModel?)value; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusKey), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Key for the Status of this object, read-only")]
        public string? StatusKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Name for the Status of this object, read-only")]
        public string? StatusName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Display Name for the Status of this object, read-only")]
        public string? StatusDisplayName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
                Description = "Translation Key for the Status of this object, read-only")]
        public string? StatusTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(StatusSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Sort Order for the Status of this object, read-only")]
        public int? StatusSortOrder { get; set; }
        #endregion
    }
}
