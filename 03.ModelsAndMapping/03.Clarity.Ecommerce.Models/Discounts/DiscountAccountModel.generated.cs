// <autogenerated>
// <copyright file="DiscountAccountModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Discount Account.</summary>
    public partial class DiscountAccountModel
        : AmARelationshipTableBaseModel
            , IDiscountAccountModel
    {
        #region IAmFilterableByAccount Properties
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByAccountModel.AccountID { get => SlaveID; set => SlaveID = value; } // A2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByAccountModel.AccountKey { get => SlaveKey; set => SlaveKey = value; } // A2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByAccountModel.AccountName { get => SlaveName; set => SlaveName = value; } // A2

        /// <inheritdoc cref="IAmFilterableByAccountModel.Account"/>
        [JsonIgnore]
        IAccountModel? IAmFilterableByAccountModel.Account { get => Slave; set => Slave = (AccountModel?)value; } // A2
        #endregion
        #region IAmARelationshipTable<IDiscountModel,IAccountModel>
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

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IAccountModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "AccountModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public AccountModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IAccountModel? IAmARelationshipTableBaseModel<IAccountModel>.Slave { get => Slave; set => Slave = (AccountModel?)value; }
        #endregion
        #region IAmADiscountFilterRelationshipTable<IAccountModel>
        /// <inheritdoc/>
        [JsonIgnore]
        int IAmADiscountFilterRelationshipTableModel<IAccountModel>.DiscountID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmADiscountFilterRelationshipTableModel<IAccountModel>.DiscountKey { get => MasterKey; set => MasterKey = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmADiscountFilterRelationshipTableModel<IAccountModel>.DiscountName { get => MasterName; set => MasterName = value; }
        #endregion
    }
}
