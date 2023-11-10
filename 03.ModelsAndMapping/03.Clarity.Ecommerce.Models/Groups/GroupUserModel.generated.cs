// <autogenerated>
// <copyright file="GroupUserModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data transfer model for Group User.</summary>
    public partial class GroupUserModel
        : AmARelationshipTableBaseModel
            , IGroupUserModel
    {
        #region IAmFilterableByUser Properties
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(SlaveUserName), DataType = "string", ParameterType = "body", IsRequired = false,
                Description = "The Username of the User.")]
        public string? SlaveUserName { get; set; } // U2

        /// <inheritdoc/>
        [JsonIgnore]
        int IAmFilterableByUserModel.UserID { get => SlaveID; set => SlaveID = value; } // U2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByUserModel.UserKey { get => SlaveKey; set => SlaveKey = value; } // U2

        /// <inheritdoc/>
        [JsonIgnore]
        string? IAmFilterableByUserModel.UserUsername { get => SlaveUserName; set => SlaveUserName = value; } // U2

        /// <inheritdoc cref="IAmFilterableByUserModel.User"/>
        [JsonIgnore]
        IUserModel? IAmFilterableByUserModel.User { get => Slave; set => Slave = (UserModel?)value; } // U2
        #endregion
        #region IAmARelationshipTable<IGroupModel,IUserModel>
        /// <inheritdoc/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(MasterName), DataType = "string?", ParameterType = "body", IsRequired = false,
                Description = "The Name of the Master record.")]
        public string? MasterName { get; set; }

        /// <inheritdoc/>
        public GroupModel? Master { get; set; }

        /// <inheritdoc cref="IAmARelationshipTableBaseModel{IUserModel}.Slave"/>
        [DefaultValue(null),
            DataMember(EmitDefaultValue = false),
            ApiMember(Name = nameof(Slave), DataType = "UserModel?", ParameterType = "body", IsRequired = false,
                Description = "The Slave record (may only be partially mapped out).")]
        public UserModel? Slave { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        IUserModel? IAmARelationshipTableBaseModel<IUserModel>.Slave { get => Slave; set => Slave = (UserModel?)value; }
        #endregion
    }
}
