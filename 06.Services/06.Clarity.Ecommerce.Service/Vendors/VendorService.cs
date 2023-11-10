// <copyright file="VendorService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Vendors/Vendor/{UserName}", "Get", Summary = "Use to get a specific account vendor")]
    public class GetVendorByUserName : IReturn<VendorModel>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "path", IsRequired = true)]
        public string UserName { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Vendors/Vendor/AssignAccountToUserName", "Post", Summary = "Use to assign accounts to an account vendor")]
    public class AssignAccountToUserName : IReturn<bool>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;

        [ApiMember(Name = nameof(AccountToken), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string AccountToken { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Vendors/Vendor/ResetToken", "Post", Summary = "Use to reset an account vendor's security token")]
    public class ResetToken : IReturn<bool>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Vendors/Vendor/UpdatePassword", "Put", Summary = "Use to change an account vendor's password")]
    public class UpdatePassword : IReturn<bool>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;

        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Password { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Vendors/Vendor/UserMustResetPassword", "Post", Summary = "Use to check if an account vendor needs to change password")]
    public class UserMustResetPassword : IReturn<string>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Vendors/Vendor/Login", "Post", Summary = "Use to check if an account vendor needs to change password")]
    public class Login : IReturn<string>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;

        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Password { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Vendors/Vendor/Current/Administration", "GET",
            Summary = "Use to get the vendor that the current user has administrative rights to (limited to vendor admins)")]
    public partial class GetCurrentVendorAdministration : IReturn<CEFActionResponse<VendorModel>>
    {
    }

    public partial class VendorService
    {
        public async Task<object?> Get(GetVendorByUserName request)
        {
            return await Workflows.Vendors.GetByUserNameAsync(request.UserName, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(AssignAccountToUserName request)
        {
            return await Workflows.Vendors.AssignAccountToUserNameAsync(request.UserName, request.AccountToken, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(ResetToken request)
        {
            return await Workflows.Vendors.ResetTokenAsync(request.UserName, null).ConfigureAwait(false);
        }

        public async Task<object?> Put(UpdatePassword request)
        {
            return await Workflows.Vendors.UpdatePasswordAsync(request.UserName, request.Password, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(UserMustResetPassword request)
        {
            return await Workflows.Vendors.UserMustResetPasswordAsync(request.UserName, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(Login request)
        {
            return await Workflows.Vendors.LoginAsync(request.UserName, request.Password, null).ConfigureAwait(false);
        }

        // Vendor Administration
        public async Task<object?> Get(GetCurrentVendorAdministration _)
        {
            try
            {
                var result = (VendorModel)(await Workflows.Vendors.GetAsync(
                        await CurrentVendorForVendorAdminIDOrThrow401Async().ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false))!;
                return result.WrapInPassingCEFAR();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        "GetCurrentVendorAdministration Error",
                        ex.Message,
                        ex,
                        null)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<VendorModel>(
                    "Unable to locate current Vendor the user would be administrator of");
            }
        }
    }
}
