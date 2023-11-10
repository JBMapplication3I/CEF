// <copyright file="ISalesCollectionBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesCollectionBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for sales collection base search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public interface ISalesCollectionBaseSearchModel
        : IHaveATypeBaseSearchModel,
            IHaveAStatusBaseSearchModel,
            IHaveAStateBaseSearchModel,
            IAmFilterableByAccountSearchModel,
            IAmFilterableByBrandSearchModel,
            IAmFilterableByFranchiseSearchModel,
            IAmFilterableByStoreSearchModel,
            IAmFilterableByUserSearchModel
    {
        // TODO@BE: Populate ISalesCollectionBaseSearchModel with more search properties
        #region Dates
        /// <summary>Gets or sets the minimum date.</summary>
        /// <value>The minimum date.</value>
        DateTime? MinDate { get; set; }

        /// <summary>Gets or sets the maximum date.</summary>
        /// <value>The maximum date.</value>
        DateTime? MaxDate { get; set; }
        #endregion

        #region User
        /// <summary>Gets or sets the identifier of the user external.</summary>
        /// <value>The identifier of the user external.</value>
        string? UserExternalID { get; set; }
        #endregion

        #region Billing Contact Info
        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        string? Phone { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        string? Email { get; set; }

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string? FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string? LastName { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        string? PostalCode { get; set; }
        #endregion

        #region Product
        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the product i ds.</summary>
        /// <value>The product i ds.</value>
        List<int>? ProductIDs { get; set; }
        #endregion
    }
}
