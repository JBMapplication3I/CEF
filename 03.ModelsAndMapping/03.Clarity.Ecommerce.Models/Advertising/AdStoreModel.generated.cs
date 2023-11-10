// <autogenerated>
// <copyright file="AdStoreModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Ad Store.</summary>
    public partial class AdStoreModel
        : AmARelationshipTableBaseModel
            , IAdStoreModel
    {
        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveSeoUrl), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The SEO URL of the Store.")]
        public string? SlaveSeoUrl { get; set; } // S2

        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByStoreModel.StoreID { get => SlaveID; set => SlaveID = value; } // S2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByStoreModel.StoreKey { get => SlaveKey; set => SlaveKey = value; } // S2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByStoreModel.StoreName { get => SlaveName; set => SlaveName = value; } // S2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByStoreModel.StoreSeoUrl { get => SlaveSeoUrl; set => SlaveSeoUrl = value; } // S2

        /// <inheritdoc cref="IAmFilterableByStoreModel.Store"/>
        [JsonIgnore]
        IStoreModel? IAmFilterableByStoreModel.Store { get => Slave; set => Slave = (StoreModel?)value; } // S2
        #endregion
        #region IAmARelationshipTable<IAdModel,IStoreModel>
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
        public AdModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IStoreModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "StoreModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public StoreModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IStoreModel? IAmARelationshipTableBaseModel<IStoreModel>.Slave { get => Slave; set => Slave = (StoreModel?)value; }
        #endregion
    }
}
