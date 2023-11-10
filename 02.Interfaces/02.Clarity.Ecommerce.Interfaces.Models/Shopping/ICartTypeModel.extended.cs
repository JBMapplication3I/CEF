// <copyright file="ICartTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface ICartTypeModel : ITypeModel
    {
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int? CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user key.</summary>
        /// <value>The created by user key.</value>
        string? CreatedByUserKey { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        IUserModel? CreatedByUser { get; set; }
    }
}
