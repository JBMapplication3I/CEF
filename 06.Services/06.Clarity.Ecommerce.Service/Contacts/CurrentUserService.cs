// <copyright file="CurrentUserService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Authenticate,
        Route("/Contacts/CurrentUser/Update", "PUT",
            Summary = "Use to update the current user's details")]
    public partial class UpdateCurrentUser : UserModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Contacts/CurrentUser", "GET",
            Summary = "Use to get the current user's details")]
    public partial class GetCurrentUser : IReturn<UserModel>
    {
    }

    [PublicAPI,
        Route("/Contacts/CurrentUser/UserName", "GET",
            Summary = "Use to get the current user's Username. Note: this only returns the UserName in the UserModel, no additional data")]
    public partial class GetCurrentUserName : IReturn<CEFActionResponse<Dictionary<string, object>>>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Contacts/CurrentUser/GetOnlineStatus", "GET",
            Summary = "Use to get the current user's online status.")]
    public partial class GetCurrentUserOnlineStatus : IReturn<StatusModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Contacts/CurrentUser/SetOnlineStatus", "POST",
            Summary = "Use to set the current user's online status.")]
    public partial class SetCurrentUserOnlineStatus : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(OnlineStatus), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string OnlineStatus { get; set; } = null!;
    }

    [PublicAPI,
        UsedInStorefront,
        Authenticate,
        Route("/Contacts/SupervisorsForCurrentUser", "POST",
            Summary = "Use to get the current user's supervisor(s)")]
    public partial class GetSupervisorsForCurrentUser : IReturn<List<UserModel>>
    {
    }

    [PublicAPI]
    public class CurrentUserService : ClarityEcommerceServiceBase
    {
        public async Task<object?> Post(GetSupervisorsForCurrentUser request)
        {
            return await Workflows.Users
                .GetSupervisorsOnAccountForUserAsync(CurrentUserIDOrThrow401, CurrentAccountIDOrThrow401, contextProfileName: ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Put(UpdateCurrentUser request)
        {
            request.ID = CurrentUserIDOrThrow401;
            var result = (await Workflows.Users.UpdateAsync(
                        request,
                        contextProfileName: null)
                    .ConfigureAwait(false) != null)
                .BoolToCEFAR();
            await GetAuthedSSSessionOrThrow401().ClearSessionUserAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<object?> Get(GetCurrentUser _)
        {
            return await GetAuthedSSSessionOrThrow401().UserAsync().ConfigureAwait(false);
        }

        public Task<object?> Get(GetCurrentUserName _)
        {
            try
            {
                var session = GetAuthedSSSessionOrThrow401();
                var result = new Dictionary<string, object>
                {
                    ["UserId"] = session.UserAuthId,
                    ["UserName"] = session.UserName,
                    ["DisplayName"] = session.DisplayName,
                    ["SessionId"] = session.Id,
                    ["UserAuthName"] = session.UserAuthName,
                };
                return Task.FromResult<object?>(
                    new CEFActionResponse<Dictionary<string, object>>
                    {
                        ActionSucceeded = Contract.CheckValidKey(session.UserName),
                        Result = result,
                    });
            }
            catch
            {
                return Task.FromResult<object?>(CEFAR.FailingCEFAR("No user currently logged in"));
            }
        }

        public async Task<object?> Get(GetCurrentUserOnlineStatus _)
        {
            return await Workflows.Users.GetOnlineStatusAsync(
                    CurrentUserIDOrThrow401,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(SetCurrentUserOnlineStatus request)
        {
            return await Workflows.Users.SetOnlineStatusAsync(
                    CurrentUserIDOrThrow401,
                    request.OnlineStatus,
                    null)
                .ConfigureAwait(false);
        }
    }
}
