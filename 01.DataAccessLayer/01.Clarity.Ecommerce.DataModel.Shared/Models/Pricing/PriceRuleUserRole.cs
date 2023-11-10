// <copyright file="PriceRuleUserRole.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule user role class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IPriceRuleUserRole : IBase
    {
        /// <summary>Gets or sets the name of the role.</summary>
        /// <value>The name of the role.</value>
        string? RoleName { get; set; }

        /// <summary>Gets or sets the identifier of the price rule.</summary>
        /// <value>The identifier of the price rule.</value>
        int PriceRuleID { get; set; }

        /// <summary>Gets or sets the price rule.</summary>
        /// <value>The price rule.</value>
        PriceRule? PriceRule { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Pricing", "PriceRuleUserRole")]
    public class PriceRuleUserRole : Base, IPriceRuleUserRole
    {
        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), DefaultValue(null)]
        public string? RoleName { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PriceRule)), DefaultValue(0)]
        public int PriceRuleID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, AllowMapInWithRelateWorkflowsButDontAutoGenerate, JsonIgnore, DefaultValue(null)]
        public virtual PriceRule? PriceRule { get; set; }
        #endregion
    }
}
