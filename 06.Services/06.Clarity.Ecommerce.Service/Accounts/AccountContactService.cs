// <copyright file="AccountContactService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account contact service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Utilities;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A mark account contact as neither billing nor shipping.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Route("/Accounts/AccountContact/MarkAsNotBillingOrShipping/ID/{ID}", "PATCH")]
    public partial class MarkAccountContactAsNeitherBillingNorShipping : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A mark account contact as primary shipping.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Route("/Accounts/AccountContact/MarkAsPrimaryShipping/ID/{ID}", "PATCH")]
    public partial class MarkAccountContactAsPrimaryShipping : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A mark account contact as default billing.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Route("/Accounts/AccountContact/MarkAsDefaultBilling/ID/{ID}", "PATCH")]
    public partial class MarkAccountContactAsDefaultBilling : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    public partial class AccountContactService
    {
        public override async Task<object?> Post(CreateAccountContact request)
        {
            if (CurrentAPIKind == Enums.APIKind.Storefront)
            {
                request.MasterID = await LocalAdminAccountIDOrThrow401Async(request.MasterID).ConfigureAwait(false);
            }
            request.Slave!.Type = null;
            request.Slave.TypeKey = null;
            request.Slave.TypeName = null;
            request.Slave.TypeDisplayName = null;
            request.Slave.TypeID = 1;
            request.Slave.SerializableAttributes ??= new();
            request.Slave.SerializableAttributes.TryAdd("userId", new SerializableAttributeObject { Key = "userId", Value = CurrentUserIDOrThrow401.ToString() });
            // request.CustomKey = $"{await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false)}|{request.Slave.AddressKey}";
            var accountContactResult = await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountContactDataAsync,
                    () => Workflows.AccountContacts.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
            //using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            //var accountContact = await context.AccountContacts.FilterByID(accountContactResult.Result).SingleOrDefaultAsync().ConfigureAwait(false);
            //var acctAttrs = accountContact.Slave!.SerializableAttributes;
            //if (acctAttrs is not null && !acctAttrs.Keys.Contains("userId"))
            //{
            //    var updated = accountContact.Slave!.JsonAttributes.DeserializeAttributesDictionary();
            //    updated.TryAdd("userId", new SerializableAttributeObject { Key = "userId", Value = CurrentUserIDOrThrow401.ToString() });
            //    accountContact.Slave.JsonAttributes = updated.SerializeAttributesDictionary();
            //    context.Contacts.Add(accountContact.Slave);
            //    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            //}
            return accountContactResult;
        }



        public async Task<object?> Patch(MarkAccountContactAsNeitherBillingNorShipping request)
        {
            return await Workflows.AccountContacts.MarkAccountContactAsNeitherBillingNorShippingAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(MarkAccountContactAsPrimaryShipping request)
        {
            return await Workflows.AccountContacts.MarkAccountContactAsPrimaryShippingAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(MarkAccountContactAsDefaultBilling request)
        {
            return await Workflows.AccountContacts.MarkAccountContactAsDefaultBillingAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
