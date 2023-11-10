// <copyright file="ISettingModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISettingModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for setting model.</summary>
    public partial interface ISettingModel
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the setting group.</summary>
        /// <value>The identifier of the setting group.</value>
        int? SettingGroupID { get; set; }

        /// <summary>Gets or sets the setting group key.</summary>
        /// <value>The setting group key.</value>
        string? SettingGroupKey { get; set; }

        /// <summary>Gets or sets the name of the setting group.</summary>
        /// <value>The name of the setting group.</value>
        string? SettingGroupName { get; set; }

        /// <summary>Gets or sets the group the setting belongs to.</summary>
        /// <value>The setting group.</value>
        ISettingGroupModel? SettingGroup { get; set; }
        #endregion
    }
}
