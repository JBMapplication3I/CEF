// <autogenerated>
// <copyright file="ContactService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contact service class</summary>
// </autogenerated>
// ReSharper disable InvalidXmlDocComment, PartialTypeWithSinglePart, RedundantUsingDirective
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A ServiceStack Route to get a list of contacts.</summary>
    /// <seealso cref="ContactSearchModel"/>
    /// <seealso cref="IReturn{ContactPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Contacts/Contacts", "GET", Priority = 1,
            Summary = "Use to get a list of contacts")]
    public partial class GetContacts : ContactSearchModel, IReturn<ContactPagedResults> { }

    /// <summary>A ServiceStack Route to get contacts for connect.</summary>
    /// <seealso cref="ContactSearchModel"/>
    /// <seealso cref="IReturn{List{ContactModel}}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.View"),
        PublicAPI,
        Route("/Contacts/ContactsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all contacts")]
    public partial class GetContactsForConnect : ContactSearchModel, IReturn<List<ContactModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all contacts.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.View"),
        PublicAPI,
        Route("/Contacts/ContactsDigest", "GET",
            Summary = "Use to get a hash representing each contacts")]
    public partial class GetContactsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get contact.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{ContactModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Contacts/Contact/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific contact")]
    public partial class GetContactByID : ImplementsIDBase, IReturn<ContactModel> { }

    /// <summary>A ServiceStack Route to get contact.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{ContactModel}"/>
    [PublicAPI,
        Route("/Contacts/Contact/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific contact by the custom key")]
    public partial class GetContactByKey : ImplementsKeyBase, IReturn<ContactModel> { }

    /// <summary>A ServiceStack Route to check contact exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.View"),
        PublicAPI,
        Route("/Contacts/Contact/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckContactExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check contact exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.View"),
        PublicAPI,
        Route("/Contacts/Contact/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckContactExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create contact.</summary>
    /// <seealso cref="ContactModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Contacts/Contact/Create", "POST", Priority = 1,
            Summary = "Use to create a new contact.")]
    public partial class CreateContact : ContactModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert contact.</summary>
    /// <seealso cref="ContactModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Contacts/Contact/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing contact (as needed).")]
    public partial class UpsertContact : ContactModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update contact.</summary>
    /// <seealso cref="ContactModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Contacts/Contact/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing contact.")]
    public partial class UpdateContact : ContactModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate contact.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Contacts/Contact/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific contact from the system [Soft-Delete]")]
    public partial class DeactivateContactByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate contact by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Deactivate"),
        PublicAPI,
        Route("/Contacts/Contact/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific contact from the system [Soft-Delete]")]
    public partial class DeactivateContactByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate contact.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Contacts/Contact/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific contact from the system [Restore from Soft-Delete]")]
    public partial class ReactivateContactByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate contact by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Reactivate"),
        PublicAPI,
        Route("/Contacts/Contact/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific contact from the system [Restore from Soft-Delete]")]
    public partial class ReactivateContactByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete contact.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Contacts/Contact/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific contact from the system [Hard-Delete]")]
    public partial class DeleteContactByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete contact by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Contacts.Contact.Delete"),
        PublicAPI,
        Route("/Contacts/Contact/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific contact from the system [Hard-Delete]")]
    public partial class DeleteContactByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear contact cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Contacts/Contact/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all contact calls.")]
    public class ClearContactCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class ContactServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetContacts"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetContacts request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IContactModel, ContactModel, IContactSearchModel, ContactPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Contacts)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetContactsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetContactsForConnect request)
        {
            return await Workflows.Contacts.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetContactsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetContactsDigest request)
        {
            return await Workflows.Contacts.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetContactByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetContactByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Contacts, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetContactByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetContactByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Contacts, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckContactExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckContactExistsByID request)
        {
            return await Workflows.Contacts.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckContactExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckContactExistsByKey request)
        {
            return await Workflows.Contacts.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertContact"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertContact request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateContact"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateContact request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateContact"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateContact request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateContactByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateContactByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateContactByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateContactByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateContactByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateContactByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateContactByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateContactByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteContactByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteContactByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteContactByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteContactByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedContactDataAsync,
                    () => Workflows.Contacts.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearContactCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearContactCache request)
        {
            await ClearCachedContactDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedContactDataAsync()
        {
            var urn = string.Empty;
            if (JSConfigs.CEFConfigDictionary.BrandsEnabled)
            {
                urn += ":" + new System.Uri(Request.AbsoluteUri).Host.Replace(":", "{colon}");
            }
            foreach (var key in CoreUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
            if (AdditionalUrnIDs == null) { return; }
            foreach (var key in AdditionalUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
        }

        private List<string> CoreUrnIDs
        {
            get
            {
                if (coreUrnIDs != null) { return coreUrnIDs; }
                return coreUrnIDs = new()
                {
                    UrnId.Create<GetContacts>(string.Empty),
                    UrnId.Create<GetContactByID>(string.Empty),
                    UrnId.Create<GetContactByKey>(string.Empty),
                    UrnId.Create<CheckContactExistsByID>(string.Empty),
                    UrnId.Create<CheckContactExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class ContactService : ContactServiceBase { }
}
