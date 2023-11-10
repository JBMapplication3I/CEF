// <autogenerated>
// <copyright file="FranchiseAuctionModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Franchise Auction.</summary>
    public partial class FranchiseAuctionModel
        : AmARelationshipTableBaseModel
            , IFranchiseAuctionModel
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
        #region IAmARelationshipTable<IFranchiseModel,IAuctionModel>
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

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IAuctionModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "AuctionModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public AuctionModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IAuctionModel? IAmARelationshipTableBaseModel<IAuctionModel>.Slave { get => Slave; set => Slave = (AuctionModel?)value; }
        #endregion
    }
}
