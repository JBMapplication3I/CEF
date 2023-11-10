// <copyright file="ImplementsCartLookupForAdminBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements cart lookup for admin base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements cart lookup for admin base.</summary>
    /// <seealso cref="ImplementsCartLookupBase"/>
    public abstract class ImplementsCartLookupForAdminBase : ImplementsCartLookupBase
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier of the user to use, if any")]
        public int UserID { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [ApiMember(Name = nameof(AccountID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier of the brand to use, if any")]
        public int AccountID { get; set; }
    }
}