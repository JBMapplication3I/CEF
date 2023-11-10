// <copyright file="AuthenticationService.Registration.Membership.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin,
        Route("/Authentication/ValidateInvitation", "POST",
            Summary = "Membership Registration Process step 2: Validate Invitation (User clicked the link in the email with an invite token)")]
    public class ValidateInvitation : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email to send the invitation to")]
        public string Email { get; set; } = null!;

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The encrypted token to use for validation")]
        public string Token { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ValidateEmail", "POST",
            Summary = "Pass in user credentials to log into the site.")]
    public class ValidateEmail : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email to send the invitation to")]
        public string Email { get; set; } = null!;

        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The encrypted token to use for validation")]
        public string Token { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin,
        Route("/Authentication/CreateLiteAccountAndSendValidationEmail", "POST",
            Summary = "Membership Registration Process step 3: Send Email confirmation Email (User gets an email with validation token)")]
    public class CreateLiteAccountAndSendValidationEmail : ICreateLiteAccountAndSendValidationEmail, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(FirstName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "First Name of the User")]
        public string FirstName { get; set; } = null!;

        [ApiMember(Name = nameof(LastName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Last Name of the User")]
        public string LastName { get; set; } = null!;

        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The UserName")]
        public string? UserName { get; set; }

        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email to send the invitation to")]
        public string Email { get; set; } = null!;

        [ApiMember(Name = nameof(SellerType), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Seller Type")]
        public string SellerType { get; set; } = null!;

        [ApiMember(Name = nameof(Membership), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Membership Level")]
        public string Membership { get; set; } = null!;

        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The encrypted token to use for validation")]
        public string Token { get; set; } = null!;

        [ApiMember(Name = nameof(MembershipType), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Membership Type")]
        public string MembershipType { get; set; } = null!;

        [ApiMember(Name = nameof(RootURL), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The RootURL is used to override the settings generated RootURL")]
        public string RootURL { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin,
        Route("/Authentication/CompleteRegistration", "PATCH",
            Summary = "Membership Registration Process step 4: Validate Email and complete Registration")]
    public class CompleteRegistration : ICompleteRegistration, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(FirstName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "First Name of the User")]
        public string FirstName { get; set; } = null!;

        [ApiMember(Name = nameof(LastName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Last Name of the User")]
        public string LastName { get; set; } = null!;

        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email to send the invitation to")]
        public string Email { get; set; } = null!;

        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "UserName")]
        public string UserName { get; set; } = null!;

        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Password")]
        public string Password { get; set; } = null!;

        [ApiMember(Name = nameof(ResetToken), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Reset Token")]
        public string ResetToken { get; set; } = null!;

        [ApiMember(Name = nameof(Phone), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Phone")]
        public string Phone { get; set; } = null!;

        [ApiMember(Name = nameof(CompanyName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "CompanyName")]
        public string CompanyName { get; set; } = null!;

        [ApiMember(Name = nameof(RoleName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "RoleName")]
        public string? RoleName { get; set; }

        [ApiMember(Name = nameof(Address), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Address")]
        public AddressModel Address { get; set; } = null!;

        IAddressModel? ICompleteRegistration.Address { get => Address; set => Address = (AddressModel)value!; }

        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "TypeName")]
        public string? TypeName { get; set; }

        [ApiMember(Name = nameof(Website), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Website")]
        public string? Website { get; set; }

        [ApiMember(Name = nameof(ProfileType), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "ProfileType")]
        public string? ProfileType { get; set; }

        [ApiMember(Name = nameof(StoreContacts), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "StoreContacts")]
        public List<StoreContactModel>? StoreContacts { get; set; }

        List<IStoreContactModel>? ICompleteRegistration.StoreContacts
        {
            get => StoreContacts?.ToList<IStoreContactModel>();
            set => StoreContacts = value?.Cast<StoreContactModel>().ToList();
        }
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ApproveUser", "PATCH",
            Summary = "Membership Registration Process step 5: Administrator approves the user")]
    public class ApproveUserRegistration : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The encrypted token to use for validation")]
        public string Token { get; set; } = null!;

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "CEF User ID")]
        public int ID { get; set; }
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(ValidateInvitation request)
        {
            return await Workflows.Authentication.ValidateInvitationAsync(
                    request.Email,
                    request.Token)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ValidateEmail request)
        {
            return await Workflows.Authentication.ValidateEmailAsync(
                    request.Email,
                    request.Token,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CreateLiteAccountAndSendValidationEmail request)
        {
            return await Workflows.Authentication.CreateLiteAccountAndSendValidationEmailAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(CompleteRegistration request)
        {
            return await Workflows.Authentication.CompleteRegistrationAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(ApproveUserRegistration request)
        {
            return await Workflows.Authentication.ApproveUserRegistrationAsync(
                    request.Token,
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
