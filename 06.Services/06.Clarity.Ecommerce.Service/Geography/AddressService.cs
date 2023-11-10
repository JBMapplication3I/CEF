// <copyright file="AddressService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Mapper;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Route("/Geography/AddressBook/{AccountID}", "GET",
            Summary = "Use get the address book for a specific account")]
    public partial class GetAddressBook : IReturn<List<AccountContactModel>>
    {
        [ApiMember(Name = nameof(AccountID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int AccountID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBookPaged/CurrentAccount", "POST",
            Summary = "Gets a paged address book.")]
    public partial class GetAddressBookPaged : AccountContactSearchModel, IReturn<AccountContactPagedResults>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBookAsAdmin/{AccountID}", "GET",
            Summary = "Use get the address book for a specific account")]
    public partial class GetAddressBookAsAdmin : IReturn<List<AccountContactModel>>
    {
        [ApiMember(Name = nameof(AccountID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int AccountID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/Create", "POST",
            Summary = "Use to add an address in the address book")]
    public partial class CreateAddressInBook : AccountContactModel, IReturn<AccountContactModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/Update", "PUT",
            Summary = "Use to update address")]
    public partial class UpdateAddressInBook : AccountContactModel, IReturn<AccountContactModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/Deactivate/ID/{ID}", "PATCH",
            Summary = "Use to deactivate an address in the address book")]
    public partial class DeactivateAddressInBook : ImplementsIDBase, IReturn<bool>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/Deactivate/Key/{Key*}", "PATCH",
            Summary = "Use to deactivate an address in the address book")]
    public partial class DeactivateAddressInBookByKey : ImplementsKeyBase, IReturn<bool>
    {
    }

    [PublicAPI,
        Route("/Geography/TimeZonesList", "GET",
            Summary = "Use to get a list of the world's time zones")]
    public partial class GetTimeZonesList : IReturn<List<TimeZoneInfo>>
    {
    }

    [PublicAPI]
    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetAddressBook request)
        {
            return (await Workflows.AddressBooks.GetAddressBookAsync(
                        await LocalAdminAccountIDOrThrow401Async(request.AccountID).ConfigureAwait(false),
                        ServiceContextProfileName)
                    .ConfigureAwait(false))
                .Cast<AccountContactModel>()
                .Where(x => x.Slave!.TypeID == 4
                    || (x.Slave.TypeID == 1
                        && x.Slave.SerializableAttributes.TryGetValue("userId", out var userId)
                        && userId.Value == CurrentUserIDOrThrow401.ToString()
                        && (x.EndDate > DateExtensions.GenDateTime || x.EndDate == null)))
                .ToList();
        }

        public async Task<object?> Post(GetAddressBookPaged request)
        {
            request.MasterID = await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var res = request.IsBilling == false || request.IsBilling == null
                ? context.AccountContacts
                    .AsNoTracking()
                    .FilterAccountContactsByEndDate()
                    .FilterAccountContactsByAccountID(request.MasterID)
                    .FilterAccountContactsBySearchModel(request)
                    .FilterByActive(true)
                    .ApplySorting(request.Sorts, request.Groupings, ServiceContextProfileName)
                    .SelectFullAccountContactAndMapToAccountContactModel(ServiceContextProfileName)
                    .Cast<AccountContactModel>()
                    .Where(x => x.Slave!.TypeID == 4
                        || (x.Slave.TypeID == 1
                            && x.Slave.SerializableAttributes.TryGetValue("userId", out var userId)
                            && userId.Value == CurrentUserIDOrThrow401.ToString()))
                : context.AccountContacts
                    .AsNoTracking()
                    .FilterAccountContactsByEndDate()
                    .FilterAccountContactsByAccountID(request.MasterID)
                    .FilterAccountContactsBySearchModel(request)
                    .FilterByActive(true)
                    .FilterAccountContactsByAccountID(request.MasterID)
                    .Where(x => x.JsonAttributes != null && x.JsonAttributes.Contains("\"Key\":\"IsBilling\""))
                    .SelectFullAccountContactAndMapToAccountContactModel(ServiceContextProfileName)
                    .Cast<AccountContactModel>()
                    .ToList();
            return new AccountContactPagedResults
            {
                CurrentCount = request.Paging?.Size ?? res.Count(),
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalCount = res.Count(),
                TotalPages = (int)Math.Ceiling(res.Count() / (double)(request.Paging?.Size ?? 1)),
                Groupings = request.Groupings,
                Sorts = request.Sorts,
                Results = res.ToList(),
            };
        }

        public async Task<object?> Get(GetAddressBookAsAdmin request)
        {
            return (await Workflows.AddressBooks.GetAddressBookAsync(
                        Contract.RequiresValidID(request.AccountID),
                        ServiceContextProfileName)
                    .ConfigureAwait(false))
                .Cast<AccountContactModel>()
                .ToList();
        }

        public async Task<object?> Post(CreateAddressInBook request)
        {
            request.MasterID = await LocalAdminAccountIDOrThrow401Async(request.MasterID).ConfigureAwait(false);
            return (AccountContactModel?)await Workflows.AddressBooks.CreateAddressInBookAsync(
                    request,
                    CurrentUserIDOrThrow401,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(UpdateAddressInBook request)
        {
            return (AccountContactModel?)await Workflows.AddressBooks.UpdateAddressInBookAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(DeactivateAddressInBook request)
        {
            return await Workflows.AddressBooks.DeactivateAddressInBookAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Post(DeactivateAddressInBookByKey request)
        {
            return await Workflows.AddressBooks.DeactivateAddressInBookAsync(request.Key, ServiceContextProfileName).ConfigureAwait(false);
        }

        public object Get(GetTimeZonesList _)
        {
            return TimeZoneInfo.GetSystemTimeZones().ToList();
        }
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/CurrentUser", "GET",
            Summary = "Use to get the address book for the current Account")]
    public partial class GetCurrentUserAddressBook : IReturn<List<AccountContactModel>>
    {
        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccountID { get; set; }
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetCurrentUserAddressBook request)
        {
            // TODO: Cached Research
            return (await Workflows.AddressBooks.GetAddressBookAsync(
                        await LocalAdminAccountIDOrThrow401Async(request.AccountID).ConfigureAwait(false),
                        ServiceContextProfileName)
                    .ConfigureAwait(false))
                .Cast<AccountContactModel>()
                .ToList();
        }
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/CurrentAccount", "GET",
            Summary = "Use to get the address book for the current Account")]
    public partial class GetCurrentAccountAddressBook : IReturn<List<AccountContactModel>>
    {
        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccountID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/CurrentAccount/PrimaryShipping", "GET",
            Summary = "Use to get the address book for the current Account")]
    public partial class GetCurrentAccountPrimaryShippingAddress : IReturn<AccountContactModel>
    {
        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccountID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Geography/AddressBook/CurrentAccount/PrimaryBilling", "GET",
            Summary = "Use to get the address book for the current Account")]
    public partial class GetCurrentAccountPrimaryBillingAddress : IReturn<AccountContactModel>
    {
        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccountID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        UsedInStorefront,
        Route("/Geography/AddressBook/CurrentAccount/Suggest", "GET",
            Summary = "Search the current account contacts. Returns a range of data")]
    public class SuggestAddressBookCurrentAccount
        : ContactSearchModel, IReturn<List<AccountContactPagedResults>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetCurrentAccountAddressBook request)
        {
            return await Workflows.AddressBooks.GetAddressBookAsync(
                    await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false),
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetCurrentAccountPrimaryShippingAddress request)
        {
            return await Workflows.AddressBooks.GetAddressBookPrimaryShippingAsync(
                    await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false),
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetCurrentAccountPrimaryBillingAddress request)
        {
            return await Workflows.AddressBooks.GetAddressBookPrimaryBillingAsync(
                    await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false),
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(SuggestAddressBookCurrentAccount request)
        {
            var currentAccountID = await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            Contract.RequiresValidKey(request.IDOrCustomKeyOrNameOrDescription);
            Contract.Requires<ArgumentException>(request.IDOrCustomKeyOrNameOrDescription!.Length > 3);
            var contactIDs = await context.Contacts
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.Address!.Street1!.Contains(request.IDOrCustomKeyOrNameOrDescription)
                        || x.Address.PostalCode!.Contains(request.IDOrCustomKeyOrNameOrDescription)
                        || x.Address.City!.Contains(request.IDOrCustomKeyOrNameOrDescription))
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            var query = context.AccountContacts
                .AsNoTracking()
                .FilterAccountContactsByAccountID(currentAccountID)
                .FilterAccountContactsByEndDate(DateExtensions.GenDateTime)
                .Where(ac => contactIDs.Contains(ac.SlaveID))
                .ApplySorting(request.Sorts, request.Groupings, ServiceContextProfileName)
                .FilterByPaging(request.Paging, out var totalPages, out var totalCount);
            var testing = context.AccountContacts
                .AsNoTracking()
                .FilterAccountContactsByAccountID(currentAccountID)
                .FilterAccountContactsByEndDate(DateExtensions.GenDateTime)
                .Where(ac => contactIDs.Contains(ac.SlaveID))
                .SelectListAccountContactAndMapToAccountContactModel(ServiceContextProfileName)
                    .Where(x => x.Slave!.TypeID == 4
                        || (x.Slave.TypeID == 1
                            && x.Slave.SerializableAttributes.TryGetValue("userId", out var userId)
                            && userId.Value == CurrentUserIDOrThrow401.ToString()));
            return new AccountContactPagedResults
            {
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Groupings = request.Groupings,
                Sorts = request.Sorts,
                Results = query
                    .SelectListAccountContactAndMapToAccountContactModel(ServiceContextProfileName)
                    .Where(x => x.Slave!.TypeID == 4
                        || (x.Slave.TypeID == 1
                            && x.Slave.SerializableAttributes.TryGetValue("userId", out var userId)
                            && userId.Value == CurrentUserIDOrThrow401.ToString()))
                    .Cast<AccountContactModel>()
                    .ToList(),
            };
        }
    }
}
