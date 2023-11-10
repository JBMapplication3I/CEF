// <copyright file="IAccountSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for account search model.</summary>
    public partial interface IAccountSearchModel
    {
        /// <summary>Gets or sets the identifier of the accessible from account.</summary>
        /// <value>The identifier of the accessible from account.</value>
        int? AccessibleFromAccountID { get; set; }
    }
}
