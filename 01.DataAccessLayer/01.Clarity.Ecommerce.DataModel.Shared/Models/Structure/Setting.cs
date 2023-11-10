// <copyright file="Setting.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISetting
        : IHaveATypeBase<SettingType>,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the setting group.</summary>
        /// <value>The identifier of the setting group.</value>
        int? SettingGroupID { get; set; }

        /// <summary>Gets or sets the group the setting belongs to.</summary>
        /// <value>The setting group.</value>
        SettingGroup? SettingGroup { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("System", "Setting")]
    public class Setting : Base, ISetting
    {
        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual SettingType? Type { get; set; }
        #endregion

        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        /// <inheritdoc/>
        [Required, DefaultValue(0)]
        public string? Value { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SettingGroup))]
        public int? SettingGroupID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual SettingGroup? SettingGroup { get; set; }
        #endregion
    }
}
