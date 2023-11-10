// <copyright file="CurrentAccountService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the current account service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Route("/Accounts/CurrentAccount", "GET",
            Summary = "Get account for the current user logged in")]
    public partial class GetCurrentAccount : IReturn<AccountModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Accounts/CurrentAccount", "PUT",
            Summary = "Use to update the account associated to the user currently logged into the system")]
    public partial class UpdateCurrentAccount : AccountModel, IReturn<AccountModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Accounts/SelectAssociatedAccount", "POST",
            Summary = "Use to update the current user's account to the selected associated account.")]
    public partial class SelectAssociatedAccount : IReturnVoid
    {
        public int? AccountID { get; set; }
    }

    [PublicAPI]
    public class CurrentAccountService : ClarityEcommerceServiceBase
    {
        public async Task Post(SelectAssociatedAccount request)
        {
            var session = GetAuthedSSSessionOrThrow401();
            if (Contract.CheckInvalidID(request.AccountID))
            {
                session.SelectedAccountID = null;
            }
            else
            {
                var canSwitchAccount = await Workflows.Accounts.CheckCanEmulateAccountForCurrentUserAsync(
                        CurrentUserIDOrThrow401,
                        request.AccountID!.Value,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
                if (!canSwitchAccount)
                {
                    throw HttpError.Unauthorized("This user is not authorized to emulate this account.");
                }
                session.SelectedAccountID = request.AccountID;
            }
            Request.SaveSession(session);
        }

        public async Task<object?> Get(GetCurrentAccount request)
        {
            if (!GetSession().IsAuthenticated)
            {
                return null;
            }
            var origAccountID = CurrentAccountIDOrThrow401;
            var accountID = await LocalAdminAccountIDOrThrow401Async(origAccountID).ConfigureAwait(false);
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Accounts.GetLastModifiedForResultAsync(accountID, contextProfileName: null),
                    () => origAccountID == accountID
                        ? CurrentAccountAsync()
                        : Workflows.Accounts.GetAsync(accountID, contextProfileName: null),
                    varyByUser: true,
                    varyByRoles: new[] { "CEF Local Administrator", "CEF Affiliate Administrator" })
                .ConfigureAwait(false);
        }

        public async Task<object?> Put(UpdateCurrentAccount request)
        {
            var session = GetAuthedSSSession();
            var origAccountID = CurrentAccountIDOrThrow401;
            var accountID = await LocalAdminAccountIDOrThrow401Async(origAccountID).ConfigureAwait(false);
            request.ID = origAccountID == accountID ? origAccountID : accountID;
            var updated = await Workflows.Accounts.UpdateAsync(request, contextProfileName: null).ConfigureAwait(false);
            await session!.ClearSessionAccountAsync().ConfigureAwait(false);
            Cache.RemoveByPattern($"*urn*{nameof(GetCurrentAccount)}*{session.Id}*");
            return updated;
        }
    }

    [PublicAPI,
        Authenticate, RequiresAnyRole("CEF Local Administrator", "Supervisor"),
        Route("/Accounts/UsersForCurrentAccount", "GET",
            Summary = "Get Users for the current account (as a local administrator)")]
    public partial class GetUsersForCurrentAccount : UserSearchModel, IReturn<UserPagedResults>
    {
    }

    [PublicAPI,
        Authenticate, RequiredRole("CEF Local Administrator"),
        Route("/Accounts/AllUserIDsForCurrentAccount", "GET",
            Summary = "Get all User IDs for the current account (as a local administrator)")]
    public partial class GetAllUserIDsForCurrentAccount : UserSearchModel, IReturn<List<int>>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Accounts/AccountsForCurrentAccount", "GET",
            Summary = "Get Accounts for the current account (as a local administrator)")]
    public partial class GetAccountsForCurrentAccount : AccountSearchModel, IReturn<AccountPagedResults>
    {
    }

    [PublicAPI]
    public class LocalAdministratorService : ClarityEcommerceServiceBase
    {
        public async Task<object?> Get(GetUsersForCurrentAccount request)
        {
            request.AccessibleFromAccountID = await LocalAdminAccountIDOrThrow401Async(
                    request.AccountID ?? CurrentAccountIDOrThrow401)
                .ConfigureAwait(false);
            var (results, totalPages, totalCount) = await Workflows.Accounts.GetUsersForCurrentAccountAsync(
                                request,
                                contextProfileName: null)
                            .ConfigureAwait(false);
            return new UserPagedResults
            {
                Results = results.Cast<UserModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }

        public async Task<object?> Get(GetAllUserIDsForCurrentAccount request)
        {
            request.AccessibleFromAccountID = await LocalAdminAccountIDOrThrow401Async(
                    request.AccountID ?? CurrentAccountIDOrThrow401)
                .ConfigureAwait(false);
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Users.GetLastModifiedForResultSetAsync(request, contextProfileName: null),
                    () => Workflows.Users.SearchAsIDListAsync(request, contextProfileName: null),
                    varyByUser: true,
                    varyByRoles: new[] { "CEF Local Administrator", "CEF Affiliate Administrator" })
                .ConfigureAwait(false);
        }

        //public async Task<object?> Get(GetAccountsForCurrentAccount request)
        //{
        //    request.AccessibleFromAccountID = CurrentAccountIDOrThrow401;
        //    return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IAccountModel, AccountModel, IAccountSearchModel, AccountPagedResults>(
        //            request,
        //            request.AsListing,
        //            Workflows.Accounts,
        //            varyByUser: true,
        //            varyByRoles: new[] { "CEF Local Administrator", "CEF Affiliate Administrator" })
        //        .ConfigureAwait(false);
        //}

        public async Task<object?> Get(GetAccountsForCurrentAccount request)
        {
            request.AccessibleFromAccountID = CurrentAccountIDOrThrow401;
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var rawUserAttrs = await context.Users
                .AsNoTracking()
                .FilterByID(CurrentUserIDOrThrow401)
                .Select(x => x.JsonAttributes)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var associatedAcctsIDs = rawUserAttrs
                .DeserializeAttributesDictionary()
                .TryGetValue("associatedAccounts", out var associatedAccounts)
                    ? associatedAccounts.Value.Split(',').Select(x => int.Parse(x)).ToArray()
                    : null;
            if (Contract.CheckEmpty(associatedAcctsIDs))
            {
                return null;
            }
            var accounts = (await context.Accounts
                    .AsNoTracking()
                    .FilterByIDs(associatedAcctsIDs)
                    .Select(x => new { x.ID, x.CustomKey, x.Name })
                    .ToListAsync()
                .ConfigureAwait(false))
                .Select(y => new AccountModel
                {
                    ID = y.ID,
                    CustomKey = y.CustomKey,
                    Name = y.Name,
                })
                .ToList();
            return new AccountPagedResults
            {
                CurrentCount = accounts.Count,
                CurrentPage = 1,
                Results = accounts,
                TotalCount = accounts.Count,
                TotalPages = 1,
            };
        }
    }
}
