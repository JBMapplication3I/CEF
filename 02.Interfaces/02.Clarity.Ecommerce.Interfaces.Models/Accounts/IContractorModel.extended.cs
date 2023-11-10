// <copyright file="IContractorModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contractor model interface.</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IContractorModel
    {
        #region Related Objects
        /// <summary>Gets or sets the ID of this contractor's account.</summary>
        /// <value>The ID of this contractor's account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the key of the contractor's account.</summary>
        /// <value>The key of the contractor's account.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the contractor's account.</summary>
        /// <value>The name of the contractor's account.</value>
        string? AccountName { get; set; }

        /// <summary>Gets or sets this contractor's account.</summary>
        /// <value>This contractor's account.</value>
        IAccountModel? Account { get; set; }

        /// <summary>Gets or sets the ID of this contractor's user.</summary>
        /// <value>The ID of this contractor's user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the key of the contractor's user.</summary>
        /// <value>The key of the contractor's user.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the name of the contractor's user.</summary>
        /// <value>The name of the contractor's user.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets this contractor's user.</summary>
        /// <value>This contractor's user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the ID of this contractor's store.</summary>
        /// <value>The ID of this contractor's store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the key of the contractor's store.</summary>
        /// <value>The key of the contractor's store.</value>
        string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the contractor's store.</summary>
        /// <value>The name of the contractor's store.</value>
        string? StoreName { get; set; }

        /// <summary>Gets or sets this contactor's store.</summary>
        /// <value>This contractor's store.</value>
        IStoreModel? Store { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the service areas for this contractor.</summary>
        /// <value>The service areas for this contractor.</value>
        List<IServiceAreaModel>? ServiceAreas { get; set; }
        #endregion
    }
}
