// <copyright file="AuthenticationService.Forgot.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ForgotUsername", "POST",
            Summary = "Sends an email to the user if the provided email matches an existing user with the Username in it.")]
    public class ForgotUsername : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email")]
        public string Email { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ForgotPassword", "POST",
            Summary = "Sends an email to the user if the provided email matches an existing user with a Password Reset Token in it.")]
    public class ForgotPassword : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email")]
        public string Email { get; set; } = null!;
    }

    /// <summary>A forgot password return.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_String}"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ForgotPassword/Return", "POST",
            Summary = "Sends an email to the user if the provided email matches an existing user with a Password Reset Token in it.")]
    public class ForgotPasswordReturn : IReturn<CEFActionResponse<string>>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email")]
        public string Email { get; set; } = null!;

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Token")]
        public string Token { get; set; } = null!;

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Password")]
        public string Password { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ForcedPasswordReset", "POST",
            Summary = "When the user is required to reset their password, this endpoint handles it.")]
    public class ForcedPasswordReset : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email")]
        public string Email { get; set; } = null!;

        /// <summary>Gets or sets the old password.</summary>
        /// <value>The old password.</value>
        [ApiMember(Name = nameof(OldPassword), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Old Password")]
        public string OldPassword { get; set; } = null!;

        /// <summary>Gets or sets the new password.</summary>
        /// <value>The new password.</value>
        [ApiMember(Name = nameof(NewPassword), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "New Password")]
        public string NewPassword { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
        Route("/Authentication/CheckForcedPasswordReset", "POST",
            Summary = "Check if the user is required to reset their password.")]
    public class CheckForcedPasswordReset : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the UserName.</summary>
        /// <value>The UserName.</value>
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "UserName")]
        public string UserName { get; set; } = null!;

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Password")]
        public string Password { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ChangePassword", "POST",
            Summary = "When provided with the valid existing password, changes the password for a user to a new value.")]
    public class ChangePassword : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "UserName")]
        public string UserName { get; set; } = null!;

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Current Password")]
        public string Password { get; set; } = null!;

        /// <summary>Gets or sets the new password.</summary>
        /// <value>The new password.</value>
        [ApiMember(Name = nameof(NewPassword), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "New Password")]
        public string NewPassword { get; set; } = null!;
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(ForgotUsername request)
        {
            return await Workflows.Authentication.ForgotUsernameAsync(
                    request.Email,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ForgotPassword request)
        {
            return await Workflows.Authentication.ForgotPasswordAsync(
                    request.Email,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ForgotPasswordReturn request)
        {
            return await Workflows.Authentication.ForgotPasswordReturnAsync(
                    request.Email,
                    request.Password,
                    request.Token,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ForcedPasswordReset request)
        {
            return await Workflows.Authentication.ForcePasswordResetAsync(
                    request.Email,
                    request.OldPassword,
                    request.NewPassword,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CheckForcedPasswordReset request)
        {
            return await Workflows.Authentication.CheckForcePasswordResetAsync(
                    request.UserName,
                    request.Password,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ChangePassword request)
        {
            var session = GetAuthedSSSessionOrThrow401();
            return await Workflows.Authentication.ChangePasswordAsync(
                    session.UserAuthName,
                    request.Password,
                    request.NewPassword,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
