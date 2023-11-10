// <copyright file="AccountUsageBalanceModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account usage balance model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the account usage balance.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IAccountUsageBalanceModel"/>
    public partial class AccountUsageBalanceModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Quantity), DataType = "int", ParameterType = "body", IsRequired = false,
            Description = "Quantity used")]
        public int Quantity { get; set; }
    }
}
