// <autogenerated>
// <copyright file="FavoriteManufacturerModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Favorite Manufacturer.</summary>
    public partial class FavoriteManufacturerModel
        : AmARelationshipTableBaseModel
            , IFavoriteManufacturerModel
    {
        #region IAmFilterableByManufacturer Properties
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByManufacturerModel.ManufacturerID { get => SlaveID; set => SlaveID = value; } // M2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByManufacturerModel.ManufacturerKey { get => SlaveKey; set => SlaveKey = value; } // M2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByManufacturerModel.ManufacturerName { get => SlaveName; set => SlaveName = value; } // M2

        /// <inheritdoc cref="IAmFilterableByManufacturerModel.Manufacturer"/>
        [JsonIgnore]
        IManufacturerModel? IAmFilterableByManufacturerModel.Manufacturer { get => Slave; set => Slave = (ManufacturerModel?)value; } // M2
        #endregion
        #region IAmARelationshipTable<IUserModel,IManufacturerModel>

        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Slave record.")]
        public string? SlaveName { get; set; }

        /// <inheritdoc/>
        public UserModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IManufacturerModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "ManufacturerModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public ManufacturerModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IManufacturerModel? IAmARelationshipTableBaseModel<IManufacturerModel>.Slave { get => Slave; set => Slave = (ManufacturerModel?)value; }
        #endregion
        #region IAmAFavoriteRelationshipTable
        /// <inheritdoc/>
        [JsonIgnore]
        public int FavoriteID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string? FavoriteKey { get => SlaveKey; set => SlaveKey = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string? FavoriteName { get => SlaveName; set => SlaveName = value; }

        /// <inheritdoc cref="IFavoriteManufacturerModel.Favorite"/>
        [JsonIgnore]
        public ManufacturerModel? Favorite { get => Slave; set => Slave = (ManufacturerModel?)value; }

        /// <inheritdoc/>
        [JsonIgnore]
        IManufacturerModel? IFavoriteManufacturerModel.Favorite { get => Slave; set => Slave = (ManufacturerModel?)value; }
        #endregion
    }
}
