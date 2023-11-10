// <copyright file="AccountSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    public partial class AccountSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccessibleFromAccountID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "A UserID that is associated with the Account")]
        public int? AccessibleFromAccountID { get; set; }
    }
}
