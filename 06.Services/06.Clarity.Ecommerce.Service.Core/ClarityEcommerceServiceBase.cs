// <copyright file="ClarityEcommerceServiceBase.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity eCommerce service base class</summary>
// ReSharper disable MemberCanBePrivate.Global, UnusedMember.Global
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Core;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using Interfaces.Workflow;
    using JSConfigs;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Text;
    using Utilities;

    /// <inheritdoc/>
    /// <summary>A clarity ecommerce service base.</summary>
    /// <seealso cref="Service" />
    public abstract partial class ClarityEcommerceServiceBase : Service
    {
        /// <summary>(Immutable) Name of the service context profile in the service project, which is always null.</summary>
        public const string? ServiceContextProfileName = null;

        /// <summary>The workflow controller.</summary>
        private IWorkflowsController workflowController = null!;

        /// <summary>Gets or sets the current API kind.</summary>
        /// <value>The current API kind.</value>
        public static Enums.APIKind CurrentAPIKind { get; set; }

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        protected IWorkflowsController Workflows => workflowController
            ??= RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets paged results.</summary>
        /// <typeparam name="TIModel">      Type of the model's interface.</typeparam>
        /// <typeparam name="TModel">       Type of the model.</typeparam>
        /// <typeparam name="TISearchModel">Type of the search model's interface.</typeparam>
        /// <typeparam name="TPagedResults">Type of the paged results.</typeparam>
        /// <param name="request">  The request.</param>
        /// <param name="asListing">True to as listing.</param>
        /// <param name="workflow"> The workflow.</param>
        /// <returns>The paged results.</returns>
        protected static async Task<TPagedResults> GetPagedResultsAsync<TIModel, TModel, TISearchModel, TPagedResults>(
                TISearchModel request,
                bool asListing,
                IWorkflowBaseHasSearch<TIModel, TISearchModel> workflow)
            where TIModel : IBaseModel
            where TModel : class, TIModel
            where TISearchModel : IBaseSearchModel
            where TPagedResults : PagedResultsBase<TModel>, new()
        {
            var (results, totalPages, totalCount) = await workflow.SearchAsync(
                    Contract.RequiresNotNull(request),
                    asListing,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return new()
            {
                Results = results.Cast<TModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }

        /// <summary>Gets ss session.</summary>
        /// <returns>The ss session.</returns>
        protected ICMSAuthUserSession? GetSSSession()
        {
            return GetSession() as ICMSAuthUserSession;
        }

        /// <summary>Gets authed ss session.</summary>
        /// <returns>The authed ss session.</returns>
        protected ICMSAuthUserSession? GetAuthedSSSession()
        {
            var session = GetSession() as ICMSAuthUserSession;
            return session?.IsAuthenticated != true ? null : session;
        }

        /// <summary>Gets authed ss session or throw 401.</summary>
        /// <param name="message">The message.</param>
        /// <returns>The authed ss session or throw 401.</returns>
        protected ICMSAuthUserSession GetAuthedSSSessionOrThrow401(string message = "No user currently logged in")
        {
            var session = GetSession() as ICMSAuthUserSession;
            if (session?.IsAuthenticated != true)
            {
                throw HttpError.Unauthorized(message ?? "No user currently logged in");
            }
            return session;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Throw if no rights to record sales invoice.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task.</returns>
        protected Task ThrowIfNoRightsToRecordSalesInvoiceAsync(int id)
        {
            return ThrowIfNoRightsToRecordSalesCollectionAsync<ISalesInvoiceWorkflow,
                ISalesInvoiceModel,
                ISalesInvoiceSearchModel,
                ISalesInvoice,
                SalesInvoice,
                SalesInvoiceStatus,
                SalesInvoiceType,
                ISalesInvoiceItem,
                SalesInvoiceItem,
                AppliedSalesInvoiceDiscount,
                SalesInvoiceState,
                SalesInvoiceFile,
                SalesInvoiceContact,
                AppliedSalesInvoiceItemDiscount,
                SalesInvoiceItemTarget,
                SalesInvoiceEvent,
                SalesInvoiceEventType>(
                    id,
                    Workflows.SalesInvoices);
        }

        /// <summary>Throw if no rights to record sales order.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task.</returns>
        protected Task ThrowIfNoRightsToRecordSalesOrderAsync(int id)
        {
            return ThrowIfNoRightsToRecordSalesCollectionAsync<ISalesOrderWorkflow,
                ISalesOrderModel,
                ISalesOrderSearchModel,
                ISalesOrder,
                SalesOrder,
                SalesOrderStatus,
                SalesOrderType,
                ISalesOrderItem,
                SalesOrderItem,
                AppliedSalesOrderDiscount,
                SalesOrderState,
                SalesOrderFile,
                SalesOrderContact,
                AppliedSalesOrderItemDiscount,
                SalesOrderItemTarget,
                SalesOrderEvent,
                SalesOrderEventType>(
                    id,
                    Workflows.SalesOrders);
        }

        /// <summary>Throw if no rights to record sales quote.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task.</returns>
        protected Task ThrowIfNoRightsToRecordSalesQuoteAsync(int id)
        {
            return ThrowIfNoRightsToRecordSalesCollectionAsync<ISalesQuoteWorkflow,
                ISalesQuoteModel,
                ISalesQuoteSearchModel,
                ISalesQuote,
                SalesQuote,
                SalesQuoteStatus,
                SalesQuoteType,
                ISalesQuoteItem,
                SalesQuoteItem,
                AppliedSalesQuoteDiscount,
                SalesQuoteState,
                SalesQuoteFile,
                SalesQuoteContact,
                AppliedSalesQuoteItemDiscount,
                SalesQuoteItemTarget,
                SalesQuoteEvent,
                SalesQuoteEventType>(
                    id,
                    Workflows.SalesQuotes);
        }

        /// <summary>Throw if no rights to record sales return.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task.</returns>
        protected Task ThrowIfNoRightsToRecordSalesReturnAsync(int id)
        {
            return ThrowIfNoRightsToRecordSalesCollectionAsync<ISalesReturnWorkflow,
                ISalesReturnModel,
                ISalesReturnSearchModel,
                ISalesReturn,
                SalesReturn,
                SalesReturnStatus,
                SalesReturnType,
                ISalesReturnItem,
                SalesReturnItem,
                AppliedSalesReturnDiscount,
                SalesReturnState,
                SalesReturnFile,
                SalesReturnContact,
                AppliedSalesReturnItemDiscount,
                SalesReturnItemTarget,
                SalesReturnEvent,
                SalesReturnEventType>(
                    id,
                    Workflows.SalesReturns);
        }

        private async Task ThrowIfNoRightsToRecordSalesCollectionAsync<TWorkflow,
            TIModel,
            TISearchModel,
            TIEntity,
            TEntity,
            TStatus,
            TType,
            TISalesItem,
            TSalesItem,
            TDiscount,
            TState,
            TStoredFile,
            TContact,
            TItemDiscount,
            TItemTarget,
            TSalesEvent,
            TSalesEventType>(
                int id,
                TWorkflow workflow)
            where TWorkflow : ISalesCollectionWorkflowBase<TIModel,
                TISearchModel,
                TIEntity,
                TEntity,
                TStatus,
                TType,
                TISalesItem,
                TSalesItem,
                TDiscount,
                TState,
                TStoredFile,
                TContact,
                TItemDiscount,
                TItemTarget,
                TSalesEvent,
                TSalesEventType>
            where TIModel : class, ISalesCollectionBaseModel
            where TISearchModel : class, ISalesCollectionBaseSearchModel
            where TIEntity : ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TEntity : class, TIEntity, new()
            where TISalesItem : ISalesItemBase<TSalesItem, TItemDiscount, TItemTarget>
            where TSalesItem : class, TISalesItem, IHaveAppliedDiscountsBase<TSalesItem, TItemDiscount>, new()
            where TStatus : class, IStatusableBase, new()
            where TState : class, IStateableBase, new()
            where TType : class, ITypableBase, new()
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TItemDiscount : IAppliedDiscountBase<TSalesItem, TItemDiscount>
            where TItemTarget : ISalesItemTargetBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    await workflow.CheckExistsAndOwnershipByPortalAdminAsync(
                            id: id,
                            userID: CurrentUserIDOrThrow401,
                            accountIDs: null,
                            portalID: await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false),
                            currentAPIKind: CurrentAPIKind,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    await workflow.CheckExistsAndOwnershipByPortalAdminAsync(
                            id: id,
                            userID: CurrentUserIDOrThrow401,
                            accountIDs: null,
                            portalID: await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false),
                            currentAPIKind: CurrentAPIKind,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    await workflow.CheckExistsAndOwnershipByPortalAdminAsync(
                            id: id,
                            userID: CurrentUserIDOrThrow401,
                            accountIDs: null,
                            portalID: await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false),
                            currentAPIKind: CurrentAPIKind,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin:
                {
                    await workflow.CheckExistsAndOwnershipByPortalAdminAsync(
                            id: id,
                            userID: CurrentUserIDOrThrow401,
                            accountIDs: null,
                            portalID: await CurrentManufacturerForManufacturerAdminIDOrThrow401Async().ConfigureAwait(false),
                            currentAPIKind: CurrentAPIKind,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.VendorAdmin:
                {
                    await workflow.CheckExistsAndOwnershipByPortalAdminAsync(
                            id: id,
                            userID: CurrentUserIDOrThrow401,
                            accountIDs: null,
                            portalID: await CurrentVendorForVendorAdminIDOrThrow401Async().ConfigureAwait(false),
                            currentAPIKind: CurrentAPIKind,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.Storefront:
                {
                    await workflow.CheckExistsAndOwnershipByPortalAdminAsync(
                            id: id,
                            userID: CurrentUserIDOrThrow401,
                            accountIDs: CurrentAccountAndOneLevelDownAssociatedOrThrow401,
                            portalID: 0,
                            currentAPIKind: CurrentAPIKind,
                            contextProfileName: ServiceContextProfileName)
                        .ConfigureAwait(false);
                    break;
                }
                default:
                {
                    // Global/Admin have no additional checks
                    break;
                }
            }
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Gets the current user name.</summary>
        /// <value>The name of the current user.</value>
        protected string? CurrentUserName => GetAuthedSSSession()?.UserName;

        /// <summary>Gets the identifier for the current user.</summary>
        /// <value>The identifier of the current user.</value>
        protected int? CurrentUserID => GetAuthedSSSession()?.UserID;

        /// <summary>Gets the current user identifier or throw 401.</summary>
        /// <value>The current user identifier or throw 401.</value>
        protected int CurrentUserIDOrThrow401
        {
            get
            {
                GetAuthedSSSessionOrThrow401();
                return CurrentUserID!.Value;
            }
        }

        /// <summary>Gets the current user name or throw 401.</summary>
        /// <value>The current user name or throw 401.</value>
        protected string CurrentUserNameOrThrow401
        {
            get
            {
                GetAuthedSSSessionOrThrow401();
                return CurrentUserName!;
            }
        }

        /// <summary>Gets the current user.</summary>
        /// <returns>The current user.</returns>
        protected Task<IUserModel?> CurrentUserAsync()
        {
            var session = GetAuthedSSSession();
            return (session is null ? Task.FromResult<IUserModel?>(null)! : session.UserAsync())!;
        }

        /// <summary>Gets the current user or throw 401.</summary>
        /// <returns>The current user or throw 401.</returns>
        protected Task<IUserModel> CurrentUserOrThrow401Async()
        {
            GetAuthedSSSessionOrThrow401();
            return CurrentUserAsync()!;
        }

        /// <summary>Selected User or Current User or Throw 401.</summary>
        /// <param name="requestUserID">Identifier for the request User.</param>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> SelectedUserOrCurrentUserOrThrow401Async(int? requestUserID)
        {
            if (!GetSession().HasRole("CEF Local Administrator", AuthRepository))
            {
                return CurrentUserIDOrThrow401;
            }
            if (Contract.CheckValidKey(Request.GetItemOrCookie(SelectedAffiliateUserKeyCookieName)))
            {
                var key = Request.GetItemOrCookie(SelectedAffiliateUserKeyCookieName).UrlDecode();
                var id = await Workflows.Users.CheckExistsByUsernameAsync(key, contextProfileName: null).ConfigureAwait(false);
                if (Contract.CheckValidID(id))
                {
                    return id.Value;
                }
            }
            return Contract.RequiresValidID(requestUserID, "UserID is required");
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>The selected affiliate account key cookie name.</summary>
        protected const string SelectedAffiliateAccountKeyCookieName = "cefSelectedAffiliateAccountKey";

        /// <summary>The selected affiliate user key cookie name.</summary>
        protected const string SelectedAffiliateUserKeyCookieName = "cefSelectedAffiliateUserKey";

        /// <summary>Gets the current account identifier.</summary>
        /// <value>The identifier of the current account.</value>
        protected int? CurrentAccountID => GetAuthedSSSession()?.AccountID;

        /// <summary>Gets the current account identifier or throw 401.</summary>
        /// <value>The current account identifier or throw 401.</value>
        protected int CurrentAccountIDOrThrow401
        {
            get
            {
                var session = GetAuthedSSSessionOrThrow401("Could not locate logged in account");
                if (!Contract.CheckValidID(session.AccountID))
                {
                    // ReSharper disable once ThrowExceptionInUnexpectedLocation
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw HttpError.Unauthorized("Could not locate logged in account");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                return session.AccountID!.Value;
            }
        }

        /// <summary>Gets the current account customKey.</summary>
        /// <value>The customKey of the current account.</value>
        protected string? CurrentAccountKey => GetAuthedSSSession()?.AccountKey;

        /// <summary>Gets the current account identifier or throw 401.</summary>
        /// <value>The current account identifier or throw 401.</value>
        protected string CurrentAccountKeyOrThrow401
        {
            get
            {
                var session = GetAuthedSSSessionOrThrow401("Could not locate logged in account");
                if (!Contract.CheckValidKey(session.AccountKey))
                {
                    // ReSharper disable once ThrowExceptionInUnexpectedLocation
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw HttpError.Unauthorized("Could not locate logged in account");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                return session.AccountKey!;
            }
        }

        /// <summary>Gets the current account and one level down associated or throw 401.</summary>
        /// <value>The current account and one level down associated or throw 401.</value>
        protected List<int> CurrentAccountAndOneLevelDownAssociatedOrThrow401
        {
            get
            {
                var session = GetAuthedSSSessionOrThrow401("Could not locate logged in account");
                if (!Contract.CheckValidID(session.AccountID))
                {
                    // ReSharper disable once ThrowExceptionInUnexpectedLocation
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw HttpError.Unauthorized("Could not locate logged in account");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                var accountIDs = new List<int>
                {
                    session.AccountID!.Value,
                };
                accountIDs.AddRange(
                    Workflows.AccountAssociations.GetOneLevelDownAccountAssociationIDs(
                        session.AccountID.Value,
                        ServiceContextProfileName));
                return accountIDs;
            }
        }

        /// <summary>Current account.</summary>
        /// <returns>A Task{IAccountModel}.</returns>
        protected Task<IAccountModel?> CurrentAccountAsync()
        {
            var session = GetAuthedSSSession();
            return (session is null
                ? Task.FromResult<IAccountModel?>(null)!
                : session.AccountAsync())!;
        }

        /// <summary>Current account type name.</summary>
        /// <returns>A Task{string}.</returns>
        protected async Task<string?> CurrentAccountTypeNameAsync()
        {
            var session = GetAuthedSSSession();
            return session is null
                ? null
                : (await session.AccountAsync().ConfigureAwait(false))?.TypeName;
        }

        /// <summary>Current account or throw 401.</summary>
        /// <returns>A Task{IAccountModel}.</returns>
        protected Task<IAccountModel> CurrentAccountOrThrow401Async()
        {
            var session = GetAuthedSSSessionOrThrow401("Could not locate logged in account");
            if (!Contract.CheckValidID(session.AccountID))
            {
                throw HttpError.Unauthorized("Could not locate logged in account");
            }
            return session.AccountAsync()!;
        }

        /// <summary>Current account price point key.</summary>
        /// <returns>A Task{string}.</returns>
        protected async Task<string?> CurrentAccountPricePointKeyAsync()
        {
            return (await CurrentAccountAsync().ConfigureAwait(false))
                ?.AccountPricePoints?.SingleOrDefault(x => x.Active)?.SlaveKey
                ?? CEFConfigDictionary.PricingProviderTieredDefaultPricePointKey;
        }

        /// <summary>Local admin account identifier or throw 401.</summary>
        /// <param name="requestAccountID">Identifier for the request account.</param>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> LocalAdminAccountIDOrThrow401Async(int? requestAccountID)
        {
            if (!GetSession().HasRole("CEF Local Administrator", AuthRepository)
                && (!GetSession().HasRole("CEF Affiliate Administrator", AuthRepository)
                    && !GetSession().HasRole("Supervisor", AuthRepository)))
            {
                return CurrentAccountIDOrThrow401;
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(Request.GetItemOrCookie(SelectedAffiliateAccountKeyCookieName)))
            {
                var key = Request.GetItemOrCookie(SelectedAffiliateAccountKeyCookieName).UrlDecode();
                var id = await Workflows.Accounts.CheckExistsAsync(key, ServiceContextProfileName).ConfigureAwait(false);
                if (Contract.CheckValidID(id))
                {
                    return id!.Value;
                }
            }
            return Contract.RequiresValidID(requestAccountID, "AccountID is required");
        }

        /// <summary>Local admin account identifier or null.</summary>
        /// <param name="requestAccountID">Identifier for the requested account.</param>
        /// <returns>A Task{int}.</returns>
        protected async Task<int?> LocalAdminAccountIDAsync(int? requestAccountID)
        {
            if (!GetSession().HasRole("CEF Local Administrator", AuthRepository)
                && !GetSession().HasRole("CEF Affiliate Administrator", AuthRepository)
                && !GetSession().HasRole("Supervisor", AuthRepository))
            {
                return CurrentAccountID;
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(Request.GetItemOrCookie(SelectedAffiliateAccountKeyCookieName)))
            {
                var key = Request.GetItemOrCookie(SelectedAffiliateAccountKeyCookieName).UrlDecode();
                var id = await Workflows.Accounts.CheckExistsAsync(key, ServiceContextProfileName).ConfigureAwait(false);
                if (Contract.CheckValidID(id))
                {
                    return id!.Value;
                }
            }
            return requestAccountID;
        }

        /// <summary>Executes the account on hold check.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CheckoutResult}.</returns>
        protected async Task<ICheckoutResult?> DoAccountOnHoldCheckAsync(string? contextProfileName)
        {
            if (CEFConfigDictionary.PurchaseAllowAccountOnHoldOrders
                || !((await CurrentAccountAsync().ConfigureAwait(false))?.IsOnHold ?? false))
            {
                return null;
            }
            var result = RegistryLoaderWrapper.GetInstance<ICheckoutResult>(contextProfileName);
            result.ErrorMessage = "Your account is currently on hold. Please contact customer support for assistance.";
            return result;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Current store for store admin identifier or throw 401.</summary>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> CurrentStoreForStoreAdminIDOrThrow401Async()
        {
            var isStoreAdmin = GetAuthedSSSessionOrThrow401().HasRole("CEF Store Administrator", AuthRepository);
            if (!isStoreAdmin)
            {
                throw HttpError.Unauthorized("This user is not a store administrator");
            }
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var storeID = await context.StoreUsers
                .AsNoTracking()
                .FilterByActive(true)
                .FilterIAmARelationshipTableBySlaveID<StoreUser, Store, User>(CurrentUserID)
                .Select(x => x.MasterID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(storeID))
            {
                throw HttpError.Unauthorized("This user is not assigned to a store");
            }
            return storeID;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        private Dictionary<string, int?> CachedUrlReferrersToBrandIDs { get; } = new();

        private Dictionary<string, IBrandModel?> CachedUrlReferrersToBrands { get; } = new();

        /// <summary>Parse request URL referrer to brand identifier.</summary>
        /// <returns>A Task{int?}.</returns>
        protected async Task<int?> ParseRequestUrlReferrerToBrandIDAsync()
        {
            if (Request.UrlReferrer is null)
            {
                return null;
            }
            var referrer = Request.UrlReferrer.ToString();
            if (CachedUrlReferrersToBrandIDs.ContainsKey(referrer))
            {
                return CachedUrlReferrersToBrandIDs[referrer];
            }
            CachedUrlReferrersToBrandIDs[referrer] = await Workflows.Brands.CheckExistsByHostUrlAsync(referrer, ServiceContextProfileName).ConfigureAwait(false);
            return CachedUrlReferrersToBrandIDs[referrer];
        }

        /// <summary>Parse request URL referrer to brand last modified.</summary>
        /// <returns>A Task{DateTime?}.</returns>
        protected async Task<DateTime?> ParseRequestUrlReferrerToBrandLastModifiedAsync()
        {
            if (Request.UrlReferrer is null)
            {
                return null;
            }
            var referrer = Request.UrlReferrer.ToString();
            if (CachedUrlReferrersToBrands.ContainsKey(referrer))
            {
                return CachedUrlReferrersToBrands[referrer]?.UpdatedDate ?? CachedUrlReferrersToBrands[referrer]?.CreatedDate;
            }
            CachedUrlReferrersToBrands[referrer] = await Workflows.Brands.GetByHostUrlAsync(referrer, ServiceContextProfileName).ConfigureAwait(false);
            return CachedUrlReferrersToBrands[referrer]?.UpdatedDate ?? CachedUrlReferrersToBrands[referrer]?.CreatedDate;
        }

        /// <summary>Parse request URL referrer to brand.</summary>
        /// <returns>A Task{IBrandModel}.</returns>
        protected async Task<IBrandModel?> ParseRequestUrlReferrerToBrandAsync()
        {
            if (Request.UrlReferrer is null)
            {
                return null;
            }
            var referrer = Request.UrlReferrer.ToString();
            if (CachedUrlReferrersToBrands.ContainsKey(referrer))
            {
                return CachedUrlReferrersToBrands[referrer];
            }
            CachedUrlReferrersToBrands[referrer] = await Workflows.Brands.GetByHostUrlAsync(referrer, ServiceContextProfileName).ConfigureAwait(false);
            return CachedUrlReferrersToBrands[referrer];
        }

        /// <summary>Current brand for brand admin identifier or throw 401.</summary>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> CurrentBrandForBrandAdminIDOrThrow401Async()
        {
            var user = CurrentUserIDOrThrow401;
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var brandIDsForUser = await context.BrandUsers
                .AsNoTracking()
                .FilterByActive(true)
                .Select(x => x.MasterID)
                .ToListAsync()
                .ConfigureAwait(false);
            if (!brandIDsForUser.Any())
            {
                throw HttpError.Unauthorized("This user is not assigned to a brand");
            }
            var currentBrandID = await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(currentBrandID))
            {
                throw HttpError.Unauthorized("The current brand could not be identified");
            }
            if (!brandIDsForUser.Contains(currentBrandID!.Value))
            {
                throw HttpError.Unauthorized("This user is not assigned to the current brand");
            }
            var session = GetAuthedSSSessionOrThrow401();
            if (!session.HasRole("CEF Brand Administrator", AuthRepository))
            {
                throw HttpError.Unauthorized("This user is not a Brand Administrator");
            }
            return currentBrandID.Value;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        private Dictionary<string, int?> CachedUrlReferrersToFranchiseIDs { get; } = new();

        private Dictionary<string, IFranchiseModel?> CachedUrlReferrersToFranchises { get; } = new();

        /// <summary>Parse request URL referrer to franchise identifier.</summary>
        /// <returns>A Task{int?}.</returns>
        protected async Task<int?> ParseRequestUrlReferrerToFranchiseIDAsync()
        {
            if (Request.UrlReferrer is null)
            {
                return null;
            }
            var referrer = Request.UrlReferrer.ToString();
            if (CachedUrlReferrersToFranchiseIDs.ContainsKey(referrer))
            {
                return CachedUrlReferrersToFranchiseIDs[referrer];
            }
            CachedUrlReferrersToFranchiseIDs[referrer] = await Workflows.Franchises.CheckExistsByHostUrlAsync(referrer, ServiceContextProfileName).ConfigureAwait(false);
            return CachedUrlReferrersToFranchiseIDs[referrer];
        }

        /// <summary>Parse request URL referrer to franchise last modified.</summary>
        /// <returns>A Task{DateTime?}.</returns>
        protected async Task<DateTime?> ParseRequestUrlReferrerToFranchiseLastModifiedAsync()
        {
            if (Request.UrlReferrer is null)
            {
                return null;
            }
            var referrer = Request.UrlReferrer.ToString();
            if (CachedUrlReferrersToFranchises.ContainsKey(referrer))
            {
                return CachedUrlReferrersToFranchises[referrer]?.UpdatedDate ?? CachedUrlReferrersToFranchises[referrer]?.CreatedDate;
            }
            CachedUrlReferrersToFranchises[referrer] = await Workflows.Franchises.GetByHostUrlAsync(referrer, ServiceContextProfileName).ConfigureAwait(false);
            return CachedUrlReferrersToFranchises[referrer]?.UpdatedDate ?? CachedUrlReferrersToFranchises[referrer]?.CreatedDate;
        }

        /// <summary>Parse request URL referrer to franchise.</summary>
        /// <returns>A Task{IFranchiseModel}.</returns>
        protected async Task<IFranchiseModel?> ParseRequestUrlReferrerToFranchiseAsync()
        {
            if (Request.UrlReferrer is null)
            {
                return null;
            }
            var referrer = Request.UrlReferrer.ToString();
            if (CachedUrlReferrersToFranchises.ContainsKey(referrer))
            {
                return CachedUrlReferrersToFranchises[referrer];
            }
            CachedUrlReferrersToFranchises[referrer] = await Workflows.Franchises.GetByHostUrlAsync(referrer, ServiceContextProfileName).ConfigureAwait(false);
            return CachedUrlReferrersToFranchises[referrer];
        }

        /// <summary>Current franchise for franchise admin identifier or throw 401.</summary>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> CurrentFranchiseForFranchiseAdminIDOrThrow401Async()
        {
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            if (user?.Franchises is null)
            {
                throw HttpError.Unauthorized("This user is not assigned to a franchise");
            }
            // TODO: User must be the admin of the franchise
            var currentFranchiseID = await ParseRequestUrlReferrerToFranchiseIDAsync().ConfigureAwait(false);
            var userFranchise = user.Franchises.FirstOrDefault(x => x.MasterID == currentFranchiseID);
            if (userFranchise?.MasterID is null)
            {
                throw HttpError.Unauthorized("This user is not assigned to the current franchise");
            }
            var session = GetAuthedSSSessionOrThrow401();
            if (!session.HasRole("CEF Franchise Administrator", AuthRepository))
            {
                throw HttpError.Unauthorized("This user is not a Franchise Administrator");
            }
            return userFranchise.MasterID;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Current vendor for vendor admin identifier or throw 401.</summary>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> CurrentVendorForVendorAdminIDOrThrow401Async()
        {
            var response = await Workflows.Vendors.GetIDByAssignedUserIDAsync(
                    GetAuthedSSSessionOrThrow401().UserID!.Value,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                throw HttpError.Unauthorized("Unable to locate vendor for current user");
            }
            return response.Result!.Value;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Current manufacturer for manufacturer admin identifier or throw 401.</summary>
        /// <returns>A Task{int}.</returns>
        protected async Task<int> CurrentManufacturerForManufacturerAdminIDOrThrow401Async()
        {
            var response = await Workflows.Manufacturers.GetIDByAssignedUserIDAsync(
                    GetAuthedSSSessionOrThrow401().UserID!.Value,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                throw HttpError.Unauthorized("Unable to locate manufacturer for current user");
            }
            return response.Result!.Value;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Context for the pricing factory.</summary>
        private IPricingFactoryContextModel? pricingFactoryContext;

        /// <summary>Gets a context for the pricing factory.</summary>
        /// <returns>The pricing factory context.</returns>
        protected async Task<IPricingFactoryContextModel> GetPricingFactoryContextAsync()
        {
            if (pricingFactoryContext is not null)
            {
                return pricingFactoryContext;
            }
            pricingFactoryContext = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>();
            ////pricingFactoryContext.StoreID = await ParseRequestUrlReferrerToStoreIDAsync().ConfigureAwait(false);
            pricingFactoryContext.BrandID = await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false);
            pricingFactoryContext.FranchiseID = await ParseRequestUrlReferrerToFranchiseIDAsync().ConfigureAwait(false);
            var session = GetSession() as ICMSAuthUserSession;
            if (session?.IsAuthenticated == true)
            {
                var currentUserID = CurrentUserID;
                if (Contract.CheckValidID(currentUserID))
                {
                    var currentUser = await Workflows.Users.GetForPricingAsync(currentUserID!.Value, ServiceContextProfileName).ConfigureAwait(false);
                    if (currentUser is null)
                    {
                        throw HttpError.Unauthorized(
                            "Your user is disabled, please contact the site administrator for assistance.");
                    }
                    pricingFactoryContext.UserID = currentUserID;
                    pricingFactoryContext.UserKey = CurrentUserName;
                    pricingFactoryContext.UserRoles = currentUser.Roles!.ToList();
                    pricingFactoryContext.CountryID = currentUser.CountryID;
                    var affAccountID = await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false);
                    var account = await Workflows.Accounts.GetForPricingByUserIDAsync(currentUserID.Value, affAccountID, ServiceContextProfileName).ConfigureAwait(false);
                    if (account is not null)
                    {
                        pricingFactoryContext.AccountID = account.ID;
                        pricingFactoryContext.AccountKey = account.CustomKey;
                        pricingFactoryContext.AccountTypeID = account.TypeID;
                        pricingFactoryContext.PricePoint = account.AccountPricePoints
                                ?.FirstOrDefault(x => x.Active)
                                ?.SlaveKey
                            ?? CEFConfigDictionary.PricingProviderTieredDefaultPricePointKey;
                    }
                    pricingFactoryContext.CurrencyID = currentUser.CurrencyID;
                    if (!Contract.CheckValidID(pricingFactoryContext.StoreID)
                        && Contract.CheckValidID(currentUser.PreferredStoreID))
                    {
                        pricingFactoryContext.StoreID = currentUser.PreferredStoreID;
                    }
                }
                else
                {
                    pricingFactoryContext.UserRoles = new();
                }
            }
            else
            {
                pricingFactoryContext.UserRoles = new();
            }
            await PickupShoppingCartCookieAsync().ConfigureAwait(false);
            pricingFactoryContext.SessionID = await GetSessionShoppingCartGuidAsync().ConfigureAwait(false);
            return pricingFactoryContext;
        }

        /// <summary>Pricing factory context for other user.</summary>
        /// <param name="userID">  Identifier for the user.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>A Task{IPricingFactoryContextModel}.</returns>
        protected async Task<IPricingFactoryContextModel> PricingFactoryContextForOtherUserAsync(int userID, decimal quantity = 1m)
        {
            var factoryContext = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>();
            ////factoryContext.StoreID = await ParseRequestUrlReferrerToStoreIDAsync().ConfigureAwait(false);
            factoryContext.BrandID = await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false);
            factoryContext.FranchiseID = await ParseRequestUrlReferrerToFranchiseIDAsync().ConfigureAwait(false);
            factoryContext.Quantity = quantity;
            var user = await Workflows.Users.GetAsync(userID, contextProfileName: ServiceContextProfileName).ConfigureAwait(false);
            if (user is null)
            {
                return factoryContext;
            }
            factoryContext.UserID = userID;
            factoryContext.UserKey = user.CustomKey;
            // TODO: User's roles, needs the PricingRules Overhaul PR to merge first
            factoryContext.CurrencyID = user.CurrencyID;
            var account = Contract.CheckValidID(user.AccountID)
                ? await Workflows.Accounts.GetAsync(user.AccountID!.Value, contextProfileName: ServiceContextProfileName).ConfigureAwait(false)
                : null;
            if (account is not null)
            {
                factoryContext.AccountID = account.ID;
                factoryContext.AccountKey = account.CustomKey;
                factoryContext.AccountTypeID = account.TypeID;
                factoryContext.PricePoint = account.AccountPricePoints?.SingleOrDefault()?.SlaveKey
                    ?? CEFConfigDictionary.PricingProviderTieredDefaultPricePointKey;
            }
            if (!Contract.CheckValidID(factoryContext.StoreID) && Contract.CheckValidID(user.PreferredStoreID))
            {
                factoryContext.StoreID = user.PreferredStoreID;
            }
            return factoryContext;
        }

        /// <summary>Pricing factory context with quantity.</summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns>An IPricingFactoryContext.</returns>
        protected async Task<IPricingFactoryContextModel> GetPricingFactoryContextWithQuantityAsync(decimal quantity)
        {
            var temp = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            temp.Quantity = quantity;
            return temp;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Identifier for the visit key session.</summary>
        private const string VisitKeyCookieName = "cef_visit_key";

        /// <summary>Identifier for the visitor key session.</summary>
        private const string VisitorKeyCookieName = "cef_visitor_key";

        /// <summary>Gets a unique identifier of the session visit.</summary>
        /// <returns>Unique identifier of the session visit.</returns>
        protected async Task<Guid> GetSessionVisitGuidAsync()
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                return default;
            }
            var key = $"session:{GetSession().Id}:{VisitKeyCookieName}";
            Guid value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                value = await client.GetAsync<Guid>(key).ConfigureAwait(false);
            }
            else
            {
                value = Guid.NewGuid();
                await client.AddAsync(key, value).ConfigureAwait(false);
            }
            return value;
        }

        /// <summary>Gets a unique identifier of the session visitor.</summary>
        /// <returns>Unique identifier of the session visitor.</returns>
        protected async Task<Guid> GetSessionVisitorGuidAsync()
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                return default;
            }
            var key = $"session:{GetSession().Id}:{VisitorKeyCookieName}";
            Guid value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                value = await client.GetAsync<Guid>(key).ConfigureAwait(false);
            }
            else
            {
                value = Guid.NewGuid();
                await client.AddAsync(key, value).ConfigureAwait(false);
            }
            return value;
        }

        /// <summary>Pickup visit cookie.</summary>
        /// <returns>A Task.</returns>
        protected async Task PickupVisitCookieAsync()
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                return;
            }
            if (Contract.CheckValidKey(Request.GetItemOrCookie(VisitKeyCookieName)))
            {
                var guid = Guid.Parse(Request.GetItemOrCookie(VisitKeyCookieName));
                if (await GetSessionVisitGuidAsync().ConfigureAwait(false) != guid)
                {
                    await OverrideSessionVisitGuidAsync(guid).ConfigureAwait(false);
                }
            }
            else
            {
                Response.SetCookie(MakeACookie(VisitKeyCookieName, (await GetSessionVisitGuidAsync().ConfigureAwait(false)).ToString()));
            }
        }

        /// <summary>Pickup visitor cookie.</summary>
        /// <returns>A Task.</returns>
        protected async Task PickupVisitorCookieAsync()
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                return;
            }
            if (Contract.CheckValidKey(Request.GetItemOrCookie(VisitorKeyCookieName)))
            {
                var guid = Guid.Parse(Request.GetItemOrCookie(VisitorKeyCookieName));
                if (await GetSessionVisitorGuidAsync().ConfigureAwait(false) != guid)
                {
                    await OverrideSessionVisitorGuidAsync(guid).ConfigureAwait(false);
                }
            }
            else
            {
                Response.SetCookie(MakeACookie(VisitorKeyCookieName, (await GetSessionVisitorGuidAsync().ConfigureAwait(false)).ToString()));
            }
        }

        /// <summary>Override session visit unique identifier.</summary>
        /// <param name="guid">Unique identifier.</param>
        /// <returns>A Task.</returns>
        private async Task OverrideSessionVisitGuidAsync(Guid guid)
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                return;
            }
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            var key = $"session:{GetSession().Id}:{VisitKeyCookieName}";
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                await client.RemoveAsync(key).ConfigureAwait(false);
            }
            await client.AddAsync(key, guid).ConfigureAwait(false);
        }

        /// <summary>Override session visitor unique identifier.</summary>
        /// <param name="guid">Unique identifier.</param>
        /// <returns>A Task.</returns>
        private async Task OverrideSessionVisitorGuidAsync(Guid guid)
        {
            if (!CEFConfigDictionary.TrackingEnabled)
            {
                return;
            }
            var key = $"session:{GetSession().Id}:{VisitorKeyCookieName}";
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                await client.RemoveAsync(key).ConfigureAwait(false);
            }
            await client.AddAsync(key, guid).ConfigureAwait(false);
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Identifier for the cart session.</summary>
        protected const string ShoppingCartCookieName = "cef_cart_shopping";

        /// <summary>Identifier for the quote cart session.</summary>
        protected const string QuoteCartCookieName = "cef_cart_quote";

        /// <summary>Identifier for the samples cart session.</summary>
        protected const string SampleCartCookieName = "cef_cart_sample";

        /// <summary>Identifier for the session compare cart.</summary>
        protected const string CompareCartCookieName = "cef_cart_compare";

        /// <summary>Create a cookie option with the specified settings.</summary>
        /// <param name="name">     The name of the cookie.</param>
        /// <param name="value">    The value of the cookie.</param>
        /// <param name="expiresIn">How long before the cookie expires, null to never expire.</param>
        /// <returns>The cookie object.</returns>
        protected static Cookie MakeACookie(string name, string value, TimeSpan? expiresIn = null)
        {
            var cookie = new Cookie
            {
                Name = name,
                Value = value,
                Path = CEFConfigDictionary.CookiesPath,
                Domain = HostConfig.Instance.RestrictAllCookiesToDomain,
            };
            if (expiresIn.HasValue)
            {
                cookie.Expires = DateTime.Now.Add(expiresIn.Value);
            }
            if (CEFConfigDictionary.CookiesRequireSecure)
            {
                cookie.Secure = true;
            }
            cookie.HttpOnly = CEFConfigDictionary.CookiesRequireHTTPOnly;
            return cookie;
        }

        /// <summary>Gets session shopping cart unique identifier.</summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The session shopping cart unique identifier.</returns>
        protected async Task<Guid> GetSessionCorrectCartGuidAsync(string typeName)
        {
            return typeName.ToLower() switch
            {
                "sample cart" => await GetSessionSampleCartGuidAsync().ConfigureAwait(false),
                "quote cart" => await GetSessionQuoteCartGuidAsync().ConfigureAwait(false),
                _ => await GetSessionShoppingCartGuidAsync().ConfigureAwait(false),
            };
        }

        /// <summary>Gets a unique identifier of the session compare cart.</summary>
        /// <returns>Unique identifier of the session compare cart.</returns>
        protected Task<Guid> GetSessionCompareCartGuidAsync()
        {
            return GetSessionGuidAsync(CompareCartCookieName);
        }

        /// <summary>Pickup correct cart cookie asynchronous.</summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>A Task.</returns>
        protected async Task PickupCorrectCartCookieAsync(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "sample cart":
                {
                    await PickupSampleCartCookieAsync().ConfigureAwait(false);
                    break;
                }
                case "quote cart":
                {
                    await PickupQuoteCartCookieAsync().ConfigureAwait(false);
                    break;
                }
                default:
                {
                    await PickupShoppingCartCookieAsync().ConfigureAwait(false);
                    break;
                }
            }
        }

        /// <summary>Pickup compare cart cookie.</summary>
        /// <returns>A Task.</returns>
        protected async Task PickupCompareCartCookieAsync()
        {
            if (Contract.CheckValidKey(Request.GetItemOrCookie(CompareCartCookieName)))
            {
                var guid = Guid.Parse(Request.GetItemOrCookie(CompareCartCookieName));
                if (await GetSessionCompareCartGuidAsync().ConfigureAwait(false) != guid)
                {
                    await OverrideCompareCartSessionGuidAsync(guid).ConfigureAwait(false);
                }
            }
            else
            {
                Response.SetCookie(MakeACookie(
                    CompareCartCookieName,
                    (await GetSessionCompareCartGuidAsync().ConfigureAwait(false)).ToString()));
            }
        }

        /// <summary>Generates a cart by identifier lookup key.</summary>
        /// <param name="cartID">     Identifier for the cart.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="accountID">  Identifier for the account.</param>
        /// <param name="brandID">    Identifier for the brand.</param>
        /// <param name="franchiseID">Identifier for the franchise.</param>
        /// <param name="storeID">    Identifier for the store.</param>
        /// <returns>The lookup key.</returns>
        protected async Task<CartByIDLookupKey> GenCartByIDLookupKeyAsync(
            int cartID,
            int? userID = null,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            return new(
                cartID: cartID,
                userID: userID ?? CurrentUserID,
                accountID: accountID ?? await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false),
                brandID: brandID ?? await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false),
                franchiseID: franchiseID,
                storeID: storeID);
        }

        /// <summary>Generates a cart by identifier lookup key.</summary>
        /// <param name="lookupData">Information describing the lookup.</param>
        /// <param name="cartID">    Identifier for the cart.</param>
        /// <returns>The cart by identifier lookup key.</returns>
        protected CartByIDLookupKey GenCartByIDLookupKey(ImplementsIDForAdminBase lookupData, int? cartID = null)
        {
            return new(
                cartID: cartID ?? lookupData.ID,
                userID: lookupData.UserID,
                accountID: lookupData.AccountID,
                brandID: lookupData.BrandID,
                franchiseID: lookupData.FranchiseID,
                storeID: lookupData.StoreID);
        }

        /// <summary>Generates a cart by identifier lookup key.</summary>
        /// <param name="lookupData">Information describing the lookup.</param>
        /// <param name="cartID">    Identifier for the cart.</param>
        /// <returns>The cart by identifier lookup key.</returns>
        protected CartByIDLookupKey GenCartByIDLookupKey(ImplementsIDOnBodyForAdminBase lookupData, int? cartID = null)
        {
            return new(
                cartID: cartID ?? lookupData.ID,
                userID: lookupData.UserID,
                accountID: lookupData.AccountID,
                brandID: lookupData.BrandID,
                franchiseID: lookupData.FranchiseID,
                storeID: lookupData.StoreID);
        }

        /// <summary>Generates a session lookup key.</summary>
        /// <param name="typeKey">The type key.</param>
        /// <returns>The session lookup key.</returns>
        protected async Task<SessionCartBySessionAndTypeLookupKey> GenSessionLookupKeyAsync(string? typeKey)
        {
            return new(
                typeKey: typeKey ?? "Cart",
                sessionID: await GetSessionCorrectCartGuidAsync(typeKey ?? "Cart").ConfigureAwait(false),
                userID: CurrentUserID,
                accountID: await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false),
                brandID: await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false));
        }

        /// <summary>Generates a static lookup key.</summary>
        /// <param name="typeKey">The type key.</param>
        /// <returns>The static lookup key.</returns>
        protected async Task<StaticCartLookupKey> GenStaticLookupKeyAsync(string? typeKey)
        {
            return new(
                userID: CurrentUserIDOrThrow401,
                typeKey: typeKey ?? "Wish List",
                accountID: await LocalAdminAccountIDAsync(CurrentAccountIDOrThrow401).ConfigureAwait(false),
                brandID: await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false));
        }

        /// <summary>Generates a compare lookup key.</summary>
        /// <returns>The compare lookup key.</returns>
        protected async Task<CompareCartBySessionLookupKey> GenCompareLookupKeyAsync()
        {
            return new(
                sessionID: await GetSessionCompareCartGuidAsync().ConfigureAwait(false),
                userID: CurrentUserID,
                accountID: await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false),
                brandID: await ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false));
        }

        /// <summary>Clear session shopping cart unique identifier.</summary>
        /// <returns>A Task.</returns>
        protected async Task ClearSessionShoppingCartGuidAsync()
        {
            var key = $"session:{GetSession().Id}:{ShoppingCartCookieName}";
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                await client.RemoveAsync(key).ConfigureAwait(false);
            }
            Response.DeleteCookie(ShoppingCartCookieName);
        }

        /// <summary>Clear session quote cart unique identifier.</summary>
        /// <returns>A Task.</returns>
        protected async Task ClearSessionQuoteCartGuidAsync()
        {
            var key = $"session:{GetSession().Id}:{QuoteCartCookieName}";
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                await client.RemoveAsync(key).ConfigureAwait(false);
            }
            Response.DeleteCookie(QuoteCartCookieName);
        }

        /// <summary>Clear session samples cart unique identifier.</summary>
        /// <returns>A Task.</returns>
        protected async Task ClearSessionSampleCartGuidAsync()
        {
            var key = $"session:{GetSession().Id}:{SampleCartCookieName}";
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                await client.RemoveAsync(key).ConfigureAwait(false);
            }
            Response.DeleteCookie(SampleCartCookieName);
        }

        /// <summary>Override session correct cart unique identifier asynchronous.</summary>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="guid">    Unique identifier.</param>
        /// <returns>A Task.</returns>
        protected async Task OverrideSessionCorrectCartGuidAsync(string typeName, Guid guid)
        {
            switch (typeName.ToLower())
            {
                case "sample cart":
                {
                    await OverrideSessionSampleCartGuidAsync(guid).ConfigureAwait(false);
                    break;
                }
                case "quote cart":
                {
                    await OverrideSessionQuoteCartGuidAsync(guid).ConfigureAwait(false);
                    break;
                }
                default:
                {
                    await OverrideSessionCartGuidAsync(guid).ConfigureAwait(false);
                    break;
                }
            }
        }

        /// <summary>Override session compare cart unique identifier.</summary>
        /// <param name="guid">Unique identifier.</param>
        /// <returns>A Task.</returns>
        protected async Task OverrideCompareCartSessionGuidAsync(Guid guid)
        {
            Contract.Requires<ArgumentException>(
                guid.ToString() != "00000000-0000-0000-0000-000000000000",
                "ERROR! Compare cart cookie cannot be assigned to a default Guid");
            var key = $"session:{GetSession().Id}:{CompareCartCookieName}";
            // if (await client.ExistsAsync(key).ConfigureAwait(false))
            // {
            //     await client.RemoveAsync(key).ConfigureAwait(false);
            // }
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            await client!.AddAsync(key, guid).ConfigureAwait(false);
            Response.SetCookie(MakeACookie(CompareCartCookieName, guid.ToString()));
        }

        /// <summary>Gets a unique identifier of the session shopping cart.</summary>
        /// <returns>Unique identifier of the session shopping cart.</returns>
        protected Task<Guid> GetSessionShoppingCartGuidAsync()
        {
            return GetSessionGuidAsync(ShoppingCartCookieName);
        }

        /// <summary>Gets a unique identifier of the session quote cart.</summary>
        /// <returns>Unique identifier of the quote session quote cart.</returns>
        protected Task<Guid> GetSessionQuoteCartGuidAsync()
        {
            return GetSessionGuidAsync(QuoteCartCookieName);
        }

        /// <summary>Gets a unique identifier of the session samples cart.</summary>
        /// <returns>Unique identifier of the sample session samples cart.</returns>
        private Task<Guid> GetSessionSampleCartGuidAsync()
        {
            return GetSessionGuidAsync(SampleCartCookieName);
        }

        /// <summary>Pickup shopping cart cookie.</summary>
        /// <returns>A Task.</returns>
        private async Task PickupShoppingCartCookieAsync()
        {
            if (Contract.CheckValidKey(Request.GetItemOrCookie(ShoppingCartCookieName)))
            {
                var guid = Guid.Parse(Request.GetItemOrCookie(ShoppingCartCookieName));
                if (await GetSessionShoppingCartGuidAsync().ConfigureAwait(false) != guid)
                {
                    await OverrideSessionCartGuidAsync(guid).ConfigureAwait(false);
                }
            }
            else
            {
                Response.SetCookie(MakeACookie(
                    ShoppingCartCookieName,
                    (await GetSessionShoppingCartGuidAsync().ConfigureAwait(false)).ToString()));
            }
        }

        /// <summary>Pickup quote cart cookie.</summary>
        /// <returns>A Task.</returns>
        private async Task PickupQuoteCartCookieAsync()
        {
            if (Contract.CheckValidKey(Request.GetItemOrCookie(QuoteCartCookieName)))
            {
                var guid = Guid.Parse(Request.GetItemOrCookie(QuoteCartCookieName));
                if (await GetSessionQuoteCartGuidAsync().ConfigureAwait(false) != guid)
                {
                    await OverrideSessionQuoteCartGuidAsync(guid).ConfigureAwait(false);
                }
            }
            else
            {
                Response.SetCookie(MakeACookie(
                    QuoteCartCookieName,
                    (await GetSessionQuoteCartGuidAsync().ConfigureAwait(false)).ToString()));
            }
        }

        /// <summary>Pickup sample cart cookie.</summary>
        /// <returns>A Task.</returns>
        private async Task PickupSampleCartCookieAsync()
        {
            if (Contract.CheckValidKey(Request.GetItemOrCookie(SampleCartCookieName)))
            {
                var guid = Guid.Parse(Request.GetItemOrCookie(SampleCartCookieName));
                if (await GetSessionSampleCartGuidAsync().ConfigureAwait(false) != guid)
                {
                    await OverrideSessionSampleCartGuidAsync(guid).ConfigureAwait(false);
                }
            }
            else
            {
                Response.SetCookie(MakeACookie(
                    SampleCartCookieName,
                    (await GetSessionSampleCartGuidAsync().ConfigureAwait(false)).ToString()));
            }
        }

        /// <summary>Override session shopping cart unique identifier.</summary>
        /// <param name="guid">Unique identifier.</param>
        /// <returns>A Task.</returns>
        private async Task OverrideSessionCartGuidAsync(Guid guid)
        {
            Contract.Requires<ArgumentException>(
                guid.ToString() != "00000000-0000-0000-0000-000000000000",
                "ERROR! Shopping cart cookie cannot be assigned to a default Guid");
            var key = $"session:{GetSession().Id}:{ShoppingCartCookieName}";
            // if (await client.ExistsAsync(key).ConfigureAwait(false))
            // {
            //     await client.RemoveAsync(key).ConfigureAwait(false);
            // }
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            await client!.AddAsync(key, guid).ConfigureAwait(false);
            Response.SetCookie(MakeACookie(ShoppingCartCookieName, guid.ToString()));
        }

        /// <summary>Override session quote cart unique identifier.</summary>
        /// <param name="guid">Unique identifier.</param>
        /// <returns>A Task.</returns>
        private async Task OverrideSessionQuoteCartGuidAsync(Guid guid)
        {
            Contract.Requires<ArgumentException>(
                guid.ToString() != "00000000-0000-0000-0000-000000000000",
                "ERROR! Quote cart cookie cannot be assigned to a default Guid");
            var key = $"session:{GetSession().Id}:{QuoteCartCookieName}";
            // if (await client.ExistsAsync(key).ConfigureAwait(false))
            // {
            //     await client.RemoveAsync(key).ConfigureAwait(false);
            // }
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            await client!.AddAsync(key, guid).ConfigureAwait(false);
            Response.SetCookie(MakeACookie(QuoteCartCookieName, guid.ToString()));
        }

        /// <summary>Override session sample cart unique identifier.</summary>
        /// <param name="guid">Unique identifier.</param>
        /// <returns>A Task.</returns>
        private async Task OverrideSessionSampleCartGuidAsync(Guid guid)
        {
            Contract.Requires<ArgumentException>(
                guid.ToString() != "00000000-0000-0000-0000-000000000000",
                "ERROR! Sample cart cookie cannot be assigned to a default Guid");
            var key = $"session:{GetSession().Id}:{SampleCartCookieName}";
            // if (await client.ExistsAsync(key).ConfigureAwait(false))
            // {
            //     await client.RemoveAsync(key).ConfigureAwait(false);
            // }
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            await client!.AddAsync(key, guid).ConfigureAwait(false);
            Response.SetCookie(MakeACookie(SampleCartCookieName, guid.ToString()));
        }

        private async Task<Guid> GetSessionGuidAsync(string outerKey)
        {
            var key = $"session:{GetSession().Id}:{outerKey}";
            Guid value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (await client!.ExistsAsync(key).ConfigureAwait(false))
            {
                value = await client.GetAsync<Guid>(key).ConfigureAwait(false);
                if (value.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    return value;
                }
                // Bad value, force a new one
                await client.RemoveAsync(outerKey).ConfigureAwait(false);
            }
            value = Guid.NewGuid();
            await client.AddAsync(key, value).ConfigureAwait(false);
            return value;
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Gets tax provider (when being logged in is not required).</summary>
        /// <returns>The tax provider (when being logged in is not required).</returns>
        protected static Task<ITaxesProviderBase?> GetTaxProviderAsync()
        {
            return RegistryLoaderWrapper.GetTaxProviderAsync(ServiceContextProfileName);
        }
    }

    public abstract partial class ClarityEcommerceServiceBase
    {
        /// <summary>Executes the clear cache action and return result operation.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="clearAsync">The clear.</param>
        /// <param name="factoryFn"> The factory function.</param>
        /// <returns>A TResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        protected static async Task<TResult> DoClearCacheActionAndReturnResult<TResult>(
            Func<Task> clearAsync,
            Func<Task<TResult>> factoryFn)
#pragma warning restore IDE1006 // Naming Styles
        {
            var result = await factoryFn().ConfigureAwait(false);
            await clearAsync().ConfigureAwait(false);
            return result;
        }

        /// <summary>Creates if not present and returns a cached result.</summary>
        /// <typeparam name="TRequest">Type of the request.</typeparam>
        /// <typeparam name="TResult"> Type of the result.</typeparam>
        /// <param name="request">  The request.</param>
        /// <param name="factoryFn">The factory function.</param>
        /// <returns>The cached result.</returns>
        protected object CreateAndReturnCachedResult<TRequest, TResult>(
            TRequest request,
            Func<TResult> factoryFn)
        {
            return Request.ToOptimizedResultUsingCache(
                Cache,
                /*{CEFCache.CachePrefix}:*/$"{UrnId.Create<TRequest>(request!.ToQueryString().Replace("?", string.Empty))}",
                CEFConfigDictionary.CachingTimeoutTimeSpan,
                factoryFn);
        }

        /// <summary>Creates and return cached result.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="factoryFn">        The factory function.</param>
        /// <param name="lastModified">     The last modified.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The new and return cached result.</returns>
        protected async Task<object?> CreateAndReturnCachedResultAsync<TResult>(
            Func<Task<TResult>> factoryFn,
            DateTime? lastModified,
            bool localCache = false,
            bool noCompression = false,
            TimeSpan? maxAge = null,
            bool varyByUser = false,
            string[]? varyByRoles = null,
            string[]? varyByHeaders = null,
            CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
            long? noCache = null)
        {
            var root = Request.RawUrl;
            if (Contract.CheckValidKey(CEFConfigDictionary.APIStorefrontRouteRelativePath))
            {
                root = root.Replace(CEFConfigDictionary.APIStorefrontRouteRelativePath!, "/");
            }
            var urlPart = root.Replace('/', ':');
            var modifiers = new StringBuilder();
            AppendJsonPToKey(modifiers);
            AppendUserSessionToKey(modifiers, varyByUser);
            AppendRolesToKey(modifiers, varyByRoles);
            AppendHeadersToKey(modifiers, varyByHeaders);
            if (CEFConfigDictionary.BrandsEnabled)
            {
                AppendReferralUrlToKey(modifiers);
            }
            var cacheKey = $"res:{urlPart}";
            if (modifiers.Length > 0)
            {
                cacheKey = modifiers.Insert(0, $"res:{urlPart}:").ToString();
            }
            var retVal = await Request.CEFToOptimizedResultUsingCacheAsync(
                    cacheClient: Cache,
                    cacheKey: cacheKey.CollapseDuplicates(':'),
                    expireCacheIn: CEFConfigDictionary.CachingTimeoutTimeSpan,
                    factoryFn: factoryFn,
                    lastModified: lastModified)
                .ConfigureAwait(false);
            return retVal;
        }

        /// <summary>Use last modified for 304 or create and return cached result.</summary>
        /// <typeparam name="TResult"> Type of the result.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="lastModFn">        The last modifier function.</param>
        /// <param name="factoryFn">        The factory function.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The HttpResult with HTTP Cache headers.</returns>
        protected async Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultAsync<TResult>(
            object? request,
            Func<Task<DateTime?>> lastModFn,
            Func<Task<TResult>> factoryFn,
            bool localCache = false,
            bool noCompression = false,
            TimeSpan? maxAge = null,
            bool varyByUser = false,
            string[]? varyByRoles = null,
            string[]? varyByHeaders = null,
            CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
            long? noCache = null)
        {
            var isNoCache = noCache.HasValue;
            if (CurrentAPIKind != Enums.APIKind.Storefront || isNoCache)
            {
                // Circumvent all caching attempts
                return await factoryFn().ConfigureAwait(false);
            }
            var lastModified = await lastModFn().ConfigureAwait(false);
            if (Request.HasValidCache(lastModified))
            {
                return HttpResult.NotModified();
            }
            if (CEFConfigDictionary.APIRequestsAlwaysVeryByReferer)
            {
                varyByHeaders ??= Array.Empty<string>();
                if (!varyByHeaders.Contains("Referer"))
                {
                    varyByHeaders = new List<string>(varyByHeaders) { "Referer" }.ToArray();
                }
            }
            var raw = await CreateAndReturnCachedResultAsync(
                    factoryFn: factoryFn,
                    lastModified: lastModified,
                    localCache: localCache,
                    noCompression: noCompression,
                    maxAge: maxAge,
                    varyByUser: varyByUser,
                    varyByRoles: varyByRoles,
                    varyByHeaders: varyByHeaders,
                    cacheControlFlags: cacheControlFlags,
                    noCache: noCache)
                .ConfigureAwait(false);
            return raw;
        }

        /// <summary>Use last modified for 304 or create and return cached result by identifier single.</summary>
        /// <typeparam name="TRequest">Type of the request.</typeparam>
        /// <typeparam name="TResult"> Type of the result.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="workflow">         The workflow.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The HttpResult with HTTP Cache headers.</returns>
        protected Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync<TRequest, TResult>(
                TRequest request,
                IWorkflowBaseHasGet<TResult> workflow,
                bool localCache = false,
                bool noCompression = false,
                TimeSpan? maxAge = null,
                bool varyByUser = false,
                string[]? varyByRoles = null,
                string[]? varyByHeaders = null,
                CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
                long? noCache = null)
            where TRequest : IImplementsIDBase
            where TResult : IBaseModel
        {
            return UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                request,
                () => workflow.GetLastModifiedForResultAsync(request.ID, ServiceContextProfileName),
                () => workflow.GetAsync(request.ID, ServiceContextProfileName),
                localCache,
                noCompression,
                maxAge,
                varyByUser,
                varyByRoles,
                varyByHeaders,
                cacheControlFlags,
                noCache);
        }

        /// <summary>Use last modified for 304 or create and return cached result by key single.</summary>
        /// <typeparam name="TRequest">Type of the request.</typeparam>
        /// <typeparam name="TResult"> Type of the result.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="workflow">         The workflow.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The HttpResult with HTTP Cache headers.</returns>
        protected Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync<TRequest, TResult>(
                TRequest request,
                IWorkflowBaseHasGet<TResult> workflow,
                bool localCache = false,
                bool noCompression = false,
                TimeSpan? maxAge = null,
                bool varyByUser = false,
                string[]? varyByRoles = null,
                string[]? varyByHeaders = null,
                CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
                long? noCache = null)
            where TRequest : IImplementsKeyBase
            where TResult : IBaseModel
        {
            return UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                request,
                () => workflow.GetLastModifiedForResultAsync(Contract.RequiresValidKey(request.Key), ServiceContextProfileName),
                () => workflow.GetAsync(Contract.RequiresValidKey(request.Key), ServiceContextProfileName),
                localCache,
                noCompression,
                maxAge,
                varyByUser,
                varyByRoles,
                varyByHeaders,
                cacheControlFlags,
                noCache);
        }

        /// <summary>Use last modified for 304 or create and return cached result by name single.</summary>
        /// <typeparam name="TRequest">     Type of the request.</typeparam>
        /// <typeparam name="TResult">      Type of the result.</typeparam>
        /// <typeparam name="TISearchModel">Type of the search model.</typeparam>
        /// <typeparam name="TIEntity">     Type of the entity's interface.</typeparam>
        /// <typeparam name="TEntity">      Type of the entity.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="workflow">         The workflow.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The HttpResult with HTTP Cache headers.</returns>
        protected Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync<TRequest, TResult, TISearchModel, TIEntity, TEntity>(
                TRequest request,
                INameableWorkflowBase<TResult, TISearchModel, TIEntity, TEntity> workflow,
                bool localCache = false,
                bool noCompression = false,
                TimeSpan? maxAge = null,
                bool varyByUser = false,
                string[]? varyByRoles = null,
                string[]? varyByHeaders = null,
                CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
                long? noCache = null)
            where TRequest : ImplementsNameBase
            where TResult : INameableBaseModel
            where TIEntity : INameableBase
            where TEntity : class, TIEntity, new()
        {
            return UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                request,
                () => workflow.GetLastModifiedForByNameResultAsync(Contract.RequiresValidKey(request.Name), ServiceContextProfileName),
                () => workflow.GetByNameAsync(Contract.RequiresValidKey(request.Name), ServiceContextProfileName),
                localCache,
                noCompression,
                maxAge,
                varyByUser,
                varyByRoles,
                varyByHeaders,
                cacheControlFlags,
                noCache);
        }

        /// <summary>Use last modified for 304 or create and return cached result by display name single.</summary>
        /// <typeparam name="TRequest">     Type of the request.</typeparam>
        /// <typeparam name="TResult">      Type of the result.</typeparam>
        /// <typeparam name="TISearchModel">Type of the search model.</typeparam>
        /// <typeparam name="TIEntity">     Type of the entity's interface.</typeparam>
        /// <typeparam name="TEntity">      Type of the entity.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="workflow">         The workflow.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The HttpResult with HTTP Cache headers.</returns>
        protected Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync<TRequest, TResult, TISearchModel, TIEntity, TEntity>(
                TRequest request,
                IDisplayableWorkflowBase<TResult, TISearchModel, TIEntity, TEntity> workflow,
                bool localCache = false,
                bool noCompression = false,
                TimeSpan? maxAge = null,
                bool varyByUser = false,
                string[]? varyByRoles = null,
                string[]? varyByHeaders = null,
                CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
                long? noCache = null)
            where TRequest : ImplementsDisplayNameBase
            where TResult : IDisplayableBaseModel
            where TIEntity : IDisplayableBase
            where TEntity : class, TIEntity, new()
        {
            return UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                request,
                () => workflow.GetLastModifiedForByDisplayNameResultAsync(Contract.RequiresValidKey(request.DisplayName), ServiceContextProfileName),
                () => workflow.GetByDisplayNameAsync(Contract.RequiresValidKey(request.DisplayName), ServiceContextProfileName),
                localCache,
                noCompression,
                maxAge,
                varyByUser,
                varyByRoles,
                varyByHeaders,
                cacheControlFlags,
                noCache);
        }

        /// <summary>Use last modified for 304 or create and return cached result by display name single.</summary>
        /// <typeparam name="TRequest">Type of the request.</typeparam>
        /// <typeparam name="TResult"> Type of the result.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="workflow">         The workflow.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <param name="noCache">          The no cache.</param>
        /// <returns>The HttpResult with HTTP Cache headers.</returns>
        protected Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultBySeoUrlSingleAsync<TRequest, TResult>(
                TRequest request,
                IWorkflowBaseHasGetBySeoUrl<TResult> workflow,
                bool localCache = false,
                bool noCompression = false,
                TimeSpan? maxAge = null,
                bool varyByUser = false,
                string[]? varyByRoles = null,
                string[]? varyByHeaders = null,
                CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate,
                long? noCache = null)
            where TRequest : ImplementsSeoUrlBase
            where TResult : IHaveSeoBaseModel
        {
            return UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                request,
                () => workflow.GetLastModifiedForBySeoUrlResultAsync(Contract.RequiresValidKey(request.SeoUrl), ServiceContextProfileName),
                () => workflow.GetBySeoUrlAsync(Contract.RequiresValidKey(request.SeoUrl), ServiceContextProfileName),
                localCache,
                noCompression,
                maxAge,
                varyByUser,
                varyByRoles,
                varyByHeaders,
                cacheControlFlags,
                noCache);
        }

        /// <summary>Use last modified for 304 or create and return cached result set.</summary>
        /// <typeparam name="TIModel">      Type of the model's interface.</typeparam>
        /// <typeparam name="TModel">       Type of the model.</typeparam>
        /// <typeparam name="TISearchModel">Type of the search model's interface.</typeparam>
        /// <typeparam name="TPagedResults">Type of the paged results.</typeparam>
        /// <param name="request">          The request.</param>
        /// <param name="asListing">        True to as listing.</param>
        /// <param name="workflow">         The workflow.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <returns>A Task{object}.</returns>
        protected Task<object?> UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<TIModel, TModel, TISearchModel, TPagedResults>(
                TISearchModel request,
                bool asListing,
                IWorkflowBaseHasSearch<TIModel, TISearchModel> workflow,
                bool localCache = false,
                bool noCompression = false,
                TimeSpan? maxAge = null,
                bool varyByUser = false,
                string[]? varyByRoles = null,
                string[]? varyByHeaders = null,
                CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate)
            where TIModel : IBaseModel
            where TModel : class, TIModel
            where TISearchModel : IBaseSearchModel
            where TPagedResults : PagedResultsBase<TModel>, new()
        {
            return UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                request,
                () => workflow.GetLastModifiedForResultSetAsync(request, ServiceContextProfileName),
                () => GetPagedResultsAsync<TIModel, TModel, TISearchModel, TPagedResults>(request, asListing, workflow),
                localCache,
                noCompression,
                maxAge,
                varyByUser,
                varyByRoles,
                varyByHeaders,
                cacheControlFlags,
                request.noCache);
        }

        /// <summary>Creates and return cached string result.</summary>
        /// <typeparam name="TRequest">Type of the request.</typeparam>
        /// <param name="request">  The request.</param>
        /// <param name="factoryFn">The factory function.</param>
        /// <returns>The new and return cached string result.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
#pragma warning disable IDE1006 // Naming Styles
        protected async Task<string> CreateAndReturnCachedStringResult<TRequest>(
            TRequest request,
            Func<Task<string>> factoryFn)
#pragma warning restore IDE1006 // Naming Styles
        {
            var urn = $"{UrnId.Create<TRequest>(request!.ToQueryString().Replace("?", string.Empty))}";
            if (CEFConfigDictionary.BrandsEnabled || CEFConfigDictionary.FranchisesEnabled)
            {
                urn += ":" + new Uri(Request.AbsoluteUri).Host.Replace(":", "{colon}");
            }
            string? cached = null;
            if (Cache.GetKeysByPattern(urn).Any(x => x == urn))
            {
                cached = Cache.Get<string?>(urn);
            }
            if (cached is not null)
            {
                return cached;
            }
            var toStore = await factoryFn().ConfigureAwait(false);
            Cache.Set(urn, toStore, CEFConfigDictionary.CachingTimeoutTimeSpan);
            cached = toStore;
            return cached;
        }

        /// <summary>Creates and return cached string result.</summary>
        /// <typeparam name="TRequest">Type of the request.</typeparam>
        /// <param name="request">  The request.</param>
        /// <param name="factoryFn">The factory function.</param>
        /// <returns>The new and return cached string result.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        protected string CreateAndReturnCachedStringResult<TRequest>(
            TRequest request,
            Func<string> factoryFn)
        {
            var urn = $"{UrnId.Create<TRequest>(request!.ToQueryString().Replace("?", string.Empty))}";
            if (CEFConfigDictionary.BrandsEnabled || CEFConfigDictionary.FranchisesEnabled)
            {
                urn += ":" + new Uri(Request.AbsoluteUri).Host.Replace(":", "{colon}");
            }
            string? cached = null;
            if (Cache.GetKeysByPattern(urn).Any(x => x == urn))
            {
                cached = Cache.Get<string?>(urn);
            }
            if (cached is not null)
            {
                return cached;
            }
            var toStore = factoryFn();
            Cache.Set(urn, toStore, CEFConfigDictionary.CachingTimeoutTimeSpan);
            cached = toStore;
            return cached;
        }

        /// <summary>Clears the cache prefixed described by urnKeyStartsWith.</summary>
        /// <param name="urnKeyStartsWith">The URN key starts with.</param>
        /// <returns>A Task.</returns>
        protected Task ClearCachePrefixedAsync(string? urnKeyStartsWith = null)
        {
            return ClearCacheAsync(urnKeyStartsWith);
        }

        /// <summary>Clears the cache described by urnKeyStartsWith.</summary>
        /// <param name="urnKeyStartsWith">The URN key starts with.</param>
        /// <returns>A Task.</returns>
        protected async Task ClearCacheAsync(string? urnKeyStartsWith = null)
        {
            try
            {
                var pattern = Contract.CheckValidKey(urnKeyStartsWith)
                    ? urnKeyStartsWith + (urnKeyStartsWith!.EndsWith("*") ? string.Empty : "*")
                    : "*";
                var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
                await client!.RemoveByPatternAsync(pattern).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                // Do Nothing
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        "ClearCacheAsync",
                        "Failed " + ex.Message,
                        ex,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
        }

        private void AppendJsonPToKey(StringBuilder modifiers)
        {
            if (Request.ResponseContentType != MimeTypes.Json)
            {
                return;
            }
            var jsonpCallback = Request.GetJsonpCallback();
            if (jsonpCallback == null)
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append(':');
            }
            modifiers.Append("jsonp=").Append(jsonpCallback.SafeVarName());
        }

        private void AppendUserSessionToKey(StringBuilder modifiers, bool varyByUser)
        {
            if (!varyByUser)
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append(':');
            }
            modifiers.Append("user=").Append(Request.GetSessionId());
        }

        private void AppendRolesToKey(StringBuilder modifiers, string[]? varyByRoles)
        {
            if (varyByRoles == null || varyByRoles.Length == 0)
            {
                return;
            }
            var session = Request.GetSession();
            if (session == null)
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append('+');
            }
            var authRepository = HostContext.AppHost.GetAuthRepository(Request);
            foreach (var role in varyByRoles)
            {
                if (!session.HasRole(role, authRepository))
                {
                    continue;
                }
                if (modifiers.Length > 0)
                {
                    modifiers.Append('+');
                }
                modifiers.Append("role=").Append(role.Replace(":", "{c}"));
            }
        }

        private void AppendHeadersToKey(StringBuilder modifiers, string[]? varyByHeaders)
        {
            if (varyByHeaders == null || varyByHeaders.Length == 0)
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append(':');
            }
            foreach (var varyByHeader in varyByHeaders)
            {
                var header = Request.GetHeader(varyByHeader);
                if (string.IsNullOrEmpty(header))
                {
                    continue;
                }
                if (modifiers.Length > 0)
                {
                    modifiers.Append('+');
                }
                modifiers.Append(varyByHeader).Append('=').Append(header.Replace(":", "{c}"));
            }
        }

        private void AppendReferralUrlToKey(StringBuilder modifiers)
        {
            if (!Contract.CheckValidKey(Request.UrlReferrer?.Host))
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append('+');
            }
            modifiers.Append("referralUrl:").Append(Request.UrlReferrer!.Host);
        }
    }
}
