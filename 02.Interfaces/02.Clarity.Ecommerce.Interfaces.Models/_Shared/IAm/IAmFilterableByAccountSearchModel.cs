// <copyright file="IAmFilterableByAccountSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByAccountSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by account search model.</summary>
    public interface IAmFilterableByAccountSearchModel
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account identifier include null.</summary>
        /// <value>The account identifier include null.</value>
        bool? AccountIDIncludeNull { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        // /// <summary>Gets or sets the account key strict.</summary>
        // /// <value>The account key strict.</value>
        // bool? AccountKeyStrict { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        string? AccountName { get; set; }

        /// <summary>Gets or sets the account name strict.</summary>
        /// <value>The account name strict.</value>
        bool? AccountNameStrict { get; set; }

        /// <summary>Gets or sets the account name include null.</summary>
        /// <value>The account name include null.</value>
        bool? AccountNameIncludeNull { get; set; }

        /// <summary>Gets or sets information describing the account identifier or custom key or name or.</summary>
        /// <value>Information describing the account identifier or custom key or name or.</value>
        string? AccountIDOrCustomKeyOrNameOrDescription { get; set; }
    }
}
