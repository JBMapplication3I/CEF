// <copyright file="SettingModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the setting.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ISettingModel"/>
    public partial class SettingModel
    {
        /// <inheritdoc/>
        public string? Value { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? SettingGroupID { get; set; }

        /// <inheritdoc/>
        public string? SettingGroupKey { get; set; }

        /// <inheritdoc/>
        public string? SettingGroupName { get; set; }

        /// <inheritdoc cref="ISettingModel.SettingGroup"/>
        public SettingGroupModel? SettingGroup { get; set; }

        /// <inheritdoc/>
        ISettingGroupModel? ISettingModel.SettingGroup { get => SettingGroup; set => SettingGroup = (SettingGroupModel?)value; }
        #endregion
    }
}
