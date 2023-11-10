// <copyright file="IAmALitelyTrackedEventBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmALitelyTrackedEventBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a litely tracked event base model.</summary>
    public interface IAmALitelyTrackedEventBaseModel
        : INameableBaseModel
    {
        /// <summary>Gets or sets the IP address.</summary>
        /// <value>The IP address.</value>
        string? IPAddress { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        int? Score { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the address key.</summary>
        /// <value>The address key.</value>
        string? AddressKey { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        IAddressModel? Address { get; set; }

        /// <summary>Gets or sets the identifier of the IP organization.</summary>
        /// <value>The identifier of the IP organization.</value>
        int? IPOrganizationID { get; set; }

        /// <summary>Gets or sets the IP organization key.</summary>
        /// <value>The IP organization key.</value>
        string? IPOrganizationKey { get; set; }

        /// <summary>Gets or sets the name of the IP organization.</summary>
        /// <value>The name of the IP organization.</value>
        string? IPOrganizationName { get; set; }

        /// <summary>Gets or sets the IP organization.</summary>
        /// <value>The IP organization.</value>
        IIPOrganizationModel? IPOrganization { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }
        #endregion
    }
}
