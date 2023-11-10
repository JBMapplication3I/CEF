// <copyright file="IAmALitelyTrackedEventBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmALitelyTrackedEventBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for am a litely tracked event base.</summary>
    public interface IAmALitelyTrackedEventBase
        : INameableBase
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

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        Address? Address { get; set; }

        /// <summary>Gets or sets the identifier of the IP organization.</summary>
        /// <value>The identifier of the IP organization.</value>
        int? IPOrganizationID { get; set; }

        /// <summary>Gets or sets the IP organization.</summary>
        /// <value>The IP organization.</value>
        IPOrganization? IPOrganization { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }
        #endregion
    }
}
