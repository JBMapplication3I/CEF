// <copyright file="SalesOrderService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order service class</summary>
#nullable enable
#pragma warning disable 1584, SA1118 // Parameter should not span multiple lines
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using LinqToExcel.Extensions;
    using Models;
    using Providers.Emails;
    using ServiceStack;
    using Utilities;

    #region General/Storefront
    [PublicAPI,
        Route("/Ordering/SecureSalesOrder/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales order and check for ownership by the current Account.")]
    public partial class GetSecureSalesOrder : ImplementsIDBase, IReturn<SalesOrderModel>
    {
    }

    /// <summary>A get discounts for order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{DiscountsForOrder}"/>
    [PublicAPI,
        Route("/Providers/Ordering/Queries/Secured/DiscountsFor/{ID}", "GET",
            Summary = "Use to get the discounts for the top level and item levels at the same time.")]
    public partial class GetDiscountsForOrder : ImplementsIDBase, IReturn<DiscountsForOrderResponse>
    {
    }

    /// <summary>Information about the discounts for a order.</summary>
    public class DiscountsForOrderResponse
    {
        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        public List<AppliedSalesOrderDiscountModel> Discounts { get; set; } = null!;

        /// <summary>Gets or sets the item discounts.</summary>
        /// <value>The item discounts.</value>
        public List<AppliedSalesOrderItemDiscountModel> ItemDiscounts { get; set; } = null!;
    }

    /// <summary>Send Sales order confirmation to another email.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Route("/Ordering/SalesOrder/SendReceiptEmail", "POST",
            Summary = "Send an email with the sales order receipt.")]
    public partial class SendSalesOrderConfirmationEmail : ImplementsIDBase, IReturn<CEFActionResponse>
    {
        public string Email { get; set; } = null!;
    }

    /// <summary>A get current store sales orders.</summary>
    /// <seealso cref="SalesOrderSearchModel"/>
    /// <seealso cref="IReturn{SalesOrderPagedResults}"/>
    [PublicAPI,
        Authenticate, RequiresAnyPermission("Ordering.SalesOrder.View", "Storefront.StoreDashboard.SalesOrders.View"),
        Route("/Ordering/CurrentStore/SalesOrders", "POST",
            Summary = "Use to get history of sales order for the current store")]
    public partial class GetCurrentStoreSalesOrders : SalesOrderSearchModel, IReturn<SalesOrderPagedResults>
    {
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Get(GetSecureSalesOrder request)
        {
            // await ThrowIfNoRightsToRecordSalesOrderAsync(request.ID).ConfigureAwait(false);
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
            return await Workflows.SalesOrders.SecureSalesOrderAsync(
                    request.ID,
                    allowedAccounts,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Get handler for <see cref="GetDiscountsForOrder"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{DiscountsForOrder}"/>.</returns>
        public async Task<object?> Get(GetDiscountsForOrder request)
        {
            _ = Contract.RequiresValidID(
                await Workflows.SalesOrders.CheckExistsAsync(
                        Contract.RequiresValidID(request.ID),
                        ServiceContextProfileName)
                    .ConfigureAwait(false));
            DiscountsForOrderResponse result = new();
            var topLevelSearch = new AppliedSalesOrderDiscountSearchModel
            {
                Active = true,
                AsListing = true,
                MasterID = request.ID,
            };
            var (results, _, _) = await Workflows.AppliedSalesOrderDiscounts.SearchAsync(
                    search: topLevelSearch,
                    asListing: true,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            result.Discounts = results.Cast<AppliedSalesOrderDiscountModel>().ToList();
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var innerSearch = new AppliedSalesOrderItemDiscountSearchModel
            {
                Active = true,
                AsListing = true,
                MasterIDs = await context.SalesOrderItems
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterSalesItemsByMasterID<SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(request.ID)
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false),
            };
            var innerDiscounts = await Workflows.AppliedSalesOrderItemDiscounts.SearchAsync(
                    search: innerSearch,
                    asListing: true,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            result.ItemDiscounts = innerDiscounts.results.Cast<AppliedSalesOrderItemDiscountModel>().ToList();
            return result;
        }

        public async Task<object?> Post(SendSalesOrderConfirmationEmail request)
        {
            _ = Contract.RequiresValidKey(request.Email);
            try
            {
                var session = GetAuthedSSSessionOrThrow401();
                var accountID = await Workflows.Accounts.GetIDByUserIDAsync(
                        session.UserID!.Value,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
                var salesOrder = await Workflows.SalesOrders.GetAsync(
                        request.ID,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
                if (salesOrder is null)
                {
                    return CEFAR.FailingCEFAR($"Order {request.ID} not found");
                }
                if (salesOrder.AccountID != accountID)
                {
                    // Order doesn't belong to this user
                    throw HttpError.Forbidden("This order is not assigned to your account.");
                }
                return await new SalesOrdersForwardReceiptToCustomerEmail().QueueAsync(
                        parameters: new()
                        {
                            ["salesOrder"] = salesOrder,
                            ["email"] = request.Email,
                        },
                        to: request.Email,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            catch (Exception err)
            {
                await Logger.LogErrorAsync("SendSalesOrderConfirmationEmail.Error", err.Message, ServiceContextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR("There was an error sending the confirmation email to a new recipient.");
            }
        }

        public async Task<object?> Post(GetCurrentStoreSalesOrders request)
        {
            request.Active = true;
            request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            return await GetPagedResultsAsync<ISalesOrderModel, SalesOrderModel, ISalesOrderSearchModel, SalesOrderPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesOrders)
                .ConfigureAwait(false);
        }
    }
    #endregion

    #region Current User
    /// <summary>A get current user sales orders.</summary>
    /// <seealso cref="SalesOrderSearchModel"/>
    /// <seealso cref="IReturn{SalesOrderPagedResults}"/>
    [PublicAPI,
        Authenticate,
        Route("/Ordering/CurrentUser/SalesOrders", "POST",
            Summary = "Use to get history of sales order for the current user")]
    public partial class GetCurrentUserSalesOrders : SalesOrderSearchModel, IReturn<SalesOrderPagedResults>
    {
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Post(GetCurrentUserSalesOrders request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<ISalesOrderModel, SalesOrderModel, ISalesOrderSearchModel, SalesOrderPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesOrders)
                .ConfigureAwait(false);
        }
    }
    #endregion

    #region Current Account
    /// <summary>A get current account sales orders.</summary>
    /// <seealso cref="SalesOrderSearchModel"/>
    /// <seealso cref="IReturn{SalesOrderPagedResults}"/>
    [PublicAPI,
        Authenticate, RequiresAnyPermission("Ordering.SalesOrder.View", "Storefront.UserDashboard.SalesOrders.View"),
        Route("/Ordering/CurrentAccount/SalesOrders", "POST",
            Summary = "Use to get history of sales orders for the current account")]
    public partial class GetCurrentAccountSalesOrders : SalesOrderSearchModel, IReturn<SalesOrderPagedResults>
    {
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Post(GetCurrentAccountSalesOrders request)
        {
            request.Active = true;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false);
            return await GetPagedResultsAsync<ISalesOrderModel, SalesOrderModel, ISalesOrderSearchModel, SalesOrderPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesOrders)
                .ConfigureAwait(false);
        }
    }
    #endregion

    #region X-Portal
    [PublicAPI,
        Authenticate,
        Route("/Ordering/Admin/Portal/SalesOrders", "POST",
            Summary = "Returns the current brand's orders")]
    public partial class AdminGetSalesOrdersForPortal : SalesOrderSearchModel, IReturn<SalesOrderPagedResults>
    {
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Post(AdminGetSalesOrdersForPortal request)
        {
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                default:
                {
                    throw HttpError.Forbidden("Invalid operation");
                }
            }
            request.Active = true;
            request.AsListing = true;
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.SalesOrders.GetLastModifiedForResultSetAsync(request, ServiceContextProfileName),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.SalesOrders.SearchAsync(
                                request,
                                request.AsListing,
                                ServiceContextProfileName)
                            .ConfigureAwait(false);
                        return new SalesOrderPagedResults
                        {
                            Results = results.Cast<SalesOrderModel>().ToList(),
                            CurrentCount = request.Paging?.Size ?? totalCount,
                            CurrentPage = request.Paging?.StartIndex ?? 1,
                            TotalPages = totalPages,
                            TotalCount = totalCount,
                            Sorts = request.Sorts,
                            Groupings = request.Groupings,
                        };
                    })
                .ConfigureAwait(false);
        }
    }
    #endregion

    #region Brand Admin
    [PublicAPI, UsedInBrandAdmin,
        Authenticate,
        Route("/Brands/Brand/BrandOrdersStatus", "POST",
            Summary = "Returns the CEFActionResponse from updating sales order statuses")]
    public partial class AdminUpdateStatusForBrandOrders : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the StatusKey.</summary>
        /// <value>The StatusKey.</value>
        [ApiMember(Name = "StatusID", DataType = "query", ParameterType = "int", IsRequired = true)]
        public int StatusID { get; set; } = 0;

        /// <summary>Gets or sets the array of OrderIDs.</summary>
        /// <value>The array of OrderIDs.</value>
        [ApiMember(Name = "OrderIDs", DataType = "query", ParameterType = "int[]", IsRequired = true)]
        public int[] OrderIDs { get; set; } = null!;
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Post(AdminUpdateStatusForBrandOrders request)
        {
            var statusID = await Workflows.SalesOrderStatuses.CheckExistsAsync(
                    id: request.StatusID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            foreach (var o in request.OrderIDs)
            {
                await ThrowIfNoRightsToRecordSalesOrderAsync(o).ConfigureAwait(false);
                var order = await Workflows.SalesOrders.GetAsync(id: o, ServiceContextProfileName).ConfigureAwait(false);
                order!.StatusID = statusID.Value;
                var response = await Workflows.SalesOrders.UpdateAsync(model: order, ServiceContextProfileName).ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    return CEFAR.FailingCEFAR("Unable to Update Statuses");
                }
            }
            return CEFAR.PassingCEFAR();
        }
    }
    #endregion

    #region Franchise Admin
    [PublicAPI, UsedInFranchiseAdmin,
        Authenticate,
        Route("/Franchises/Franchise/FranchiseOrdersStatus", "POST",
            Summary = "Returns the CEFActionResponse from updating sales order statuses")]
    public partial class AdminUpdateStatusForFranchiseOrders : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the StatusKey.</summary>
        /// <value>The StatusKey.</value>
        [ApiMember(Name = "StatusID", DataType = "query", ParameterType = "int", IsRequired = true)]
        public int StatusID { get; set; } = 0;

        /// <summary>Gets or sets the array of OrderIDs.</summary>
        /// <value>The array of OrderIDs.</value>
        [ApiMember(Name = "OrderIDs", DataType = "query", ParameterType = "int[]", IsRequired = true)]
        public int[] OrderIDs { get; set; } = null!;
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Post(AdminUpdateStatusForFranchiseOrders request)
        {
            var statusID = await Workflows.SalesOrderStatuses.CheckExistsAsync(
                    id: request.StatusID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            foreach (var o in request.OrderIDs)
            {
                await ThrowIfNoRightsToRecordSalesOrderAsync(o).ConfigureAwait(false);
                var order = await Workflows.SalesOrders.GetAsync(id: o, ServiceContextProfileName).ConfigureAwait(false);
                order!.StatusID = statusID!.Value;
                var response = await Workflows.SalesOrders.UpdateAsync(model: order, ServiceContextProfileName).ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    return CEFAR.FailingCEFAR("Unable to Update Statuses");
                }
            }
            return CEFAR.PassingCEFAR();
        }
    }
    #endregion

    #region Store Admin
    [PublicAPI, UsedInStoreAdmin,
        Authenticate,
        Route("/Ordering/SalesOrderForStoreAdmin/{ID}", "PATCH",
            Summary = "Use to replace an item in a sales order for store admin.")]
    public class EditSalesOrder : ImplementsIDBase, IReturn<SalesOrderModel>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int ProductID { get; set; }
    }

    [PublicAPI, UsedInStoreAdmin,
        Authenticate,
        Route("/Stores/Store/StoreOrdersStatus", "POST",
            Summary = "Returns the CEFActionResponse from updating sales order statuses")]
    public partial class AdminUpdateStatusForStoreOrders : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the StatusID.</summary>
        /// <value>The StatusID.</value>
        [ApiMember(Name = "StatusID", DataType = "query", ParameterType = "int", IsRequired = true)]
        public int StatusID { get; set; } = 0;

        /// <summary>Gets or sets the array of OrderIDs.</summary>
        /// <value>The array of OrderIDs.</value>
        [ApiMember(Name = "OrderIDs", DataType = "query", ParameterType = "int[]", IsRequired = true)]
        public int[] OrderIDs { get; set; } = null!;
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Patch(EditSalesOrder request)
        {
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            return await Workflows.SalesOrders.EditSalesOrderAsync(
                    request.ID,
                    request.ProductID,
                    pricingFactoryContext,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AdminUpdateStatusForStoreOrders request)
        {
            var statusID = await Workflows.SalesOrderStatuses.CheckExistsAsync(
                    id: request.StatusID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            foreach (var o in request.OrderIDs)
            {
                await ThrowIfNoRightsToRecordSalesOrderAsync(o).ConfigureAwait(false);
                var order = await Workflows.SalesOrders.GetAsync(id: o, ServiceContextProfileName).ConfigureAwait(false);
                order!.StatusID = statusID!.Value;
                var response = await Workflows.SalesOrders.UpdateAsync(model: order, ServiceContextProfileName).ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    return CEFAR.FailingCEFAR("Unable to Update Statuses");
                }
            }
            return CEFAR.PassingCEFAR();
        }
    }
    #endregion

    #region Subscriptions
    [PublicAPI,
        Authenticate,
        Route("/Payments/CurrentUser/Subscriptions/OnDemand", "POST",
            Summary = "Use to get all on-demand subscriptions.")]
    public partial class ViewOnDemandSubscriptions : SubscriptionSearchModel, IReturn<SubscriptionPagedResults>
    {
        [ApiMember(Name = nameof(UID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int UID { get; set; }
    }

    /// <summary>View subscription.</summary>
    /// <seealso cref="SubscriptionSearchModel"/>
    /// <seealso cref="IReturn{SubscriptionModel}"/>
    [PublicAPI,
        Authenticate, /*RequiredPermission("Ordering.SalesOrder.View", "Storefront.UserDashboard.SalesOrders.View"),*/
        Route("/Ordering/SalesOrder/Detail/Subscription", "GET",
            Summary = "View subscription to update. Get by subscriptionID.")]
    public partial class GetSubscriptionBySalesOrderID : SubscriptionSearchModel, IReturn<SubscriptionModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Ordering/SalesOrder/Detail/Subscription/History", "POST",
        Summary = "Use to get all on-demand subscriptions.")]
    public partial class GetSubscriptionHistoryBySubscriptionID : SubscriptionHistorySearchModel, IReturn<SubscriptionHistoryPagedResults>
    {
        [ApiMember(Name = nameof(SubID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int SubID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Payments/CurrentUser/Subscriptions/OnDemand/{ID}", "PATCH",
            Summary = "Use to get all on-demand subscriptions.")]
    public partial class RefillOnDemandSubscription : ImplementsIDOnBodyBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Payments/User/CancelSubscription/{ID}", "PATCH",
            Summary = "Use to cancel the subscription for a user by using the sales order ID.")]
    public class CancelSubscription : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    public partial class SalesOrderService
    {
        public async Task<object?> Post(ViewOnDemandSubscriptions request)
        {
            var (subscriptions, totalPages, totalCount) = await Workflows.SalesOrders.GetOnDemandSubscriptionsByUserAsync(
                    request.UID, // TODO: delete and use CurrentAccountIDOrThrow401 or request.ID?
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (!subscriptions.Any())
            {
                return new SubscriptionPagedResults
                {
                    Results = new(),
                    CurrentCount = 0,
                    CurrentPage = 0,
                    TotalPages = 0,
                    TotalCount = 0,
                    Sorts = request.Sorts,
                    Groupings = request.Groupings,
                };
            }
            return new SubscriptionPagedResults
            {
                Results = subscriptions.Cast<SubscriptionModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }

        public async Task<object?> Get(GetSubscriptionBySalesOrderID request)
        {
            return (SubscriptionModel)(await Workflows.SalesOrders.GetSubscriptionBySalesOrder(
                    Contract.RequiresValidID(request.SalesOrderID),
                    ServiceContextProfileName)
                .ConfigureAwait(false))!;
        }

        public async Task<object?> Post(GetSubscriptionHistoryBySubscriptionID request)
        {
            var (subscriptions, totalPages, totalCount) = await Workflows.SalesOrders.GetSubscriptionHistoryBySubID(
                    request.SubID,
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (!subscriptions.Any())
            {
                return new SubscriptionHistoryPagedResults
                {
                    Results = new(),
                    CurrentCount = 0,
                    CurrentPage = 0,
                    TotalPages = 0,
                    TotalCount = 0,
                    Sorts = request.Sorts,
                    Groupings = request.Groupings,
                };
            }
            return new SubscriptionHistoryPagedResults
            {
                Results = subscriptions.Cast<SubscriptionHistoryModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }

        public async Task<object?> Patch(RefillOnDemandSubscription request)
        {
            return await Workflows.SalesOrders.RefillOnDemandSubscriptionAsync(
                    request.ID, // TODO: delete and use CurrentAccountIDOrThrow401 or request.ID?
                    CurrentAccountIDOrThrow401,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(CancelSubscription request)
        {
            return await Workflows.SalesOrders.CancelSubscriptionAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
    #endregion

    /*
    /// <summary>A split sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{SalesOrderModel[]}}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Ordering.SalesOrder.Split"),
        Route("/Ordering/SalesOrder/SplitSalesOrder/{ID}", "PATCH",
            Summary = "Some items on the order have available stock while others to not. Close this order (the status"
                + " will be 'Split') while creating two new orders (the order keys will be Key.1 and Key.2). An email"
                + " notification will be sent to the customer.")]
    public class SplitSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse<SalesOrderModel[]>> { }

    [PublicAPI]
    public partial class SalesOrderService
    {
        public async Task<object?> Patch(SplitSalesOrder request)
        {
            return await Workflows.SalesOrders.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(request.ID, ServiceContextProfileName).ConfigureAwait(false);
        }
    }
    */
}
