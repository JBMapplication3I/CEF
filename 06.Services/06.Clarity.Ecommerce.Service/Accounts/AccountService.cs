// <copyright file="AccountService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Models;
    using JetBrains.Annotations;
    using ServiceStack;

    [PublicAPI,
     Route("/Accounts/Account/GetIDByAttributeValue", "GET", Summary = "Use to get a specific account")]
    public class GetAccountIDByAttributeValue : IReturn<int?>
    {
        [ApiMember(Name = nameof(AttributeName), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string AttributeName { get; set; } = null!;

        [ApiMember(Name = nameof(AttributeValue), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string AttributeValue { get; set; } = null!;
    }

    [PublicAPI, Route("/Accounts/Account/ExistsNonNull/Key/{Key*}", "GET", Priority = 1)]
    public partial class CheckAccountExistsNonNullByKey : ImplementsKeyBase, IReturn<DigestModel>
    {
    }

    [PublicAPI, UsedInStorefront, Route("/Accounts/Account/RolesForAccountByAccountID/ID/{ID}", "GET", Priority = 1)]
    public partial class GetRolesForNamesAccount : ImplementsIDBase, IReturn<List<string>?>
    {
    }

    public partial class AccountService
    {
        public async Task<object?> Get(GetRolesForNamesAccount request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var roleIDs = await context.AccountUserRoles
                .FilterByActive(true)
                .Where(x => x.MasterID == request.ID)
                .Select(y => y.SlaveID)
                .ToListAsync()
                .ConfigureAwait(false);
            var roleNames = await context.Roles.Where(x => roleIDs.Any(y => y == x.Id)).Select(z => z.Name).ToListAsync();
            return roleNames;
        }

        public async Task<object?> Get(GetAccountIDByAttributeValue request)
        {
            // TODO: Cached Research
            return await Workflows.Accounts.GetAccountIDByAttributeValueAsync(
                    request.AttributeName,
                    request.AttributeValue,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Get(CheckAccountExistsNonNullByKey request)
        {
            var model = new DigestModel();
            var id = await Workflows.Accounts.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
            model.ID = id ?? 0;
            return model;
        }
    }
}
