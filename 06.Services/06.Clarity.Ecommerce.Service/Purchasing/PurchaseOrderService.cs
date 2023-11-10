// <copyright file="PurchaseOrderService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order service class</summary>
#nullable enable
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Purchasing/Admin/Portal/PurchaseOrders", "GET",
            Summary = "Use to get a list of purchase orders (for the current x-portal we can administrate only)")]
    public partial class AdminGetPurchaseOrdersForPortal : PurchaseOrderSearchModel, IReturn<PurchaseOrderPagedResults>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Purchasing/PurchaseOrder/Checkout", "POST")]
    public class PurchaseOrderCheckout : IReturnVoid
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int ID { get; set; }

        /// <summary>Gets or sets the vendor IDs.</summary>
        /// <value>The vendor IDs.</value>
        [ApiMember(Name = nameof(VendorIDs), DataType = "List<int>", ParameterType = "body", IsRequired = false,
            Description = "List of Vendor IDs")]
        public List<int> VendorIDs { get; set; } = null!;
    }

    public partial class PurchaseOrderService
    {
        public override async Task<object?> Post(CreatePurchaseOrder request)
        {
            request.BillingContactID ??= (await CurrentUserAsync().ConfigureAwait(false))!.ContactID;
            return await Workflows.PurchaseOrders.CreateAsync(request, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task Post(PurchaseOrderCheckout request)
        {
            await Workflows.PurchaseOrders.CheckoutAsync(request.ID, ServiceContextProfileName, request.VendorIDs).ConfigureAwait(false);
        }

        public async Task<object?> Get(AdminGetPurchaseOrdersForPortal request)
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
                /* TODO: Manufacturer Purchase Orders
                case Enums.APIKind.ManufacturerAdmin:
                {
                    request.ManufacturerID = await CurrentManufacturerForManufacturerAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                */
                case Enums.APIKind.StoreAdmin:
                {
                    request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.VendorAdmin:
                {
                    request.VendorID = await CurrentVendorForVendorAdminIDOrThrow401Async().ConfigureAwait(false);
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
                    () => Workflows.PurchaseOrders.GetLastModifiedForResultSetAsync(request, contextProfileName: null),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.PurchaseOrders.SearchAsync(
                                request,
                                request.AsListing,
                                contextProfileName: null)
                            .ConfigureAwait(false);
                        return new PurchaseOrderPagedResults
                        {
                            Results = results.Cast<PurchaseOrderModel>().ToList(),
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
}
