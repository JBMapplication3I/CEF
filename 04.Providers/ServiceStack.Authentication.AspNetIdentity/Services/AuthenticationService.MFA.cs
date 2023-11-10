// <copyright file="AuthenticationService.MFA.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A check for mfa for username.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_MFARequirementsModel}"/>
    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/CheckForMFA/{Username*}", "GET",
            Summary = "Check if multi-factor authentication is enabled for a specific user.")]
    public class CheckForMFAForUsername : IReturn<CEFActionResponse<MFARequirementsModel>>
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [ApiMember(Name = nameof(Username), DataType = "string", ParameterType = "path", IsRequired = true,
            Description = "The Username to check.")]
        public string Username { get; set; } = null!;
    }

    /// <summary>A request mfa for username.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/RequestMFA/{Username*}", "GET",
            Summary = "Request a multi-factor authentication token for a specific user.")]
    public class RequestMFAForUsername : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets a value indicating whether this RequestMFAForUsername should use the phone SMS option.</summary>
        /// <value>True if use phone, false if not.</value>
        [ApiMember(Name = nameof(UsePhone), DataType = "bool", ParameterType = "query", IsRequired = false,
            Description = "If true, send over SMS instead of email.")]
        public bool UsePhone { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        [ApiMember(Name = nameof(Username), DataType = "string", ParameterType = "path", IsRequired = true,
            Description = "The Username to check.")]
        public string Username { get; set; } = null!;
    }

    public partial class CEFSharedService
    {
        /// <summary>ServiceStack GET handler for the <see cref="CheckForMFAForUsername" /> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object?}.</returns>
        public async Task<object?> Get(CheckForMFAForUsername request)
        {
            return await Workflows.Authentication.CheckForMFAForUsernameAsync(
                    username: request.Username,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>ServiceStack GET handler for the <see cref="RequestMFAForUsername" /> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object?}.</returns>
        public async Task<object?> Get(RequestMFAForUsername request)
        {
            return await Workflows.Authentication.RequestMFAForUsernameAsync(
                    username: request.Username,
                    usePhone: request.UsePhone,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
