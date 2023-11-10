// <autogenerated>
// <copyright file="FranchiseVendorModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Franchise Vendor.</summary>
    public partial class FranchiseVendorModel
        : AmARelationshipTableBaseModel
            , IFranchiseVendorModel
    {
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
        #region IAmFilterableByVendor Properties
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByVendorModel.VendorID { get => SlaveID; set => SlaveID = value; } // V2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByVendorModel.VendorKey { get => SlaveKey; set => SlaveKey = value; } // V2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByVendorModel.VendorName { get => SlaveName; set => SlaveName = value; } // V2

        /// <inheritdoc cref="IAmFilterableByVendorModel.Vendor"/>
        [JsonIgnore]
        IVendorModel? IAmFilterableByVendorModel.Vendor { get => Slave; set => Slave = (VendorModel?)value; } // V2
        #endregion
        #region IAmARelationshipTable<IFranchiseModel,IVendorModel>
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

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IVendorModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "VendorModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public VendorModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IVendorModel? IAmARelationshipTableBaseModel<IVendorModel>.Slave { get => Slave; set => Slave = (VendorModel?)value; }
        #endregion
    }
}
