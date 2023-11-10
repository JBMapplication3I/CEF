// <copyright file="DiscountUserRole.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount account type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IDiscountUserRole : IBase
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the master.</summary>
        /// <value>The master.</value>
        Discount? Master { get; set; }

        /// <summary>Gets or sets the name of the role.</summary>
        /// <value>The name of the role.</value>
        string? RoleName { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Discounts", "DiscountUserRole")]
    public class DiscountUserRole : Base, IDiscountUserRole
    {
        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), DefaultValue(null)]
        public string? RoleName { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, AllowMapInWithRelateWorkflowsButDontAutoGenerate, JsonIgnore, DefaultValue(null)]
        public virtual Discount? Master { get; set; }
        #endregion
    }
}
