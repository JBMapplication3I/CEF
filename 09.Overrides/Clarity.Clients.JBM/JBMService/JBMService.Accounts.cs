// <copyright file="JBMService.Accounts.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Mapper;
    using Ecommerce.Models;
    using Ecommerce.Service;
    using Ecommerce.Utilities;

    public partial class JBMService : ClarityEcommerceServiceBase
    {
        public async Task<UserModel> Post(GetUserByUsername request)
        {
            return (await Workflows.Users.GetByUserNameAsync(
                    request.Username!,
                    ServiceContextProfileName)
                .ConfigureAwait(false)) as UserModel ?? new UserModel();
        }

        public async Task<CEFActionResponse?> Post(PriceListToAccountAndProductRoles request)
        {
            if (request.PriceListName!.ToLower() == "medsurg retail price list" || request.PriceListName.ToLower() == "ems retail price list")
            {
                return CEFAR.PassingCEFAR();
            }
            var acctID = await JBMWorkflow.CheckAccountExistsAndReturnIDAsync(request.AccountKey!, ServiceContextProfileName).ConfigureAwait(false);
            var productsDigest = await Workflows.Products.GetDigestAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (Contract.CheckInvalidID(acctID))
            {
                return CEFAR.FailingCEFAR();
            }
            await JBMWorkflow.EnsureAccountUserRoleAsync((int)acctID, request.PriceListName!, ServiceContextProfileName).ConfigureAwait(false);
            var priceListItems = new List<string>();
            var offset = 0;
            var queryParams = CreateQueryParams(onlyData: true, fields: new string[] { "Item" }, limit: 500);
            var hasMore = true;
            while (hasMore)
            {
                var res = await GetResponseAsync<PriceListItemsResponse>(
                        resource: $"{JBMConfig.JBMSalesAPI}/priceLists/{request.PriceListID}/child/items",
                        queryParams: queryParams)
                    .ConfigureAwait(false);
                if (Contract.CheckNotNull(res) && Contract.CheckNotEmpty(res?.items))
                {
                    priceListItems.AddRange(res!.items.Select(x => x.Item!));
                }
                if (res is null)
                {
                    return CEFAR.PassingCEFAR();
                }
                if (!res!.HasMore!.Value)
                {
                    hasMore = false;
                    break;
                }
                if (!Contract.CheckNotNull(res) || Contract.CheckEmpty(res?.items))
                {
                    hasMore = false;
                    break;
                }
                offset += res!.Count!.Value;
                if (queryParams.TryGetValue("offset", out _))
                {
                    queryParams["offset"] = offset.ToString();
                }
                else
                {
                    queryParams.Add("offset", offset.ToString());
                }
            }
            return await JBMWorkflow.AssociateProductsToAccountUserRoleAsync(
                    request.PriceListName!,
                    priceListItems,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        // Accounts
        public async Task<object?> Post(UpdateAccountFromFusion request)
        {
            var account = await Workflows.Accounts.GetAsync((int)request.AccountID!, ServiceContextProfileName).ConfigureAwait(false);
            account!.CustomKey = request.CustomKey;
            await Workflows.Accounts.UpdateAsync(account, ServiceContextProfileName).ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        // Accounts
        public async Task<object?> Post(SyncNewAccountsAndContactsToFusion request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var compareDate = DateTime.Parse(request.CompareDate);
            var accounts = context.Accounts
                .FilterByActive(true)
                .FilterByUpdatedOrCreatedDate(compareDate, null)
                .SelectFullAccountAndMapToAccountModel(ServiceContextProfileName);
            return await Task.FromResult(accounts.Where(x => long.TryParse(x.CustomKey, out _)).ToList());
        }

        // Accounts
        public async Task<object?> Post(AccountSites request)
        {
            return await JBMWorkflow.UpsertAccountsContactsAndUsersFromFusionAsync(
                    addresses: request.CustomerAccountInformation!.CustomerAddresses!,
                    sites: request.CustomerAccountInformation!.Sites!,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(CurrentAccountPriceLists request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var attrs = await context.Accounts
                .FilterByActive(true)
                .FilterByCustomKey(request.AccountKey)
                .Select(x => x.JsonAttributes)
                .SingleOrDefaultAsync();
            if (Contract.CheckValidKey(attrs))
            {
                var attrsList = attrs.DeserializeAttributesDictionary();
                if (attrsList.TryGetValue("priceLists", out var priceLists) && !string.IsNullOrWhiteSpace(priceLists?.Value))
                {
                    return CEFAR.PassingCEFAR(priceLists!.Value, new string[] { });
                }
            }
            return CEFAR.PassingCEFAR(string.Empty, new string[] { });
        }
    }
}