// <copyright file="IIPOrganizationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the iip organization model class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for iip organization model.</summary>
    public partial interface IIPOrganizationModel
    {
        /// <summary>Gets or sets the IP address.</summary>
        /// <value>The IP address.</value>
        string? IPAddress { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        int? Score { get; set; }

        /// <summary>Gets or sets the visitor key.</summary>
        /// <value>The visitor key.</value>
        string? VisitorKey { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the primary user.</summary>
        /// <value>The identifier of the primary user.</value>
        int? PrimaryUserID { get; set; }

        /// <summary>Gets or sets the primary user key.</summary>
        /// <value>The primary user key.</value>
        string? PrimaryUserKey { get; set; }

        /// <summary>Gets or sets the primary user.</summary>
        /// <value>The primary user.</value>
        IUserModel? PrimaryUser { get; set; }

        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the address key.</summary>
        /// <value>The address key.</value>
        string? AddressKey { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        IAddressModel? Address { get; set; }
        #endregion
    }
}
