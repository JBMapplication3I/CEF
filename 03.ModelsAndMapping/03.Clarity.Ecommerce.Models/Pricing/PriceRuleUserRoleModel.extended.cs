// <copyright file="PriceRuleUserRoleModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule user role model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the price rule user role.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IPriceRuleUserRoleModel"/>
    public partial class PriceRuleUserRoleModel
    {
        /// <inheritdoc/>
        public string? RoleName { get; set; }

        /// <inheritdoc/>
        public int PriceRuleID { get; set; }

        /// <inheritdoc/>
        public string? PriceRuleKey { get; set; }

        /// <inheritdoc/>
        public string? PriceRuleName { get; set; }

        /// <inheritdoc cref="IPriceRuleUserRoleModel.PriceRule"/>
        public PriceRuleModel? PriceRule { get; set; }

        /// <inheritdoc/>
        IPriceRuleModel? IPriceRuleUserRoleModel.PriceRule { get => PriceRule; set => PriceRule = (PriceRuleModel?)value; }
    }
}
