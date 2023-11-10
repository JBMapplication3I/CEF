// <copyright file="IStoredFileSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoredFileSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IStoredFileSearchModel
    {
        /// <summary>Gets or sets the account ID to filter stored files by.</summary>
        /// <value>The account ID to filter stored files by.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the category ID to filter stored files by.</summary>
        /// <value>The category ID to filter stored files by.</value>
        int? CategoryID { get; set; }
    }
}
