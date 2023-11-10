// <copyright file="ICompleteRegistration.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICompleteRegistration interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for complete registration.</summary>
    public interface ICompleteRegistration
    {
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string UserName { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        string Password { get; set; }

        /// <summary>Gets or sets the reset token.</summary>
        /// <value>The reset token.</value>
        string ResetToken { get; set; }

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string LastName { get; set; }

        /// <summary>Gets or sets the name of the company.</summary>
        /// <value>The name of the company.</value>
        string CompanyName { get; set; }

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        string Phone { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>Gets or sets the name of the role.</summary>
        /// <value>The name of the role.</value>
        string? RoleName { get; set; }

        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        string? TypeName { get; set; }

        /// <summary>Gets or sets the website.</summary>
        /// <value>The website.</value>
        string? Website { get; set; }

        /// <summary>Gets or sets the type of the profile.</summary>
        /// <value>The type of the profile.</value>
        string? ProfileType { get; set; }

        /// <summary>Gets or sets the store contacts.</summary>
        /// <value>The store contacts.</value>
        List<IStoreContactModel>? StoreContacts { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        IAddressModel? Address { get; set; }
    }
}
