// <copyright file="SalesGroupService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales group service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A get current account sales groups.</summary>
    /// <seealso cref="SalesGroupSearchModel"/>
    /// <seealso cref="IReturn{SalesGroupPagedResults}"/>
    [PublicAPI,
        Authenticate, RequiresAnyPermission("Sales.SalesGroup.View", "Storefront.UserDashboard.SalesGroups.View"),
        Route("/Sales/CurrentAccount/SalesGroups", "POST",
            Summary = "Use to get history of sales groups for the current account")]
    public partial class GetCurrentAccountSalesGroups : SalesGroupSearchModel, IReturn<SalesGroupPagedResults>
    {
    }

    [PublicAPI,
        Route("/Sales/SecureSalesGroup/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales group and check for ownership by the current Account.")]
    public partial class GetSecureSalesGroup : ImplementsIDBase, IReturn<SalesGroupModel>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetCurrentAccountSalesGroups request)
        {
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(
                    request.AccountID ?? CurrentAccountIDOrThrow401)
                .ConfigureAwait(false);
            return await GetPagedResultsAsync<ISalesGroupModel, SalesGroupModel, ISalesGroupSearchModel, SalesGroupPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesGroups)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetSecureSalesGroup request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var userAttrs = await context.Users
                .AsNoTracking()
                .FilterByID(CurrentUserIDOrThrow401)
                .Select(x => x.JsonAttributes)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var allowedAccounts = userAttrs
                    .DeserializeAttributesDictionary()
                    ?.SingleOrDefault(x => x.Key == "associatedAccounts")
                    .Value
                    ?.Value
                    .Split(',')
                    .Select(y => int.Parse(y.Trim()))
                    .ToList();
            allowedAccounts ??= new();
            allowedAccounts.Add(CurrentAccountIDOrThrow401);
            return await Workflows.SalesGroups.SecureSalesGroupAsync(
                    request.ID,
                    allowedAccounts,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
