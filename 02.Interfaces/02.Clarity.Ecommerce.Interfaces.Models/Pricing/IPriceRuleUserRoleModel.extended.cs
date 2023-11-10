// <copyright file="IPriceRuleUserRoleModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPriceRuleUserRoleModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IPriceRuleUserRoleModel
    {
        /// <summary>Gets or sets the name of the role.</summary>
        /// <value>The name of the role.</value>
        string? RoleName { get; set; }

        /// <summary>Gets or sets the identifier of the price rule.</summary>
        /// <value>The identifier of the price rule.</value>
        int PriceRuleID { get; set; }

        /// <summary>Gets or sets the price rule key.</summary>
        /// <value>The price rule key.</value>
        string? PriceRuleKey { get; set; }

        /// <summary>Gets or sets the name of the price rule.</summary>
        /// <value>The name of the price rule.</value>
        string? PriceRuleName { get; set; }

        /// <summary>Gets or sets the price rule.</summary>
        /// <value>The price rule.</value>
        IPriceRuleModel? PriceRule { get; set; }
    }
}
