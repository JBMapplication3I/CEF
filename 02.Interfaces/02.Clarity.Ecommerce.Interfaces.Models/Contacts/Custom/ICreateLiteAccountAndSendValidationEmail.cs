// <copyright file="ICreateLiteAccountAndSendValidationEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICreateLiteAccountAndSendValidationEmail interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for create lite account and send validation email.</summary>
    public interface ICreateLiteAccountAndSendValidationEmail
    {
        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string LastName { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets the type of the seller.</summary>
        /// <value>The type of the seller.</value>
        string SellerType { get; set; }

        /// <summary>Gets or sets the membership.</summary>
        /// <value>The membership.</value>
        string Membership { get; set; }

        /// <summary>Gets or sets the membership type.</summary>
        /// <value>The membership type.</value>
        string MembershipType { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        string Token { get; set; }

        /// <summary>Gets or sets the RootURL.</summary>
        /// <value>The RootURL.</value>
        string RootURL { get; set; }
    }
}
