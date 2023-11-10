// <copyright file="DiscountCode.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount code class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for discount code.</summary>
    public interface IDiscountCode : IBase
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the discount.</summary>
        /// <value>The identifier of the discount.</value>
        int DiscountID { get; set; }

        /// <summary>Gets or sets the discount.</summary>
        /// <value>The discount.</value>
        Discount? Discount { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }
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

    [SqlSchema("Discounts", "DiscountCode")]
    public class DiscountCode : Base, IDiscountCode
    {
        #region Discount Properties
        /// <inheritdoc/>
        [Required, StringLength(20), DefaultValue(null)]
        public string? Code { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Discount)), DefaultValue(0)]
        public int DiscountID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DontMapOutEver, JsonIgnore, DefaultValue(null)]
        public virtual Discount? Discount { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, JsonIgnore, DefaultValue(null)]
        public virtual User? User { get; set; }
        #endregion
    }
}
