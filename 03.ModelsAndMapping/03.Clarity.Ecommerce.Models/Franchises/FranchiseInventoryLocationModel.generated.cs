// <autogenerated>
// <copyright file="FranchiseInventoryLocationModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Franchise Inventory Location.</summary>
    public partial class FranchiseInventoryLocationModel
        : AmARelationshipTableBaseModel
            , IFranchiseInventoryLocationModel
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
        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByFranchiseModel.FranchiseID { get => MasterID; set => MasterID = value; } // B1

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByFranchiseModel.FranchiseKey { get => MasterKey; set => MasterKey = value; } // B1

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByFranchiseModel.FranchiseName { get => MasterName; set => MasterName = value; } // B1

        /// <inheritdoc cref="IAmFilterableByFranchiseModel.Franchise"/>
        [JsonIgnore]
        IFranchiseModel? IAmFilterableByFranchiseModel.Franchise { get => Master; set => Master = (FranchiseModel?)value; } // B1
        #endregion
        #region IAmARelationshipTable<IFranchiseModel,IInventoryLocationModel>
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
        public FranchiseModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IInventoryLocationModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "InventoryLocationModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public InventoryLocationModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IInventoryLocationModel? IAmARelationshipTableBaseModel<IInventoryLocationModel>.Slave { get => Slave; set => Slave = (InventoryLocationModel?)value; }
        #endregion
    }
}
