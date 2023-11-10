// <copyright file="UserService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Route("/Contacts/User/CreateWithCode", "POST", Priority = 1,
            Summary = "Use to create user with invite code")]
    public partial class CreateUserWithCode : UserModel, IReturn<UserModel>
    {
        [ApiMember(Name = nameof(InvitationCode), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "InvitationCode")]
        public string InvitationCode { get; set; } = null!;
    }

    public partial class CreateUser
    {
        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Password")]
        public string? Password { get; set; }

        /// <summary>Gets or sets the create CMS user.</summary>
        /// <value>The create CMS user.</value>
        [ApiMember(Name = nameof(CreateCMSUser), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "Should the system create a user on the CMS side")]
        public bool? CreateCMSUser { get; set; }

        /// <summary>Gets or sets the create cef user.</summary>
        /// <value>The create cef user.</value>
        [ApiMember(Name = nameof(CreateCEFUser), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "Should the system create a CEF User")]
        public bool? CreateCEFUser { get; set; }
    }

    public partial class UpdateUser
    {
        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Password")]
        public string? Password { get; set; }

        /// <summary>Gets or sets the create CMS user.</summary>
        /// <value>The create CMS user.</value>
        [ApiMember(Name = nameof(CreateCMSUser), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "Should the system create a user on the CMS side")]
        public bool? CreateCMSUser { get; set; }
    }

    [Route("/Contacts/User/UserNameByID/{ID}", "GET",
        Summary = "Use to get the UserName that matches a User ID")]
    public class GetUserNameByUserID : ImplementsIDBase, IReturn<CEFActionResponse<string>>
    {
    }

    [Route("/Contacts/User/Email/{Email}", "GET",
        Summary = "Use to get the User by Email")]
    public class GetUserByEmail : ImplementsEmailBase, IReturn<UserModel>
    {
    }

    [Route("/Contacts/User/CustomKey/{Key*}", "GET",
        Summary = "Use to get the User by CustomKey Only")]
    public class GetUserByCustomKey : ImplementsKeyBase, IReturn<UserModel>
    {
    }

    public partial class UserService
    {
        public override async Task<object?> Get(GetUserByKey request)
        {
            return await Workflows.Users.GetAsync(request.Key, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetUserByCustomKey request)
        {
            return await Workflows.Users.GetByKeyAsync(request.Key, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetUserByEmail request)
        {
            return await Workflows.Users.GetByEmailAsync(request.Email!, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetUserNameByUserID request)
        {
            var user = await Workflows.Users.GetAsync(
                    Contract.RequiresValidID(request.ID),
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            return user == null
                ? CEFAR.FailingCEFAR<string?>("Could not locate a user with this ID")
                : (user.UserName ?? user.Email).WrapInPassingCEFAR();
        }

        public async Task<object?> Post(CreateUserWithCode request)
        {
            request.ID = (await Workflows.Users.GetByEmailAndInvitationCodeAsync(
                        Contract.RequiresValidKey(request.Contact?.Email1 ?? request.Email),
                        Contract.RequiresValidKey(request.InvitationCode),
                        ServiceContextProfileName)
                    .ConfigureAwait(false))
                .ID;
            return await Workflows.Users.UpdateAsync(request, ServiceContextProfileName).ConfigureAwait(false);
        }

        public override async Task<object?> Post(CreateUser request)
        {
            return request.CreateCEFUser ?? true
                ? await Workflows.Users.CreateAsync(request, ServiceContextProfileName).ConfigureAwait(false)
                : null;
        }

        public override async Task<object?> Put(UpdateUser request)
        {
            return await Workflows.Users.UpdateAsync(request, ServiceContextProfileName).ConfigureAwait(false);
        }
    }

    [PublicAPI,
     Authenticate,
     Route("/Contacts/Users/AccountAssignments", "POST",
        Summary = "For Connect, get a list of all current user/account pairs")]
    public class GetUsersAccountAssignments : IReturn<List<UserAccountAssignment>>
    {
    }

    [PublicAPI,
     Authenticate,
     Route("/Contacts/Users/AccountAssignments", "PATCH",
        Summary = "For Connect, update the user/account pairs for everything in the provided list")]
    public class UpdateUsersAccountAssignments : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets to update.</summary>
        /// <value>to update.</value>
        [ApiMember(Name = nameof(ToUpdate), DataType = "List<UserAccountAssignment>", ParameterType = "body", IsRequired = true)]
        public List<UserAccountAssignment> ToUpdate { get; set; } = null!;
    }

    [Route("/Contacts/VerifyRecaptcha", "POST", Summary = "Use to verify reCAPTCHA")]
    public class VerifyReCaptcha
    {
        /// <summary>Gets or sets the response.</summary>
        /// <value>The response.</value>
        [ApiMember(Name = nameof(Response), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Response { get; set; } = null!;
    }

    [Authenticate,
        Route("/Contacts/User/GetUserByUserName", "GET",
            Summary = "Use to get user by username")]
    public class GetUserByUserName : IReturn<UserModel>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string UserName { get; set; } = null!;
    }

    [PublicAPI]
    public partial class UserService
    {
        public async Task<object?> Post(GetUsersAccountAssignments _)
        {
            return await Workflows.Users.GetUsersAccountAssignmentsAsync(ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Patch(UpdateUsersAccountAssignments request)
        {
            return await Workflows.Users.UpdateUsersAccountAssignmentsAsync(
                    request.ToUpdate,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(VerifyReCaptcha verifyReCaptcha)
        {
            if (!Contract.CheckAllValidKeys(verifyReCaptcha.Response, CEFConfigDictionary.ReCaptchaSecret))
            {
                return false;
            }
            using var client = new WebClient();
            var response = JsonConvert.DeserializeObject<ReCaptchaResponse>(
                await client.DownloadStringTaskAsync(
                        "https://www.google.com/recaptcha/api/siteverify"
                        + $"?secret={CEFConfigDictionary.ReCaptchaSecret}"
                        + $"&response={verifyReCaptcha.Response}")
                    .ConfigureAwait(false));
            return response!.Success;
        }

        public async Task<object?> Get(GetUserByUserName request)
        {
            return await Workflows.Users.GetByUserNameAsync(
                    request.UserName,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        [PublicAPI]
        private class ReCaptchaResponse
        {
            public bool Success { get; set; }

            public DateTime TimeStamp { get; set; }

            public string? HostName { get; set; }
        }
    }
}
