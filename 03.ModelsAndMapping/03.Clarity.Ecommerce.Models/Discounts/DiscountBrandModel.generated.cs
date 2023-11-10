// <autogenerated>
// <copyright file="DiscountBrandModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Discount Brand.</summary>
    public partial class DiscountBrandModel
        : AmARelationshipTableBaseModel
            , IDiscountBrandModel
    {
        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByBrandModel.BrandID { get => SlaveID; set => SlaveID = value; } // B2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByBrandModel.BrandKey { get => SlaveKey; set => SlaveKey = value; } // B2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByBrandModel.BrandName { get => SlaveName; set => SlaveName = value; } // B2

        /// <inheritdoc cref="IAmFilterableByBrandModel.Brand"/>
        [JsonIgnore]
        IBrandModel? IAmFilterableByBrandModel.Brand { get => Slave; set => Slave = (BrandModel?)value; } // B2
        #endregion
        #region IAmARelationshipTable<IDiscountModel,IBrandModel>
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
        public DiscountModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IBrandModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "BrandModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public BrandModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IBrandModel? IAmARelationshipTableBaseModel<IBrandModel>.Slave { get => Slave; set => Slave = (BrandModel?)value; }
        #endregion
        #region IAmADiscountFilterRelationshipTable<IBrandModel>
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmADiscountFilterRelationshipTableModel<IBrandModel>.DiscountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmADiscountFilterRelationshipTableModel<IBrandModel>.DiscountKey { get => MasterKey; set => MasterKey = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmADiscountFilterRelationshipTableModel<IBrandModel>.DiscountName { get => MasterName; set => MasterName = value; }
        #endregion
    }
}
